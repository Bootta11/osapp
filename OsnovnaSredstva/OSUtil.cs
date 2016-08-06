using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsnovnaSredstva
{
    static class OSUtil
    {
        public static Dictionary<string, string> columnNames;
        public static Dictionary<string, string> columnNamesToTableMapping;
        public static Dictionary<string, string> columnNamesToItemFieldMapping;
        public static void init()
        {
            columnNames = new Dictionary<string, string>()
        {
            {"id", "ID" },
            {"inventurniBroj", "Inventurni broj" },
            { "naziv", "Naziv"},
            {"kolicina", "Količina" },
            { "datumNabavke", "Datum nabavke"},
            { "datumAmortizacije", "Datum amortizacije"},
            { "datumVrijednosti", "Datum vrijednosti"},
            { "vrijednostNaDatum","Vr. na datum" },
            { "nabavnaVrijednost", "Nabavna vrijednost"},
            { "ispravkaVrijednosti", "Ispravka vrijednosti"},
            { "sadasnjaVrijednost", "Sadašnja vrijednost"},
            { "konto", "Konto"},
            {"vek", "Vek" },
            {"datumOtpisa", "Datum otpisa" },
            { "jedinicaMjere", "Jedinica mjere"},
            {"dobavljac", "Dobavljač" },
            {"racunDobavljaca", "Račun dobavljača" },
            { "racunopolagac", "Računopolagač"},
            { "lokacija", "Lokacija"},
            {"smjestaj", "Smještaj" },
            { "metodaAmortizacije", "Metoda amortizacije"},
            { "poreskeGrupe", "Poreske grupe"},
            { "brojPoNabavci", "Br.po.nab."},
            { "amortizacionaGrupa", "Amortizaciona grupa"},
            { "stopaAmortizacije", "Stop amortizacije"},
            { "active", "Aktivan"},
            { "#", "R.br."}
        };

            columnNamesToTableMapping = new Dictionary<string, string>()
        {
            {"id", "id" },
            {"inventurniBroj", "inventurni_broj" },
            { "naziv", "naziv"},
            {"kolicina", "kolicina" },
            { "datumNabavke", "datum_nabavke"},
            { "datumAmortizacije", "datum_amortizacije"},
            { "datumVrijednosti", "Datum vrijednosti"},
            { "vrijednostNaDatum","vr_na_datum_amortizacije" },
            { "nabavnaVrijednost", "nabavna_vrijednost"},
            { "ispravkaVrijednosti", "ispravka_vrijednosti"},
            { "sadasnjaVrijednost", "sadasnja_vrijednost"},
            { "konto", "konto"},
            {"vek", "vek" },
            {"datumOtpisa", "datum_otpisa" },
            { "jedinicaMjere", "jedinica_mjere"},
            {"dobavljac", "dobavljac" },
            {"racunDobavljaca", "racun_dok_dobavljaca" },
            { "racunopolagac", "racunopolagac"},
            { "lokacija", "lokacija"},
            {"smjestaj", "smjestaj" },
            { "metodaAmortizacije", "metoda_amortizacije"},
            { "poreskeGrupe", "poreske_grupe"},
            { "brojPoNabavci", "broj_po_nabavci"},
            { "amortizacionaGrupa", "amortizaciona_grupa"},
            { "stopaAmortizacije", "stop_amortizacije"},
            { "active", "active"},
            { "#", "R.br."}
        };
        }

        public static double ispravkaVrijednosti(double nabavnaVrijednost, DateTime currentDate, DateTime amortizacijeDate, double stopaAmortizacije)
        {
            double nbVrijednost = nabavnaVrijednost;
            DateTime currDate = currentDate;
            
            double ret = 1;
            double sadasnjaVrijednost;
            double step = 0;
            int yearDiff = currDate.Year - amortizacijeDate.Year;
            sadasnjaVrijednost = nbVrijednost;
            if (yearDiff > 0)
            {
                int count = 0;


                while (count < yearDiff)
                {
                    if (count == 0)
                    {
                        int daysInYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                        int daysLeftInYear = daysInYear - amortizacijeDate.DayOfYear; // Result is in range 0-365.
                        step = (Math.Round( (nbVrijednost * (stopaAmortizacije / 100) / daysInYear), 4) * (daysLeftInYear));
                        ret = step;
                        sadasnjaVrijednost -= step;
                    }
                    else
                    {
                        step = nabavnaVrijednost * (stopaAmortizacije / 100);
                        ret += step;
                        sadasnjaVrijednost -= step;
                    }
                    count++;
                }
                int daysInCurrentYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                step = (Math.Round((nbVrijednost * (stopaAmortizacije / 100) / daysInCurrentYear),4)  * currDate.DayOfYear);
                ret += step;
            }
            else
            {
                int daysInYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                ret = ( Math.Round( ((nbVrijednost * (stopaAmortizacije / 100))  /daysInYear), 4 ) * (Math.Floor((currDate - amortizacijeDate).TotalDays)));
            }
            //ret = Math.Round(ret, 3);
            
            if (ret < 0) ret = 0;
            if (ret > nbVrijednost) ret = nbVrijednost;
            return ret;
        }

        public static double ispravkaVrijednostiNaDatum(double nabavnaVrijednost, DateTime currentDate, DateTime amortizacijeDate, double stopaAmortizacije, double vrijednostNaDatum, DateTime datumVrijednosti)
        {
            double nbVrijednost = nabavnaVrijednost;
            DateTime currDate = currentDate;
            
                nbVrijednost = vrijednostNaDatum;
                currDate = currentDate;
            
            double ret = 1;
            double sadasnjaVrijednost;
            double step = 0;
            int yearDiff = currDate.Year - amortizacijeDate.Year;
            sadasnjaVrijednost = nbVrijednost;
            if (yearDiff > 0)
            {
                int count = 0;
                

                while (count < yearDiff)
                {
                    currDate =  new DateTime(amortizacijeDate.Year+count, currDate.Month, currDate.Day, 0, 0, 0); ;
                    if (count == 0)
                    {
                        int daysInYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                        int daysLeftInYear = daysInYear - datumVrijednosti.DayOfYear; // Result is in range 0-365.
                        step = (Math.Round((nabavnaVrijednost * (stopaAmortizacije / 100) / daysInYear), 4) * (daysLeftInYear));
                        ret = step;
                        sadasnjaVrijednost -= step;
                    }
                    else
                    {
                        step = nabavnaVrijednost * (stopaAmortizacije / 100);
                        ret += step;
                        sadasnjaVrijednost -= step;
                    }
                    count++;
                }
                currDate = new DateTime(amortizacijeDate.Year + count, currDate.Month, currDate.Day, 0, 0, 0);
                int daysInCurrentYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                step = (Math.Round((nabavnaVrijednost * (stopaAmortizacije / 100) / daysInCurrentYear), 4) * currDate.DayOfYear);
                ret += step;
            }
            else
            {
                int daysInYear = DateTime.IsLeapYear(currDate.Year) ? 366 : 365;
                ret = (Math.Round(((nabavnaVrijednost * (stopaAmortizacije / 100)) / daysInYear), 4) * (Math.Floor((currDate - datumVrijednosti).TotalDays)));
            }
            //ret = Math.Round(ret, 3);

            if (ret < 0) ret = 0;
            if (ret > nbVrijednost) ret = nbVrijednost;
            return ret;
        }

        public static double ispravkaVrijednostiRegresivna(double nabavnaVrijednost, DateTime currentDate, DateTime amortizacijeDate, double stopaAmortizacije)
        {
            double ret = 1;
            double sadasnjaVrijednost;
            double step = 0;
            int yearDiff = currentDate.Year - amortizacijeDate.Year;
            sadasnjaVrijednost = nabavnaVrijednost;
            if (yearDiff > 0)
            {
                int count = 0;


                while (count < yearDiff)
                {
                    if (count == 0)
                    {
                        int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                        int daysLeftInYear = daysInYear - amortizacijeDate.DayOfYear; // Result is in range 0-365.
                        step = (nabavnaVrijednost * (stopaAmortizacije / 100) * ((daysLeftInYear) * 1.0 / daysInYear));
                        ret = step;
                        sadasnjaVrijednost -= step;
                    }
                    else
                    {
                        step = sadasnjaVrijednost * (stopaAmortizacije / 100);
                        ret += step;
                        sadasnjaVrijednost -= step;
                    }
                    count++;
                }
                int daysInCurrentYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                step = (sadasnjaVrijednost * (stopaAmortizacije / 100) * ((currentDate.DayOfYear * 1.0) / daysInCurrentYear));
                ret += step;
            }
            else
            {
                int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                ret = (nabavnaVrijednost * (stopaAmortizacije / 100) * (Math.Floor((currentDate - amortizacijeDate).TotalDays) / daysInYear));
            }

            return (ret < 0 ? ret = 0 : ret);
        }

        public static T ConvertToTypeInParameter<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static string generateHashFromString(string s)
        {
            string ret = "";
            byte[] HashValue;

            string MessageString = s;

            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] MessageBytes = UE.GetBytes(MessageString);

            //Create a new instance of the SHA1Managed class to create 
            //the hash value.
            System.Security.Cryptography.SHA256Managed SHhash = new System.Security.Cryptography.SHA256Managed();

            //Create the hash value from the array of bytes.
            HashValue = SHhash.ComputeHash(MessageBytes);

            //Display the hash value to the console. 
            foreach (byte b in HashValue)
            {
                ret += b;
            }

            return ret;
        }
    }
}
