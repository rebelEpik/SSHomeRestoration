namespace SSHomeRestoration
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.savePath = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.generalLogTb = new System.Windows.Forms.TextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.saveGeneralLogBtn = new System.Windows.Forms.Button();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.errorLogTb = new System.Windows.Forms.TextBox();
            this.saveErrorLogBtn = new System.Windows.Forms.Button();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saveErrorLogBtn);
            this.groupBox1.Controls.Add(this.errorLogTb);
            this.groupBox1.Controls.Add(this.materialLabel2);
            this.groupBox1.Controls.Add(this.saveGeneralLogBtn);
            this.groupBox1.Controls.Add(this.materialLabel1);
            this.groupBox1.Controls.Add(this.generalLogTb);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-2, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logging";
            // 
            // generalLogTb
            // 
            this.generalLogTb.Location = new System.Drawing.Point(10, 51);
            this.generalLogTb.Name = "generalLogTb";
            this.generalLogTb.ReadOnly = true;
            this.generalLogTb.Size = new System.Drawing.Size(250, 26);
            this.generalLogTb.TabIndex = 0;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(6, 29);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(123, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "General Log Path";
            // 
            // saveGeneralLogBtn
            // 
            this.saveGeneralLogBtn.BackgroundImage = global::SSHomeRestoration.Properties.Resources.if_file_16610;
            this.saveGeneralLogBtn.Location = new System.Drawing.Point(274, 47);
            this.saveGeneralLogBtn.Name = "saveGeneralLogBtn";
            this.saveGeneralLogBtn.Size = new System.Drawing.Size(33, 34);
            this.saveGeneralLogBtn.TabIndex = 2;
            this.saveGeneralLogBtn.UseVisualStyleBackColor = true;
            this.saveGeneralLogBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(6, 92);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(105, 19);
            this.materialLabel2.TabIndex = 3;
            this.materialLabel2.Text = "Error Log Path";
            // 
            // errorLogTb
            // 
            this.errorLogTb.Location = new System.Drawing.Point(10, 114);
            this.errorLogTb.Name = "errorLogTb";
            this.errorLogTb.ReadOnly = true;
            this.errorLogTb.Size = new System.Drawing.Size(250, 26);
            this.errorLogTb.TabIndex = 4;
            // 
            // saveErrorLogBtn
            // 
            this.saveErrorLogBtn.BackgroundImage = global::SSHomeRestoration.Properties.Resources.if_file_16610;
            this.saveErrorLogBtn.Location = new System.Drawing.Point(274, 110);
            this.saveErrorLogBtn.Name = "saveErrorLogBtn";
            this.saveErrorLogBtn.Size = new System.Drawing.Size(33, 34);
            this.saveErrorLogBtn.TabIndex = 5;
            this.saveErrorLogBtn.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = null;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(-24, 234);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(367, 95);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(248, 247);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(77, 42);
            this.materialRaisedButton1.TabIndex = 2;
            this.materialRaisedButton1.Text = "Save Settings";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(337, 301);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog savePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveGeneralLogBtn;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.TextBox generalLogTb;
        private System.Windows.Forms.Button saveErrorLogBtn;
        private System.Windows.Forms.TextBox errorLogTb;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
    }
}