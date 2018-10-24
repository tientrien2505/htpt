using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private Connect connect = null;
        public Server()
        {
            InitializeComponent();
            connect = new Connect();
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            connect.close();
        }
    }
}
