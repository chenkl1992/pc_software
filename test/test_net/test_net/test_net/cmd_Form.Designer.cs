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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cmd_Form));
            this.cmd_send_button = new System.Windows.Forms.Button();
            this.command_textbox = new System.Windows.Forms.TextBox();
            this.command_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.add_item_button = new System.Windows.Forms.Button();
            this.del_item_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmd_send_button
            // 
            this.cmd_send_button.Location = new System.Drawing.Point(373, 7);
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
            this.command_textbox.Size = new System.Drawing.Size(348, 21);
            this.command_textbox.TabIndex = 2;
            // 
            // command_listView
            // 
            this.command_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.command_listView.FullRowSelect = true;
            this.command_listView.Location = new System.Drawing.Point(13, 41);
            this.command_listView.Name = "command_listView";
            this.command_listView.Size = new System.Drawing.Size(407, 267);
            this.command_listView.TabIndex = 3;
            this.command_listView.UseCompatibleStateImageBehavior = false;
            this.command_listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 57;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据格式";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 87;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数据";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "延时";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 39;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 40;
            // 
            // add_item_button
            // 
            this.add_item_button.Image = ((System.Drawing.Image)(resources.GetObject("add_item_button.Image")));
            this.add_item_button.Location = new System.Drawing.Point(13, 316);
            this.add_item_button.Name = "add_item_button";
            this.add_item_button.Size = new System.Drawing.Size(32, 30);
            this.add_item_button.TabIndex = 4;
            this.add_item_button.UseVisualStyleBackColor = true;
            this.add_item_button.Click += new System.EventHandler(this.add_item_button_Click);
            // 
            // del_item_button
            // 
            this.del_item_button.Image = ((System.Drawing.Image)(resources.GetObject("del_item_button.Image")));
            this.del_item_button.Location = new System.Drawing.Point(52, 316);
            this.del_item_button.Name = "del_item_button";
            this.del_item_button.Size = new System.Drawing.Size(32, 30);
            this.del_item_button.TabIndex = 5;
            this.del_item_button.UseVisualStyleBackColor = true;
            this.del_item_button.Click += new System.EventHandler(this.del_item_button_Click);
            // 
            // cmd_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 358);
            this.Controls.Add(this.del_item_button);
            this.Controls.Add(this.add_item_button);
            this.Controls.Add(this.command_listView);
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
        private System.Windows.Forms.ListView command_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button add_item_button;
        private System.Windows.Forms.Button del_item_button;
    }
}