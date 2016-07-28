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
            cnn = new SQLiteConnection("Data Source=" + dbName);
            //cnn.SetPassword("OOs.app33");
            cnn.Open();

            //cnn.ChangePassword("OOs.app33");
            //cnn.ChangePassword((string)null);
            string sqlCreateTable = "CREATE TABLE IF NOT EXISTS osnovna_sredstva(id INTEGER PRIMARY KEY   AUTOINCREMENT, inventurni_broj varchar(50) UNIQUE, naziv varchar(200), kolicina double, datum_nabavke varchar(30), nabavna_vrijednost double, konto varchar(100), datum_amortizacije varchar(30), ispravka_vrijednosti double, vek double, datum_otpisa varchar(30), sadasnja_vrednost double, jedinica_mjere varchar(50), dobavljac varchar(100), racun_dok_dobavljaca varchar(200), racunopolagac varchar(200), lokacija varchar(200), smjestaj varchar(200), metoda_amortizacije varchar(200), poreske_grupe varchar(100), broj_po_nabavci integer, amortizaciona_grupa varchar(100), stopa_amortizacije double, active varchar(20));";
            SQLiteCommand command = new SQLiteCommand(sqlCreateTable, cnn);
            command.ExecuteNonQuery();

        }

        public static string insertOS(OSItem item)
        {
            string ret = "";
            try { 
            CultureInfo provider = CultureInfo.InvariantCulture;
            string sql = "insert into osnovna_sredstva (inventurni_broj, naziv , kolicina, datum_nabavke, nabavna_vrijednost, konto, datum_amortizacije, ispravka_vrijednosti, vek, datum_otpisa, sadasnja_vrednost, jedinica_mjere, dobavljac, racun_dok_dobavljaca, racunopolagac, lokacija, smjestaj, metoda_amortizacije, poreske_grupe, broj_po_nabavci, amortizaciona_grupa, stopa_amortizacije, active)" +
                "  values ('" + item.inventurniBroj + "', '" + item.naziv + "' , '" + item.kolicina + "', @datum_nabavke, '" + item.nabavnaVrijednost + "', '" + item.konto + "', @datum_amortizacije, '" + item.ispravkaVrijednosti + "', '" + item.vek + "', @datum_otpisa, '" + item.sadasnjaVrijednost + "', '" + item.jedinicaMjere + "', '" + item.dobavljac + "', '" + item.racunDobavljaca + "', '" + item.racunDobavljaca + "', '" + item.lokacija + "', '" + item.smjestaj + "', '" + item.metodaAmortizacije + "', '" + item.poreskeGrupe + "', '" + item.brojPoNabavci + "', '" + item.amortizacionaGrupa + "', '" + item.stopaAmortizacije + "', '" + item.active + "')";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.Parameters.AddWithValue("@datum_nabavke", DateTime.ParseExact(item.datumNabavke, "dd.MM.yyyy.", provider));
            command.Parameters.AddWithValue("@datum_amortizacije", DateTime.ParseExact(item.datumAmortizacije, "dd.MM.yyyy.", provider));
            command.Parameters.AddWithValue("@datum_otpisa", DateTime.ParseExact(item.datumOtpisa, "dd.MM.yyyy.", provider));
            command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                ret = ex.Message;
                
            }
            return ret;
        }

        public static int deleteOS(string id)
        {
            int ret = 0;
            string sql = "DELETE FROM osnovna_sredstva WHERE id=@id;";
            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.Parameters.AddWithValue("@id", id);
            ret = command.ExecuteNonQuery();
            return ret;
        }

        public static OSItem GetItem(string id)
        {
            OSItem ret = new OSItem();

            string sql = "SELECT * FROM osnovna_sredstva where id=@id ";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.Parameters.AddWithValue("@id", id);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                OSItem item = new OSItem();
                //numRows = Convert.ToInt32(reader["cnt"]);
                item.id = reader["id"].ToString();

                item.inventurniBroj = reader["inventurni_broj"].ToString();
                

                item.naziv = reader["naziv"].ToString();
                

                item.kolicina = double.Parse(reader["kolicina"].ToString());
                

                //item.datumNabavke = reader["datum_nabavke"].ToString();
                item.nabavnaVrijednost = double.Parse(reader["nabavna_vrijednost"].ToString());
                
                item.konto = reader["konto"].ToString();
                
                Console.WriteLine("D: " + reader.GetDateTime(4).ToString("dd.MM.yyyy."));
                item.datumNabavke = reader.GetString(4).ToString();
               
                item.datumAmortizacije = reader.GetString(7).ToString();
                
                item.ispravkaVrijednosti = double.Parse(reader["ispravka_vrijednosti"].ToString());
                
                item.vek = double.Parse(reader["vek"].ToString());
                
                item.datumOtpisa = reader.GetString(10).ToString();
                
                item.sadasnjaVrijednost = double.Parse(reader["sadasnja_vrednost"].ToString());
                
                item.jedinicaMjere = reader["jedinica_mjere"].ToString();
                
                item.dobavljac = reader["dobavljac"].ToString();
               
                item.racunDobavljaca = reader["racun_dok_dobavljaca"].ToString();
                
                item.racunopolagac = reader["racunopolagac"].ToString();
                
                item.lokacija = reader["lokacija"].ToString();
                
                item.smjestaj = reader["smjestaj"].ToString();
                
                item.metodaAmortizacije = reader["metoda_amortizacije"].ToString();
                
                item.poreskeGrupe = reader["poreske_grupe"].ToString();
                
                item.brojPoNabavci = int.Parse(reader["broj_po_nabavci"].ToString());
                
                item.amortizacionaGrupa = reader["amortizaciona_grupa"].ToString();
                
                item.stopaAmortizacije = double.Parse(reader["stopa_amortizacije"].ToString());
                
                item.active = reader["active"].ToString();
                
                Console.WriteLine(item.nabavnaVrijednost);

               
                ret=item;
            }

            return ret;
        }

        public static int UpdateItem(OSItem item)
        {
            string sql = "UPDATE osnovna_sredstva " +
                "SET inventurni_broj=@inventurni_broj, naziv=@naziv , kolicina=@kolicina, datum_nabavke=@datum_nabavke, nabavna_vrijednost=@nabavna_vrijednost, konto=@konto, datum_amortizacije=@datum_amortizacije, ispravka_vrijednosti=@ispravka_vrijednosti, vek=@vek, datum_otpisa=@datum_otpisa, sadasnja_vrednost=@sadasnja_vrednost, jedinica_mjere=@jedinica_mjere, dobavljac=@dobavljac, racun_dok_dobavljaca=@racun_dok_dobavljaca, racunopolagac=@racunopolagac, lokacija=@lokacija, smjestaj=@smjestaj, metoda_amortizacije=@metoda_amortizacije, poreske_grupe=@poreske_grupe, broj_po_nabavci=@broj_po_nabavci, amortizaciona_grupa=@amortizaciona_grupa, stopa_amortizacije=@stopa_amortizacije, active=@active " +
                "where id=@id;";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            command.Parameters.AddWithValue("@inventurni_broj", item.naziv);
            command.Parameters.AddWithValue("@naziv",item.naziv);
            command.Parameters.AddWithValue("@kolicina", item.kolicina);
            command.Parameters.AddWithValue("@datum_nabavke", item.datumNabavke);
            command.Parameters.AddWithValue("@nabavna_vrijednost", item.nabavnaVrijednost);
            command.Parameters.AddWithValue("@konto", item.konto);
            command.Parameters.AddWithValue("@datum_amortizacije", item.datumAmortizacije);
            command.Parameters.AddWithValue("@ispravka_vrijednosti", item.ispravkaVrijednosti);
            command.Parameters.AddWithValue("@vek", item.vek);
            command.Parameters.AddWithValue("@datum_otpisa", item.datumOtpisa);
            command.Parameters.AddWithValue("@sadasnja_vrednost", item.sadasnjaVrijednost);
            command.Parameters.AddWithValue("@jedinica_mjere", item.jedinicaMjere);
            command.Parameters.AddWithValue("@dobavljac", item.dobavljac);
            command.Parameters.AddWithValue("@racun_dok_dobavljaca", item.racunDobavljaca);
            command.Parameters.AddWithValue("@racunopolagac", item.racunopolagac);
            command.Parameters.AddWithValue("@lokacija", item.lokacija);
            command.Parameters.AddWithValue("@smjestaj", item.smjestaj);
            command.Parameters.AddWithValue("@metoda_amortizacije", item.metodaAmortizacije);
            command.Parameters.AddWithValue("@poreske_grupe", item.poreskeGrupe);
            command.Parameters.AddWithValue("@broj_po_nabavci", item.brojPoNabavci);
            command.Parameters.AddWithValue("@amortizaciona_grupa", item.amortizacionaGrupa);
            command.Parameters.AddWithValue("@stopa_amortizacije", item.stopaAmortizacije);
            command.Parameters.AddWithValue("@active", item.active);
            command.Parameters.AddWithValue("@id", item.id);

            return command.ExecuteNonQuery();
        }

        public static ListWithFieldMaxLengths GetAll()
        {
            ListWithFieldMaxLengths lwf = new ListWithFieldMaxLengths();
            
            List<OSItem> ret = new List<OSItem>();
            string sql = "SELECT * FROM osnovna_sredstva";

            SQLiteCommand command = new SQLiteCommand(sql, cnn);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                OSItem item = new OSItem();
                //numRows = Convert.ToInt32(reader["cnt"]);
                item.id = reader["id"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("id")) lwf.fieldMaxLength.Add("id", "ID");
                if (lwf.fieldMaxLength["id"].Length < item.id.Length) lwf.fieldMaxLength["id"] = item.id;

                item.inventurniBroj = reader["inventurni_broj"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("inventurniBroj")) lwf.fieldMaxLength.Add("inventurniBroj", "Inventurni Broj");
                if (lwf.fieldMaxLength["inventurniBroj"].Length < item.inventurniBroj.Length) lwf.fieldMaxLength["inventurniBroj"] = item.inventurniBroj;

                item.naziv = reader["naziv"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("naziv")) lwf.fieldMaxLength.Add("naziv", "naziv");
                if (lwf.fieldMaxLength["naziv"].Length < item.naziv.Length) lwf.fieldMaxLength["naziv"] = item.naziv;

                item.kolicina = double.Parse(reader["kolicina"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("kolicina")) lwf.fieldMaxLength.Add("kolicina", "kolicina");
                if (lwf.fieldMaxLength["kolicina"].Length < item.kolicina.ToString().Length) lwf.fieldMaxLength["kolicina"] = item.kolicina.ToString();

                //item.datumNabavke = reader["datum_nabavke"].ToString();
                item.nabavnaVrijednost = double.Parse(reader["nabavna_vrijednost"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("nabavnaVrijednost")) lwf.fieldMaxLength.Add("nabavnaVrijednost", "Nabavna Vrijednost");
                if (lwf.fieldMaxLength["nabavnaVrijednost"].Length < item.nabavnaVrijednost.ToString().Length) lwf.fieldMaxLength["nabavnaVrijednost"] = item.nabavnaVrijednost.ToString();

                item.konto = reader["konto"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("konto")) lwf.fieldMaxLength.Add("konto", "konto");
                if (lwf.fieldMaxLength["konto"].Length < item.konto.Length) lwf.fieldMaxLength["konto"] = item.konto;

                Console.WriteLine("D: " + reader.GetDateTime(4).ToString("dd.MM.yyyy."));
                item.datumNabavke = reader.GetString(4).ToString();
                if (!lwf.fieldMaxLength.ContainsKey("datumNabavke")) lwf.fieldMaxLength.Add("datumNabavke", "Datum Nabavke");
                if (lwf.fieldMaxLength["datumNabavke"].Length < item.datumNabavke.Length) lwf.fieldMaxLength["datumNabavke"] = item.datumNabavke;

                item.datumAmortizacije = reader.GetString(7).ToString();
                if (!lwf.fieldMaxLength.ContainsKey("datumAmortizacije")) lwf.fieldMaxLength.Add("datumAmortizacije", "Datum Amortizacije");
                if (lwf.fieldMaxLength["datumAmortizacije"].Length < item.datumAmortizacije.Length) lwf.fieldMaxLength["datumAmortizacije"] = item.datumAmortizacije;

                item.ispravkaVrijednosti = double.Parse(reader["ispravka_vrijednosti"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) lwf.fieldMaxLength.Add("ispravkaVrijednosti", "Ispravka Vrijednosti");
                if (lwf.fieldMaxLength["ispravkaVrijednosti"].Length < item.ispravkaVrijednosti.ToString().Length) lwf.fieldMaxLength["ispravkaVrijednosti"] = item.ispravkaVrijednosti.ToString();

                item.vek = double.Parse(reader["vek"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("vek")) lwf.fieldMaxLength.Add("vek", "vek");
                if (lwf.fieldMaxLength["vek"].Length < item.vek.ToString().Length) lwf.fieldMaxLength["Vek"] = item.vek.ToString();

                item.datumOtpisa = reader.GetString(10).ToString();
                if (!lwf.fieldMaxLength.ContainsKey("datumOtpisa")) lwf.fieldMaxLength.Add("datumOtpisa", "Datum Otpisa");
                if (lwf.fieldMaxLength["datumOtpisa"].Length < item.datumOtpisa.Length) lwf.fieldMaxLength["datumOtpisa"] = item.datumOtpisa;

                item.sadasnjaVrijednost = double.Parse(reader["sadasnja_vrednost"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) lwf.fieldMaxLength.Add("sadasnjaVrijednost", "Sadasnja Vrijednost");
                if (lwf.fieldMaxLength["sadasnjaVrijednost"].Length < item.sadasnjaVrijednost.ToString().Length) lwf.fieldMaxLength["sadasnjaVrijednost"] = item.sadasnjaVrijednost.ToString();

                item.jedinicaMjere = reader["jedinica_mjere"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("jednicaMjere")) lwf.fieldMaxLength.Add("jednicaMjere", "Jednica Mjere");
                if (lwf.fieldMaxLength["jednicaMjere"].Length < item.jedinicaMjere.Length) lwf.fieldMaxLength["jednicaMjere"] = item.jedinicaMjere;

                item.dobavljac = reader["dobavljac"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("dobavljac")) lwf.fieldMaxLength.Add("dobavljac", "Dobavljac");
                if (lwf.fieldMaxLength["dobavljac"].Length < item.dobavljac.Length) lwf.fieldMaxLength["dobavljac"] = item.dobavljac;

                item.racunDobavljaca = reader["racun_dok_dobavljaca"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("racunDobavljaca")) lwf.fieldMaxLength.Add("racunDobavljaca", "Racun Dobavljaca");
                if (lwf.fieldMaxLength["racunDobavljaca"].Length < item.racunDobavljaca.Length) lwf.fieldMaxLength["racunDobavljaca"] = item.racunDobavljaca;

                item.racunopolagac = reader["racunopolagac"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("racunoPolagac")) lwf.fieldMaxLength.Add("racunoPolagac", "Racuno Polagac");
                if (lwf.fieldMaxLength["racunoPolagac"].Length < item.racunopolagac.Length) lwf.fieldMaxLength["racunoPolagac"] = item.racunopolagac;

                item.lokacija = reader["lokacija"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("lokacija")) lwf.fieldMaxLength.Add("lokacija", "Lokacija");
                if (lwf.fieldMaxLength["lokacija"].Length < item.lokacija.Length) lwf.fieldMaxLength["lokacija"] = item.lokacija;

                item.smjestaj = reader["smjestaj"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("smjestaj")) lwf.fieldMaxLength.Add("smjestaj", "Smjestaj");
                if (lwf.fieldMaxLength["smjestaj"].Length < item.smjestaj.Length) lwf.fieldMaxLength["smjestaj"] = item.smjestaj;

                item.metodaAmortizacije = reader["metoda_amortizacije"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("metodaAmortizacije")) lwf.fieldMaxLength.Add("metodaAmortizacije", "Metoda Amortizacije");
                if (lwf.fieldMaxLength["metodaAmortizacije"].Length < item.metodaAmortizacije.Length) lwf.fieldMaxLength["metodaAmortizacije"] = item.metodaAmortizacije;

                item.poreskeGrupe = reader["poreske_grupe"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("poreskeGrupe")) lwf.fieldMaxLength.Add("poreskeGrupe", "Poreske Grupe");
                if (lwf.fieldMaxLength["poreskeGrupe"].Length < item.poreskeGrupe.Length) lwf.fieldMaxLength["poreskeGrupe"] = item.poreskeGrupe;

                item.brojPoNabavci = int.Parse(reader["broj_po_nabavci"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("brojPoNabavci")) lwf.fieldMaxLength.Add("brojPoNabavci", "Broj Po Nabavci");
                if (lwf.fieldMaxLength["brojPoNabavci"].Length < item.brojPoNabavci.ToString().Length) lwf.fieldMaxLength["brojPoNabavci"] = item.brojPoNabavci.ToString();

                item.amortizacionaGrupa = reader["amortizaciona_grupa"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("amortizacionaGrupa")) lwf.fieldMaxLength.Add("amortizacionaGrupa", "Amortizaciona Grupa");
                if (lwf.fieldMaxLength["amortizacionaGrupa"].Length < item.amortizacionaGrupa.Length) lwf.fieldMaxLength["amortizacionaGrupa"] = item.amortizacionaGrupa.ToString();

                item.stopaAmortizacije = double.Parse(reader["stopa_amortizacije"].ToString());
                if (!lwf.fieldMaxLength.ContainsKey("stopaAmortizacije")) lwf.fieldMaxLength.Add("stopaAmortizacije", "Stopa Amortizacije");
                if (lwf.fieldMaxLength["stopaAmortizacije"].Length < item.stopaAmortizacije.ToString().Length) lwf.fieldMaxLength["stopa_amortizacije"] = item.stopaAmortizacije.ToString();

                item.active = reader["active"].ToString();
                if (!lwf.fieldMaxLength.ContainsKey("active")) lwf.fieldMaxLength.Add("active", "Active");
                if (lwf.fieldMaxLength["active"].Length < item.active.Length) lwf.fieldMaxLength["active"] = item.active;

                Console.WriteLine(item.nabavnaVrijednost);

                lwf.items.Add(item);
                ret.Add(item);
            }

            return lwf;
        }

        public static ListWithFieldMaxLengths GetAllSaIspravkaVrijednostiISadasnjaVrijednost(DateTime pickedDate)
        {
            ListWithFieldMaxLengths ret = new ListWithFieldMaxLengths();
            ListWithFieldMaxLengths allItemsFromDB = GetAll();
            foreach (OSItem item in allItemsFromDB.items)
            {
                item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, pickedDate, DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), item.stopaAmortizacije), 2);
                if (!allItemsFromDB.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) allItemsFromDB.fieldMaxLength.Add("ispravkaVrijednosti", item.inventurniBroj);
                else if (allItemsFromDB.fieldMaxLength["ispravkaVrijednosti"].Length < item.inventurniBroj.Length) allItemsFromDB.fieldMaxLength.Add("ispravkaVrijednosti", item.ispravkaVrijednosti.ToString());

                item.sadasnjaVrijednost = Math.Round(item.nabavnaVrijednost - item.ispravkaVrijednosti, 2);
                if (!allItemsFromDB.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) allItemsFromDB.fieldMaxLength.Add("sadasnjaVrijednost", item.inventurniBroj);
                else if (allItemsFromDB.fieldMaxLength["sadasnjaVrijednost"].Length < item.inventurniBroj.Length) allItemsFromDB.fieldMaxLength.Add("sadasnjaVrijednost", item.sadasnjaVrijednost.ToString());

                Console.WriteLine("Nabavna: " + item.nabavnaVrijednost+" ispracka: "+item.ispravkaVrijednosti+ " sadasnja: "+item.sadasnjaVrijednost);
                ret.items.Add(item);
            }
            
            return allItemsFromDB;
        }

        
    }
}
