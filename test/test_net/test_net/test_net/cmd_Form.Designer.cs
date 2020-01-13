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
            this.add_item_button = new System.Windows.Forms.Button();
            this.del_item_button = new System.Windows.Forms.Button();
            this.clean_lst_button = new System.Windows.Forms.Button();
            this.command_listView = new ListViewEmbeddedControls.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // add_item_button
            // 
            this.add_item_button.Image = ((System.Drawing.Image)(resources.GetObject("add_item_button.Image")));
            this.add_item_button.Location = new System.Drawing.Point(12, 289);
            this.add_item_button.Name = "add_item_button";
            this.add_item_button.Size = new System.Drawing.Size(32, 30);
            this.add_item_button.TabIndex = 4;
            this.add_item_button.UseVisualStyleBackColor = true;
            this.add_item_button.Click += new System.EventHandler(this.add_item_button_Click);
            // 
            // del_item_button
            // 
            this.del_item_button.Image = ((System.Drawing.Image)(resources.GetObject("del_item_button.Image")));
            this.del_item_button.Location = new System.Drawing.Point(50, 289);
            this.del_item_button.Name = "del_item_button";
            this.del_item_button.Size = new System.Drawing.Size(32, 30);
            this.del_item_button.TabIndex = 5;
            this.del_item_button.UseVisualStyleBackColor = true;
            this.del_item_button.Click += new System.EventHandler(this.del_item_button_Click);
            // 
            // clean_lst_button
            // 
            this.clean_lst_button.Location = new System.Drawing.Point(342, 289);
            this.clean_lst_button.Name = "clean_lst_button";
            this.clean_lst_button.Size = new System.Drawing.Size(53, 28);
            this.clean_lst_button.TabIndex = 6;
            this.clean_lst_button.Text = "清空";
            this.clean_lst_button.UseVisualStyleBackColor = true;
            this.clean_lst_button.Click += new System.EventHandler(this.clean_lst_button_Click);
            // 
            // command_listView
            // 
            this.command_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.command_listView.FullRowSelect = true;
            this.command_listView.Location = new System.Drawing.Point(12, 12);
            this.command_listView.MultiSelect = false;
            this.command_listView.Name = "command_listView";
            this.command_listView.Size = new System.Drawing.Size(383, 271);
            this.command_listView.TabIndex = 0;
            this.command_listView.UseCompatibleStateImageBehavior = false;
            this.command_listView.View = System.Windows.Forms.View.Details;
            this.command_listView.SelectedIndexChanged += new System.EventHandler(this.command_listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 1;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数据格式";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数据";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 141;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "延时";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 40;
            // 
            // cmd_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 329);
            this.Controls.Add(this.clean_lst_button);
            this.Controls.Add(this.command_listView);
            this.Controls.Add(this.del_item_button);
            this.Controls.Add(this.add_item_button);
            this.Name = "cmd_Form";
            this.Text = "命令列表";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button add_item_button;
        private System.Windows.Forms.Button del_item_button;
        //private System.Windows.Forms.ListView command_listView;
        private ListViewEmbeddedControls.ListViewEx command_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button clean_lst_button;
    }
}