using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace test_net
{
    public partial class net_tool : Form
    {
        //连接
        enum connect_state { CONNECT, DISCONNECT, LISTEN, ABORT, UDP_CONNECT, UDP_DISCON };
        //enum role_state { SERVER, CLIENT, UDP };
        int net_connect_state = (int)connect_state.ABORT;
        //int ne_role_state = (int)role_state.SERVER;

        Thread socket_thread_receive;
        Socket server_socket;
        Socket socket;

        //客户端
        //将远程连接的客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> clientSocket = new Dictionary<string, Socket>();
        Dictionary<string, Thread> clientThread = new Dictionary<string, Thread>();
        Dictionary<int, string> udpList = new Dictionary<int, string>();
        //服务器

        public net_tool()
        {
            InitializeComponent();
        }

        //加载时显示
        private void net_tool_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            net_setting_load();
        }

        //log 打印
        private void logMsg(string str)
        {
            if (time_stamp_checkbox.Checked == true)
            {
                recive_box.AppendText(DateTime.Now.ToLocalTime().ToString() + " ");
            }
            recive_box.AppendText(str + "\r\n");
            recive_box.ScrollToCaret();
        }
        //log 清空
        private void clearMsg()
        {
            recive_box.Text = "";
        }

        //显示网络设置
        private void net_setting_load()
        {
            //角色下拉列表
            net_type_box.Items.Add("TCP Server");
            net_type_box.Items.Add("TCP Client");
            net_type_box.Items.Add("UDP");
            net_type_box.SelectedIndex = 0;

            //ip地址列表
            ip_box.Text = get_localhost_ip();

            //端口号
            port_box.Text = "6000";

            //此时无客户端连接
            clear_client_info();
        }

        //获取本机ip地址
        private string get_localhost_ip()
        {
#pragma warning disable CS0618 // 类型或成员已过时
            IPHostEntry localhost = Dns.GetHostByName(Dns.GetHostName());
#pragma warning restore CS0618 // 类型或成员已过时

            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        //socket 绑定和连接
        private void socket_init()
        {
            //try
            {
                if (net_connect_state == (int)connect_state.LISTEN)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ip = IPAddress.Parse(ip_box.Text.Trim());
                    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port_box.Text.Trim()));
                    try
                    {
                        socket.Bind(point);
                        logMsg("监听成功！");
                        socket.Listen(10);
                        socket.BeginAccept(Listen, socket);
                    }
                    catch
                    {
                        net_state_change();
                        socket_init();
                        logMsg("该端口已被占用！");
                    }
                }
                else if (net_connect_state == (int)connect_state.CONNECT)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ip = IPAddress.Parse(ip_box.Text.Trim());
                    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port_box.Text.Trim()));
                    socket_thread_receive = new Thread(Recive);
                    socket_thread_receive.IsBackground = true;
                    try
                    {
                        socket.Connect(point);
                        logMsg("连接成功");
                        socket_thread_receive.Start();
                    }
                    catch
                    {
                        net_state_change();
                        socket_init();
                        logMsg("未找到服务器！");
                    }
                }
                else if (net_connect_state == (int)connect_state.UDP_CONNECT)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPAddress ip = IPAddress.Parse(ip_box.Text.Trim());
                    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port_box.Text.Trim()));
                    socket_thread_receive = new Thread(Recive);
                    socket_thread_receive.IsBackground = true;
                    try
                    {
                        socket.Bind(point);
                        logMsg("udp 绑定成功！");
                        socket_thread_receive.Start();
                    }
                    catch
                    {
                        net_state_change();
                        socket_init();
                        logMsg("该端口已被占用！");
                    }
                }
                else
                {
                    if (net_connect_state == (int)connect_state.ABORT)
                    {
                        server_socket_close();
                    }
                    else if (net_connect_state == (int)connect_state.DISCONNECT)
                    {
                        client_socket_close();
                    }
                    else if (net_connect_state == (int)connect_state.UDP_DISCON)
                    {
                        client_socket_close();
                    }
                    logMsg("关闭");
                }
            }
            //catch { }
        }

        //服务器端关闭
        private void server_socket_close()
        {
            periodic_state_change();
            if (server_socket != null)
            {
                server_socket.Shutdown(SocketShutdown.Both);
            }
            //客户端清除
            clear_list_combobox();
            clientSocket.Clear();
            clientThread.Clear();

            //服务器清除
            socket.Close();
            socket = null;
        }

        //服务器所连接的客户端socket关闭
        private void server_connected_close()
        {
            if (client_list_combobox.SelectedItem != null)
            {
                string ip = client_list_combobox.SelectedItem.ToString();
                int idx = client_list_combobox.SelectedIndex;

                //socket 清除
                clientSocket[ip].Close();
                clientSocket[ip] = null;
                clientSocket.Remove(ip);

                //接收 thread 清除 
                clientThread[ip].Abort();
                clientThread.Remove(ip);

                client_list_combobox.Items.RemoveAt(idx);
            }
        }

        //客户端关闭
        private void client_socket_close()
        {
            socket_thread_receive.Abort();
            socket.Close();
            socket = null;
        }

        private void net_type_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TCP Server
            if (net_type_box.SelectedIndex == 0)
            {
                net_connect_state = (int)connect_state.LISTEN;
            }
            //TCP Client UDP
            else if(net_type_box.SelectedIndex == 1)
            {
                net_connect_state = (int)connect_state.CONNECT;
            }
            else if(net_type_box.SelectedIndex == 2)
            {
                net_connect_state = (int)connect_state.UDP_CONNECT;
            }
            net_state_change();
        }

        private void netsetting_display(bool i)
        {
            if (i == true)
            {
                net_connect_button.BackColor = System.Drawing.Color.LightGreen;
                port_box.Enabled = true;
                ip_box.Enabled = true;
                net_type_box.Enabled = true;
            }
            else 
            {
                net_connect_button.BackColor = System.Drawing.Color.Tomato;
                port_box.Enabled = false;
                ip_box.Enabled = false;
                net_type_box.Enabled = false;
            }
        }

        //改变 状态
        private void net_state_change()
        {
            //状态改变，按钮变化
            if (net_connect_state == (int)connect_state.DISCONNECT)
            {
                net_connect_state = (int)connect_state.CONNECT;
                net_connect_button.Text = "断开连接";
                netsetting_display(false);
            }
            else if (net_connect_state == (int)connect_state.CONNECT)
            {
                net_connect_state = (int)connect_state.DISCONNECT;
                net_connect_button.Text = "连接";
                netsetting_display(true);
            }
            else if (net_connect_state == (int)connect_state.UDP_DISCON)
            {
                net_connect_state = (int)connect_state.UDP_CONNECT;
                net_connect_button.Text = "断开连接";
                netsetting_display(false);
                add_udp_info();
            }
            else if (net_connect_state == (int)connect_state.UDP_CONNECT)
            {
                net_connect_state = (int)connect_state.UDP_DISCON;
                net_connect_button.Text = "udp绑定";
                netsetting_display(true);
                clear_client_info();
            }
            else if (net_connect_state == (int)connect_state.ABORT)
            {
                net_connect_state = (int)connect_state.LISTEN;
                net_connect_button.Text = "终止";
                netsetting_display(false);
            }
            else if (net_connect_state == (int)connect_state.LISTEN)
            {
                net_connect_state = (int)connect_state.ABORT;
                net_connect_button.Text = "监听";
                netsetting_display(true);
                clear_client_info();
            }
        }

        //监听是否有设备连接
        void Listen(IAsyncResult ar)
        {
            //Socket socketWatch = ar.AsyncState as Socket;
            //{
                //try
                //{
                if (socket != null)
                {
                    server_socket = socket.EndAccept(ar);

                    add_client_info(server_socket);
                    socket.BeginAccept(Listen, socket);
                }
                //}
                //catch { }
            //}     
        }

        private void add_client_info(Socket s)
        {
            // 将远程连接的客户端的IP地址和Socket存入集合中
            clientSocket.Add(s.RemoteEndPoint.ToString(), server_socket);
            client_list_combobox.Items.Add(s.RemoteEndPoint.ToString());

            logMsg(server_socket.RemoteEndPoint.ToString() + ":" + "连接");
            // 客户端连接成功之后，服务器应该接受客户端发来的消息
            socket_thread_receive = new Thread(Recive);
            socket_thread_receive.IsBackground = true;
            socket_thread_receive.Start(server_socket);
            socket_thread_receive.Name = "server receive";

            clientThread.Add(s.RemoteEndPoint.ToString(), socket_thread_receive);

            if (client_groupbox.Visible == false)
            {
                //显示界面变化
                recive_box.Height = 105;
                client_groupbox.Visible = true;
                client_list_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
                client_list_combobox.SelectedIndex = 0;
            }
        }

        private void add_udp_info()
        {
            if (client_groupbox.Visible == false)
            {
                //显示界面变化
                recive_box.Height = 105;
                client_groupbox.Visible = true;
                client_list_combobox.DropDownStyle = ComboBoxStyle.DropDown;
                discon_client_button.Text = "清除";
            }
        }

        private void clear_client_info()
        {
            recive_box.Height = 140;
            client_groupbox.Visible = false;
            discon_client_button.Text = "断开";
        }

        private void hex_send_checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void hex_display_checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void display_format_change_udp(byte[] bytes, int r, EndPoint point)
        {
            string str;
            if (hex_display_checkbox.Checked == true)
            {
                str = ByteToHexString(bytes, r);
            }
            else
            {
                str = Encoding.UTF8.GetString(bytes, 0, r);
            }
            logMsg("[" + point.ToString() + " Rx]: " + str + "\r");
        }

        private void display_format_change(byte[] bytes, int r, Socket socket_receive)
        {
            string str;
            if (hex_display_checkbox.Checked == true)
            {
                str = ByteToHexString(bytes, r);
            }
            else
            {
                str = Encoding.UTF8.GetString(bytes, 0, r);
            }
            logMsg("[" + socket_receive.RemoteEndPoint + " Rx]: " + str + "\r");
        }

        private byte[] send_format_change(string str)
        {
            byte[] buffer;
            if (hex_send_checkbox.Checked == true)
            {
                buffer = HexStringToByteArray(str);
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(str);
            }
            return buffer;
        }

        public string ByteToHexString(byte[] bytes, int r)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for(int i = 0; i < r; i++ )
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            if ((s.Length % 2) != 0)
            {
                s += " ";
            }
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length/2; i++)
            {
                buffer[i] = (byte)Convert.ToByte(s.Substring(i*2, 2), 16);
            }
            return buffer;
        }

        //客户端数据接收
        void Recive(object obj)
        {
            Socket socket_receive = obj as Socket;
            //try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024];
                    if (net_connect_state == (int)connect_state.LISTEN)
                    {
                        int r = socket_receive.Receive(buffer);
                        if (r == 0)
                        {
                            break;
                        }
                        display_format_change(buffer, r, socket_receive);
                    }
                    else if (net_connect_state == (int)connect_state.CONNECT)
                    {
                        // 实际接收到的有效字节数
                        if (socket != null)
                        {
                            int r = socket.Receive(buffer);
                            if (r == 0)
                            {
                                break;
                            }
                            display_format_change(buffer, r, socket_receive);
                        }
                    }
                    else if (net_connect_state == (int)connect_state.UDP_CONNECT)
                    {
                        EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                        int r = socket.ReceiveFrom(buffer, ref point);

                        if (client_list_combobox.Items.Count == 0)
                        {
                            udpList.Add(client_list_combobox.Items.Count, point.ToString());
                            client_list_combobox.Items.Add(point.ToString());
                            client_list_combobox.SelectedIndex = 0;
                        }
                        else 
                        {
                            //若不重复， 添加到列表中
                            bool udp_list_no_repeat = true;
                            for (int i = 0; i < client_list_combobox.Items.Count; i++)
                            {
                                if (udpList[i].Equals(point.ToString()) == true)
                                {
                                    udp_list_no_repeat = false;
                                }
                            }
                            if (udp_list_no_repeat == true)
                            {
                                udpList.Add(client_list_combobox.Items.Count, point.ToString());
                                client_list_combobox.Items.Add(point.ToString());
                            }
                        }
                        display_format_change_udp(buffer, r, point);
                    }
                }
            }
            //catch { }
        }

        private void net_connect_button_Click(object sender, EventArgs e)
        {
            //状态变化
            net_state_change();
            //建立/关闭socket
            socket_init();
        }

        private void clear_recive_wind_button_Click(object sender, EventArgs e)
        {
            clearMsg();
        }

        //数据发送
        private void send()
        {
            try
            {
                if (send_box.Text == "")
                {
                    logMsg("发送内容为空");
                    return;
                }
                string str = send_box.Text.Trim();
                byte[] buffer = send_format_change(str);

                if (net_connect_state == (int)connect_state.CONNECT)
                {
                    try
                    {
                        socket.Send(buffer);
                    }
                    catch
                    {
                        logMsg("客户端 发送失败！");
                    }
                }
                else if (net_connect_state == (int)connect_state.LISTEN)
                {
                    string ip = client_list_combobox.SelectedItem.ToString();
                    try
                    {
                        clientSocket[ip].Send(buffer);
                    }
                    catch 
                    {
                        logMsg("服务器 发送失败！");
                    }
                }
                else if(net_connect_state == (int)connect_state.UDP_CONNECT)
                {
                    string point_str = client_list_combobox.Text.ToString();
                    point_str = point_str.Replace(" ", "");
                    int off_set = 0;
                    off_set = point_str.IndexOf(":");
                    if (off_set != 0)
                    {
                        string port = point_str.Substring(off_set + 1);
                        string add = point_str.Substring(0, off_set);
                        EndPoint point = new IPEndPoint(IPAddress.Parse(add), int.Parse(port));
                        try
                        {
                            socket.SendTo(buffer, point);
                        }
                        catch
                        {
                            logMsg("udp 发送失败！");
                        }
                    }
                }
                if (display_send_checkbox.Checked == true)
                {
                    logMsg("[Tx]: " + str + "\r");
                }
            }
            catch 
            {
                periodic_state_change();
                net_state_change();
                socket_init();
                logMsg("服务器关闭！");
            }
        }

        private void periodic_state_change()
        {
                send_peri_textbox.Enabled = true;
                //若正在周期发送-->停止
                if (send_button.Enabled == false)
                {
                    periodic_send_timer.Enabled = false;
                    send_peri_checkbox.Checked = false;
                    periodic_send_timer.Stop();
                    send_button.Enabled = true;
                }
        }

        private void send_peri_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (send_peri_checkbox.Checked == false)
            {
                periodic_state_change();
            }
            else
            {
                send_peri_textbox.Enabled = false;
            }
        }

        private void periodic_send_judge()
        {
            if (send_peri_checkbox.Checked == true)
            {
                if (send_peri_textbox.Text != "")
                {
                    periodic_send_timer.Interval = Convert.ToInt32(send_peri_textbox.Text);
                    periodic_send_timer.Enabled = true;
                    periodic_send_timer.Start();
                    send_button.Enabled = false;
                }
            }
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            //判断是否需要周期发送
            periodic_send_judge();
            send();
        }

        private void periodic_send_timer_Tick(object sender, EventArgs e)
        {
            send();
        }

        private void clearScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearMsg();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件|*.txt";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                // 创建文件，将recive_box 中的内容保存到文件中
                // saveDlg.FileName 是用户指定的文件路径
                FileStream fs = File.Open(saveDlg.FileName,
                    FileMode.Create,
                    FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                // 保存recive_box 中所有内容（所有行)
                foreach (string line in recive_box.Lines)
                {
                    sw.WriteLine(line);
                }
                //关闭文件
                sw.Flush();
                sw.Close();
                fs.Close();
                // 提示用户：文件保存的位置和文件名
                MessageBox.Show("文件已成功保存到" + saveDlg.FileName);
            }
        }

        private void command_button_Click(object sender, EventArgs e)
        {
            cmd_Form cmd_form = new cmd_Form();
            cmd_form.send_command += f_add_send_command;
            cmd_form.Show();
            //cmd_form.Close();
        }

        void f_add_send_command(string str)
        {
            send_box.Text = str;
            send();
        }

        private void clear_list_combobox()
        {
            client_list_combobox.Text = "";
            if (client_list_combobox.SelectedItem != null)
            {
                client_list_combobox.Items.Clear();
                udpList.Clear();
            }
        }

        private void discon_client_button_Click(object sender, EventArgs e)
        {
            if (net_connect_state == (int)connect_state.CONNECT)
            {
                server_connected_close();
            }
            else if (net_connect_state == (int)connect_state.UDP_CONNECT)
            {
                clear_list_combobox();
            }
        }
    }
}