using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OsnovnaSredstva
{
    public class OSItem
    {
        public string id { set; get; }
        public string inventurniBroj { set; get; }
        public string naziv { set; get; }
        public double kolicina { set; get; }
        public string datumNabavke { set; get; }
        public double nabavnaVrijednost { set; get; }
        public string konto { set; get; }
        public string datumAmortizacije { set; get; }
        public string datumVrijednosti { set; get; }
        public double vrijednostNaDatum { set; get; }
        public double ispravkaVrijednosti { set; get; }
        public double vek { set; get; }
        public string datumOtpisa { set; get; }
        public double sadasnjaVrijednost { set; get; }
        public string jedinicaMjere { set; get; }
        public string dobavljac { set; get; }
        public string racunDobavljaca { set; get; }
        public string racunopolagac { set; get; }
        public string lokacija { set; get; }
        public string smjestaj { set; get; }
        public string metodaAmortizacije { set; get; }
        public string poreskeGrupe { set; get; }
        public int brojPoNabavci { set; get; }
        public string amortizacionaGrupa { set; get; }
        public double stopaAmortizacije { set; get; }
        public string active { set; get; }

        public override string ToString()
        {
            string ret = "{ ";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(this);
                //Console.WriteLine("{0}={1}", name, value);
                ret += name + "=" + value + " ";
            }
            ret += "}";
            return ret;
        }
    }

    public class ListWithFieldMaxLengths
    {
        public List<OSItem> items=new List<OSItem>();
        public Dictionary<string, string> fieldMaxLength = new Dictionary<string, string>();

    }

    
}
