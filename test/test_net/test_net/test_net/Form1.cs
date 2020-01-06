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
        enum connect_state { CONNECT, DISCONNECT, LISTEN, ABORT };
        //enum role_state { SERVER, CLIENT, UDP };
        int net_connect_state = (int)connect_state.ABORT;
        //int ne_role_state = (int)role_state.SERVER;

        Thread socket_thread_receive;
        Socket server_socket;
        Socket socket;

        //客户端
        //将远程连接的客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
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
                else
                {
                    if (net_connect_state == (int)connect_state.ABORT)
                    {
                        if(server_socket != null)
                        {
                            server_socket.Shutdown(SocketShutdown.Both);
                        }
                        socket.Close();
                        socket = null;
                        //server_socket.Close();
                        //server_socket = null;
                    }
                    else if (net_connect_state == (int)connect_state.DISCONNECT)
                    {
                        socket_thread_receive.Abort();
                        socket.Close();
                        socket = null;
                    }
                    logMsg("关闭");
                }
            }
            //catch { }
        }

        private void net_type_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TCP Server
            if (net_type_box.SelectedIndex == 0)
            {
                net_connect_state = (int)connect_state.LISTEN;
            }
            //TCP Client UDP
            else 
            {
                net_connect_state = (int)connect_state.CONNECT;
            }
            net_state_change();
        }

        //改变 状态
        private void net_state_change()
        {
            //状态改变，按钮变化
            if (net_connect_state == (int)connect_state.DISCONNECT)
            {
                net_connect_state = (int)connect_state.CONNECT;
                net_connect_button.Text = "断开连接";
                net_connect_button.BackColor = System.Drawing.Color.Tomato;
                port_box.Enabled = false;
                ip_box.Enabled = false;
                net_type_box.Enabled = false;
            }
            else if (net_connect_state == (int)connect_state.CONNECT)
            {
                net_connect_state = (int)connect_state.DISCONNECT;
                net_connect_button.Text = "连接";
                net_connect_button.BackColor = System.Drawing.Color.LightGreen;
                port_box.Enabled = true;
                ip_box.Enabled = true;
                net_type_box.Enabled = true;
            }
            else if (net_connect_state == (int)connect_state.ABORT)
            {
                net_connect_state = (int)connect_state.LISTEN;
                net_connect_button.Text = "终止";
                net_connect_button.BackColor = System.Drawing.Color.Tomato;
                port_box.Enabled = false;
                ip_box.Enabled = false;
                net_type_box.Enabled = false;
            }
            else if (net_connect_state == (int)connect_state.LISTEN)
            {
                net_connect_state = (int)connect_state.ABORT;
                net_connect_button.Text = "监听";
                net_connect_button.BackColor = System.Drawing.Color.LightGreen;
                port_box.Enabled = true;
                ip_box.Enabled = true;
                net_type_box.Enabled = true;
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
                    // 将远程连接的客户端的IP地址和Socket存入集合中
                    //dicSocket.Add(socketSend.RemoteEndPoint.ToString(),socketSend);
                    //comboBox1.Items.Add(socketSend.RemoteEndPoint.ToString());
                    logMsg(server_socket.RemoteEndPoint.ToString() + ":" + "连接");
                    // 客户端连接成功之后，服务器应该接受客户端发来的消息
                    socket_thread_receive = new Thread(Recive);
                    socket_thread_receive.IsBackground = true;
                    socket_thread_receive.Start(server_socket);
                    socket_thread_receive.Name = "server receive";
                    socket.BeginAccept(Listen, socket);
                }
                //}
                //catch { }
            //}     
        }

        private void hex_send_checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void hex_display_checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void display_format_change(byte[] bytes, int r)
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
            logMsg("[" + socket.RemoteEndPoint + " Rx]: " + str + "\r");
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
            //try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024];
                    if (net_connect_state == (int)connect_state.LISTEN)
                    {
                        // 实际接收到的有效字节数
                        Socket socket_receive = obj as Socket;
                        int r = socket_receive.Receive(buffer);
                        if (r == 0)
                        {
                            break;
                        }
                        display_format_change(buffer, r);
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
                            display_format_change(buffer, r);
                        }
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
                    socket.Send(buffer);
                }
                else if (net_connect_state == (int)connect_state.LISTEN)
                {
                    server_socket.Send(buffer);
                }
                logMsg("[Tx]: " + str + "\r");
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

        }
    }
}