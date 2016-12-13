using Image.Transform.Rotate.Services;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image.Transform.Rotate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnOpenOriginal_ClickAsync(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "開啟檔案",
                Filter = "PNG (.png)|*.png|JPEG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|點陣圖檔案 (*.bmp;*.dib)|*.bmp;*.dib",
                RestoreDirectory = true,
                InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap previewBitmap = await Task.Factory.StartNew(() =>
                                                      new Bitmap(openFileDialog.FileName));
                    if (numRotateDegrees.Value.Equals(0) == false)
                    {
                        previewBitmap = await this.ApplyFilterAsync(previewBitmap);
                    }
                    picPreview.Image = previewBitmap;
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
            if (originalBitmap == null)
            {
                return null;
            }
            return Task.Factory.StartNew(() =>
                        new ImageTransformService()
                            .RotateImage(originalBitmap, (double)numRotateDegrees.Value));
        }

        private async void numRotateDegrees_ValueChangedAsync(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                picPreview.Image = await this.ApplyFilterAsync(picPreview.Image as Bitmap);
            }
        }

        private async void btnSaveNewImage_ClickAsync(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "儲存檔案",
                    Filter = "PNG 圖片 (.png)|*.png|JPG 圖片 (.jpg)|*.jpg|BMP 圖片 (.bmp)|*.bmp",
                    RestoreDirectory = true,
                    InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat imgFormat = GetImageFormat(saveFileDialog);
                    using (Stream stream = saveFileDialog.OpenFile())
                    {
                        await Task.Factory.StartNew(() =>
                                   picPreview.Image.Save(stream, imgFormat));
                    }
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
    }
}