﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListenerDecktop
{
    public partial class PinAutorizationForm : Form
    {
        public string pinNumber { get; set; }

        public PinAutorizationForm()
        {
            InitializeComponent();
        }

        private void txbPinNumber_TextChanged(object sender, EventArgs e)
        {
            pinNumber = txbPinNumber.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pinNumber))
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
