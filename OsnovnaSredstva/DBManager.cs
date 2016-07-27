using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

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
            string sqlCreateTable = "CREATE TABLE IF NOT EXISTS osnovna_sredstva(id INTEGER PRIMARY KEY   AUTOINCREMENT, inventurni_broj varchar(50), naziv varchar(200), kolicina double, datum_nabavke varchar(30), nabavna_vrijednost double, konto varchar(100), datum_amortizacije varchar(30), ispravka_vrijednosti double, vek double, datum_otpisa varchar(30), sadasnja_vrednost double, jedinica_mjere varchar(50), dobavljac varchar(100), racun_dok_dobavljaca varchar(200), racunopolagac varchar(200), lokacija varchar(200), smjestaj varchar(200), metoda_amortizacije varchar(200), poreske_grupe varchar(100), broj_po_nabavci integer, amortizaciona_grupa varchar(100), stopa_amortizacije double, active varchar(20));";
            SQLiteCommand command = new SQLiteCommand(sqlCreateTable, cnn);
            command.ExecuteNonQuery();

        }

        public static void insertOS(OSItem item)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string sql = "insert into osnovna_sredstva (inventurni_broj, naziv , kolicina, datum_nabavke, nabavna_vrijednost, konto, datum_amortizacije, ispravka_vrijednosti, vek, datum_otpisa, sadasnja_vrednost, jedinica_mjere, dobavljac, racun_dok_dobavljaca, racunopolagac, lokacija, smjestaj, metoda_amortizacije, poreske_grupe, broj_po_nabavci, amortizaciona_grupa, stopa_amortizacije, active)" +
                "  values ('"+item.inventurniBroj+ "', '" + item.naziv + "' , '" + item.kolicina + "', @datum_nabavke, '" + item.nabavnaVrijednost + "', '" + item.konto + "', @datum_amortizacije, '" + item.ispravka_vrijednosti + "', '" + item.vek + "', @datum_otpisa, '" + item.sadasnjaVrijednost + "', '" + item.jednicaMjere + "', '" + item.dobavljac + "', '" + item.racunDobavljaca + "', '" + item.racunDobavljaca + "', '" + item.lokacija + "', '" + item.smjestaj + "', '" + item.metodaAmortizacije + "', '" + item.poreskeGrupe + "', '"+ item.brojPoNabavci +"', '" + item.amortizacionaGrupa + "', '" + item.stopaAmortizacije + "', '" + item.active + "')";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.Parameters.AddWithValue("@datum_nabavke", DateTime.ParseExact(item.datumNabavke,"dd.MM.yyyy.",provider) );
            command.Parameters.AddWithValue("@datum_amortizacije", DateTime.ParseExact(item.datumAmortizacije, "dd.MM.yyyy.", provider));
            command.Parameters.AddWithValue("@datum_otpisa", DateTime.ParseExact(item.datumOtpisa, "dd.MM.yyyy.", provider));
            command.ExecuteNonQuery();
        }

        public static List<OSItem>  GetAll()
        {
            List<OSItem> ret = new List<OSItem>();
            string sql = "SELECT * FROM osnovna_sredstva";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            int numRows;
            while (reader.Read())
            {
                OSItem item = new OSItem();
                //numRows = Convert.ToInt32(reader["cnt"]);
                
                item.inventurniBroj = reader["inventurni_broj"].ToString();
                item.naziv = reader["naziv"].ToString();
                item.kolicina = double.Parse(reader["kolicina"].ToString());
                //item.datumNabavke = reader["datum_nabavke"].ToString();
                item.nabavnaVrijednost = double.Parse(reader["nabavna_vrijednost"].ToString());
                item.konto = reader["konto"].ToString();
                Console.WriteLine("D: "+reader.GetDateTime(4).ToString("dd.MM.yyyy."));
                item.datumNabavke = reader.GetString(4).ToString(); 
                item.datumAmortizacije = reader.GetString(7).ToString();
                item.ispravka_vrijednosti = double.Parse(reader["ispravka_vrijednosti"].ToString());
                item.vek = double.Parse(reader["vek"].ToString());
                item.datumOtpisa = reader.GetString(10).ToString();
                item.sadasnjaVrijednost = double.Parse(reader["sadasnja_vrednost"].ToString());
                item.jednicaMjere = reader["jedinica_mjere"].ToString();
                item.dobavljac = reader["dobavljac"].ToString();
                item.racunDobavljaca = reader["racun_dok_dobavljaca"].ToString();
                item.racunoPolagac = reader["racunopolagac"].ToString();
                item.lokacija = reader["lokacija"].ToString();
                item.smjestaj = reader["smjestaj"].ToString();
                item.metodaAmortizacije = reader["metoda_amortizacije"].ToString();
                item.poreskeGrupe = reader["poreske_grupe"].ToString();
                item.brojPoNabavci = int.Parse(reader["broj_po_nabavci"].ToString());
                item.amortizacionaGrupa = reader["amortizaciona_grupa"].ToString();
                item.stopaAmortizacije = double.Parse(reader["stopa_amortizacije"].ToString());
                item.active = reader["active"].ToString();
                Console.WriteLine(item.nabavnaVrijednost);
                ret.Add(item);
            }

            return ret;
        }
    }
}
