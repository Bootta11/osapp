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
using System.Drawing.Printing;
using System.Globalization;


namespace OsnovnaSredstva
{
    public partial class PregledForm : Form
    {
        static int itemPrintNum = 0;
        List<OSItem> changedItems = new List<OSItem>();
        static ListWithFieldMaxLengths itemsForList = new ListWithFieldMaxLengths();
        Dictionary<int, string> dictFields = new Dictionary<int, string>();
        public PregledForm()
        {
            InitializeComponent();



            itemsForList = DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(DateTime.Now);
            dgvPregled.Font = new Font("Courier New", 8);



            cbFieldName.Items.Insert(0, "-ime polja-");
            cbFieldName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFieldName.SelectedIndex = 0;
            cbCondition.Items.Insert(0, "-uslov za pretragu-");
            cbCondition.SelectedIndex = 0;
            cbCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            int cbi = 1;
            foreach (string ck in OSUtil.columnNamesToTableMapping.Keys)
            {
                dictFields.Add(cbi, ck);
                cbFieldName.Items.Insert(cbi, OSUtil.columnNames[ck]);
                cbi++;
            }
            FillGridView(itemsForList);
        }

        public void FillGridView(ListWithFieldMaxLengths itemslist)
        {
            itemsForList = itemslist;
            dgvPregled.Rows.Clear();
            Type myType = typeof(OSItem);
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            Console.WriteLine(props.Count);
            dgvPregled.ColumnCount = props.Count;
            for (int i = 0; i < props.Count; i++)
            {
                //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                //object propValue = prop.GetValue(myObject, null);
                dgvPregled.Columns[i].Name = OSUtil.columnNames[props.ElementAt(i).Name];
                // Do something with propValue
            }

            foreach (OSItem item in itemslist.items)
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
            //string underLine = "------------------------------------------";
            //graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            int offsetY = startY;
            int offsetX = startX;
            string[] columns = { "#", "inventurniBroj", "naziv", "kolicina", "datumNabavke", "datumAmortizacije", "nabavnaVrijednost", "ispravkaVrijednosti", "sadasnjaVrijednost", "konto" };


            foreach (string cname in columns)
            {
                SizeF maxStringSize = new SizeF();
                if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], dgvPregled.Font); maxStringSize.Width += 2; }
                else maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], dgvPregled.Font);

                graphics.DrawRectangle(Pens.Black, offsetX, offsetY, maxStringSize.Width, dgvPregled.Rows[0].Height);
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(offsetX + 1, offsetY + 1, (int)maxStringSize.Width - 1, dgvPregled.Rows[0].Height));
                RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)maxStringSize.Width, (float)dgvPregled.Rows[0].Height);
                StringFormat stringDrawFormat = new StringFormat();
                stringDrawFormat.Alignment = StringAlignment.Center;
                stringDrawFormat.LineAlignment = StringAlignment.Center;

                graphics.DrawString(OSUtil.columnNames[cname], dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);

                offsetX += (int)maxStringSize.Width;
            }
            offsetY += dgvPregled.Rows[0].Height;
            int a = itemsForList.items.Count;
            int c = dgvPregled.Columns.Count;
            Type ositemType = typeof(OSItem);
            int rb = 1;
            for (; itemPrintNum < a; itemPrintNum++)
            {
                offsetX = startX;
                if ((offsetY) > (e.PageBounds.Height - startY - 10))
                {

                    offsetY = startY;
                    e.HasMorePages = true;
                    break;
                }
                else
                {
                    if (itemPrintNum >= a)
                    {
                        itemPrintNum = 0;
                    }
                }
                foreach (string cname in columns)
                {
                    PropertyInfo prop = ositemType.GetProperty(cname);
                    SizeF maxStringSize = new SizeF();

                    if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], dgvPregled.Font); maxStringSize.Width += 2; }
                    else maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], dgvPregled.Font);

                    //graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawRectangle(Pens.Black, offsetX, offsetY, maxStringSize.Width, dgvPregled.Rows[itemPrintNum].Height);
                    //graphics.FillRectangle(Brushes.LightGray, new Rectangle(offsetX + 1, offsetY + 1,(int)maxStringSize.Width - 1, dgvPregled.Rows[i].Height));
                    RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)maxStringSize.Width, (float)dgvPregled.Rows[itemPrintNum].Height);
                    StringFormat stringDrawFormat = new StringFormat();
                    stringDrawFormat.Alignment = StringAlignment.Center;
                    stringDrawFormat.LineAlignment = StringAlignment.Center;
                    //Convert.ToString(dataGridView1.Rows[i].Cells[k].Value);
                    if (cname.Equals("#"))
                    {
                        graphics.DrawString((itemPrintNum + 1) + "", dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    else if (cname.StartsWith("datum"))
                    {
                        graphics.DrawString(DateTime.ParseExact(prop.GetValue(itemsForList.items[itemPrintNum]).ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy") + "", dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    else
                    {
                        graphics.DrawString(prop.GetValue(itemsForList.items[itemPrintNum]) + "", dgvPregled.Font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    //graphics.DrawString()

                    //graphics.DrawString("\t" + Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    //offsetX += dgvPregled.Columns[cname].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells,true);
                    offsetX += (int)maxStringSize.Width;
                }
                offsetY += dgvPregled.Rows[itemPrintNum].Height;

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            itemPrintNum = 0;
            zpt();

        }

        private void dgvPregled_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            bool update = true;


            OSItem item = DBManager.GetItem(dgv.Rows[e.RowIndex].Cells["id"].Value.ToString());
            Console.WriteLine("Item name: " + item.naziv);
            //item.inventurniBroj = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            PropertyInfo prop = item.GetType().GetProperty(OSUtil.columnNames.Where(c => c.Value == dgv.Columns[e.ColumnIndex].Name).Select(pair => pair.Key).ToArray()[0], BindingFlags.Public | BindingFlags.Instance);

            if (null != prop && prop.CanWrite)
            {
                if (prop.Name.ToLower().StartsWith("datum"))
                {
                    DateTime dt = new DateTime();
                    update = DateTime.TryParseExact(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "dd.MM.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    prop.SetValue(item, dt.ToString("yyyy-MM-dd HH:mm:ss"), null);
                }
                else
                {
                    prop.SetValue(item, Convert.ChangeType(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, prop.PropertyType), null);
                }
            }
            if (update)
                DBManager.UpdateItem(item);
        }

        private void dgvPregled_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Console.WriteLine(e.Row.Cells["id"].Value);

            DBManager.deleteOS((string)e.Row.Cells["id"].Value);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string itemfieldName = "";
            string selectedItem = "";

            if (cbFieldName.SelectedIndex > 0)
            {
                selectedItem = cbFieldName.Items[cbFieldName.SelectedIndex].ToString().Trim();
                if (selectedItem.ToLower().StartsWith("datum"))
                {
                    if (cbCondition.SelectedIndex > 0)
                    {
                        DBManager.Condition cond = DBManager.Condition.contain;
                        string conditionValue = cbCondition.Items[cbCondition.SelectedIndex].ToString();
                        if (conditionValue.Equals("Manje")) cond = DBManager.Condition.lower;
                        else if (conditionValue.Equals("Vece")) cond = DBManager.Condition.greater;
                        else if (conditionValue.Equals("Jednako")) cond = DBManager.Condition.equal;
                        else cond = DBManager.Condition.none;
                        dictFields.TryGetValue(cbFieldName.SelectedIndex, out itemfieldName);
                        string databaseFieldName = (cond == DBManager.Condition.none ? "": OSUtil.columnNamesToTableMapping[itemfieldName]);
                        FillGridView(DBManager.GetAllWithFilter(databaseFieldName, cond, inputDatumZaPretragu.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    else
                    {
                        FillGridView(DBManager.GetAllWithFilter("", DBManager.Condition.none, inputSearchValue.Text.Trim()));
                    }
                }
                else
                {

                    if (cbCondition.SelectedIndex > 0)
                    {
                        DBManager.Condition cond = DBManager.Condition.contain;
                        string conditionValue = cbCondition.Items[cbCondition.SelectedIndex].ToString();
                        if (conditionValue.Equals("Vanje")) cond = DBManager.Condition.lower;
                        else if (conditionValue.Equals("Vece")) cond = DBManager.Condition.greater;
                        else if (conditionValue.Equals("Sadrzi")) cond = DBManager.Condition.contain;
                        else if (conditionValue.Equals("Jednako")) cond = DBManager.Condition.equal;
                        else cond = DBManager.Condition.none;
                        
                        dictFields.TryGetValue(cbFieldName.SelectedIndex, out itemfieldName);
                        string databaseFieldName = (cond == DBManager.Condition.none ? "" : OSUtil.columnNamesToTableMapping[itemfieldName]);
                        FillGridView(DBManager.GetAllWithFilter(databaseFieldName, cond, inputSearchValue.Text.Trim()));
                    }
                    else
                    {
                        FillGridView(DBManager.GetAllWithFilter("", DBManager.Condition.none, inputSearchValue.Text.Trim()));
                    }


                }
            }
            else
            {
                FillGridView(DBManager.GetAllWithFilter("", DBManager.Condition.none, inputSearchValue.Text.Trim()));
            }

        }

        private void cbFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFieldName.SelectedIndex > 0)
            {
                int index = 1;
                string fieldName = cbFieldName.Items[cbFieldName.SelectedIndex].ToString().Trim();
                cbCondition.Items.Clear();
                cbCondition.Items.Insert(0, "-uslov za pretragu-");
                if (cbCondition.Items.Count > 0)
                    cbCondition.SelectedIndex = 0;
                if (fieldName.ToLower().StartsWith("datum"))
                {
                    inputDatumZaPretragu.Visible = true;
                    inputSearchValue.Visible = false;

                    cbCondition.Items.Insert(1, "Vece");
                    cbCondition.Items.Insert(2, "Manje");
                    cbCondition.Items.Insert(3, "Jednako");
                }
                else
                {

                    inputDatumZaPretragu.Visible = false;
                    inputSearchValue.Visible = true;

                    PropertyInfo prop = typeof(OSItem).GetProperty(OSUtil.columnNames.Where(c => c.Value == fieldName).Select(pair => pair.Key).ToArray()[0], BindingFlags.Public | BindingFlags.Instance);

                    if (prop.PropertyType == typeof(string))
                    {
                        cbCondition.Items.Insert(1, "Sadrzi");
                        cbCondition.Items.Insert(2, "Jednako");
                    }
                    else
                    {
                        cbCondition.Items.Insert(1, "Vece");
                        cbCondition.Items.Insert(2, "Manje");
                        cbCondition.Items.Insert(3, "Jednako");
                    }
                }
            }
            else
            {
                cbCondition.Items.Clear();
                cbCondition.Items.Insert(0, "-uslov za pretragu-");
                if (cbCondition.Items.Count > 0)
                    cbCondition.SelectedIndex = 0;
            }
        }
    }
}
