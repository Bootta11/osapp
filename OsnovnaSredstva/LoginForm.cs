using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsnovnaSredstva
{
    public partial class LoginForm : Form
    {
        static Form staticForm;
        static Label lblMessageHolderForTimer = null;

        public LoginForm()
        {
            InitializeComponent();
            DBManager.init();
            lblMessage.Text = "";
            staticForm = this;
            inputKorisnickoIme.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool loginresult = DBManager.checkUserLogin(inputKorisnickoIme.Text.Trim(), inputLozinka.Text.Trim());
            Console.WriteLine(loginresult);
            if (loginresult)
            {
                this.DialogResult = DialogResult.OK;
                Form1.setKorisnik(inputKorisnickoIme.Text.Trim());
            }else
            {
                showErrorMessage("Error: Korisnicko ime ili lozinka nisu validni");
            }
        }

        private static void messageHideTimer(Label lblMsg, double afterMiliseconds)
        {
            lblMessageHolderForTimer = lblMsg;
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e)=> OnTimedEventTest(sender,e,lblMsg);
            
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)source;
            timer.Enabled = false;
            staticForm.Invoke((MethodInvoker)delegate {

                lblMessageHolderForTimer.Visible = false; // runs on UI thread
            });
            //lblMessageHolderForTimer.Visible = false;

        }

        private static void OnTimedEventTest(Object source, System.Timers.ElapsedEventArgs e, Label lblMsg)
        {
            System.Timers.Timer timer = (System.Timers.Timer)source;
            timer.Enabled = false;
            staticForm.Invoke((MethodInvoker)delegate {

               // lblMessageHolderForTimer.Visible = false; // runs on UI thread
            });
            lblMsg.Visible = false;
            //lblMessageHolderForTimer.Visible = false;

        }

        public void showErrorMessage(string msg)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = msg;
            lblMessage.BackColor = Color.White;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            messageHideTimer(lblMessage, 3000);
        }

        public void showSuccessMessage(string msg)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = msg;
            lblMessage.BackColor = Color.White;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            messageHideTimer(lblMessage, 3000);
        }

        private void inputLozinka_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void inputLozinka_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
            }
        }

        private void inputKorisnickoIme_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter)
            {
                inputLozinka.Focus();
                e.Handled = true;
            }
        }
    }
}
