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
            { "sadasnjaVrijednost", "Sadasnja vrijednost"},
            { "konto", "Konto"},
            {"vek", "Vek" },
            {"datumOtpisa", "Datum otpisa" },
            { "jedinicaMjere", "Jedinica mjere"},
            {"dobavljac", "Dobavljac" },
            {"racunDobavljaca", "Račun dobavljača" },
            { "racunopolagac", "Računopolagač"},
            { "lokacija", "Lokacija"},
            {"smjestaj", "Smještaj" },
            { "metodaAmortizacije", "Metoda amortizacije"},
            { "poreskeGrupe", "Poreske grupe"},
            { "brojPoNabavci", "Broj nabavke"},
            { "amortizacionaGrupa", "Amortizaciona grupa"},
            { "stopaAmortizacije", "Stop amortizacije"},
            { "active", "Aktivan"},
            { "#", "R.br."}
        };
        }
        public static double ispravkaVrijednosti(double nabavnaVrijednost, DateTime currentDate, DateTime amortizacijeDate, double stopaAmortizacije)
        {
            double ret = 1;

            int yearDiff = currentDate.Year - amortizacijeDate.Year;

            if (yearDiff > 0)
            {
                int count = 0;


                while (count < yearDiff)
                {
                    if (count == 0)
                    {
                        int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                        int daysLeftInYear = daysInYear - amortizacijeDate.DayOfYear; // Result is in range 0-365.
                        ret = (nabavnaVrijednost * (stopaAmortizacije / 100) * ((daysLeftInYear) * 1.0 / daysInYear));
                    }
                    else
                    {
                        ret = ret * (stopaAmortizacije / 100);
                    }
                    count++;
                }
                int daysInCurrentYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                ret = (ret * (stopaAmortizacije / 100) * ((currentDate.DayOfYear * 1.0) / daysInCurrentYear));
            }
            else
            {
                int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
                ret = (nabavnaVrijednost * (stopaAmortizacije / 100) * (Math.Floor((currentDate - amortizacijeDate).TotalDays) / daysInYear));
            }

            return ret;
        }

        public static T ConvertToTypeInParameter<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
    }
}
