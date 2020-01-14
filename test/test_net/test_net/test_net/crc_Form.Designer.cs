namespace test_net
{
    partial class crc_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.crc_cal_button = new System.Windows.Forms.Button();
            this.crc_textbox = new System.Windows.Forms.TextBox();
            this.crc_cal_combobox = new System.Windows.Forms.ComboBox();
            this.crc_cal_result_textbox = new System.Windows.Forms.TextBox();
            this.crc_cpy_clipbd_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "计算结果：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "校验算法：";
            // 
            // crc_cal_button
            // 
            this.crc_cal_button.Location = new System.Drawing.Point(195, 214);
            this.crc_cal_button.Name = "crc_cal_button";
            this.crc_cal_button.Size = new System.Drawing.Size(59, 23);
            this.crc_cal_button.TabIndex = 2;
            this.crc_cal_button.Text = "计算";
            this.crc_cal_button.UseVisualStyleBackColor = true;
            this.crc_cal_button.Click += new System.EventHandler(this.crc_cal_button_Click);
            // 
            // crc_textbox
            // 
            this.crc_textbox.Location = new System.Drawing.Point(13, 13);
            this.crc_textbox.Multiline = true;
            this.crc_textbox.Name = "crc_textbox";
            this.crc_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.crc_textbox.Size = new System.Drawing.Size(241, 195);
            this.crc_textbox.TabIndex = 3;
            // 
            // crc_cal_combobox
            // 
            this.crc_cal_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.crc_cal_combobox.FormattingEnabled = true;
            this.crc_cal_combobox.Location = new System.Drawing.Point(83, 216);
            this.crc_cal_combobox.Name = "crc_cal_combobox";
            this.crc_cal_combobox.Size = new System.Drawing.Size(94, 20);
            this.crc_cal_combobox.TabIndex = 4;
            // 
            // crc_cal_result_textbox
            // 
            this.crc_cal_result_textbox.Location = new System.Drawing.Point(83, 251);
            this.crc_cal_result_textbox.Name = "crc_cal_result_textbox";
            this.crc_cal_result_textbox.Size = new System.Drawing.Size(70, 21);
            this.crc_cal_result_textbox.TabIndex = 5;
            // 
            // crc_cpy_clipbd_button
            // 
            this.crc_cpy_clipbd_button.Location = new System.Drawing.Point(161, 249);
            this.crc_cpy_clipbd_button.Name = "crc_cpy_clipbd_button";
            this.crc_cpy_clipbd_button.Size = new System.Drawing.Size(93, 23);
            this.crc_cpy_clipbd_button.TabIndex = 6;
            this.crc_cpy_clipbd_button.Text = "复制到剪贴板";
            this.crc_cpy_clipbd_button.UseVisualStyleBackColor = true;
            this.crc_cpy_clipbd_button.Click += new System.EventHandler(this.crc_cpy_clipbd_button_Click);
            // 
            // crc_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 285);
            this.Controls.Add(this.crc_cpy_clipbd_button);
            this.Controls.Add(this.crc_cal_result_textbox);
            this.Controls.Add(this.crc_cal_combobox);
            this.Controls.Add(this.crc_textbox);
            this.Controls.Add(this.crc_cal_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "crc_Form";
            this.Text = "crc计算";
            this.Load += new System.EventHandler(this.crc_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button crc_cal_button;
        private System.Windows.Forms.TextBox crc_textbox;
        private System.Windows.Forms.ComboBox crc_cal_combobox;
        private System.Windows.Forms.TextBox crc_cal_result_textbox;
        private System.Windows.Forms.Button crc_cpy_clipbd_button;
    }
}