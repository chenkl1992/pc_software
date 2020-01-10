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
        struct LST_SIZE
        {
            public int INFO_WIDTH;
            public int FORMAT_WIDTH;
            public int DATA_WIDTH;
            public int DLY_WIDTH;
            public int BTN_WIDTH;
            public int LIST_HEIGHT;
        }
        struct LST_NAME
        {
            public string INFO_NAME;
            public string FORMAT_NAME;
            public string DATA_NAME;
            public string DLY_NAME;
            public string BTN_NAME;
        }

        int item_num = 0;
        LST_SIZE lst_size;
        LST_NAME lst_name;
        public cmd_Form()
        {
            InitializeComponent();
            cmd_load();
        }

        private void cmd_load()
        {
            //添加控件大小信息
            lst_size.INFO_WIDTH = columnHeader2.Width;
            lst_size.FORMAT_WIDTH = columnHeader3.Width;
            lst_size.DATA_WIDTH = columnHeader4.Width;
            lst_size.DLY_WIDTH = columnHeader5.Width;
            lst_size.BTN_WIDTH = columnHeader6.Width;
            lst_size.LIST_HEIGHT = 26;
            //添加控件名称信息
            lst_name.INFO_NAME = "Info:";
            lst_name.FORMAT_NAME = "Fmt:";
            lst_name.DATA_NAME = "Data:";
            lst_name.DLY_NAME = "Dly:";
            lst_name.BTN_NAME = "Btn:";

            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
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
            //先将焦点移到最上方
            //panel1.AutoScrollPosition = new Point(0, 0);
            //command_listView.AutoScrollOffset = new Point(0, 0);

            listview(true);
            

            //添加控件
            //add_info_text(i, lst_size);
            //add_format_choice(i, lst_size);
            //add_send_text(i, lst_size);
            //add_sendDelay_text(i, lst_size);
            add_send_button(i, lst_size);
            //将滚动条移至最后
            //Point newPoint = new Point(0, this.panel1.Height - panel1.AutoScrollPosition.Y);
            //panel1.AutoScrollPosition = newPoint;

            //this.command_listView.Items[this.command_listView.Items.Count - 1].EnsureVisible();
            //command_listView.AutoScrollOffset = new Point(0, lst_size.LIST_HEIGHT*i);
        }

        private void delete_item(int i)
        {        
            //删除选中项
            remove_item(i);   
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
                cmdlst_item[0] = new ListViewItem(new string[] { "", item_num.ToString(), "2", "3", "4", "5" });
                this.command_listView.Items.AddRange(cmdlst_item);
            }
            else
            {
                this.command_listView.Items.Remove(command_listView.SelectedItems[0]);    
            }
        }

        enum item_info{ HEAD, TEXTBOX_INFO, COMBOBOX, TEXTBOX_DATA, TEXTBOX_DELAY, BUTTON_SEND };
        
        private void add_send_button(int i, LST_SIZE lst)
        {
            Button m_button = new Button();
            //按钮的位置
            m_button.Click += new EventHandler(item_send_button_Click);
            m_button.Text = "发送"+ i.ToString();
            m_button.Tag = lst_name.BTN_NAME + i.ToString();
            m_button.BackColor = Color.LightBlue;
            m_button.Font = new Font("黑体", 7);
            m_button.Size = new Size(lst.BTN_WIDTH, lst.LIST_HEIGHT);
            m_button.Location = new Point(1+lst.INFO_WIDTH+ lst.FORMAT_WIDTH+ lst.DATA_WIDTH+ lst.DLY_WIDTH, lst.LIST_HEIGHT * i);
            //this.command_listView.Controls.Add(m_button);
            command_listView.AddEmbeddedControl(m_button, 3, 0);
        }

        private void add_sendDelay_text(int i, LST_SIZE lst)
        {
            TextBox delay_parm = new TextBox();
            //延时框的位置
            delay_parm.Tag = lst_name.DLY_NAME + i.ToString();
            delay_parm.Text = "0";
            delay_parm.Size = new Size(lst.DLY_WIDTH, lst.LIST_HEIGHT);
            delay_parm.Location = new Point(1+lst.INFO_WIDTH + lst.FORMAT_WIDTH + lst.DATA_WIDTH , lst.LIST_HEIGHT * i);
            this.command_listView.Controls.Add(delay_parm);
        }

        private void add_send_text(int i, LST_SIZE lst)
        {
            TextBox send_data = new TextBox();
            //发送框的位置
            send_data.Tag = lst_name.DATA_NAME + i.ToString();
            send_data.Text = item_num.ToString();
            send_data.Size = new Size(lst.DATA_WIDTH, lst.LIST_HEIGHT);
            send_data.Location = new Point(1 + lst.INFO_WIDTH + lst.FORMAT_WIDTH, lst.LIST_HEIGHT * i);
            this.command_listView.Controls.Add(send_data);
        }

        private void add_format_choice(int i, LST_SIZE lst)
        {
            ComboBox send_format = new ComboBox();
            send_format.Tag = lst_name.FORMAT_NAME + i.ToString();
            send_format.Items.Add("十六进制");
            send_format.Items.Add("字符串");
            send_format.SelectedIndex = 0;
            send_format.Size = new Size(lst.FORMAT_WIDTH, lst.LIST_HEIGHT);
            send_format.Location = new Point(1 + lst.INFO_WIDTH, lst.LIST_HEIGHT * i);
            this.command_listView.Controls.Add(send_format);
        }

        private void add_info_text(int i, LST_SIZE lst)
        {
            TextBox add_info = new TextBox();
            add_info.Tag = lst_name.INFO_NAME + i.ToString();
            add_info.Size = new Size(lst.INFO_WIDTH, lst.LIST_HEIGHT);
            add_info.Location = new Point(1, lst.LIST_HEIGHT * i);
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
            foreach (Control item in this.panel1.Controls)
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
                            string id = test.Tag.ToString().Substring(0, off_set+1);
                            if (id.Equals(lst_name.BTN_NAME))
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
                            string id = test.Tag.ToString().Substring(0, off_set+1);
                            if (id.Equals(lst_name.FORMAT_NAME))
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
                            string id = test.Tag.ToString().Substring(0, off_set+1);
                            if (id.Equals(lst_name.DATA_NAME))
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
                            string id = test.Tag.ToString().Substring(0, off_set+1);
                            if (id.Equals(lst_name.DLY_NAME))
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
                            string id = test.Tag.ToString().Substring(0, off_set+1);
                            if (id.Equals(lst_name.INFO_NAME))
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
                this.panel1.Controls.Remove(item);
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
                        item.Tag = lst_name.INFO_NAME + j.ToString();
                        item.Location = new Point();
                    }
                    else if (i == (int)item_info.COMBOBOX)
                    {
                        item.Tag = lst_name.FORMAT_NAME + j.ToString();
                    }
                    else if (i == (int)item_info.TEXTBOX_DATA)
                    {
                        item.Tag = lst_name.DATA_NAME + j.ToString();
                    }
                    else if (i == (int)item_info.TEXTBOX_DELAY)
                    {
                        item.Tag = lst_name.DLY_NAME + j.ToString();
                    }
                    else if (i == (int)item_info.BUTTON_SEND)
                    {
                        item.Tag = lst_name.BTN_NAME + j.ToString();
                    }
                }
            }
        }

        private void del_item_button_Click(object sender, EventArgs e)
        {
            int idx_num;
            if (command_listView.SelectedItems[0] != null)
            {
                idx_num = command_listView.SelectedItems[0].Index;
                delete_item(idx_num);
                move_up_item(idx_num, item_num);
                item_num--;
            }
        }
    }
}
