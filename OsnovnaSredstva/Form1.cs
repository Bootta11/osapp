using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace OsnovnaSredstva
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Opens an unencrypted database
            
            SQLiteConnection cnn = new SQLiteConnection("Data Source=osapdb.db3");
            cnn.SetPassword("OSAPP");
            cnn.Open();
        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
