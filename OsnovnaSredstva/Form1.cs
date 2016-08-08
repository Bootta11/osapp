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
        public static Form staticForm;
        static Label lblMessageHolderForTimer = null;
        string izmijenitiItemId = null;
        static string korisnik = "";
        public static CultureInfo culture;

        public static void setKorisnik(string k)
        {
            korisnik = k;
        }
        public Form1()
        {
            InitializeComponent();
            OSUtil.init();
            // Opens an unencrypted database

            DBManager.init();
            AppSettingsReader settings = new AppSettingsReader();
            string strculture = (string)settings.GetValue("DefaultCulture", typeof(string));
            Console.WriteLine(strculture);
            culture = new CultureInfo(strculture);
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
            inputMetodaAmortizacije.Items.Insert(0, "Linearna");
            changeFontOfChildLabels(tblInput, new Font(label1.Font.FontFamily, (float)8));
            inputInventurniBroj.Focus();
        }

        public bool checkFieldsOK()
        {
            return true;
        }

        private void btnSnimiti_Click(object sender, EventArgs e)
        {
            List<string> errorMsgs = new List<string>();
            bool parseOK = true;
            bool requiredFiledsOK = true;
            double temp = 0;
            int tempInt = 0;
            OSItem item = new OSItem();
            if (izmijenitiItemId != null)
                item.id = izmijenitiItemId;
            if (inputInventurniBroj.Text.Length == 0)
            {
                errorMsgs.Add("Nije unijet inventurni broj"); requiredFiledsOK = false;
            }
            else
                item.inventurniBroj = inputInventurniBroj.Text;

            item.naziv = inputNaziv.Text;

            if (!double.TryParse(inputKolicina.Text.Replace('.', ','), out temp))
            {
                parseOK = false;

            }
            item.kolicina = temp;
            temp = 0;
            item.datumNabavke = inputDatumNabavke.Text;


            if (!double.TryParse(inputNabavnaVrednost.Text.Replace('.', ','), out temp))
            {
                parseOK = false;
                requiredFiledsOK = false;
                errorMsgs.Add("Nije unijeta nabavna vrijednost");
            }
            item.nabavnaVrijednost = temp;
            temp = 0;
            if (inputKonto.Text.Length < 1)
            {
                requiredFiledsOK = false;
                errorMsgs.Add("Nije unijet konto");
            }
            else
                item.konto = inputKonto.Text;
            item.datumAmortizacije = inputDatumAmortizacije.Text;
            item.datumVrijednosti = inputDatumVrijednosti.Text;
            //item.ispravkaVrijednosti = double.Parse(inputIspravkaVrijednosti.Text);

            if (!double.TryParse(inputVek.Text.Replace('.', ','), out temp))
            {
                parseOK = false;

            }
            item.vek = temp;
            temp = 0;
            item.datumOtpisa = inputDatumOtpisa.Text;

            if (!double.TryParse(inputVrijednostNaDatumAmortizacije.Text.Replace('.', ','), out temp))
            {
                parseOK = false;
                item.vrijednostNaDatum = -1;
            }
            else
                item.vrijednostNaDatum = temp;
            item.jedinicaMjere = inputjednicaMjere.Text;
            if (inputDobavljac.Text.Length < 1)
            {
                requiredFiledsOK = false;
                errorMsgs.Add("Nije unijet dobavljac");
            }
            item.dobavljac = inputDobavljac.Text;
            item.racunDobavljaca = inputRacunDokDobavljaca.Text;
            item.racunopolagac = inputRacunopolagac.Text;
            item.lokacija = inputLokacija.Text;
            item.smjestaj = inputSmjestaj.Text;
            try
            {
                if (inputMetodaAmortizacije.SelectedIndex == -1)
                {
                    requiredFiledsOK = false;
                    errorMsgs.Add("Nije unet metod amortizacije");
                }
                else
                    item.metodaAmortizacije = inputMetodaAmortizacije.Items[inputMetodaAmortizacije.SelectedIndex].ToString();

            }
            catch (Exception ex)
            {
                parseOK = false;
            }
            item.poreskeGrupe = inputPoreskeGrupe.Text;
            parseOK = int.TryParse(inputBrojPoNabavci.Text, out tempInt);
            if (!int.TryParse(inputBrojPoNabavci.Text, out tempInt))
            {
                parseOK = false;
                requiredFiledsOK = false;
                errorMsgs.Add("Nije unet broj po nabavci");
            }
            else
                item.brojPoNabavci = tempInt;
            tempInt = 0;
            item.amortizacionaGrupa = inputAmortizacijaGrupe.Text;
            AppSettingsReader settings = new AppSettingsReader();
            string strculture = (string)settings.GetValue("DefaultCulture", typeof(string));
            CultureInfo culture = new CultureInfo(strculture);

            if (!double.TryParse(inputStopaAmortizacije.Text.Replace('.', ','), NumberStyles.Any, culture, out temp))
            {
                parseOK = false;
                requiredFiledsOK = false;
                errorMsgs.Add("Nije unijeta stopa amortizacije");
            }
            else
                item.stopaAmortizacije = temp;
            temp = 0;
            item.active = "active";

            SQLiteErrorCode msg = SQLiteErrorCode.Ok;
            if (requiredFiledsOK)
            {
                msg = DBManager.insertOS(item);

            }
            else
            {
                showErrorMessage("Error: " + errorMsgs.First());
                return;
            }

            if (msg != SQLiteErrorCode.Ok)
            {
                if (msg == SQLiteErrorCode.Constraint)
                {
                    if (MessageBox.Show("OS sa unesenim inventurnim brojem već postoji. Želite li da snimite izmjene na postojećem OS?", "Izmjeniti OS?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SQLiteErrorCode result = DBManager.UpdateItem(item);
                        if (result == SQLiteErrorCode.Ok)
                        {
                            izmijenitiItemId = null;
                            clearInput(tblInput);
                            showSuccessMessage("OS sa inventurnim brojem " + item.inventurniBroj + " je izmjenjeno");
                            //showErrorMessage("OS sa inventurnim brojem " + item.inventurniBroj + "je izmjenjeno");
                        }
                    }
                    else
                    {
                        showErrorMessage("Error: Nije moguce unijeti novo OS sa postojećim inventurnim brojem");
                    }
                }
                else if (msg == SQLiteErrorCode.NotFound)
                {
                    showErrorMessage("Error: OS nije izmijenjen");
                }
                else
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
            double stopAmortizacije;
            if (double.TryParse(inputStopaAmortizacije.Text.Replace('.', ','), NumberStyles.Any, culture, out stopAmortizacije))
            {
                inputDatumOtpisa.Value = OSUtil.calculateDatumOtpisa(inputDatumAmortizacije.Value, stopAmortizacije);
            }
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
                DateTime dtDatumOtpisa = DateTime.ParseExact(inputDatumOtpisa.Text, "dd.MM.yyyy.", culture);
                DateTime dtamortizacije = DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", culture);
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
            PregledForm pregled = new PregledForm(this);
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
                lblKorisnik.Text = "Korisnik: " + korisnik;
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
                    if (tb.Name != lblMessage.Name)
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
            staticForm.Invoke((MethodInvoker)delegate
            {

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

        public void FillInputForm(OSItem item)
        {
            CultureInfo provider = culture;
            izmijenitiItemId = item.id;
            inputInventurniBroj.Text = item.inventurniBroj;
            inputNaziv.Text = item.naziv;
            inputKolicina.Text = item.kolicina.ToString("0.000");
            inputNabavnaVrednost.Text = item.nabavnaVrijednost.ToString("0.000");
            inputKonto.Text = item.konto;
            inputVrijednostNaDatumAmortizacije.Text = item.vrijednostNaDatum < 0 ? "" : item.vrijednostNaDatum.ToString();
            inputjednicaMjere.Text = item.jedinicaMjere;
            inputVek.Text = item.vek.ToString("0.000");
            inputDobavljac.Text = item.dobavljac;
            inputDobavljac.Text = item.dobavljac;
            inputRacunopolagac.Text = item.racunopolagac;
            inputLokacija.Text = item.lokacija;
            inputSmjestaj.Text = item.smjestaj;
            inputMetodaAmortizacije.SelectedIndex = inputMetodaAmortizacije.Items.IndexOf(item.metodaAmortizacije);
            inputPoreskeGrupe.Text = item.poreskeGrupe;
            inputAmortizacijaGrupe.Text = item.amortizacionaGrupa;
            inputBrojPoNabavci.Text = item.brojPoNabavci.ToString();
            inputStopaAmortizacije.Text = item.stopaAmortizacije.ToString("0.000");
            inputDatumNabavke.Value = DateTime.ParseExact(item.datumNabavke, "dd.MM.yyyy.", provider);
            inputDatumAmortizacije.Value = DateTime.ParseExact(item.datumAmortizacije, "dd.MM.yyyy.", provider);
            inputDatumOtpisa.Value = DateTime.ParseExact(item.datumOtpisa, "dd.MM.yyyy.", provider);
            inputDatumVrijednosti.Value = DateTime.ParseExact(item.datumVrijednosti, "dd.MM.yyyy.", provider);
        }

        public void changeFontOfChildLabels(Control control, Font font)
        {

            var controls = control.Controls;
            foreach (Control c in controls)
            {
                Console.WriteLine("Name: " + c.Name);
                changeFontOfChildLabels(c, font);
                if (c.GetType() == typeof(Label))
                {
                    Label lbl = (Label)c;

                    lbl.Font = font;
                }

            }
        }

        private void btnOcistitiUnose_Click(object sender, EventArgs e)
        {
            clearInput(tblInput);
        }



        private void inputStopaAmortizacije_TextChanged_1(object sender, EventArgs e)
        {
            double stopAmortizacije;
            if (double.TryParse(inputStopaAmortizacije.Text.Replace('.', ','), NumberStyles.Any, culture, out stopAmortizacije))
            {
                inputDatumOtpisa.Value = OSUtil.calculateDatumOtpisa(inputDatumAmortizacije.Value, stopAmortizacije);
            }
        }
    }
}
