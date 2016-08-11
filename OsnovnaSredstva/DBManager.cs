using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;
using System.Windows.Forms;
using log4net;
using System.Reflection;


namespace OsnovnaSredstva
{

    public class DBManager
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string dbName;
        private static SQLiteConnection cnn = null;
        static bool initialized = false;
        public static void init()
        {
            if (!initialized)
            {
                try
                {
                    System.IO.StreamReader swdbname = new System.IO.StreamReader(new System.IO.FileStream("dbname", System.IO.FileMode.Open));
                    dbName = swdbname.ReadLine().Trim();
                    cnn = new SQLiteConnection("Data Source=" + dbName);
                    System.IO.StreamReader sw = new System.IO.StreamReader(new System.IO.FileStream("app.key", System.IO.FileMode.Open));
                    string key = sw.ReadLine();
                    cnn.SetPassword(OSUtil.generateHashFromString(dbName + key + dbName));
                    cnn.Open();

                    try
                    {
                        new SQLiteCommand("SELECT name FROM sqlite_master;", cnn).ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.GetType() + " " + ex.Message);
                        log.Error(ex.GetType() + " " + ex.Message, ex);
                        cnn.Close();
                        cnn.SetPassword((string)null);
                        cnn.Open();
                    }

                    //cnn.ChangePassword("OOs.app33");
                    //cnn.ChangePassword((string)null);
                    string sqlCreateTable = "CREATE TABLE IF NOT EXISTS osnovna_sredstva(id INTEGER PRIMARY KEY   AUTOINCREMENT, inventurni_broj varchar(50) UNIQUE, naziv varchar(200), kolicina double, datum_nabavke varchar(30), nabavna_vrijednost double, konto varchar(100), datum_amortizacije varchar(30), datum_vrijednosti varchar(30), vr_na_datum_amortizacije double, ispravka_vrijednosti double, vek double, datum_otpisa varchar(30), sadasnja_vrednost double, jedinica_mjere varchar(50), dobavljac varchar(100), racun_dok_dobavljaca varchar(200), racunopolagac varchar(200), lokacija varchar(200), smjestaj varchar(200), metoda_amortizacije varchar(200), poreske_grupe varchar(100), broj_po_nabavci varchar(200), amortizaciona_grupa varchar(100), stopa_amortizacije double, active varchar(20));";
                    SQLiteCommand command = new SQLiteCommand(sqlCreateTable, cnn);
                    command.ExecuteNonQuery();
                    string sqlCreatePodesavanjaTable = "CREATE TABLE IF NOT EXISTS podesavanja(ime varchar(50) UNIQUE PRIMARY KEY, vrijednost varchar(200));";
                    command.CommandText = sqlCreatePodesavanjaTable;
                    command.ExecuteNonQuery();
                    initialized = true;
                }
                catch (Exception ex)
                {
                    string messageBoxText = "Program nije korektno inicijalizovan.\nError: " + ex.Message + "\nProgram ce se ugasiti.";
                    string caption = "Error";
                    MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(messageBoxText, ex);
                    System.Environment.Exit(0);
                }
            }

        }

        public static SQLiteErrorCode insertOS(OSItem item)
        {
            SQLiteErrorCode ret = SQLiteErrorCode.Ok;
            try
            {
                CultureInfo provider = Form1.culture;
                string sql = "insert into osnovna_sredstva (inventurni_broj, naziv , kolicina, datum_nabavke, nabavna_vrijednost, konto, datum_amortizacije, datum_vrijednosti, vr_na_datum_amortizacije, ispravka_vrijednosti, vek, datum_otpisa, sadasnja_vrednost, jedinica_mjere, dobavljac, racun_dok_dobavljaca, racunopolagac, lokacija, smjestaj, metoda_amortizacije, poreske_grupe, broj_po_nabavci, amortizaciona_grupa, stopa_amortizacije, active)" +
                    "  values ('" + item.inventurniBroj + "', '" + item.naziv + "' , '" + item.kolicina.ToString("0.0000") + "', @datum_nabavke, '" + item.nabavnaVrijednost.ToString("0.0000") + "', '" + item.konto + "', @datum_amortizacije, @datum_vrijednosti, @vr_na_datum_amortizacije, '" + item.ispravkaVrijednosti.ToString("0.0000") + "', '" + item.vek.ToString("0.0000") + "', @datum_otpisa, '" + item.sadasnjaVrijednost.ToString("0.0000") + "', '" + item.jedinicaMjere + "', '" + item.dobavljac + "', '" + item.racunDobavljaca + "', '" + item.racunopolagac + "', '" + item.lokacija + "', '" + item.smjestaj + "', '" + item.metodaAmortizacije + "', '" + item.poreskeGrupe + "', '" + item.brojPoNabavci + "', '" + item.amortizacionaGrupa + "', '" + item.stopaAmortizacije.ToString("0.0000") + "', '" + item.active + "')";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                command.Parameters.AddWithValue("@datum_nabavke", DateTime.ParseExact(item.datumNabavke, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@datum_amortizacije", DateTime.ParseExact(item.datumAmortizacije, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@datum_vrijednosti", DateTime.ParseExact(item.datumVrijednosti, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@datum_otpisa", DateTime.ParseExact(item.datumOtpisa, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@vr_na_datum_amortizacije", item.vrijednostNaDatum.ToString("0.0000"));
                command.ExecuteNonQuery();
            }
            catch (SQLiteException sqlex)
            {
                if (sqlex.ResultCode == SQLiteErrorCode.Constraint)
                {
                    ret = sqlex.ResultCode;
                    Console.WriteLine("Unique failed");

                }
            }
            catch (Exception ex)
            {
                ret = SQLiteErrorCode.Error;

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
            try
            {
                CultureInfo provider = Form1.culture;
                string sql = "SELECT * FROM osnovna_sredstva where id=@id ";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                command.Parameters.AddWithValue("@id", id);
                SQLiteDataReader reader;
                reader = command.ExecuteReader();

                var readerColumns = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    readerColumns.Add(reader.GetName(i));
                }

                if (reader.Read())
                {
                    OSItem item = new OSItem();
                    //numRows = Convert.ToInt32(reader["cnt"]);
                    item.id = reader["id"].ToString();

                    item.inventurniBroj = reader["inventurni_broj"].ToString();


                    item.naziv = reader["naziv"].ToString();


                    item.kolicina = double.Parse(reader.GetString(readerColumns.IndexOf("kolicina")), Form1.culture);


                    //item.datumNabavke = reader["datum_nabavke"].ToString();
                    item.nabavnaVrijednost = double.Parse(reader.GetString(readerColumns.IndexOf("nabavna_vrijednost")), Form1.culture);

                    item.konto = reader["konto"].ToString();

                    Console.WriteLine("D: " + reader.GetDateTime(4).ToString("dd.MM.yyyy."));
                    item.datumNabavke = DateTime.ParseExact(reader["datum_nabavke"].ToString(), "yyyy-MM-dd HH:mm:ss", provider).ToString("dd.MM.yyyy.");

                    item.datumAmortizacije = DateTime.ParseExact(reader["datum_amortizacije"].ToString(), "yyyy-MM-dd HH:mm:ss", provider).ToString("dd.MM.yyyy.");

                    item.datumVrijednosti = DateTime.ParseExact(reader["datum_vrijednosti"].ToString(), "yyyy-MM-dd HH:mm:ss", provider).ToString("dd.MM.yyyy.");

                    item.vrijednostNaDatum = double.Parse(reader.GetString(readerColumns.IndexOf("vr_na_datum_amortizacije")), Form1.culture);

                    item.ispravkaVrijednosti = double.Parse(reader.GetString(readerColumns.IndexOf("ispravka_vrijednosti")), Form1.culture);

                    item.vek = double.Parse(reader.GetString(readerColumns.IndexOf("vek")), Form1.culture);

                    item.datumOtpisa = DateTime.ParseExact(reader["datum_otpisa"].ToString(), "yyyy-MM-dd HH:mm:ss", provider).ToString("dd.MM.yyyy.");

                    item.sadasnjaVrijednost = double.Parse(reader.GetString(readerColumns.IndexOf("sadasnja_vrednost")), Form1.culture);

                    item.jedinicaMjere = reader["jedinica_mjere"].ToString();

                    item.dobavljac = reader["dobavljac"].ToString();

                    item.racunDobavljaca = reader["racun_dok_dobavljaca"].ToString();

                    item.racunopolagac = reader["racunopolagac"].ToString();

                    item.lokacija = reader["lokacija"].ToString();

                    item.smjestaj = reader["smjestaj"].ToString();

                    item.metodaAmortizacije = reader["metoda_amortizacije"].ToString();

                    item.poreskeGrupe = reader["poreske_grupe"].ToString();

                    item.brojPoNabavci = reader["broj_po_nabavci"].ToString();

                    item.amortizacionaGrupa = reader["amortizaciona_grupa"].ToString();

                    item.stopaAmortizacije = double.Parse(reader.GetString(readerColumns.IndexOf("stopa_amortizacije")), Form1.culture);

                    item.active = reader["active"].ToString();

                    Console.WriteLine(item.nabavnaVrijednost);


                    ret = item;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return ret;
        }

        public static SQLiteErrorCode UpdateItem(OSItem item)
        {
            SQLiteErrorCode ret = SQLiteErrorCode.Ok;
            string sql = "UPDATE osnovna_sredstva " +
                "SET inventurni_broj=@inventurni_broj, naziv=@naziv , kolicina=@kolicina, datum_nabavke=@datum_nabavke, nabavna_vrijednost=@nabavna_vrijednost, konto=@konto, datum_amortizacije=@datum_amortizacije, datum_vrijednosti=@datum_vrijednosti, vr_na_datum_amortizacije=@vr_na_datum_amortizacije, ispravka_vrijednosti=@ispravka_vrijednosti, vek=@vek, datum_otpisa=@datum_otpisa, sadasnja_vrednost=@sadasnja_vrednost, jedinica_mjere=@jedinica_mjere, dobavljac=@dobavljac, racun_dok_dobavljaca=@racun_dok_dobavljaca, racunopolagac=@racunopolagac, lokacija=@lokacija, smjestaj=@smjestaj, metoda_amortizacije=@metoda_amortizacije, poreske_grupe=@poreske_grupe, broj_po_nabavci=@broj_po_nabavci, amortizaciona_grupa=@amortizaciona_grupa, stopa_amortizacije=@stopa_amortizacije, active=@active " +
                "where id=@id;";
            try
            {
                CultureInfo provider = Form1.culture;
                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                command.Parameters.AddWithValue("@inventurni_broj", item.inventurniBroj);
                command.Parameters.AddWithValue("@naziv", item.naziv);
                command.Parameters.AddWithValue("@kolicina", item.kolicina.ToString("0.0000"));
                command.Parameters.AddWithValue("@datum_nabavke", DateTime.ParseExact(item.datumNabavke, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@nabavna_vrijednost", item.nabavnaVrijednost.ToString("0.0000"));
                command.Parameters.AddWithValue("@konto", item.konto);
                command.Parameters.AddWithValue("@datum_amortizacije", DateTime.ParseExact(item.datumAmortizacije, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@datum_vrijednosti", DateTime.ParseExact(item.datumVrijednosti, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@vr_na_datum_amortizacije", item.vrijednostNaDatum.ToString("0.0000"));
                command.Parameters.AddWithValue("@ispravka_vrijednosti", item.ispravkaVrijednosti.ToString("0.0000"));
                command.Parameters.AddWithValue("@vek", item.vek.ToString("0.0000"));
                command.Parameters.AddWithValue("@datum_otpisa", DateTime.ParseExact(item.datumOtpisa, "dd.MM.yyyy.", provider));
                command.Parameters.AddWithValue("@sadasnja_vrednost", item.sadasnjaVrijednost.ToString("0.0000"));
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
                command.Parameters.AddWithValue("@stopa_amortizacije", item.stopaAmortizacije.ToString("0.0000"));
                command.Parameters.AddWithValue("@active", item.active);
                command.Parameters.AddWithValue("@id", item.id);

                int rowchanged = command.ExecuteNonQuery();
                if (rowchanged < 1)
                    ret = SQLiteErrorCode.NotFound;
            }
            catch (SQLiteException sqlex)
            {
                ret = sqlex.ResultCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ret = SQLiteErrorCode.Error;
            }

            return ret;
        }

        public static ListWithFieldMaxLengths GetAll()
        {
            ListWithFieldMaxLengths lwf = new ListWithFieldMaxLengths();
            try
            {
                List<OSItem> ret = new List<OSItem>();
                string sql = "SELECT * FROM osnovna_sredstva  where active='active'";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                SQLiteDataReader reader;
                reader = command.ExecuteReader();
                var readerColumns = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    readerColumns.Add(reader.GetName(i));
                }
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

                    item.kolicina = double.Parse(reader.GetString(readerColumns.IndexOf("kolicina")).ToString().Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("kolicina")) lwf.fieldMaxLength.Add("kolicina", "kolicina");
                    if (lwf.fieldMaxLength["kolicina"].Length < item.kolicina.ToString().Length) lwf.fieldMaxLength["kolicina"] = item.kolicina.ToString();

                    //item.datumNabavke = reader["datum_nabavke"].ToString();
                    item.nabavnaVrijednost = double.Parse(reader.GetString(readerColumns.IndexOf("nabavna_vrijednost")).ToString().Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("nabavnaVrijednost")) lwf.fieldMaxLength.Add("nabavnaVrijednost", "Nabavna Vrijednost");
                    if (lwf.fieldMaxLength["nabavnaVrijednost"].Length < item.nabavnaVrijednost.ToString().Length) lwf.fieldMaxLength["nabavnaVrijednost"] = item.nabavnaVrijednost.ToString();

                    item.konto = reader["konto"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("konto")) lwf.fieldMaxLength.Add("konto", "konto");
                    if (lwf.fieldMaxLength["konto"].Length < item.konto.Length) lwf.fieldMaxLength["konto"] = item.konto;

                    Console.WriteLine("D: " + reader.GetDateTime(4).ToString("dd.MM.yyyy."));

                    item.datumNabavke = reader["datum_nabavke"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumNabavke")) lwf.fieldMaxLength.Add("datumNabavke", OSUtil.columnNames["datumNabavke"]);
                    if (lwf.fieldMaxLength["datumNabavke"].Length < item.datumNabavke.Split(' ')[0].Length) lwf.fieldMaxLength["datumNabavke"] = item.datumNabavke.Split(' ')[0];

                    item.datumAmortizacije = reader["datum_amortizacije"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumAmortizacije")) lwf.fieldMaxLength.Add("datumAmortizacije", "Datum Amortizacije");
                    if (lwf.fieldMaxLength["datumAmortizacije"].Length < item.datumAmortizacije.Length) lwf.fieldMaxLength["datumAmortizacije"] = item.datumAmortizacije;

                    item.datumVrijednosti = reader["datum_vrijednosti"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumVrijednosti")) lwf.fieldMaxLength.Add("datumVrijednosti", OSUtil.columnNames["datumVrijednosti"]);
                    if (lwf.fieldMaxLength["datumVrijednosti"].Length < item.datumVrijednosti.Length) lwf.fieldMaxLength["datumVrijednosti"] = item.datumVrijednosti;

                    item.vrijednostNaDatum = double.Parse(reader.GetString(readerColumns.IndexOf("vr_na_datum_amortizacije")).ToString().Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("vrijednostNaDatum")) lwf.fieldMaxLength.Add("vrijednostNaDatum", OSUtil.columnNames["vrijednostNaDatum"]);
                    if (lwf.fieldMaxLength["vrijednostNaDatum"].Length < item.vrijednostNaDatum.ToString().Length) lwf.fieldMaxLength["vrijednostNaDatum"] = item.vrijednostNaDatum.ToString();


                    item.ispravkaVrijednosti = double.Parse(reader.GetString(readerColumns.IndexOf("ispravka_vrijednosti")).ToString().Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) lwf.fieldMaxLength.Add("ispravkaVrijednosti", "Ispravka Vrijednosti");
                    if (lwf.fieldMaxLength["ispravkaVrijednosti"].Length < item.ispravkaVrijednosti.ToString().Length) lwf.fieldMaxLength["ispravkaVrijednosti"] = item.ispravkaVrijednosti.ToString();

                    item.vek = double.Parse(reader.GetString(readerColumns.IndexOf("vek")).ToString().Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("vek")) lwf.fieldMaxLength.Add("vek", "vek");
                    if (lwf.fieldMaxLength["vek"].Length < item.vek.ToString().Length) lwf.fieldMaxLength["Vek"] = item.vek.ToString();

                    item.datumOtpisa = reader["datum_otpisa"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumOtpisa")) lwf.fieldMaxLength.Add("datumOtpisa", "Datum Otpisa");
                    if (lwf.fieldMaxLength["datumOtpisa"].Length < item.datumOtpisa.Length) lwf.fieldMaxLength["datumOtpisa"] = item.datumOtpisa;

                    item.sadasnjaVrijednost = double.Parse(reader.GetString(readerColumns.IndexOf("sadasnja_vrednost")).ToString().Replace('.', ','), Form1.culture);
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

                    item.brojPoNabavci = reader["broj_po_nabavci"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("brojPoNabavci")) lwf.fieldMaxLength.Add("brojPoNabavci", OSUtil.columnNames["brojPoNabavci"]);
                    if (lwf.fieldMaxLength["brojPoNabavci"].Length < item.brojPoNabavci.ToString().Length) lwf.fieldMaxLength["brojPoNabavci"] = item.brojPoNabavci.ToString();

                    item.amortizacionaGrupa = reader["amortizaciona_grupa"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("amortizacionaGrupa")) lwf.fieldMaxLength.Add("amortizacionaGrupa", "Amortizaciona Grupa");
                    if (lwf.fieldMaxLength["amortizacionaGrupa"].Length < item.amortizacionaGrupa.Length) lwf.fieldMaxLength["amortizacionaGrupa"] = item.amortizacionaGrupa.ToString();

                    item.stopaAmortizacije = double.Parse(reader.GetString(readerColumns.IndexOf("stopa_amortizacije")).Replace('.', ','), Form1.culture);
                    if (!lwf.fieldMaxLength.ContainsKey("stopaAmortizacije")) lwf.fieldMaxLength.Add("stopaAmortizacije", "Stopa Amortizacije");
                    if (lwf.fieldMaxLength["stopaAmortizacije"].Length < item.stopaAmortizacije.ToString().Length) lwf.fieldMaxLength["stopa_amortizacije"] = item.stopaAmortizacije.ToString();

                    item.active = reader["active"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("active")) lwf.fieldMaxLength.Add("active", "Active");
                    if (lwf.fieldMaxLength["active"].Length < item.active.Length) lwf.fieldMaxLength["active"] = item.active;

                    Console.WriteLine(item.nabavnaVrijednost);

                    lwf.items.Add(item);
                    ret.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return lwf;
        }

        public enum Condition { contain, greater, lower, equal, none }

        public static ListWithFieldMaxLengths GetAllWithFilter(List<FieldConditionValue> fcvlist, DateTime datumAmortizacije)
        {
            ListWithFieldMaxLengths lwf = new ListWithFieldMaxLengths();
            try
            {
                string sql = "";
                List<OSItem> ret = new List<OSItem>();
                sql = "SELECT * FROM osnovna_sredstva where ";
                bool first = true;
                string and = "";
                foreach (FieldConditionValue fcv in fcvlist)
                {
                    if (fcv.condition == Condition.contain)
                    {
                        sql += and + @fcv.field + " like '%" + @fcv.value + "%'";
                    }
                    else if (fcv.condition == Condition.lower)
                    {
                        sql += and + @fcv.field + " < '" + @fcv.value + "'";
                    }
                    else if (fcv.condition == Condition.greater)
                    {
                        sql += and + @fcv.field + " > '" + @fcv.value + "'";
                    }
                    else if (fcv.condition == Condition.equal)
                    {
                        sql += and + @fcv.field + " = '" + @fcv.value + "'";
                    }
                    else
                    {

                    }
                    if (first)
                    {
                        and = " and ";
                        first = false;
                    }
                }
                sql += " and active='active';";
                SQLiteCommand command = new SQLiteCommand(sql, cnn);

                SQLiteDataReader reader;
                reader = command.ExecuteReader();
                Console.WriteLine("SQl command filter: " + command.CommandText);

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
                    item.datumNabavke = reader["datum_nabavke"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumNabavke")) lwf.fieldMaxLength.Add("datumNabavke", "Datum Nabavke");
                    if (lwf.fieldMaxLength["datumNabavke"].Length < item.datumNabavke.Length) lwf.fieldMaxLength["datumNabavke"] = item.datumNabavke;

                    item.datumAmortizacije = reader["datum_amortizacije"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumAmortizacije")) lwf.fieldMaxLength.Add("datumAmortizacije", "Datum Amortizacije");
                    if (lwf.fieldMaxLength["datumAmortizacije"].Length < item.datumAmortizacije.Length) lwf.fieldMaxLength["datumAmortizacije"] = item.datumAmortizacije;

                    item.datumVrijednosti = reader["datum_vrijednosti"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumVrijednosti")) lwf.fieldMaxLength.Add("datumVrijednosti", OSUtil.columnNames["datumVrijednosti"]);
                    if (lwf.fieldMaxLength["datumVrijednosti"].Length < item.datumVrijednosti.Length) lwf.fieldMaxLength["datumVrijednosti"] = item.datumVrijednosti;


                    item.vrijednostNaDatum = double.Parse(reader["vr_na_datum_amortizacije"].ToString());
                    if (!lwf.fieldMaxLength.ContainsKey("vrijednostNaDatum")) lwf.fieldMaxLength.Add("vrijednostNaDatum", OSUtil.columnNames["vrijednostNaDatum"]);
                    if (lwf.fieldMaxLength["vrijednostNaDatum"].Length < item.vrijednostNaDatum.ToString().Length) lwf.fieldMaxLength["vrijednostNaDatum"] = item.vrijednostNaDatum.ToString();

                    item.vek = double.Parse(reader["vek"].ToString());
                    if (!lwf.fieldMaxLength.ContainsKey("vek")) lwf.fieldMaxLength.Add("vek", "vek");
                    if (lwf.fieldMaxLength["vek"].Length < item.vek.ToString().Length) lwf.fieldMaxLength["Vek"] = item.vek.ToString();

                    item.datumOtpisa = reader["datum_otpisa"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumOtpisa")) lwf.fieldMaxLength.Add("datumOtpisa", "Datum Otpisa");
                    if (lwf.fieldMaxLength["datumOtpisa"].Length < item.datumOtpisa.Length) lwf.fieldMaxLength["datumOtpisa"] = item.datumOtpisa;


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

                    item.brojPoNabavci = reader["broj_po_nabavci"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("brojPoNabavci")) lwf.fieldMaxLength.Add("brojPoNabavci", OSUtil.columnNames["brojPoNabavci"]);
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

                    double vrijednostNaDatum = item.nabavnaVrijednost;
                    double sadasnjaVrijednost;
                    if (item.vrijednostNaDatum >= 0)
                    {
                        vrijednostNaDatum = item.vrijednostNaDatum;
                        sadasnjaVrijednost = vrijednostNaDatum;
                        //DateTime datumAmortizacije2 = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime datumVrijednosti = DateTime.ParseExact(item.datumVrijednosti, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednostiNaDatum(item.nabavnaVrijednost, datumAmortizacije, DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), item.stopaAmortizacije, vrijednostNaDatum, datumVrijednosti), 2);
                    }
                    else
                    {
                        sadasnjaVrijednost = item.nabavnaVrijednost;
                        //DateTime datumAmortizacije2 = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, datumAmortizacije, DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), item.stopaAmortizacije), 2);
                    }

                    //item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, datumAmortizacije, DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), item.stopaAmortizacije), 2);
                    if (!lwf.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) lwf.fieldMaxLength.Add("ispravkaVrijednosti", item.inventurniBroj);
                    else if (lwf.fieldMaxLength["ispravkaVrijednosti"].Length < item.inventurniBroj.Length) lwf.fieldMaxLength["ispravkaVrijednosti"] = item.ispravkaVrijednosti.ToString();

                    item.sadasnjaVrijednost = Math.Round(sadasnjaVrijednost - item.ispravkaVrijednosti, 2);
                    if (!lwf.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) lwf.fieldMaxLength.Add("sadasnjaVrijednost", item.inventurniBroj);
                    else if (lwf.fieldMaxLength["sadasnjaVrijednost"].Length < item.inventurniBroj.Length) lwf.fieldMaxLength["sadasnjaVrijednost"] = item.sadasnjaVrijednost.ToString();

                    Console.WriteLine(item.nabavnaVrijednost);

                    lwf.items.Add(item);
                    ret.Add(item);
                }
                Console.WriteLine("Count filter: " + lwf.items.Count);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return lwf;
        }

        public static ListWithFieldMaxLengths GetAllWithFilterWithStartDate(List<FieldConditionValue> fcvlist, DateTime startDatumAmortizacije, DateTime endDatumAmortizacije)
        {
            ListWithFieldMaxLengths lwf = new ListWithFieldMaxLengths();
            try
            {
                string sql = "";
                List<OSItem> ret = new List<OSItem>();
                sql = "SELECT * FROM osnovna_sredstva where ";
                bool first = true;
                string and = "";
                foreach (FieldConditionValue fcv in fcvlist)
                {
                    if (fcv.condition == Condition.contain)
                    {
                        sql += and + @fcv.field + " like '%" + @fcv.value + "%'";
                    }
                    else if (fcv.condition == Condition.lower)
                    {
                        sql += and + @fcv.field + " < '" + @fcv.value + "'";
                    }
                    else if (fcv.condition == Condition.greater)
                    {
                        sql += and + @fcv.field + " > '" + @fcv.value + "'";
                    }
                    else if (fcv.condition == Condition.equal)
                    {
                        sql += and + @fcv.field + " = '" + @fcv.value + "'";
                    }
                    else
                    {

                    }
                    if (first)
                    {
                        and = " and ";
                        first = false;
                    }
                }
                sql += " and active='active';";
                SQLiteCommand command = new SQLiteCommand(sql, cnn);

                SQLiteDataReader reader;
                reader = command.ExecuteReader();
                Console.WriteLine("SQl command filter: " + command.CommandText);

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
                    item.datumNabavke = reader["datum_nabavke"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumNabavke")) lwf.fieldMaxLength.Add("datumNabavke", "Datum Nabavke");
                    if (lwf.fieldMaxLength["datumNabavke"].Length < item.datumNabavke.Length) lwf.fieldMaxLength["datumNabavke"] = item.datumNabavke;

                    item.datumAmortizacije = startDatumAmortizacije.ToString("yyyy-MM-dd HH:mm:ss");
                    if (!lwf.fieldMaxLength.ContainsKey("datumAmortizacije")) lwf.fieldMaxLength.Add("datumAmortizacije", "Datum Amortizacije");
                    if (lwf.fieldMaxLength["datumAmortizacije"].Length < item.datumAmortizacije.Length) lwf.fieldMaxLength["datumAmortizacije"] = item.datumAmortizacije;

                    item.datumVrijednosti = reader["datum_vrijednosti"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumVrijednosti")) lwf.fieldMaxLength.Add("datumVrijednosti", OSUtil.columnNames["datumVrijednosti"]);
                    if (lwf.fieldMaxLength["datumVrijednosti"].Length < item.datumVrijednosti.Length) lwf.fieldMaxLength["datumVrijednosti"] = item.datumVrijednosti;


                    item.vrijednostNaDatum = double.Parse(reader["vr_na_datum_amortizacije"].ToString());
                    if (!lwf.fieldMaxLength.ContainsKey("vrijednostNaDatum")) lwf.fieldMaxLength.Add("vrijednostNaDatum", OSUtil.columnNames["vrijednostNaDatum"]);
                    if (lwf.fieldMaxLength["vrijednostNaDatum"].Length < item.vrijednostNaDatum.ToString().Length) lwf.fieldMaxLength["vrijednostNaDatum"] = item.vrijednostNaDatum.ToString();

                    item.vek = double.Parse(reader["vek"].ToString());
                    if (!lwf.fieldMaxLength.ContainsKey("vek")) lwf.fieldMaxLength.Add("vek", "vek");
                    if (lwf.fieldMaxLength["vek"].Length < item.vek.ToString().Length) lwf.fieldMaxLength["Vek"] = item.vek.ToString();

                    item.datumOtpisa = reader["datum_otpisa"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("datumOtpisa")) lwf.fieldMaxLength.Add("datumOtpisa", "Datum Otpisa");
                    if (lwf.fieldMaxLength["datumOtpisa"].Length < item.datumOtpisa.Length) lwf.fieldMaxLength["datumOtpisa"] = item.datumOtpisa;


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

                    item.brojPoNabavci = reader["broj_po_nabavci"].ToString();
                    if (!lwf.fieldMaxLength.ContainsKey("brojPoNabavci")) lwf.fieldMaxLength.Add("brojPoNabavci", OSUtil.columnNames["brojPoNabavci"]);
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

                    double vrijednostNaDatum = item.nabavnaVrijednost;
                    double sadasnjaVrijednost;
                    if (item.vrijednostNaDatum >= 0)
                    {
                        vrijednostNaDatum = item.vrijednostNaDatum;
                        sadasnjaVrijednost = vrijednostNaDatum;
                        //DateTime datumAmortizacije2 = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime datumVrijednosti = DateTime.ParseExact(item.datumVrijednosti, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednostiNaDatum(item.nabavnaVrijednost, endDatumAmortizacije, startDatumAmortizacije, item.stopaAmortizacije, vrijednostNaDatum, datumVrijednosti), 2);
                    }
                    else
                    {
                        sadasnjaVrijednost = item.nabavnaVrijednost;
                        //DateTime datumAmortizacije2 = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, endDatumAmortizacije, startDatumAmortizacije, item.stopaAmortizacije), 2);
                    }

                    //item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, datumAmortizacije, DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), item.stopaAmortizacije), 2);
                    if (!lwf.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) lwf.fieldMaxLength.Add("ispravkaVrijednosti", item.inventurniBroj);
                    else if (lwf.fieldMaxLength["ispravkaVrijednosti"].Length < item.inventurniBroj.Length) lwf.fieldMaxLength["ispravkaVrijednosti"] = item.ispravkaVrijednosti.ToString();

                    item.sadasnjaVrijednost = Math.Round(sadasnjaVrijednost - item.ispravkaVrijednosti, 2);
                    if (!lwf.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) lwf.fieldMaxLength.Add("sadasnjaVrijednost", item.inventurniBroj);
                    else if (lwf.fieldMaxLength["sadasnjaVrijednost"].Length < item.inventurniBroj.Length) lwf.fieldMaxLength["sadasnjaVrijednost"] = item.sadasnjaVrijednost.ToString();

                    Console.WriteLine(item.nabavnaVrijednost);

                    lwf.items.Add(item);
                    ret.Add(item);
                }
                Console.WriteLine("Count filter: " + lwf.items.Count);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return lwf;
        }

        public static ListWithFieldMaxLengths GetAllSaIspravkaVrijednostiISadasnjaVrijednost(DateTime pickedDate)
        {
            ListWithFieldMaxLengths ret = new ListWithFieldMaxLengths();
            try
            {
                ListWithFieldMaxLengths allItemsFromDB = GetAll();
                foreach (OSItem item in allItemsFromDB.items)
                {
                    ret.fieldMaxLength = allItemsFromDB.fieldMaxLength;
                    double vrijednostNaDatum = item.nabavnaVrijednost;
                    double sadasnjaVrijednost;
                    if (item.vrijednostNaDatum >= 0)
                    {
                        vrijednostNaDatum = item.vrijednostNaDatum;
                        sadasnjaVrijednost = vrijednostNaDatum;
                        DateTime datumAmortizacije = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime datumVrijednosti = DateTime.ParseExact(item.datumVrijednosti, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednostiNaDatum(item.nabavnaVrijednost, pickedDate, datumAmortizacije, item.stopaAmortizacije, vrijednostNaDatum, datumVrijednosti), 5);
                    }
                    else
                    {
                        sadasnjaVrijednost = item.nabavnaVrijednost;
                        DateTime datumAmortizacije = DateTime.ParseExact(item.datumAmortizacije, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, pickedDate, datumAmortizacije, item.stopaAmortizacije), 5);
                    }
                    if (!ret.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) ret.fieldMaxLength.Add("ispravkaVrijednosti", item.ispravkaVrijednosti.ToString());
                    else if (ret.fieldMaxLength["ispravkaVrijednosti"].Length < item.ispravkaVrijednosti.ToString().Length) ret.fieldMaxLength.Add("ispravkaVrijednosti", item.ispravkaVrijednosti.ToString());

                    item.sadasnjaVrijednost = Math.Round(sadasnjaVrijednost - item.ispravkaVrijednosti, 5);
                    if (!ret.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) ret.fieldMaxLength.Add("sadasnjaVrijednost", item.sadasnjaVrijednost.ToString());
                    else if (ret.fieldMaxLength["sadasnjaVrijednost"].Length < item.sadasnjaVrijednost.ToString().Length) ret.fieldMaxLength.Add("sadasnjaVrijednost", item.sadasnjaVrijednost.ToString());

                    Console.WriteLine("Nabavna: " + item.nabavnaVrijednost + " ispravka: " + item.ispravkaVrijednosti + " sadasnja: " + item.sadasnjaVrijednost);
                    ret.items.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return ret;
        }

        public static ListWithFieldMaxLengths GetAllSaIspravkaVrijednostiISadasnjaVrijednostWithStartDate(DateTime startDate,DateTime endDate)
        {
            ListWithFieldMaxLengths ret = new ListWithFieldMaxLengths();
            try
            {
                ListWithFieldMaxLengths allItemsFromDB = GetAll();
                foreach (OSItem item in allItemsFromDB.items)
                {
                    ret.fieldMaxLength = allItemsFromDB.fieldMaxLength;
                    double vrijednostNaDatum = item.nabavnaVrijednost;
                    double sadasnjaVrijednost;
                    item.datumAmortizacije = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                    if (item.vrijednostNaDatum >= 0)
                    {
                        vrijednostNaDatum = item.vrijednostNaDatum;
                        sadasnjaVrijednost = vrijednostNaDatum;
                        
                        DateTime datumVrijednosti = DateTime.ParseExact(item.datumVrijednosti, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednostiNaDatum(item.nabavnaVrijednost, endDate, startDate, item.stopaAmortizacije, vrijednostNaDatum, datumVrijednosti), 5);
                    }
                    else
                    {
                        sadasnjaVrijednost = item.nabavnaVrijednost;
                        
                        item.ispravkaVrijednosti = Math.Round(OSUtil.ispravkaVrijednosti(item.nabavnaVrijednost, endDate, startDate, item.stopaAmortizacije), 5);
                    }
                    if (!ret.fieldMaxLength.ContainsKey("ispravkaVrijednosti")) ret.fieldMaxLength.Add("ispravkaVrijednosti", item.ispravkaVrijednosti.ToString());
                    else if (ret.fieldMaxLength["ispravkaVrijednosti"].Length < item.ispravkaVrijednosti.ToString().Length) ret.fieldMaxLength.Add("ispravkaVrijednosti", item.ispravkaVrijednosti.ToString());

                    item.sadasnjaVrijednost = Math.Round(sadasnjaVrijednost - item.ispravkaVrijednosti, 5);
                    if (!ret.fieldMaxLength.ContainsKey("sadasnjaVrijednost")) ret.fieldMaxLength.Add("sadasnjaVrijednost", item.sadasnjaVrijednost.ToString());
                    else if (ret.fieldMaxLength["sadasnjaVrijednost"].Length < item.sadasnjaVrijednost.ToString().Length) ret.fieldMaxLength.Add("sadasnjaVrijednost", item.sadasnjaVrijednost.ToString());

                    Console.WriteLine("Nabavna: " + item.nabavnaVrijednost + " ispravka: " + item.ispravkaVrijednosti + " sadasnja: " + item.sadasnjaVrijednost);
                    ret.items.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return ret;
        }

        public class FieldConditionValue
        {
            public string field { set; get; }
            public DBManager.Condition condition { set; get; }
            public string value { set; get; }
        }

        public static bool checkUserLogin(string username, string password)
        {
            bool ret = false;

            try
            {
                string sql = "SELECT * FROM users WHERE korisnicko_ime='" + @username + "' and lozinka='" + @password + "';";

                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);

                SQLiteDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine(reader["korisnicko_ime"]);
                }
                ret = reader.HasRows;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ret;
        }

        public static List<string> GetAllFromColumnAsStrings(string columnName, string containsString)
        {
            List<string> ret = new List<string>();
            try
            {
                string sqlGetAllFromColumn = "SELECT " + @columnName + " FROM osnovna_sredstva where " + @columnName + " like '%" + @containsString + "%' ORDER BY " + @columnName + " ASC";

                SQLiteCommand cmd = new SQLiteCommand(sqlGetAllFromColumn, cnn);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ret.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;

        }

        public static List<string> GetAllFromColumnAsStringsDistinct(string columnName, string containsString)
        {
            List<string> ret = new List<string>();
            try
            {
                string sqlGetAllFromColumn = "SELECT DISTINCT " + @columnName + " FROM osnovna_sredstva where " + @columnName + " like '%" + @containsString + "%' ORDER BY " + @columnName + " ASC";

                SQLiteCommand cmd = new SQLiteCommand(sqlGetAllFromColumn, cnn);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ret.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;

        }

        public static string GetPodesavanje(string ime)
        {
            string ret = null;

            string sqlGetPodesavanje = "SELECT vrijednost FROM podesavanja WHERE ime='" + @ime + "';";
            SQLiteCommand cmd = new SQLiteCommand(sqlGetPodesavanje, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ret = reader.GetString(0);
            }
            return ret;
        }

        public static void IzmijeniPodesavanje(string ime, object vrijednost)
        {
            string sqlGetPodesavanje = "UPDATE podesavanja SET vrijednost='" + @vrijednost + "' WHERE ime='" + @ime + "';";
            SQLiteCommand cmd = new SQLiteCommand(sqlGetPodesavanje, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();

        }

        public static void KreirajPodesavanje(string ime, object vrijednost)
        {


            string sqlGetPodesavanje = "INSERT INTO podesavanja (ime,vrijednost) VALUES ('" + @ime + "', '" + @vrijednost + "');";
            SQLiteCommand cmd = new SQLiteCommand(sqlGetPodesavanje, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();

        }

    }
}
