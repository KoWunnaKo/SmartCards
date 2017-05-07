using ListenerDecktop.SocketInAction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListenerDecktop
{
    public partial class MainForm : Form
    {
        private ConnectionAcepter _connector;

        public MainForm()
        {
            InitializeComponent();

            _connector = new ConnectionAcepter();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _connector.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connector.Stop();
        }
    }
}
