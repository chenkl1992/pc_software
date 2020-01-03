using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SOCKET_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread th;
        Thread thAccept;
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // 当点击开始监听的时候，在服务器端创建一个负责监听Ip地址跟端口号的Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any; // IPAddress.Parse(txtserver.text)
                // 创建端口号对象
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text.Trim()));
                // 监听
                socketWatch.Bind(point);
                ShowMsg("监听成功");
                socketWatch.Listen(10);
                th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);
            }
            catch { }
        }
        Socket socketSend;
        /// <summary>
        /// 等待客户端的链接，并且创建与之通信用的Socket
        /// </summary>
        void Listen(object obj)
        {
            Socket socketWatch = obj as Socket;
            // 等待客户端的连接
            while(true)
            {
                try
                {
                    socketSend = socketWatch.Accept();
                    // 将远程连接的客户端的IP地址和Socket存入集合中
                    dicSocket.Add(socketSend.RemoteEndPoint.ToString(),socketSend);
                    comboBox1.Items.Add(socketSend.RemoteEndPoint.ToString());
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");
                    // 客户端连接成功之后，服务器应该接受客户端发来的消息
                    thAccept = new Thread(Recive);
                    thAccept.IsBackground = true;
                    thAccept.Start(socketSend);
                }
                catch { }
            }     
        }
        // 将远程连接的客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        /// <summary>
        /// 服务器不停的接受客户端发送过来的请求
        /// </summary>
        /// <param name="obj"></param>
        public void Recive(object obj)
        {
            Socket socketSend = obj as Socket;
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    // 实际接受到的有效字节数
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    ShowMsg(socketSend.RemoteEndPoint + ":" + str);
                }
                catch
                {              
                }        
            }       
        }
        void ShowMsg(string str)
        {
            richTextBox1.AppendText(str+"\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 服务器给客户端发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(richTextBox2.Text.Trim() == "")
                {
                    MessageBox.Show("当前无发送内容");
                    return;
                }
                string str = richTextBox2.Text.Trim();
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                List<byte> ls = new List<byte>();
                ls.Add(0);
                ls.AddRange(buffer);
                byte[] newBuffer = ls.ToArray();
                // 获得用户在下拉框中选中的IP地址
                //socketSend.Send(buffer);
                string ip = comboBox1.SelectedItem.ToString();
                dicSocket[ip].Send(newBuffer);
                richTextBox2.Clear();
            }
            catch { }
        }
        /// <summary>
        /// 选择要发送的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"D:\";
                ofd.Title = "请选择！";
                ofd.Filter = "所有文件|*.*";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    textBox1.Text = ofd.FileName;
                }          
            }
            catch { }        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("请选择客户端IP");
                    return;
                }
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("当前无发送内容!");
                    return;
                }
                // 获得要发送文件的路径
                string strPath = textBox1.Text.Trim();
                using (FileStream fsRead = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    int r = fsRead.Read(buffer, 0, buffer.Length);
                    List<byte> ls = new List<byte>();
                    ls.Add(1);
                    ls.AddRange(buffer);
                    byte[] newBuffer = ls.ToArray();
                    dicSocket[comboBox1.SelectedItem.ToString()].Send(newBuffer, 0, r + 1, SocketFlags.None);
                    richTextBox2.Clear();
                }
            }
            catch { }         
        }
        /// <summary>
        /// 发送震动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = new byte[1];
                buffer[0] = 2;
                dicSocket[comboBox1.SelectedItem.ToString()].Send(buffer);
            }
            catch { }
        }
    }
}
