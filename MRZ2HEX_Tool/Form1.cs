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
    }
}
