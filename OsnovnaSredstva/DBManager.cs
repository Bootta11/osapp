using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OsnovnaSredstva
{
    
    class DBManager
    {
        private static string dbName = "osapdb.db3";
        private static SQLiteConnection cnn = null;
        public static void init()
        {
            cnn = new SQLiteConnection("Data Source="+dbName);
            //cnn.SetPassword("OOs.app33");
            cnn.Open();

            //cnn.ChangePassword("OOs.app33");
            //cnn.ChangePassword((string)null);
            string sqlCreateTable = "CREATE TABLE IF NOT EXISTS osnovna_sredstva(id INTEGER PRIMARY KEY   AUTOINCREMENT, inventurni_broj varchar(50), naziv varchar(200), kolicina double, datum_nabavke date, nabavna_vrijednost double, konto varchar(100), datum_amortizacije date, ispravka_vrijednosti double, vek double, datum_otpisa date, sadasnja_vrednost double, jedinica_mjere varchar(50), dobavljac varchar(100), racun_dok_dobavljaca varchar(200), racunopolagac varchar(200), lokacija varchar(200), smjestaj varchar(200), metoda_amortizacije varchar(200), poreske_grupe varchar(100), broj_po_nabavci integer, amortizaciona_grupa varchar(100), stopa_amortizacije double, active varchar(20));";
            SQLiteCommand command = new SQLiteCommand(sqlCreateTable, cnn);
            command.ExecuteNonQuery();

        }

        public static void insertOS(OSItem item)
        {
            string sql = "insert into osnovna_sredstva (inventurni_broj, naziv , kolicina, datum_nabavke, nabavna_vrijednost, konto, datum_amortizacije, ispravka_vrijednosti, vek, datum_otpisa, sadasnja_vrednost, jedinica_mjere, dobavljac, racun_dok_dobavljaca, racunopolagac, lokacija, smjestaj, metoda_amortizacije, poreske_grupe, broj_po_nabavci, amortizaciona_grupa, stopa_amortizacije, active)" +
                "  values ('"+item.inventurniBroj+ "', '" + item.naziv + "' , '" + item.kolicina + "', '" + item.datumNabavke + "', '" + item.nabavnaVrijednost + "', '" + item.konto + "', '" + item.datumAmortizacije + "', '" + item.ispravka_vrijednosti + "', '" + item.vek + "', '" + item.datumOtpisa + "', '" + item.sadasnjaVrijednost + "', '" + item.jednicaMjere + "', '" + item.dobavljac + "', '" + item.racunDobavljaca + "', '" + item.racunDobavljaca + "', '" + item.lokacija + "', '" + item.smjestaj + "', '" + item.metodaAmortizacije + "', '" + item.poreskeGrupe + "', '"+ item.brojPoNabavci +"', '" + item.amortizacionaGrupa + "', '" + item.stopaAmortizacije + "', '" + item.active + "')";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.ExecuteNonQuery();
        }
    }
}
