namespace test_net
{
    partial class net_tool
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(net_tool));
            this.net_set_Box = new System.Windows.Forms.GroupBox();
            this.net_type_box = new System.Windows.Forms.ComboBox();
            this.port_box = new System.Windows.Forms.TextBox();
            this.ip_box = new System.Windows.Forms.TextBox();
            this.net_connect_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.send_peri_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.send_peri_checkbox = new System.Windows.Forms.CheckBox();
            this.hex_send_checkbox = new System.Windows.Forms.CheckBox();
            this.hex_display_checkbox = new System.Windows.Forms.CheckBox();
            this.clear_recive_wind_button = new System.Windows.Forms.Button();
            this.send_button = new System.Windows.Forms.Button();
            this.send_box = new System.Windows.Forms.TextBox();
            this.recive_box = new System.Windows.Forms.TextBox();
            this.receive_box_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.periodic_send_timer = new System.Windows.Forms.Timer(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.net_set_Box.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.receive_box_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // net_set_Box
            // 
            this.net_set_Box.Controls.Add(this.net_type_box);
            this.net_set_Box.Controls.Add(this.port_box);
            this.net_set_Box.Controls.Add(this.ip_box);
            this.net_set_Box.Controls.Add(this.net_connect_button);
            this.net_set_Box.Controls.Add(this.label3);
            this.net_set_Box.Controls.Add(this.label2);
            this.net_set_Box.Controls.Add(this.label1);
            this.net_set_Box.Location = new System.Drawing.Point(12, 12);
            this.net_set_Box.Name = "net_set_Box";
            this.net_set_Box.Size = new System.Drawing.Size(166, 144);
            this.net_set_Box.TabIndex = 0;
            this.net_set_Box.TabStop = false;
            this.net_set_Box.Text = "网络设置";
            // 
            // net_type_box
            // 
            this.net_type_box.AllowDrop = true;
            this.net_type_box.FormattingEnabled = true;
            this.net_type_box.Location = new System.Drawing.Point(57, 20);
            this.net_type_box.Name = "net_type_box";
            this.net_type_box.Size = new System.Drawing.Size(100, 20);
            this.net_type_box.TabIndex = 1;
            this.net_type_box.SelectedIndexChanged += new System.EventHandler(this.net_type_box_SelectedIndexChanged);
            // 
            // port_box
            // 
            this.port_box.Location = new System.Drawing.Point(57, 70);
            this.port_box.Name = "port_box";
            this.port_box.Size = new System.Drawing.Size(100, 21);
            this.port_box.TabIndex = 3;
            // 
            // ip_box
            // 
            this.ip_box.Location = new System.Drawing.Point(57, 43);
            this.ip_box.Name = "ip_box";
            this.ip_box.Size = new System.Drawing.Size(100, 21);
            this.ip_box.TabIndex = 1;
            // 
            // net_connect_button
            // 
            this.net_connect_button.BackColor = System.Drawing.Color.LightGreen;
            this.net_connect_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.net_connect_button.Location = new System.Drawing.Point(38, 107);
            this.net_connect_button.Name = "net_connect_button";
            this.net_connect_button.Size = new System.Drawing.Size(90, 28);
            this.net_connect_button.TabIndex = 1;
            this.net_connect_button.Text = "监听";
            this.net_connect_button.UseVisualStyleBackColor = false;
            this.net_connect_button.Click += new System.EventHandler(this.net_connect_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ip地址";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 100);
            this.tabPage2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.send_peri_textbox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.send_peri_checkbox);
            this.groupBox1.Controls.Add(this.hex_send_checkbox);
            this.groupBox1.Controls.Add(this.hex_display_checkbox);
            this.groupBox1.Controls.Add(this.clear_recive_wind_button);
            this.groupBox1.Controls.Add(this.send_button);
            this.groupBox1.Controls.Add(this.send_box);
            this.groupBox1.Controls.Add(this.recive_box);
            this.groupBox1.Location = new System.Drawing.Point(193, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 226);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信窗口";
            // 
            // send_peri_textbox
            // 
            this.send_peri_textbox.Location = new System.Drawing.Point(296, 169);
            this.send_peri_textbox.Name = "send_peri_textbox";
            this.send_peri_textbox.Size = new System.Drawing.Size(51, 21);
            this.send_peri_textbox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "ms/次";
            // 
            // send_peri_checkbox
            // 
            this.send_peri_checkbox.AutoSize = true;
            this.send_peri_checkbox.Location = new System.Drawing.Point(223, 171);
            this.send_peri_checkbox.Name = "send_peri_checkbox";
            this.send_peri_checkbox.Size = new System.Drawing.Size(72, 16);
            this.send_peri_checkbox.TabIndex = 9;
            this.send_peri_checkbox.Text = "定时发送";
            this.send_peri_checkbox.UseVisualStyleBackColor = true;
            this.send_peri_checkbox.CheckedChanged += new System.EventHandler(this.send_peri_checkbox_CheckedChanged);
            // 
            // hex_send_checkbox
            // 
            this.hex_send_checkbox.AutoSize = true;
            this.hex_send_checkbox.Location = new System.Drawing.Point(148, 171);
            this.hex_send_checkbox.Name = "hex_send_checkbox";
            this.hex_send_checkbox.Size = new System.Drawing.Size(66, 16);
            this.hex_send_checkbox.TabIndex = 7;
            this.hex_send_checkbox.Text = "hex发送";
            this.hex_send_checkbox.UseVisualStyleBackColor = true;
            this.hex_send_checkbox.CheckedChanged += new System.EventHandler(this.hex_send_checkbox_CheckedChanged);
            // 
            // hex_display_checkbox
            // 
            this.hex_display_checkbox.AutoSize = true;
            this.hex_display_checkbox.Location = new System.Drawing.Point(79, 171);
            this.hex_display_checkbox.Name = "hex_display_checkbox";
            this.hex_display_checkbox.Size = new System.Drawing.Size(66, 16);
            this.hex_display_checkbox.TabIndex = 6;
            this.hex_display_checkbox.Text = "hex显示";
            this.hex_display_checkbox.UseVisualStyleBackColor = true;
            this.hex_display_checkbox.CheckedChanged += new System.EventHandler(this.hex_display_checkbox_CheckedChanged);
            // 
            // clear_recive_wind_button
            // 
            this.clear_recive_wind_button.Location = new System.Drawing.Point(15, 167);
            this.clear_recive_wind_button.Name = "clear_recive_wind_button";
            this.clear_recive_wind_button.Size = new System.Drawing.Size(54, 23);
            this.clear_recive_wind_button.TabIndex = 5;
            this.clear_recive_wind_button.Text = "清除";
            this.clear_recive_wind_button.UseVisualStyleBackColor = true;
            this.clear_recive_wind_button.Click += new System.EventHandler(this.clear_recive_wind_button_Click);
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(351, 193);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(65, 24);
            this.send_button.TabIndex = 4;
            this.send_button.Text = "发送";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // send_box
            // 
            this.send_box.Location = new System.Drawing.Point(15, 196);
            this.send_box.Name = "send_box";
            this.send_box.Size = new System.Drawing.Size(332, 21);
            this.send_box.TabIndex = 1;
            // 
            // recive_box
            // 
            this.recive_box.ContextMenuStrip = this.receive_box_contextMenuStrip;
            this.recive_box.Location = new System.Drawing.Point(15, 20);
            this.recive_box.Multiline = true;
            this.recive_box.Name = "recive_box";
            this.recive_box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.recive_box.Size = new System.Drawing.Size(401, 141);
            this.recive_box.TabIndex = 0;
            // 
            // receive_box_contextMenuStrip
            // 
            this.receive_box_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.clearScreenToolStripMenuItem});
            this.receive_box_contextMenuStrip.Name = "receive_box_contextMenuStrip";
            this.receive_box_contextMenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // periodic_send_timer
            // 
            this.periodic_send_timer.Tick += new System.EventHandler(this.periodic_send_timer_Tick);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "copy";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "save as";
            // 
            // clearScreenToolStripMenuItem
            // 
            this.clearScreenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearScreenToolStripMenuItem.Image")));
            this.clearScreenToolStripMenuItem.Name = "clearScreenToolStripMenuItem";
            this.clearScreenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearScreenToolStripMenuItem.Text = "clear screen";
            this.clearScreenToolStripMenuItem.Click += new System.EventHandler(this.clearScreenToolStripMenuItem_Click);
            // 
            // net_tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 250);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.net_set_Box);
            this.Name = "net_tool";
            this.Text = "网络调试";
            this.Load += new System.EventHandler(this.net_tool_Load);
            this.net_set_Box.ResumeLayout(false);
            this.net_set_Box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.receive_box_contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox net_set_Box;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox net_type_box;
        private System.Windows.Forms.TextBox port_box;
        private System.Windows.Forms.TextBox ip_box;
        private System.Windows.Forms.Button net_connect_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.TextBox send_box;
        private System.Windows.Forms.TextBox recive_box;
        private System.Windows.Forms.TextBox send_peri_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox send_peri_checkbox;
        private System.Windows.Forms.CheckBox hex_send_checkbox;
        private System.Windows.Forms.CheckBox hex_display_checkbox;
        private System.Windows.Forms.Button clear_recive_wind_button;
        private System.Windows.Forms.Timer periodic_send_timer;
        private System.Windows.Forms.ContextMenuStrip receive_box_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearScreenToolStripMenuItem;
    }
}

