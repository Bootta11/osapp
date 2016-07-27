using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace OsnovnaSredstva
{
    public partial class PregledForm : Form
    {
        static List<OSItem> itemsForList = new List<OSItem>();
        public PregledForm()
        {
            InitializeComponent();
            itemsForList = DBManager.GetAll();

            Type myType = typeof(OSItem);
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            Console.WriteLine(props.Count);
            dgvPregled.ColumnCount = props.Count;
            for (int i = 0; i < props.Count; i++)
            {
                //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                //object propValue = prop.GetValue(myObject, null);
                dgvPregled.Columns[i].Name = props.ElementAt(i).Name;
                // Do something with propValue
            }

            foreach (OSItem item in itemsForList)
            {
                List<string> row = new List<string>();
                for (int i = 0; i < props.Count; i++)
                {
                    if (props.ElementAt(i).Name.StartsWith("datum"))
                    {
                        row.Add(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy."));
                    }
                    else
                    {
                        row.Add(props.ElementAt(i).GetValue(item, null).ToString());

                    }
                    //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                    //object propValue = prop.GetValue(myObject, null);
                    
                    // Do something with propValue
                }
                dgvPregled.Rows.Add(row.ToArray());
            }
        }
    }
}
