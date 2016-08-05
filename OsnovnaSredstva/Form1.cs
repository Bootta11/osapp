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
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Input;

namespace OsnovnaSredstva
{
    public partial class Form1 : Form
    {
        static Form staticForm ;
        static Label lblMessageHolderForTimer = null;
        public Form1()
        {
            InitializeComponent();
            OSUtil.init();
            // Opens an unencrypted database

            DBManager.init();
            AppSettingsReader settings = new AppSettingsReader();
            string strculture = (string)settings.GetValue("DefaultCulture", typeof(string));
            Console.WriteLine(strculture);
            CultureInfo culture = new CultureInfo(strculture);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            lblMessage.Text = "";

            inputKolicina.TextChanged += textbox_OnlyNumbersAndDecimal;
            inputNabavnaVrednost.TextChanged += textbox_OnlyNumbersAndDecimal;
            inputVek.TextChanged += textbox_OnlyNumbersAndDecimal;
            inputBrojPoNabavci.TextChanged += textbox_OnlyNumbers;

            inputKolicina.LostFocus += textbox_OnlyNumbersAndDecimal;
            inputNabavnaVrednost.LostFocus += textbox_OnlyNumbersAndDecimal;
            inputVek.LostFocus += textbox_OnlyNumbersAndDecimal;
            inputBrojPoNabavci.LostFocus += textbox_OnlyNumbers;
            //Controls.SetChildIndex(pictureBox1, 0);
            
            staticForm = this;
            addTabOnEnterPressToChildComponents(tblInput);
            inputMetodaAmortizacije.Items.Insert(0, "linearna");
            inputInventurniBroj.Focus();
        }

        public bool checkFieldsOK()
        {
            return true;
        }

        private void btnSnimiti_Click(object sender, EventArgs e)
        {
            bool parseOK = true;
            double temp = 0;
            int tempInt = 0;
            OSItem item = new OSItem();
            item.inventurniBroj = inputInventurniBroj.Text;
            item.naziv = inputNaziv.Text;

            parseOK = double.TryParse(inputKolicina.Text, out temp);
            item.kolicina = temp;
            temp = 0;
            item.datumNabavke = inputDatumNabavke.Text;

            parseOK = double.TryParse(inputNabavnaVrednost.Text, out temp);
            item.nabavnaVrijednost = temp;
            temp = 0;
            item.konto = inputKonto.Text;
            item.datumAmortizacije = inputDatumAmortizacije.Text;
            item.datumVrijednosti = inputDatumVrijednosti.Text;
            //item.ispravkaVrijednosti = double.Parse(inputIspravkaVrijednosti.Text);
            parseOK = double.TryParse(inputVek.Text, out temp);
            item.vek = temp;
            temp = 0;
            item.datumOtpisa = inputDatumOtpisa.Text;
            parseOK = double.TryParse(inputVrijednostNaDatumAmortizacije.Text, out temp);
            item.vrijednostNaDatum = temp;
            item.jedinicaMjere = inputjednicaMjere.Text;
            item.dobavljac = inputDobavljac.Text;
            item.racunDobavljaca = inputRacunDokDobavljaca.Text;
            item.racunopolagac = inputRacunopolagac.Text;
            item.lokacija = inputLokacija.Text;
            item.smjestaj = inputSmjestaj.Text;
            item.metodaAmortizacije = inputMetodaAmortizacije.Items[inputMetodaAmortizacije.SelectedIndex].ToString();
            item.poreskeGrupe = inputPoreskeGrupe.Text;
            parseOK = int.TryParse(inputBrojPoNabavci.Text, out tempInt);
            item.brojPoNabavci = tempInt;
            tempInt = 0;
            item.amortizacionaGrupa = inputAmortizacijaGrupe.Text;
            parseOK = double.TryParse(inputStopaAmortizacije.Text, out temp);
            item.stopaAmortizacije = temp;
            temp = 0;
            item.active = "active";

            string msg = "";
            if (parseOK)
            {
                msg = DBManager.insertOS(item);

            }
            else
            {
                showErrorMessage("Nisu unijeti validni podaci");
                return;
            }

            if (!msg.Equals(""))
            {
                showErrorMessage("Error: Nije moguce unijeti novo OS");
            }
            else
            {
                

                clearInput(tblInput);
                showSuccessMessage("OS uspjesno sačuvano u bazu");
            }
            inputInventurniBroj.Focus();
        }

        private void inputNaziv_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputNabavnaVrednost_Leave(object sender, EventArgs e)
        {
            // CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }



        private void inputDatumNabavke_ValueChanged_1(object sender, EventArgs e)
        {
            Console.WriteLine(inputDatumNabavke.Text);
            inputDatumAmortizacije.MinDate = inputDatumNabavke.Value;
            
            inputDatumOtpisa.MinDate = inputDatumNabavke.Value;
            //CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        private void inputDatumAmortizacije_ValueChanged(object sender, EventArgs e)
        {
            //CalculateIspravkaVrijednostiSadasnjaVrijednost();
            inputDatumVrijednosti.MinDate = inputDatumAmortizacije.Value;
        }

        private void inputDatumOtpisa_ValueChanged(object sender, EventArgs e)
        {
            inputDatumAmortizacije.MaxDate = inputDatumOtpisa.Value;
            inputDatumVrijednosti.MaxDate = inputDatumOtpisa.Value;
            inputDatumNabavke.MaxDate = inputDatumOtpisa.Value;
            // CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        public void CalculateIspravkaVrijednostiSadasnjaVrijednost()
        {
            try
            {
                DateTime dtDatumOtpisa = DateTime.ParseExact(inputDatumOtpisa.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtamortizacije = DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtNow = DateTime.Now;
                Console.WriteLine(dtDatumOtpisa + " - " + dtamortizacije);
                double daysDiff = Math.Floor((dtNow - dtamortizacije).TotalDays);
                double nabavnaVrijednost = double.Parse(inputNabavnaVrednost.Text);
                double stopaAmortizacije = double.Parse(inputStopaAmortizacije.Text);
                //double ispravkaVrijednosti = ((nabavnaVrijednost * stopaAmortizacije * daysDiff) / (365 * 100));
                //double ispravkaVrijednosti = OSUtil.ispravkaVrijednosti(nabavnaVrijednost, dtNow, dtamortizacije, stopaAmortizacije);
                //double sadasnjaVrijednost = nabavnaVrijednost - ispravkaVrijednosti;


                //inputIspravkaVrijednosti.Text = ispravkaVrijednosti.ToString("0.00") + "";
                //inputSadasnjaVrednost.Text = sadasnjaVrijednost.ToString("0.00") + "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Opa");
        }

        private void inputStopaAmortizacije_Leave(object sender, EventArgs e)
        {

            //CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        private void textbox_OnlyNumbers(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                lblMessage.Text = "Please enter only numbers.";
                //tb.Text.Remove(tb.Text.Length - 1);
                lblMessage.BackColor = Color.White;
                lblMessage.BorderStyle = BorderStyle.FixedSingle;
                tb.SelectionStart = tb.Text.Length;
                tb.BackColor = Color.FromArgb(255, 204, 204);
            }
            else
            {
                tb.BackColor = Color.White;
                lblMessage.Text = "";
                lblMessage.BackColor = Color.Transparent;
                lblMessage.BorderStyle = BorderStyle.None;
            }
        }

        private void textbox_OnlyNumbersAndDecimal(object sender, EventArgs e)
        {
            Console.WriteLine("Decimal separator: " + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            TextBox tb = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + "]"))
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                lblMessage.Text = "Please enter only numbers and decimal.";
                lblMessage.BackColor = Color.White;
                lblMessage.BorderStyle = BorderStyle.FixedSingle;
                tb.SelectionStart = tb.Text.Length;
                tb.BackColor = Color.FromArgb(255, 204, 204);
            }
            else
            {
                tb.BackColor = Color.White;
                lblMessage.Text = "";
                lblMessage.BackColor = Color.Transparent;
                lblMessage.BorderStyle = BorderStyle.None;
            }
        }

        private void btnPregled_Click(object sender, EventArgs e)
        {

        }

        private void btnPregled_Click_1(object sender, EventArgs e)
        {
            PregledForm pregled = new PregledForm();
            pregled.ShowDialog();

        }

        private void btnStampanje_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pd.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                pd.PrinterSettings.Copies = printDialog.PrinterSettings.Copies;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Console.WriteLine(e.RowIndex);
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

        public void hideMessage()
        {
            /*
            lblMessage.Text = "";
            lblMessage.BackColor = Color.Transparent;
            lblMessage.BorderStyle = BorderStyle.None;
            */
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm login = new LoginForm();

            if (login.ShowDialog() != DialogResult.OK)
            {
                //Handle authentication failures as necessary, for example:
                Application.Exit();
            }
            else
            {
                this.Show();
                inputInventurniBroj.Focus();
            }
        }

        private void tblInput_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnSnimiti.PerformClick();
        }

        public void clearInput(Control control)
        {

            var controls = control.Controls;
            foreach (Control c in controls)
            {
                clearInput(c);
                if (c.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    if (DateTime.Now < dtp.MinDate)
                        dtp.Value = dtp.MinDate;
                    else if (DateTime.Now > dtp.MaxDate)
                        dtp.Value = dtp.MaxDate;
                    else
                        dtp.Value = DateTime.Now;
                }
                else if (c.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)c;
                    tb.Text = "";
                }
            }
        }

        public void addTabOnEnterPressToChildComponents(Control control)
        {

            var controls = control.Controls;
            foreach (Control c in controls)
            {
                Console.WriteLine("Name: " + c.Name);
                addTabOnEnterPressToChildComponents(c);
                if (c.Name.StartsWith("input"))
                {
                    c.KeyPress += Control_KeyPress;
                }
                
            }
        }

        private static void messageHideTimer(Label lblMsg, double afterMiliseconds)
        {
            lblMessageHolderForTimer = lblMsg;
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
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

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Pressed");
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSnimiti.PerformClick();
                e.Handled = true;
            }
        }



    }
}
