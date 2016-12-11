using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                Title = "請選擇一張圖片。",
                Filter = "PNG 圖片 (.png)|*.png|JPG 圖片 (.jpg)|*.jpg|BMP 圖片 (.bmp)|*.bmp",
                RestoreDirectory = true,
                CheckPathExists = true,
                CheckFileExists = true,
                InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picPreview.Image = new Bitmap(openFileDialog.FileName);
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
    }
}
