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

namespace test_net
{
    public partial class net_tool : Form
    {
        //连接
        enum connect_state { CONNECT, DISCONNECT, LISTEN, ABORT };
        //enum role_state { SERVER, CLIENT, UDP };
        int net_connect_state = (int)connect_state.ABORT;
        //int ne_role_state = (int)role_state.SERVER;

        Thread socket_thread_listen;
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
            IPHostEntry localhost = Dns.GetHostByName(Dns.GetHostName());
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        //初始化socket
        private void socket_init()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(ip_box.Text.Trim());
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port_box.Text.Trim()));
                if (net_connect_state == (int)connect_state.LISTEN)
                {
                    socket.Bind(point);
                    logMsg("监听成功！");
                    socket.Listen(10);
                    socket_thread_listen = new Thread(Listen);
                    socket_thread_listen.IsBackground = true;
                    socket_thread_listen.Start(socket);
                }
                else if (net_connect_state == (int)connect_state.CONNECT)
                {
                    socket.Connect(point);
                    logMsg("连接成功");
                    socket_thread_receive = new Thread(Recive);
                    socket_thread_receive.IsBackground = true;
                    socket_thread_receive.Start();
                }
            }
            catch { }
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
        private void net_state_change( )
        {
            //状态改变，按钮变化
            if (net_connect_state == (int)connect_state.DISCONNECT)
            {
                net_connect_state = (int)connect_state.CONNECT;
                net_connect_button.Text = "断开连接";
                net_connect_button.BackColor = System.Drawing.Color.Tomato;
            }
            else if (net_connect_state == (int)connect_state.CONNECT)
            {
                net_connect_state = (int)connect_state.DISCONNECT;
                net_connect_button.Text = "连接";
                net_connect_button.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (net_connect_state == (int)connect_state.ABORT)
            {
                net_connect_state = (int)connect_state.LISTEN;
                net_connect_button.Text = "终止";
                net_connect_button.BackColor = System.Drawing.Color.Tomato;
            }
            else if (net_connect_state == (int)connect_state.LISTEN)
            {
                net_connect_state = (int)connect_state.ABORT;
                net_connect_button.Text = "监听";
                net_connect_button.BackColor = System.Drawing.Color.LightGreen;
            }
        }

        //监听是否有设备连接
        void Listen(object obj)
        {
            Socket socketWatch = obj as Socket;
            // 等待客户端的连接
            while(true)
            {
                //try
                {
                    server_socket = socketWatch.Accept();
                    // 将远程连接的客户端的IP地址和Socket存入集合中
                    //dicSocket.Add(socketSend.RemoteEndPoint.ToString(),socketSend);
                    //comboBox1.Items.Add(socketSend.RemoteEndPoint.ToString());
                    logMsg(server_socket.RemoteEndPoint.ToString() + ":" + "连接");
                    // 客户端连接成功之后，服务器应该接受客户端发来的消息
                    socket_thread_receive = new Thread(Recive);
                    socket_thread_receive.IsBackground = true;
                    socket_thread_receive.Start(server_socket);
                }
                //catch { }
            }     
        }

        //客户端数据接收
        void Recive(object obj)
        {
            //try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    if (net_connect_state == (int)connect_state.LISTEN)
                    {
                        // 实际接收到的有效字节数
                        int r = server_socket.Receive(buffer);
                        if (r == 0)
                        {
                            break;
                        }
                        string str = Encoding.UTF8.GetString(buffer, 0, r);
                        logMsg("[" + server_socket.RemoteEndPoint + "]:" + str + "\r");
                    }
                    else if (net_connect_state == (int)connect_state.CONNECT)
                    {
                        // 实际接收到的有效字节数
                        int r = socket.Receive(buffer);
                        if (r == 0)
                        {
                            break;
                        }
                        string str = Encoding.UTF8.GetString(buffer, 0, r);
                        logMsg("[" + socket.RemoteEndPoint + "]:" + str + "\r");
                    }
                }
            }
            //catch { }
        }

        private void net_connect_button_Click(object sender, EventArgs e)
        {
            //状态变化
            net_state_change();
            //建立socket
            socket_init();
        }

        private void clear_recive_wind_button_Click(object sender, EventArgs e)
        {
            clearMsg();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (send_box.Text == "")
                {
                    logMsg("发送内容为空");
                    return;
                }
                string str = send_box.Text.Trim();
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                if (net_connect_state == (int)connect_state.CONNECT)
                {
                    socket.Send(buffer);
                }
                else if (net_connect_state == (int)connect_state.LISTEN)
                {
                    server_socket.Send(buffer);
                }
            }
            catch { }
        }
    }
}