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
using System.Drawing;
using System.Drawing.Printing;


namespace OsnovnaSredstva
{
    public partial class PregledForm : Form
    {
        List<OSItem> changedItems = new List<OSItem>();
        static ListWithFieldMaxLengths itemsForList = new ListWithFieldMaxLengths();
        public PregledForm()
        {
            InitializeComponent();
            itemsForList = DBManager.GetAll();
            dgvPregled.Font = new Font("Courier New", 8);
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

            foreach (OSItem item in itemsForList.items)
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

        //define rw as globly variable in form
        public void zpt()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Arial", 10);
            PaperSize psz = new PaperSize("Custom", 100, 200);
            PaperSize pszA4 = new PaperSize();
            pszA4.RawKind = (int)PaperKind.A4;
            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = pszA4;
            //pdoc.DefaultPageSettings.PaperSize.Height = 820;
            //pdoc.DefaultPageSettings.PaperSize.Width = 700;
            pdoc.DefaultPageSettings.Landscape = true;
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            DialogResult res = pd.ShowDialog();
            if (res == DialogResult.OK)
            {
                PrintPreviewDialog prv = new PrintPreviewDialog();
                prv.Document = pdoc;
                res = prv.ShowDialog();
                if (res == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int Offset = 40;
            //graphics.DrawString("Welcome to Bakery Shop", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            string underLine = "------------------------------------------";
            //graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            
            int offsetY = startY;
            int offsetX = startX;
            string[] columns = { "id", "inventurniBroj", "naziv", "kolicina", "datumNabavke", "datumAmortizacije", "nabavnaVrijednost", "ispravkaVrijednosti", "sadasnjaVrijednost", "konto" };
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            columnNames.Add("id", "ID");
            columnNames.Add("inventurniBroj", "Inventurni Broj");
            columnNames.Add("naziv", "Naziv");
            columnNames.Add("kolicina", "Kolicina");
            columnNames.Add("datumNabavke", "Datum Nabavke");
            columnNames.Add("datumAmortizacije", "Datum Amortizacije");
            columnNames.Add("nabavnaVrijednost", "Nabavna Vrijednost");
            columnNames.Add("ispravkaVrijednosti", "Ispravka Vrijednosti");
            columnNames.Add("sadasnjaVrijednost", "Sadasnja Vrijednost");
            columnNames.Add("konto", "Konto");
            
            foreach (string cname in columns)
            {
                SizeF maxStringSize = new SizeF();
                Console.WriteLine("maxstr: " + itemsForList.fieldMaxLength[cname] + " cname: " + cname);
                maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], dgvPregled.Font);
                
                graphics.DrawRectangle(Pens.Black, offsetX, offsetY, maxStringSize.Width, dgvPregled.Rows[0].Height);
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(offsetX+1 , offsetY + 1, (int)maxStringSize.Width -1 , dgvPregled.Rows[0].Height));
                RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)maxStringSize.Width, (float)dgvPregled.Rows[0].Height);
                StringFormat stringDrawFormat = new StringFormat();
                stringDrawFormat.Alignment = StringAlignment.Center;
                stringDrawFormat.LineAlignment = StringAlignment.Center;
                
                graphics.DrawString(columnNames[cname], dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);
                
                offsetX += (int)maxStringSize.Width;
            }
            offsetY += dgvPregled.Rows[0].Height;
            int a = itemsForList.items.Count;
            int c = dgvPregled.Columns.Count;
            Type ositemType = typeof(OSItem);
            
            for (int i = 0; i < a; i++)
            {
                offsetX = startX;
                
                foreach (string cname in columns)
                {
                    PropertyInfo prop = ositemType.GetProperty(cname);
                    SizeF maxStringSize = new SizeF();
                    Console.WriteLine("maxstr: "+itemsForList.fieldMaxLength[cname]+" cname: "+cname);
                    maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], dgvPregled.Font);
                    //graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawRectangle(Pens.Black, offsetX, offsetY, maxStringSize.Width, dgvPregled.Rows[i].Height);
                    //graphics.FillRectangle(Brushes.LightGray, new Rectangle(offsetX + 1, offsetY + 1,(int)maxStringSize.Width - 1, dgvPregled.Rows[i].Height));
                    RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)maxStringSize.Width, (float)dgvPregled.Rows[i].Height);
                    StringFormat stringDrawFormat = new StringFormat();
                    stringDrawFormat.Alignment = StringAlignment.Center;
                    stringDrawFormat.LineAlignment = StringAlignment.Center;
                    //Convert.ToString(dataGridView1.Rows[i].Cells[k].Value);
                    graphics.DrawString(prop.GetValue(itemsForList.items[i])+"", dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);
                    
                    //graphics.DrawString()

                    //graphics.DrawString("\t" + Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    //offsetX += dgvPregled.Columns[cname].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells,true);
                    offsetX += (int)maxStringSize.Width;
                }
                offsetY += dgvPregled.Rows[i].Height;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            zpt();
            
        }

        private void dgvPregled_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            

            
            OSItem item =DBManager.GetItem(dgv.Rows[e.RowIndex].Cells["id"].Value.ToString());
            Console.WriteLine("Item name: "+item.naziv);
            item.inventurniBroj = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            PropertyInfo prop = item.GetType().GetProperty(dgv.Columns[e.ColumnIndex].Name, BindingFlags.Public | BindingFlags.Instance);
            
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(item, Convert.ChangeType(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, prop.PropertyType) , null);
            }
            DBManager.UpdateItem(item);
        }

        private void dgvPregled_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Console.WriteLine(e.Row.Cells["id"].Value);

            DBManager.deleteOS((string)e.Row.Cells["id"].Value);
        }
    }
}
