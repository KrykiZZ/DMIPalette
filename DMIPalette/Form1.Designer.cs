namespace DMIPalette
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureSrcPalette = new System.Windows.Forms.PictureBox();
            this.pictureNewPalette = new System.Windows.Forms.PictureBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonSavePalette = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSrcPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNewPalette)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(424, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(107, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(12, 12);
            this.textPath.Name = "textPath";
            this.textPath.ReadOnly = true;
            this.textPath.Size = new System.Drawing.Size(406, 23);
            this.textPath.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "DMI Files|*.dmi";
            // 
            // pictureSrcPalette
            // 
            this.pictureSrcPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureSrcPalette.Location = new System.Drawing.Point(12, 41);
            this.pictureSrcPalette.Name = "pictureSrcPalette";
            this.pictureSrcPalette.Size = new System.Drawing.Size(200, 200);
            this.pictureSrcPalette.TabIndex = 2;
            this.pictureSrcPalette.TabStop = false;
            // 
            // pictureNewPalette
            // 
            this.pictureNewPalette.BackgroundImage = global::DMIPalette.Properties.Resources.Untitled;
            this.pictureNewPalette.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureNewPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureNewPalette.Location = new System.Drawing.Point(218, 41);
            this.pictureNewPalette.Name = "pictureNewPalette";
            this.pictureNewPalette.Size = new System.Drawing.Size(200, 200);
            this.pictureNewPalette.TabIndex = 3;
            this.pictureNewPalette.TabStop = false;
            this.pictureNewPalette.Click += new System.EventHandler(this.pictureNewPalette_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "PNG Files|*.png";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(424, 71);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(107, 23);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "Swap and Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonSavePalette
            // 
            this.buttonSavePalette.Location = new System.Drawing.Point(424, 42);
            this.buttonSavePalette.Name = "buttonSavePalette";
            this.buttonSavePalette.Size = new System.Drawing.Size(107, 23);
            this.buttonSavePalette.TabIndex = 5;
            this.buttonSavePalette.Text = "Save palette";
            this.buttonSavePalette.UseVisualStyleBackColor = true;
            this.buttonSavePalette.Click += new System.EventHandler(this.buttonSavePalette_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG Files|*.png";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "DMI Files|*.dmi";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 252);
            this.Controls.Add(this.buttonSavePalette);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.pictureNewPalette);
            this.Controls.Add(this.pictureSrcPalette);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.buttonOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DMI Palette Swapper";
            ((System.ComponentModel.ISupportInitialize)(this.pictureSrcPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNewPalette)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonOpen;
        private TextBox textPath;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureSrcPalette;
        private PictureBox pictureNewPalette;
        private OpenFileDialog openFileDialog2;
        private Button buttonExport;
        private Button buttonSavePalette;
        private SaveFileDialog saveFileDialog1;
        private SaveFileDialog saveFileDialog2;
    }
}