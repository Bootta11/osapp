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
            //item.ispravkaVrijednosti = double.Parse(inputIspravkaVrijednosti.Text);
            item.vek = double.Parse(inputVek.Text);
            item.datumOtpisa = inputDatumOtpisa.Text;
            //item.sadasnjaVrijednost = double.Parse(inputSadasnjaVrednost.Text);
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

            string msg = DBManager.insertOS(item);
            if (!msg.Equals(""))
            {
                showErrorMessage("Error: Nije moguce unijeti novo OS");
            }

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
                double ispravkaVrijednosti = OSUtil.ispravkaVrijednosti(nabavnaVrijednost, dtNow, dtamortizacije, stopaAmortizacije);
                double sadasnjaVrijednost = nabavnaVrijednost - ispravkaVrijednosti;


                inputIspravkaVrijednosti.Text = ispravkaVrijednosti.ToString("0.00") + "";
                inputSadasnjaVrednost.Text = sadasnjaVrijednost.ToString("0.00") + "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        private void btnBrisanje_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"items.csv", false);


            //Console.WriteLine(OSUtil.ispravkaVrijednosti(double.Parse(inputNabavnaVrednost.Text), DateTime.ParseExact("2017-05-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), double.Parse(inputStopaAmortizacije.Text)));
            List<OSItem> itemsForList = DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(DateTime.ParseExact("27.07.2016.", "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture)).items;



            Type myType = typeof(OSItem);
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            Console.WriteLine(props.Count);
            //dgvPregled.ColumnCount = props.Count;
            for (int i = 0; i < props.Count; i++)
            {
                //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                //object propValue = prop.GetValue(myObject, null);
                //dgvPregled.Columns[i].Name = props.ElementAt(i).Name;
                file.Write(props.ElementAt(i).Name + (i < props.Count - 1 ? ";" : Environment.NewLine));
                // Do something with propValue
            }

            foreach (OSItem item in itemsForList)
            {
                List<string> row = new List<string>();
                for (int i = 0; i < props.Count; i++)
                {
                    if (props.ElementAt(i).Name.StartsWith("datum"))
                    {
                        //row.Add(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy."));
                        file.Write(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy.") + (i < props.Count - 1 ? ";" : Environment.NewLine));
                    }
                    else
                    {
                        //row.Add(props.ElementAt(i).GetValue(item, null).ToString());
                        file.Write(props.ElementAt(i).GetValue(item, null).ToString() + (i < props.Count - 1 ? ";" : Environment.NewLine));
                    }
                    //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                    //object propValue = prop.GetValue(myObject, null);

                    // Do something with propValue
                }
                //dgvPregled.Rows.Add(row.ToArray());
            }
            file.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Console.WriteLine(e.RowIndex);
        }

        public void showErrorMessage(string msg)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = msg;
        }

        public void showMessage(string msg)
        {
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = msg;
        }
    }
}
