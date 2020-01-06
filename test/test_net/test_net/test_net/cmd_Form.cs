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
        public cmd_Form()
        {
            InitializeComponent();
        }
        public event add_send_command send_command;
        private void cmd_send_button_Click(object sender, EventArgs e)
        {
            send_command(command_textbox.Text);
            this.Close();
        }
    }
}
