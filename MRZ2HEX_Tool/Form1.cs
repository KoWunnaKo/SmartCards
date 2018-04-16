using SmartCardApi.MRZ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRZ2HEX_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bnt_calc_Click(object sender, EventArgs e)
        {
            var mrzInfo = new MRZInfo(txb_license_number.Text, dtp_date_of_birth.Value , dtp_expire_date.Value);

            var str = string.Format("{0}{1}", "1", mrzInfo.ToString()).Substring(0, 16);

            var butes = Encoding.ASCII.GetBytes(str);

            var hexMRZ = ByteArrayToString(butes);

            txb_res.Text = hexMRZ;
        }

        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Vehicle1_reg_number = txb_reg_number.Text; //*******

            var Issue_date = dtp_issue_date.Value;  //************

            var Vehicle1__license_number = txb_license_number2.Text; //************

            var str = string.Format("{0}{1}{2}{3}", "1", Vehicle1_reg_number, Issue_date, Vehicle1__license_number).Substring(0, 16);

            var butes = Encoding.ASCII.GetBytes(str);

            var hexMRZ = ByteArrayToString(butes);

            txb_resVL.Text = hexMRZ;
        }
    }
}
