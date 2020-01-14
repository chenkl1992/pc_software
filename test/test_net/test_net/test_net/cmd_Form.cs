using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace test_net
{
    public delegate void add_send_command(string str, int ishex, int period);
    public partial class cmd_Form : Form
    {
        int item_num = 0;
        public event add_send_command send_command;

        public cmd_Form()
        {
            InitializeComponent();
        }

        private void add_item_button_Click(object sender, EventArgs e)
        {
            add_item_default(item_num);
            item_num++;
        }

        private void add_item_default(int i)
        {
            set_listview(true, 0);
            //添加控件
            add_info_text(i, "");
            add_format_choice(i, "0");
            add_send_text(i, "");
            add_sendDelay_text(i, "0");
            add_send_button(i);

            this.command_listView.Items[this.command_listView.Items.Count - 1].EnsureVisible();
        }

        private void delete_item(int idx)
        {
            //删除 list
            set_listview(false, idx);
        }

        private void set_listview(bool act, int idx)
        {
            if (act == true)
            {
                ListViewItem[] cmdlst_item = new ListViewItem[1];

                //设置行高
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 25);
                command_listView.SmallImageList = imgList;

                //添加元素（行）,为了居中第一列为空
                cmdlst_item[0] = new ListViewItem(new string[] { "", "", "", "", "", "" });
                this.command_listView.Items.AddRange(cmdlst_item);
            }
            else
            {
                this.command_listView.Items.Remove(command_listView.Items[idx]);    
            }
        }

        enum item_info{ HEAD, TEXTBOX_INFO, COMBOBOX, TEXTBOX_DATA, TEXTBOX_DELAY, BUTTON_SEND };
        
        private void add_send_button(int i)
        {
            Button m_button = new Button();
            //按钮的位置
            m_button.Click += new EventHandler(item_send_button_Click);
            m_button.Text = "发送";
            m_button.Tag = i;
            m_button.BackColor = Color.LightBlue;
            m_button.Font = new Font("黑体", 7);
            command_listView.AddEmbeddedControl(m_button, (int)item_info.BUTTON_SEND, i);
        }

        private void add_sendDelay_text(int i, string parm)
        {
            TextBox delay_parm = new TextBox();
            //延时框
            delay_parm.Text =  parm;
            command_listView.AddEmbeddedControl(delay_parm, (int)item_info.TEXTBOX_DELAY, i);
        }

        private void add_send_text(int i, string parm)
        {
            TextBox send_data = new TextBox();
            //发送框
            send_data.Text = parm;
            command_listView.AddEmbeddedControl(send_data, (int)item_info.TEXTBOX_DATA, i);
        }

        private void add_format_choice(int i, string parm)
        {
            ComboBox send_format = new ComboBox();
            send_format.Items.Add("字符串");
            send_format.Items.Add("十六进制");
            send_format.DropDownStyle = ComboBoxStyle.DropDownList;
            if (Convert.ToInt32(parm) <= 1)
            {
                send_format.SelectedIndex = Convert.ToInt32(parm);
            }
            command_listView.AddEmbeddedControl(send_format, (int)item_info.COMBOBOX, i);
        }

        private void add_info_text(int i, string parm)
        {
            TextBox add_info = new TextBox();
            add_info.Text = parm;
            command_listView.AddEmbeddedControl(add_info, (int)item_info.TEXTBOX_INFO, i);
        }
        private void item_send_button_Click(object sender, EventArgs e)
        {
            Control item;
            Button btn = (Button)sender;
            TextBox send_data;
            ComboBox send_select;
            TextBox send_delay;
            int index = Convert.ToInt32(btn.Tag);

            item = command_listView.GetEmbeddedControl((int)item_info.TEXTBOX_DATA, index);
            if (item != null)
            {
                send_data = item as TextBox;
                item = command_listView.GetEmbeddedControl((int)item_info.COMBOBOX, index);
                if (item != null)
                {
                    send_select = item as ComboBox;
                    item = command_listView.GetEmbeddedControl((int)item_info.TEXTBOX_DELAY, index);
                    if (item != null)
                    {
                        send_delay = item as TextBox;
                        if (Convert.ToInt32(send_delay.Text)>0 )
                        {
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.Enabled = true;
                        }
                        send_command(send_data.Text, send_select.SelectedIndex, Convert.ToInt32(send_delay.Text));
                    }
                }
            }
            else
            {
                MessageBox.Show( "ebtn:" + btn.Tag.ToString());
            }
        }
        private void move_up_buttonTag(int idx_num, int total_num)
        {
            Control item;
            for (int j = idx_num+1; j < total_num; j++)
            {
                item = command_listView.GetEmbeddedControl((int)item_info.BUTTON_SEND, j);
                if (item != null)
                {
                    item.Tag = j - 1;
                }
                else
                {
                    MessageBox.Show("err"+ j.ToString());
                }
            }
        }

        private void remove_controls(int idx_num)
        {
            Control item;
            for (int i = (int)item_info.TEXTBOX_INFO; i <= (int)item_info.BUTTON_SEND; i++)
            {
                item = command_listView.GetEmbeddedControl(i, idx_num);
                if (item != null)
                {
                    command_listView.RemoveEmbeddedControl(item);
                }
            }
            command_listView.MoveupEmbeddedControl(idx_num, 5);
        }

        private void del_item_button_Click(object sender, EventArgs e)
        {
            if (command_listView.SelectedItems.Count > 0)
            {
                if (command_listView.SelectedItems[0] != null)
                {
                    int idx_tmp = command_listView.SelectedItems[0].Index;
                    delete_one_item(idx_tmp, item_num);
                    item_num --;
                }
            }
        }

        private void delete_one_item(int idx, int total_num)
        {
            move_up_buttonTag(idx, total_num);
            remove_controls(idx);
            delete_item(idx);
        }

        private void command_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (command_listView.SelectedItems.Count > 0)
            {
                foreach(ListViewItem Items in command_listView.Items)
                {
                    Items.BackColor = Color.White;
                }
                command_listView.SelectedItems[0].BackColor = Color.Bisque;
            }
        }

        private void clean_lst_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < item_num; item_num--)
            {
                delete_one_item(0, item_num);
            }
        }

        public string Path = @"..\..\..\test.txt";
        private void cmd_Form_Load(object sender, EventArgs e)
        {
            try
            {
                load_setting();
            }
            catch
            {
                init_setting();
                MessageBox.Show("cfg init!");
            }
        }

        private void load_setting()
        {
            string line;
            int config_count = 0;
            //从文件读取并判断行数
            using (StreamReader sr = new StreamReader(Path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    config_count++;
                }

                if (config_count % 5 != 0)
                {
                    MessageBox.Show("config info error!" + config_count.ToString());
                    return;
                }
                else
                {
                    item_num = config_count / 5;
                }
            }
            //从文件读取并创建控件
            using (StreamReader sr2 = new StreamReader(Path))
            {
                string tmp_str;
                int tmp_idx;
                for (int i = 0; i < config_count/5; i++)
                {
                    set_listview(true, 0);
                    line = sr2.ReadLine();
                    tmp_idx = line.IndexOf(":") + 1;
                    tmp_str = line.Substring(tmp_idx);
                    add_info_text(i, tmp_str);

                    line = sr2.ReadLine();
                    tmp_idx = line.IndexOf(":") + 1;
                    tmp_str = line.Substring(tmp_idx);
                    add_format_choice(i, tmp_str);

                    line = sr2.ReadLine();
                    tmp_idx = line.IndexOf(":") + 1;
                    tmp_str = line.Substring(tmp_idx);
                    add_send_text(i, tmp_str);

                    line = sr2.ReadLine();
                    tmp_idx = line.IndexOf(":") + 1;
                    tmp_str = line.Substring(tmp_idx);
                    add_sendDelay_text(i, tmp_str);

                    sr2.ReadLine();
                    add_send_button(i);
                }
                this.command_listView.Items[this.command_listView.Items.Count - 1].EnsureVisible();
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            save_setting();
        }

        private void init_setting()
        {
            FileStream fs = File.Open(Path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            //关闭文件
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void save_setting()
        {
            Control item;
            FileStream fs = File.Open(Path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int j = 0; j < item_num; j++)
            {
                for (int i = (int)item_info.TEXTBOX_INFO; i <= (int)item_info.BUTTON_SEND; i++)
                {
                    item = command_listView.GetEmbeddedControl(i, j);
                    if (item != null)
                    {
                        if (i == (int)item_info.TEXTBOX_INFO)
                        {
                            sw.WriteLine("Info:" + item.Text);
                        }
                        else if (i == (int)item_info.COMBOBOX)
                        {
                            ComboBox send_select;
                            send_select = item as ComboBox;
                            sw.WriteLine("Combo:"+ send_select.SelectedIndex.ToString());
                        }
                        else if (i == (int)item_info.TEXTBOX_DATA)
                        {
                            sw.WriteLine("Data:" + item.Text);
                        }
                        else if (i == (int)item_info.TEXTBOX_DELAY)
                        {
                            sw.WriteLine("Delay:" + item.Text + "\r\n");
                        }
                    }
                }
            }
            //关闭文件
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");
        }
    }
}
