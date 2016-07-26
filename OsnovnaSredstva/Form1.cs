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

            DBManager.init();
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

        private void btnSnimiti_Click(object sender, EventArgs e)
        {
            OSItem item = new OSItem();
            item.inventurniBroj = inputInventurniBroj.Text;
            item.naziv = inputNaziv.Text;
            item.kolicina = double.Parse(inputKolicina.Text);
            item.datumNabavke = inputInventurniBroj.Text;
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
            DateTime dtDatumOtpisa =DateTime.ParseExact(inputDatumOtpisa.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtamortizacije = DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtNow = DateTime.Now;
            Console.WriteLine(dtDatumOtpisa + " - " + dtamortizacije);
            double daysDiff = Math.Floor( (dtNow - dtamortizacije).TotalDays);
            double nabavnaVrijednost = double.Parse(inputNabavnaVrednost.Text);
            double stopaAmortizacije = double.Parse(inputStopaAmortizacije.Text);
            double ispravkaVrijednosti =  ((nabavnaVrijednost*stopaAmortizacije*daysDiff)/(365*100));
            double sadasnjaVrijednost = nabavnaVrijednost - ispravkaVrijednosti;
            inputIspravkaVrijednosti.Text = ispravkaVrijednosti.ToString("#.##") + "";
            inputSadasnjaVrednost.Text = sadasnjaVrijednost.ToString("#.##") + "";
            Console.WriteLine("Opa");
        }
    }
}
