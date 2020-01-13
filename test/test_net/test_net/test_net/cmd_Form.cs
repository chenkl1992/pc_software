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
        int item_num = 0;
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

        private void add_item_button_Click(object sender, EventArgs e)
        {
            add_item(item_num);
            item_num++;
        }

        private void add_item(int i)
        {
            listview(true);
            //添加控件
            add_info_text(i);
            add_format_choice(i);
            add_send_text(i);
            add_sendDelay_text(i);
            add_send_button(i);

            this.command_listView.Items[this.command_listView.Items.Count - 1].EnsureVisible();
        }

        private void delete_item()
        {
            //删除 list
            listview(false);
        }

        private void listview(bool act)
        {
            if (act == true)
            {
                ListViewItem[] cmdlst_item = new ListViewItem[1];

                //设置行高
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 25);
                command_listView.SmallImageList = imgList;

                //添加元素
                cmdlst_item[0] = new ListViewItem(new string[] { "", "", "", "", "", "" });
                this.command_listView.Items.AddRange(cmdlst_item);
            }
            else
            {
                this.command_listView.Items.Remove(command_listView.SelectedItems[0]);    
            }
        }

        enum item_info{ HEAD, TEXTBOX_INFO, COMBOBOX, TEXTBOX_DATA, TEXTBOX_DELAY, BUTTON_SEND };
        
        private void add_send_button(int i)
        {
            Button m_button = new Button();
            //按钮的位置
            m_button.Click += new EventHandler(item_send_button_Click);
            m_button.Text = "发送"+ i.ToString();
            m_button.Tag = i;
            m_button.BackColor = Color.LightBlue;
            m_button.Font = new Font("黑体", 7);
            command_listView.AddEmbeddedControl(m_button, (int)item_info.BUTTON_SEND, i);
        }

        private void add_sendDelay_text(int i)
        {
            TextBox delay_parm = new TextBox();
            //延时框的位置
            delay_parm.Text = "0";
            command_listView.AddEmbeddedControl(delay_parm, (int)item_info.TEXTBOX_DELAY, i);
        }

        private void add_send_text(int i)
        {
            TextBox send_data = new TextBox();
            //发送框的位置
            send_data.Text = item_num.ToString()+ item_num.ToString();
            command_listView.AddEmbeddedControl(send_data, (int)item_info.TEXTBOX_DATA, i);
        }

        private void add_format_choice(int i)
        {
            ComboBox send_format = new ComboBox();
            send_format.Items.Add("十六进制");
            send_format.Items.Add("字符串");
            send_format.DropDownStyle = ComboBoxStyle.DropDownList;
            send_format.SelectedIndex = 0;
            command_listView.AddEmbeddedControl(send_format, (int)item_info.COMBOBOX, i);
        }

        private void add_info_text(int i)
        {
            TextBox add_info = new TextBox();
            command_listView.AddEmbeddedControl(add_info, (int)item_info.TEXTBOX_INFO, i);
        }
        private void item_send_button_Click(object sender, EventArgs e)
        {
            Control item;
            Button btn = (Button)sender;

            item = command_listView.GetEmbeddedControl((int)item_info.TEXTBOX_DATA, Convert.ToInt32(btn.Tag));
            //item = command_listView.GetEmbeddedControl((int)item_info.TEXTBOX_DATA, command_listView.SelectedItems[0].Index); 
            if (item != null)
            {
                TextBox test = item as TextBox;
                MessageBox.Show("Txt:" + test.Text + "btn:" + btn.Tag.ToString());
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
                    item.Tag = j-1;
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
                    move_up_buttonTag(idx_tmp, item_num);
                    remove_controls(idx_tmp);
                    delete_item();
                    item_num--;
                }
            }
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
    }
}
