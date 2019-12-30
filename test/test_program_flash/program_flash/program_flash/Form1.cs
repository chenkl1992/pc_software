using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace program_flash
{
    public partial class Form1 : Form
    {
        Process p;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void select_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "固件|*.hex";
            openFileDialog1.InitialDirectory = @"C:\Users\Administrator\Desktop";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path_box.Text = openFileDialog1.FileName;
            }
            else 
            {
                
            }
        }

        private void startprogram_Click(object sender, EventArgs e)
        {
            flashProgram();
            //loop();

        }

        private void flashProgram()
        {
            if (p != null)
            {
                p.Close();
            }
            //创建一个进程
            p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\STMicroelectronics\STM32 ST-LINK Utility\ST-LINK Utility";

            string strCMD = "";
            strCMD = @"ST-LINK_CLI.exe -c SWD UR -P ";
            strCMD = strCMD + "\"";
            strCMD = strCMD + file_path_box.Text;
            strCMD = strCMD + "\"";
            strCMD = strCMD + " -V";

            //p.StartInfo.Arguments = "/c " + strCMD;    //设定程式执行参数

            p.StartInfo.Arguments = "/c " + "ping 192.168.1.207";    //设定程式执行参数

            p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler); // 为异步获取订阅事件
            p.Start();//启动程序
            p.BeginOutputReadLine();// 异步获取命令行内容
            //p.WaitForExit();
            //p.Close();
        }

        private void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //Console.WriteLine(outLine.Data);
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                string str = outLine.Data + Environment.NewLine;
                this.logbox.AppendText(str);
                this.logbox.ScrollToCaret();
            }
        }

        private void loop()
        {
            for (var i = 0; i <= 100; i++)
            {
                program_progress.Value = i;
                Task.Delay(250);
            }
        }

        private void clear_logbox_Click(object sender, EventArgs e)
        {
            logbox.Text = "";
        }
    }
}
