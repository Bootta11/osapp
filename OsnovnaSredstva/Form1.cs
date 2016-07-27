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

namespace OsnovnaSredstva
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
        }

        public bool checkFieldsOK()
        {
            return true;
        }

        private void btnSnimiti_Click(object sender, EventArgs e)
        {
            OSItem item = new OSItem();
            item.inventurniBroj = inputInventurniBroj.Text;
            item.naziv = inputNaziv.Text;
            item.kolicina = double.Parse(inputKolicina.Text);
            item.datumNabavke = inputDatumNabavke.Text;
            item.nabavnaVrijednost = double.Parse(inputNabavnaVrednost.Text);
            item.konto = inputKonto.Text;
            item.datumAmortizacije = inputDatumAmortizacije.Text;
            item.ispravka_vrijednosti = double.Parse(inputIspravkaVrijednosti.Text);
            item.vek = double.Parse(inputVek.Text);
            item.datumOtpisa = inputDatumOtpisa.Text;
            item.sadasnjaVrijednost = double.Parse(inputSadasnjaVrednost.Text);
            item.jednicaMjere = inputjednicaMjere.Text;
            item.dobavljac = inputDobavljac.Text;
            item.racunDobavljaca = inputRacunDokDobavljaca.Text;
            item.racunoPolagac = inputRacunopolagac.Text;
            item.lokacija = inputLokacija.Text;
            item.smjestaj = inputSmjestaj.Text;
            item.metodaAmortizacije = inputMetodaAmortizacije.Text;
            item.poreskeGrupe = inputPoreskeGrupe.Text;
            item.brojPoNabavci = int.Parse(inputBrojPoNabavci.Text);
            item.amortizacionaGrupa = inputAmortizacijaGrupe.Text;
            item.stopaAmortizacije = double.Parse(inputStopaAmortizacije.Text);
            item.active = "active";
            DBManager.insertOS(item);
        }

        private void inputNaziv_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputNabavnaVrednost_Leave(object sender, EventArgs e)
        {
            CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }



        private void inputDatumNabavke_ValueChanged_1(object sender, EventArgs e)
        {
            Console.WriteLine(inputDatumNabavke.Text);
            inputDatumAmortizacije.MinDate = inputDatumNabavke.Value;
            inputDatumOtpisa.MinDate = inputDatumNabavke.Value;
            CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        private void inputDatumAmortizacije_ValueChanged(object sender, EventArgs e)
        {
            CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        private void inputDatumOtpisa_ValueChanged(object sender, EventArgs e)
        {
            inputDatumAmortizacije.MaxDate = inputDatumOtpisa.Value;
            inputDatumNabavke.MaxDate = inputDatumOtpisa.Value;
            CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        public void CalculateIspravkaVrijednostiSadasnjaVrijednost()
        {
            DateTime dtDatumOtpisa = DateTime.ParseExact(inputDatumOtpisa.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtamortizacije = DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtNow = DateTime.Now;
            Console.WriteLine(dtDatumOtpisa + " - " + dtamortizacije);
            double daysDiff = Math.Floor((dtNow - dtamortizacije).TotalDays);
            double nabavnaVrijednost = double.Parse(inputNabavnaVrednost.Text);
            double stopaAmortizacije = double.Parse(inputStopaAmortizacije.Text);
            double ispravkaVrijednosti = ((nabavnaVrijednost * stopaAmortizacije * daysDiff) / (365 * 100));
            double sadasnjaVrijednost = nabavnaVrijednost - ispravkaVrijednosti;
            inputIspravkaVrijednosti.Text = ispravkaVrijednosti.ToString("0.00") + "";
            inputSadasnjaVrednost.Text = sadasnjaVrijednost.ToString("0.00") + "";
            Console.WriteLine("Opa");
        }

        private void inputStopaAmortizacije_Leave(object sender, EventArgs e)
        {

            CalculateIspravkaVrijednostiSadasnjaVrijednost();
        }

        private void textbox_OnlyNumbers(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
            {
                lblMessage.Text= "Please enter only numbers.";
                //tb.Text.Remove(tb.Text.Length - 1);
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                tb.SelectionStart = tb.Text.Length;
                tb.BackColor = Color.FromArgb(255,204,204);
            }
            else
            {
                tb.BackColor = Color.White;
            }
        }

        private void textbox_OnlyNumbersAndDecimal(object sender, EventArgs e)
        {
            Console.WriteLine("Decimal separator: "+ CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            TextBox tb = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9"+ CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + "]"))
            {
                lblMessage.Text = "Please enter only numbers and decimal.";
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                tb.SelectionStart = tb.Text.Length;
                tb.BackColor = Color.FromArgb(255, 204, 204);
            }
            else
            {
                tb.BackColor = Color.White;
            }
        }

        private void btnPregled_Click(object sender, EventArgs e)
        {
            DBManager.GetAll();
        }
    }
}
