namespace test_net
{
    partial class cmd_Form
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
            this.cmd_send_button = new System.Windows.Forms.Button();
            this.command_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmd_send_button
            // 
            this.cmd_send_button.Location = new System.Drawing.Point(273, 7);
            this.cmd_send_button.Name = "cmd_send_button";
            this.cmd_send_button.Size = new System.Drawing.Size(47, 28);
            this.cmd_send_button.TabIndex = 1;
            this.cmd_send_button.Text = "发送";
            this.cmd_send_button.UseVisualStyleBackColor = true;
            this.cmd_send_button.Click += new System.EventHandler(this.cmd_send_button_Click);
            // 
            // command_textbox
            // 
            this.command_textbox.Location = new System.Drawing.Point(12, 12);
            this.command_textbox.Name = "command_textbox";
            this.command_textbox.Size = new System.Drawing.Size(255, 21);
            this.command_textbox.TabIndex = 2;
            // 
            // cmd_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 130);
            this.Controls.Add(this.command_textbox);
            this.Controls.Add(this.cmd_send_button);
            this.Name = "cmd_Form";
            this.Text = "命令列表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmd_send_button;
        private System.Windows.Forms.TextBox command_textbox;
    }
}