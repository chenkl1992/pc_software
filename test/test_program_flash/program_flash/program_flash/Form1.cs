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
    public partial class 固件烧写工具 : Form
    {
        Process p;
        string logstr;
        int ping_counter = 0;
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public 固件烧写工具()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            background_init();
        }

        private void background_init()
        {
            //设置 BackgroundWorker 属性
            _worker.WorkerReportsProgress = true;   //能否报告进度更新
            _worker.WorkerSupportsCancellation = true;  //是否支持异步取消

            //连接 BackgroundWorker 对象的处理程序
            _worker.DoWork += _worker_DoWork;   //开始执行后台操作时触发，即调用 BackgroundWorker.RunWorkerAsync 时触发
            _worker.ProgressChanged += _worker_ProgressChanged; //调用 BackgroundWorker.ReportProgress(System.Int32) 时触发
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;   //当后台操作已完成、被取消或引发异常时触发
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

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var worker = sender as BackgroundWorker;
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (logstr != null)
                {
                    if (String.Compare(logstr,"Unable to open file!\r\n") == 0)
                    {
                        logstr = null;
                        _worker.CancelAsync();
                        MessageBox.Show(@"无法打开文件");
                        break;
                    }
                    if (String.Compare(logstr, "No ST-LINK detected!\r\n") == 0)
                    {
                        logstr = null;
                        _worker.CancelAsync();
                        MessageBox.Show(@"请插入烧写器");
                        break;
                    }
                    if (logstr.Contains("File size"))
                    {
                        logstr = null;
                        _worker.CancelAsync();
                        MessageBox.Show(@"烧录错误");
                        break;
                    }
                }
                if (ping_counter < 9)
                {
                    ping_counter++;
                    worker.ReportProgress(ping_counter * 10);
                }
                Thread.Sleep(1800);
            }
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            program_progress.Value = e.ProgressPercentage;   //异步任务的进度百分比
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show($@"烧写完成：{program_progress.Value}%");
            program_progress.Value = 0;
            startprogram.Enabled = true;
        }

        private void startprogram_Click(object sender, EventArgs e)
        {
            logbox.Text = "";
            this.logbox.AppendText("程序烧写中...");
            ping_counter = 0;
            flashProgram();
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

            if (file_path_box.Text == "")
            {
                program_progress.Value = 0;
                logbox.Text = "";
                MessageBox.Show(@"请选择正确的文件路径");
            }
            else
            {
                startprogram.Enabled = false;
                //判断 BackgroundWorker 是否正在执行异步操作
                if (!_worker.IsBusy)
                {
                    _worker.RunWorkerAsync();   //开始执行后台操作
                }

                p.StartInfo.Arguments = "/c " + strCMD;    //设定程式执行参数

                //p.StartInfo.Arguments = "/c " + "ping 192.168.1.207 -t";    //设定程式执行参数

                p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler); // 为异步获取订阅事件

                p.Start();//启动程序
                p.BeginOutputReadLine();// 异步获取命令行内容
                //p.WaitForExit();
                //p.Close();
            }
        }

        private void SortOutputHandler(object sender, DataReceivedEventArgs outLine)
        {
            //Console.WriteLine(outLine.Data);
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                logstr = outLine.Data + Environment.NewLine;
                this.logbox.AppendText(logstr);
                this.logbox.ScrollToCaret();
                if (String.Compare(logstr, "Programming Complete.\r\n") == 0)
                {
                    ping_counter = 10;
                    program_progress.Value = 100;
                    _worker.CancelAsync();  //请求取消挂起的后台操作
                    MessageBox.Show(@"烧写完成");
                }
            }
        }

        private void clear_logbox_Click(object sender, EventArgs e)
        {
            logbox.Text = "";
        }

    }
}
