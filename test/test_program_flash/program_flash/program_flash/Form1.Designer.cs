namespace program_flash
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.select = new System.Windows.Forms.Button();
            this.startprogram = new System.Windows.Forms.Button();
            this.program_progress = new System.Windows.Forms.ProgressBar();
            this.logbox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.file_path_box = new System.Windows.Forms.TextBox();
            this.clear_logbox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // select
            // 
            this.select.Location = new System.Drawing.Point(475, 19);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(75, 23);
            this.select.TabIndex = 0;
            this.select.Text = "选择";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // startprogram
            // 
            this.startprogram.Location = new System.Drawing.Point(12, 196);
            this.startprogram.Name = "startprogram";
            this.startprogram.Size = new System.Drawing.Size(64, 43);
            this.startprogram.TabIndex = 1;
            this.startprogram.Text = "开始";
            this.startprogram.UseVisualStyleBackColor = true;
            this.startprogram.Click += new System.EventHandler(this.startprogram_Click);
            // 
            // program_progress
            // 
            this.program_progress.Location = new System.Drawing.Point(82, 219);
            this.program_progress.Name = "program_progress";
            this.program_progress.Size = new System.Drawing.Size(468, 20);
            this.program_progress.TabIndex = 2;
            // 
            // logbox
            // 
            this.logbox.Location = new System.Drawing.Point(12, 56);
            this.logbox.Multiline = true;
            this.logbox.Name = "logbox";
            this.logbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logbox.Size = new System.Drawing.Size(538, 134);
            this.logbox.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // file_path_box
            // 
            this.file_path_box.Location = new System.Drawing.Point(12, 19);
            this.file_path_box.Name = "file_path_box";
            this.file_path_box.Size = new System.Drawing.Size(448, 21);
            this.file_path_box.TabIndex = 4;
            // 
            // clear_logbox
            // 
            this.clear_logbox.Location = new System.Drawing.Point(475, 190);
            this.clear_logbox.Name = "clear_logbox";
            this.clear_logbox.Size = new System.Drawing.Size(75, 23);
            this.clear_logbox.TabIndex = 0;
            this.clear_logbox.Text = "清空";
            this.clear_logbox.Click += new System.EventHandler(this.clear_logbox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 256);
            this.Controls.Add(this.clear_logbox);
            this.Controls.Add(this.file_path_box);
            this.Controls.Add(this.logbox);
            this.Controls.Add(this.program_progress);
            this.Controls.Add(this.startprogram);
            this.Controls.Add(this.select);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button startprogram;
        private System.Windows.Forms.ProgressBar program_progress;
        private System.Windows.Forms.TextBox logbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox file_path_box;
        private System.Windows.Forms.Button clear_logbox;
    }
}

