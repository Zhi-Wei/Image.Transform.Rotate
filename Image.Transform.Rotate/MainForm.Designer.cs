namespace Image.Transform.Rotate
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelRotateDegrees = new System.Windows.Forms.Label();
            this.numRotateDegrees = new System.Windows.Forms.NumericUpDown();
            this.btnOpenOriginal = new System.Windows.Forms.Button();
            this.btnSaveNewImage = new System.Windows.Forms.Button();
            this.toolTipRotateDegrees = new System.Windows.Forms.ToolTip(this.components);
            this.panelProgressRing = new System.Windows.Forms.Panel();
            this.picProgressRing = new System.Windows.Forms.PictureBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numRotateDegrees)).BeginInit();
            this.panelProgressRing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgressRing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRotateDegrees
            // 
            this.labelRotateDegrees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRotateDegrees.AutoSize = true;
            this.labelRotateDegrees.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelRotateDegrees.Location = new System.Drawing.Point(816, 10);
            this.labelRotateDegrees.Name = "labelRotateDegrees";
            this.labelRotateDegrees.Size = new System.Drawing.Size(177, 40);
            this.labelRotateDegrees.TabIndex = 4;
            this.labelRotateDegrees.Text = "旋轉角度：";
            // 
            // numRotateDegrees
            // 
            this.numRotateDegrees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRotateDegrees.DecimalPlaces = 2;
            this.numRotateDegrees.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numRotateDegrees.Location = new System.Drawing.Point(823, 53);
            this.numRotateDegrees.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRotateDegrees.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numRotateDegrees.Name = "numRotateDegrees";
            this.numRotateDegrees.Size = new System.Drawing.Size(167, 45);
            this.numRotateDegrees.TabIndex = 2;
            this.numRotateDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTipRotateDegrees.SetToolTip(this.numRotateDegrees, "請輸入 -360°～360° 的旋轉角度。");
            this.numRotateDegrees.ValueChanged += new System.EventHandler(this.numRotateDegrees_ValueChangedAsync);
            // 
            // btnOpenOriginal
            // 
            this.btnOpenOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenOriginal.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOpenOriginal.Location = new System.Drawing.Point(823, 594);
            this.btnOpenOriginal.Name = "btnOpenOriginal";
            this.btnOpenOriginal.Size = new System.Drawing.Size(167, 50);
            this.btnOpenOriginal.TabIndex = 1;
            this.btnOpenOriginal.Text = "開啟";
            this.btnOpenOriginal.UseVisualStyleBackColor = true;
            this.btnOpenOriginal.Click += new System.EventHandler(this.btnOpenOriginal_ClickAsync);
            // 
            // btnSaveNewImage
            // 
            this.btnSaveNewImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNewImage.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSaveNewImage.Location = new System.Drawing.Point(823, 650);
            this.btnSaveNewImage.Name = "btnSaveNewImage";
            this.btnSaveNewImage.Size = new System.Drawing.Size(167, 50);
            this.btnSaveNewImage.TabIndex = 3;
            this.btnSaveNewImage.Text = "儲存";
            this.btnSaveNewImage.UseVisualStyleBackColor = true;
            this.btnSaveNewImage.Click += new System.EventHandler(this.btnSaveNewImage_ClickAsync);
            // 
            // panelProgressRing
            // 
            this.panelProgressRing.BackColor = System.Drawing.Color.Transparent;
            this.panelProgressRing.Controls.Add(this.picProgressRing);
            this.panelProgressRing.Location = new System.Drawing.Point(340, 285);
            this.panelProgressRing.Name = "panelProgressRing";
            this.panelProgressRing.Size = new System.Drawing.Size(120, 120);
            this.panelProgressRing.TabIndex = 5;
            // 
            // picProgressRing
            // 
            this.picProgressRing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picProgressRing.Image = global::Image.Transform.Rotate.Properties.Resources.Ripple;
            this.picProgressRing.Location = new System.Drawing.Point(0, 0);
            this.picProgressRing.Margin = new System.Windows.Forms.Padding(0);
            this.picProgressRing.Name = "picProgressRing";
            this.picProgressRing.Size = new System.Drawing.Size(120, 120);
            this.picProgressRing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProgressRing.TabIndex = 0;
            this.picProgressRing.TabStop = false;
            // 
            // picPreview
            // 
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picPreview.Location = new System.Drawing.Point(10, 10);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(800, 690);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 712);
            this.Controls.Add(this.panelProgressRing);
            this.Controls.Add(this.btnSaveNewImage);
            this.Controls.Add(this.btnOpenOriginal);
            this.Controls.Add(this.numRotateDegrees);
            this.Controls.Add(this.labelRotateDegrees);
            this.Controls.Add(this.picPreview);
            this.Name = "MainForm";
            this.Text = "影像旋轉";
            ((System.ComponentModel.ISupportInitialize)(this.numRotateDegrees)).EndInit();
            this.panelProgressRing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProgressRing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label labelRotateDegrees;
        private System.Windows.Forms.NumericUpDown numRotateDegrees;
        private System.Windows.Forms.Button btnOpenOriginal;
        private System.Windows.Forms.Button btnSaveNewImage;
        private System.Windows.Forms.ToolTip toolTipRotateDegrees;
        private System.Windows.Forms.Panel panelProgressRing;
        private System.Windows.Forms.PictureBox picProgressRing;
    }
}

