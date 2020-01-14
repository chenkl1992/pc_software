using CRC_CAL;
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
    public partial class crc_Form : Form
    {
        //初始化
        enum cal_name { CRC8, CRC16_MODBUS };
        public crc_Form()
        {
            InitializeComponent();
        }

        //事件
        private void crc_cal_button_Click(object sender, EventArgs e)
        {
            if (crc_textbox.Text != "")
            {
                if (crc_cal_combobox.SelectedIndex == (int)cal_name.CRC16_MODBUS)
                {
                    crc_cal_result_textbox.Text = CRC.ToModbusCRC16(crc_textbox.Text);
                }
                else if (crc_cal_combobox.SelectedIndex == (int)cal_name.CRC8)
                {
                    crc_cal_result_textbox.Text = CRC.ToCRC8(crc_textbox.Text);
                }
            }
        }

        private void crc_Form_Load(object sender, EventArgs e)
        {
            crc_cal_combobox.Items.Add("CRC 8");
            crc_cal_combobox.Items.Add("CRC 16 MODBUS");
            crc_cal_combobox.SelectedIndex = (int)cal_name.CRC8;

        }

        private void crc_cpy_clipbd_button_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(crc_cal_result_textbox.Text);
        }
    }
}
