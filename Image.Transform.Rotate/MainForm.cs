using Image.Transform.Rotate.Commons.Enums;
using Image.Transform.Rotate.Commons.Helpers;
using Image.Transform.Rotate.Factories;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image.Transform.Rotate
{
    /// <summary>
    /// 主表單。
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        /// <summary>
        /// 上次圖片旋轉的角度。
        /// </summary>
        private decimal _previousRotateDegrees;

        /// <summary>
        /// 初始化 <see cref="MainForm"/> 類別的新執行個體。
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.InitializeRotateTypeComboBox();
            this.InitializeProgressRing();
            this._previousRotateDegrees = numRotateDegrees.Value;
        }

        /// <summary>
        /// 以非同步作業的方式，處理 [開啟] 原圖按鈕的 Click 事件。
        /// </summary>
        /// <param name="sender">事件的來源。</param>
        /// <param name="e">這個 <see cref="EventArgs"/> 包含事件資料的實體。</param>
        private async void btnOpenOriginal_ClickAsync(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "開啟檔案",
                Filter = "所有圖片|*.png;*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.dib|PNG|*.png|JPEG|*.jpg;*.jpeg;*.jpe;*.jfif|點陣圖檔案|*.bmp;*.dib",
                RestoreDirectory = true,
                InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.PreventUserOperation(true);

                    picPreview.Image = null;
                    picPreview.Image = await this.ApplyFilterAsync(
                                            await Task.Factory.StartNew(() =>
                                                new Bitmap(openFileDialog.FileName)));

                    this.PreventUserOperation(false);
                }
                catch (Exception)
                {
                    MessageBox.Show("錯誤發生，無法讀取檔案。",
                                    "錯誤",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 以非同步作業的方式，套用旋轉濾鏡。
        /// </summary>
        /// <param name="originalBitmap">原點陣圖。</param>
        /// <returns>工作物件，表示非同步作業。</returns>
        private Task<Bitmap> ApplyFilterAsync(Bitmap originalBitmap)
        {
            this._previousRotateDegrees = numRotateDegrees.Value;
            RotateTransformType rotateType =
                (RotateTransformType)comboBoxRotateType.SelectedValue;
            Func<Bitmap> func = () =>
                ImageTransformStrategyFactory.GetStrategy(rotateType)
                    .RotateImage(originalBitmap, (double)numRotateDegrees.Value);
            if (originalBitmap == null)
            {
                func = () => null;
            }
            else if ((numRotateDegrees.Value % 360) == 0)
            {
                func = () => originalBitmap;
            }
            return Task.Factory.StartNew(func);
        }

        /// <summary>
        /// 以非同步作業的方式，處理旋轉角度 NumericUpDown 的 ValueChanged 事件。
        /// </summary>
        /// <param name="sender">事件的來源。</param>
        /// <param name="e">這個 <see cref="EventArgs"/> 包含事件資料的實體。</param>
        private async void numRotateDegrees_ValueChangedAsync(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                this.PreventUserOperation(true);

                using (Bitmap previewBitmap = new Bitmap(picPreview.Image))
                {
                    picPreview.Image = await this.ApplyFilterAsync(previewBitmap);
                }

                this.PreventUserOperation(false);
            }
        }

        /// <summary>
        /// 以非同步作業的方式，處理旋轉方法下拉式選單的 SelectedIndexChanged 事件。
        /// </summary>
        /// <param name="sender">事件的來源。</param>
        /// <param name="e">這個 <see cref="EventArgs"/> 包含事件資料的實體。</param>
        private async void comboBoxRotateType_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (picPreview.Image != null &&
                this._previousRotateDegrees != numRotateDegrees.Value)
            {
                this.PreventUserOperation(true);

                using (Bitmap previewBitmap = new Bitmap(picPreview.Image))
                {
                    picPreview.Image = await this.ApplyFilterAsync(previewBitmap);
                }

                this.PreventUserOperation(false);
            }
        }

        /// <summary>
        /// 以非同步作業的方式，處理 [儲存] 新圖按鈕的 Click 事件。
        /// </summary>
        /// <param name="sender">事件的來源。</param>
        /// <param name="e">這個 <see cref="EventArgs"/> 包含事件資料的實體。</param>
        private async void btnSaveNewImage_ClickAsync(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "儲存檔案",
                    Filter = "PNG 圖片|*.png|JPG 圖片|*.jpg|BMP 圖片|*.bmp",
                    RestoreDirectory = true,
                    InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.PreventUserOperation(true);

                    ImageFormat imgFormat = GetImageFormat(saveFileDialog);
                    using (Bitmap previewBitmap = new Bitmap(picPreview.Image))
                    {
                        await Task.Factory.StartNew(() =>
                               previewBitmap.Save(saveFileDialog.FileName, imgFormat));
                    }

                    this.PreventUserOperation(false);

                    MessageBox.Show("儲存成功。",
                                    "訊息",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 取得映像的檔案格式。
        /// </summary>
        /// <param name="saveFileDialog">儲存檔案的對話視窗。</param>
        /// <returns>映像的檔案格式。</returns>
        private static ImageFormat GetImageFormat(SaveFileDialog saveFileDialog)
        {
            ImageFormat imgFormat;
            switch (saveFileDialog.FilterIndex)
            {
                case 2:
                    imgFormat = ImageFormat.Jpeg;
                    break;

                case 3:
                    imgFormat = ImageFormat.Bmp;
                    break;

                default:
                    imgFormat = ImageFormat.Png;
                    break;
            }
            return imgFormat;
        }

        /// <summary>
        /// 處理圖片預覽區的 SizeChanged 事件。
        /// </summary>
        /// <param name="sender">事件的來源。</param>
        /// <param name="e">這個 <see cref="EventArgs"/> 包含事件資料的實體。</param>
        private void picPreview_SizeChanged(object sender, EventArgs e)
        {
            if (this.panelProgressRing.Visible)
            {
                this.SetProgressRingLocation();
            }
        }

        /// <summary>
        /// 預防使用者操作。
        /// </summary>
        /// <param name="isPrevented">是否預防使用者操作，是則傳入 <c>true</c>，否則傳入 <c>false</c>。</param>
        private void PreventUserOperation(bool isPrevented)
        {
            this.SwitchUserControls(isPrevented == false);
            this.ShowProgressRing(isPrevented);
        }

        /// <summary>
        /// 切換使用者控制項。
        /// </summary>
        /// <param name="isEnabled">是否啟用使用者控制項，是則傳入 <c>true</c>，否則傳入 <c>false</c>。</param>
        private void SwitchUserControls(bool isEnabled)
        {
            this.numRotateDegrees.Enabled = isEnabled;
            this.btnOpenOriginal.Enabled = isEnabled;
            this.btnSaveNewImage.Enabled = isEnabled;
        }

        #region ProgressRing

        /// <summary>
        /// 初始化 Progress Ring。
        /// </summary>
        private void InitializeProgressRing()
        {
            this.picPreview.SendToBack();
            this.panelProgressRing.Parent = this.picPreview;
            this.panelProgressRing.BringToFront();
        }

        /// <summary>
        /// 顯示 Progress Ring。
        /// </summary>
        /// <param name="isVisible">是否顯示 Progress Ring，是則傳入 <c>true</c>，否則傳入 <c>false</c>。</param>
        private void ShowProgressRing(bool isVisible)
        {
            if (isVisible)
            {
                this.SetProgressRingLocation();
            }
            this.panelProgressRing.Visible = isVisible;
        }

        /// <summary>
        /// 設定 Progress Ring 的位置。
        /// </summary>
        private void SetProgressRingLocation()
        {
            int xOffset = (int)(this.picPreview.Width / 2.0) -
                          (int)(this.panelProgressRing.Width / 2.0);
            int yOffset = (int)(this.picPreview.Height / 2.0) -
                          (int)(this.panelProgressRing.Height / 2.0);
            this.panelProgressRing.Location = new Point(xOffset, yOffset);
        }

        #endregion ProgressRing

        /// <summary>
        /// 初始化旋轉類型的 ComboBox。
        /// </summary>
        private void InitializeRotateTypeComboBox()
        {
            comboBoxRotateType.ValueMember = "Key";
            comboBoxRotateType.DisplayMember = "Value";
            comboBoxRotateType.DataSource = new BindingSource(
                EnumHelper.GetDisplayNameDictionary<RotateTransformType>(), null);
        }
    }
}