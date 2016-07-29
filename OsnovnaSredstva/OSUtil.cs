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
            double ret = 1;
            double sadasnjaVrijednost;
            double step=0;
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
                        step = Math.Round((nabavnaVrijednost * (stopaAmortizacije / 100) * ((daysLeftInYear) * 1.0 / daysInYear)),2);
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
                int daysInCurrentYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                step = (nabavnaVrijednost * (stopaAmortizacije / 100) * ((currentDate.DayOfYear * 1.0) / daysInCurrentYear));
                ret += step;
            }
            else
            {
                int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                ret = (nabavnaVrijednost * (stopaAmortizacije / 100) * (Math.Floor((currentDate - amortizacijeDate).TotalDays) / daysInYear));
            }
            if (ret < 0) ret = 0;
            if (ret > nabavnaVrijednost) ret = nabavnaVrijednost;
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
    }
}
