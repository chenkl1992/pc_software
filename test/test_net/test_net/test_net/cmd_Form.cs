using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_net
{
    public delegate void add_send_command(string str);
    public partial class cmd_Form : Form
    {
        public cmd_Form()
        {
            InitializeComponent();
        }
        public event add_send_command send_command;
        private void cmd_send_button_Click(object sender, EventArgs e)
        {
            send_command(command_textbox.Text);
            //this.Close();
        }

        int item_num = 0;
        private void add_item_button_Click(object sender, EventArgs e)
        {
            add_item(item_num);
            item_num++;
        }

        private void add_item(int i)
        {
            listview(true);
            info_text(i);
            format_choice(i);
            send_text(i);
            sendDelay_text(i);
            send_button(i);
        }

        private void delete_item(int i)
        {        
            //删除选中项
            remove_item(i);
            //将其余 item 上移


            listview(false);     
        }
        private void listview(bool act)
        {
            ListViewItem[] cmdlst_item = new ListViewItem[1];

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 25);//分别是宽和高
            command_listView.SmallImageList = imgList;

            cmdlst_item[0] = new ListViewItem(new string[] { "", "", "", "", "" });
            if (act == true)
            {
                this.command_listView.Items.AddRange(cmdlst_item);
            }
            else
            {
                this.command_listView.Items.Remove(command_listView.SelectedItems[0]);    
            }
        }

        enum item_info{  TEXTBOX_INFO, COMBOBOX, TEXTBOX_DATA, TEXTBOX_DELAY, BUTTON_SEND };
        
        private void send_button(int i)
        {
            Button m_button = new Button();
            //按钮的位置
            m_button.Click += new EventHandler(item_send_button_Click);
            m_button.Text = "发送"+ i.ToString();
            m_button.Tag = "Btn:" + i.ToString();
            m_button.BackColor = Color.LightBlue;
            m_button.Font = new Font("黑体", 7);
            m_button.Size = new Size(this.command_listView.Items[i].SubItems[(int)item_info.BUTTON_SEND].Bounds.Width - 1, this.command_listView.Items[i].SubItems[(int)item_info.BUTTON_SEND].Bounds.Height - 1);
            m_button.Location = new Point(this.command_listView.Items[i].SubItems[(int)item_info.BUTTON_SEND].Bounds.Left - 1, this.command_listView.Items[i].SubItems[(int)item_info.BUTTON_SEND].Bounds.Top - 1);
            this.command_listView.Controls.Add(m_button);
        }

        private void sendDelay_text(int i)
        {
            TextBox delay_parm = new TextBox();
            //延时框的位置
            delay_parm.Tag = "Dly:" + i.ToString();
            delay_parm.Text = "0";
            delay_parm.Size = new Size(this.command_listView.Items[i].SubItems[3].Bounds.Width - 1, this.command_listView.Items[i].SubItems[3].Bounds.Height - 1);
            delay_parm.Location = new Point(this.command_listView.Items[i].SubItems[3].Bounds.Left - 1, this.command_listView.Items[i].SubItems[3].Bounds.Top - 1);
            this.command_listView.Controls.Add(delay_parm);
        }

        private void send_text(int i)
        {
            TextBox send_data = new TextBox();
            //发送框的位置
            send_data.Tag = "Data:" + i.ToString();
            send_data.Size = new Size(this.command_listView.Items[i].SubItems[2].Bounds.Width - 1, this.command_listView.Items[i].SubItems[2].Bounds.Height - 1);
            send_data.Location = new Point(this.command_listView.Items[i].SubItems[2].Bounds.Left - 1, this.command_listView.Items[i].SubItems[2].Bounds.Top - 1);
            this.command_listView.Controls.Add(send_data);
        }

        private void format_choice(int i)
        {
            ComboBox send_format = new ComboBox();
            send_format.Tag = "Cmb:"+ i.ToString();
            send_format.Items.Add("十六进制");
            send_format.Items.Add("字符串");
            send_format.SelectedIndex = 0;
            send_format.Size = new Size(this.command_listView.Items[i].SubItems[1].Bounds.Width - 1, this.command_listView.Items[i].SubItems[1].Bounds.Height - 1);
            send_format.Location = new Point(this.command_listView.Items[i].SubItems[1].Bounds.Left - 1, this.command_listView.Items[i].SubItems[1].Bounds.Top - 1);
            this.command_listView.Controls.Add(send_format);
        }

        private void info_text(int i)
        {
            TextBox add_info = new TextBox();
            add_info.Tag = "Info:" + i.ToString();
            add_info.Size = new Size(this.command_listView.Items[i].SubItems[1].Bounds.Width - 35, this.command_listView.Items[i].SubItems[1].Bounds.Height -1);
            add_info.Location = new Point(this.command_listView.Items[i].SubItems[0].Bounds.Left, this.command_listView.Items[i].SubItems[0].Bounds.Top - 1);
            this.command_listView.Controls.Add(add_info);
        }

        private void item_send_button_Click(object sender, EventArgs e)
        {
            Button temp_btn = (Button)sender;
            Control item;
            int off_set = 0;
            off_set = temp_btn.Tag.ToString().IndexOf(":");
            if (off_set != 0)
            {
                string num = temp_btn.Tag.ToString().Substring(off_set + 1);
                item = m_find_item(Convert.ToInt32(num), (int)item_info.TEXTBOX_DATA);
                TextBox test = item as TextBox;
                MessageBox.Show(test.Text);
            }
        }
        private Control m_find_item(int item_num, int item_name)
        {
            Control err = null;
            int off_set = 0;
            foreach (Control item in this.command_listView.Controls)
            {
                if (item_name == (int)item_info.BUTTON_SEND)
                {
                    if (item is Button)
                    {
                        Button test = item as Button;
                        off_set = test.Tag.ToString().IndexOf(":");
                        if (off_set != 0)
                        {
                            string num = test.Tag.ToString().Substring(off_set + 1);
                            string id = test.Tag.ToString().Substring(0, off_set);
                            if (id.Equals("Btn"))
                            {
                                if (num.Equals(item_num.ToString()))
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
                else if (item_name == (int)item_info.COMBOBOX)
                {
                    if (item is ComboBox)
                    {
                        ComboBox test = item as ComboBox;
                        off_set = test.Tag.ToString().IndexOf(":");
                        if (off_set != 0)
                        {
                            string num = test.Tag.ToString().Substring(off_set + 1);
                            string id = test.Tag.ToString().Substring(0, off_set);
                            if (id.Equals("Cmb"))
                            {
                                if (num.Equals(item_num.ToString()))
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
                else if (item_name == (int)item_info.TEXTBOX_DATA)
                {
                    if (item is TextBox)
                    {
                        TextBox test = item as TextBox;
                        off_set = test.Tag.ToString().IndexOf(":");
                        if (off_set != 0)
                        {
                            string num = test.Tag.ToString().Substring(off_set + 1);
                            string id = test.Tag.ToString().Substring(0, off_set);
                            if (id.Equals("Data"))
                            {
                                if (num.Equals(item_num.ToString()))
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
                else if (item_name == (int)item_info.TEXTBOX_DELAY)
                {
                    if (item is TextBox)
                    {
                        TextBox test = item as TextBox;
                        off_set = test.Tag.ToString().IndexOf(":");
                        if (off_set != 0)
                        {
                            string num = test.Tag.ToString().Substring(off_set + 1);
                            string id = test.Tag.ToString().Substring(0, off_set);
                            if (id.Equals("Dly"))
                            {
                                if (num.Equals(item_num.ToString()))
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
                else if (item_name == (int)item_info.TEXTBOX_INFO)
                {
                    if (item is TextBox)
                    {
                        TextBox test = item as TextBox;
                        off_set = test.Tag.ToString().IndexOf(":");
                        if (off_set != 0)
                        {
                            string num = test.Tag.ToString().Substring(off_set + 1);
                            string id = test.Tag.ToString().Substring(0, off_set);
                            if (id.Equals("Info"))
                            {
                                if (num.Equals(item_num.ToString()))
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
            }
            return err;
        }
        private void remove_item(int idx_num)
        {
            Control item;
            for(int i = (int)item_info.TEXTBOX_INFO; i<= (int)item_info.BUTTON_SEND; i++)
            {
                item = m_find_item(idx_num, i);
                this.command_listView.Controls.Remove(item);
            }
        }

        private void move_up_item(int idx_num, int total_num)
        {
            Control item;
            for (int j = idx_num; j < total_num - 1; j++)
            {
                for (int i = (int)item_info.TEXTBOX_INFO; i <= (int)item_info.BUTTON_SEND; i++)
                {
                    item = m_find_item(j + 1, i);
                    if (i == (int)item_info.TEXTBOX_INFO)
                    {
                        item.Tag = "Info:" + j.ToString();
                    }
                    else if (i == (int)item_info.COMBOBOX)
                    {
                        item.Tag = "Cmb:" + j.ToString();
                    }
                    else if (i == (int)item_info.TEXTBOX_DATA)
                    {
                        item.Tag = "Data:" + j.ToString();
                    }
                    else if (i == (int)item_info.TEXTBOX_DELAY)
                    {
                        item.Tag = "Dly:" + j.ToString();
                    }
                    else if (i == (int)item_info.BUTTON_SEND)
                    {
                        item.Tag = "Btn:" + j.ToString();
                    }
                    item.Size = new Size(this.command_listView.Items[j].SubItems[i].Bounds.Width - 1, this.command_listView.Items[j].SubItems[i].Bounds.Height - 1);
                    item.Location = new Point(this.command_listView.Items[j].SubItems[i].Bounds.Left - 1, this.command_listView.Items[j].SubItems[i].Bounds.Top - 1);
                }
            }
        }

        private void del_item_button_Click(object sender, EventArgs e)
        {
            int idx_num = command_listView.SelectedItems[0].Index;
            delete_item(idx_num);
            move_up_item(idx_num, item_num);
            item_num--;
        }
    }
}
