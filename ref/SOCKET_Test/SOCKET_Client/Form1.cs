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

namespace SOCKET_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket socketSend;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建负责通信的Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(TxtIpAddress.Text.Trim());
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text.Trim()));
                // 获得要连接的远程服务器应用程序的ip地址和端口号
                socketSend.Connect(point);
                ShowMsg("连接成功");
                // 开启一个新的线程不停的接受服务端发来的消息
                Thread th = new Thread(Recive);
                th.IsBackground = true;
                th.Start();
            }
            catch { }     
        }
        /// <summary>
        /// 不停的接受服务器发来的消息
        /// </summary>
        void Recive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    // 实际接受到的有效字节数
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    // 判断第一位
                    switch (buffer[0])
                    {
                        case 0:
                            string str = Encoding.UTF8.GetString(buffer, 1, r-1);
                            ShowMsg(socketSend.RemoteEndPoint + ":" + str);
                            break;
                        case 1:
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = @"C:\Users\sam\Desktop";
                            sfd.Title = "请选择要保存的位置！";
                            sfd.Filter = "所有文件|*.*";
                            sfd.ShowDialog(this);
                            string strPath = sfd.FileName;
                            if(sfd.FileName != "")
                            {                          
                                using(FileStream fsWrite=new FileStream(strPath,FileMode.OpenOrCreate,FileAccess.Write))
                                {
                                    fsWrite.Write(buffer,1,r-1);
                                }
                                MessageBox.Show("保存成功！");
                            }
                            break;
                        case 2:
                            ZhenDong();
                            break;
                    }                 
                   
                }
            }
            catch { }         
        }
        /// <summary>
        /// 震动
        /// </summary>
        void ZhenDong()
        {
            int posX = this.Location.X;
            int posY = this.Location.Y;
            for (int i = 0; i < 300; i++)
            {
                this.Location = new Point(posX-200,posY-200);
                this.Location = new Point(posX, posY);
            }
        }
        void ShowMsg(string str)
        {
            richTextBox1.AppendText(str + "\r\n");
        }
        /// <summary>
        /// 客户端给服务器发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(richTextBox2.Text.Trim() == "")
                {
                    MessageBox.Show("当前无发送内容！");
                    return;
                }
                string str = richTextBox2.Text.Trim();
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                socketSend.Send(buffer);
                richTextBox2.Clear();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
