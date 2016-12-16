using Image.Transform.Rotate.Services;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image.Transform.Rotate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.InitializeProgressRing();
        }

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

        private Task<Bitmap> ApplyFilterAsync(Bitmap originalBitmap)
        {
            Func<Bitmap> func = () =>
                        new ImageTransformService()
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

        private static ImageFormat GetImageFormat(SaveFileDialog saveFileDialog)
        {
            ImageFormat imgFormat = ImageFormat.Png;
            switch (saveFileDialog.FilterIndex)
            {
                case 2:
                    imgFormat = ImageFormat.Jpeg;
                    break;

                case 3:
                    imgFormat = ImageFormat.Bmp;
                    break;
            }
            return imgFormat;
        }

        private void picPreview_SizeChanged(object sender, EventArgs e)
        {
            if (this.panelProgressRing.Visible)
            {
                this.SetProgressRingLocation();
            }
        }

        private void PreventUserOperation(bool isPrevented)
        {
            this.SwitchUserControls(isPrevented == false);
            this.ShowProgressRing(isPrevented);
        }

        private void SwitchUserControls(bool isEnabled)
        {
            this.numRotateDegrees.Enabled = isEnabled;
            this.btnOpenOriginal.Enabled = isEnabled;
            this.btnSaveNewImage.Enabled = isEnabled;
        }

        #region ProgressRing

        private void InitializeProgressRing()
        {
            this.picPreview.SendToBack();
            this.panelProgressRing.Parent = this.picPreview;
            this.panelProgressRing.BringToFront();
        }

        private void ShowProgressRing(bool isVisible)
        {
            if (isVisible)
            {
                this.SetProgressRingLocation();
            }
            this.panelProgressRing.Visible = isVisible;
        }

        private void SetProgressRingLocation()
        {
            int xOffset = (int)(this.picPreview.Width / 2.0) -
                          (int)(this.panelProgressRing.Width / 2.0);
            int yOffset = (int)(this.picPreview.Height / 2.0) -
                          (int)(this.panelProgressRing.Height / 2.0);
            this.panelProgressRing.Location = new Point(xOffset, yOffset);
        }

        #endregion ProgressRing
    }
}