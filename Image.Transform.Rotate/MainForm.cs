using Image.Transform.Rotate.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
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

        private void btnOpenOriginal_Click(object sender, EventArgs e)
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
                    Bitmap previewBitmap = new Bitmap(openFileDialog.FileName);
                    if (numRotateDegrees.Value.Equals(0) == false)
                    {
                        previewBitmap = this.ApplyFilter(previewBitmap);
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

        private Bitmap ApplyFilter(Bitmap originalBitmap)
        {
            if (originalBitmap == null)
            {
                return new Bitmap(picPreview.Width, picPreview.Height);
            }
            return new ImageTransformService()
                .RotateImage(originalBitmap, (double)numRotateDegrees.Value);
        }

        private void numRotateDegrees_ValueChanged(object sender, EventArgs e)
        {
            picPreview.Image = this.ApplyFilter(picPreview.Image as Bitmap);
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
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

                    using (Stream stream = saveFileDialog.OpenFile())
                    {
                        picPreview.Image.Save(stream, imgFormat);
                    }
                }
            }
        }
    }
}
