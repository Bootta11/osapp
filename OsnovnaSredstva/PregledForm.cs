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
using NPOI.HSSF.Model; // InternalWorkbook
using NPOI.HSSF.UserModel; // HSSFWorkbook, HSSFSheet
using System.IO;

namespace OsnovnaSredstva
{
    public partial class PregledForm : Form
    {
        static int itemPrintNum = 0;
        List<OSItem> changedItems = new List<OSItem>();
        static ListWithFieldMaxLengths itemsForList = new ListWithFieldMaxLengths();
        Dictionary<int, string> dictFields = new Dictionary<int, string>();
        List<DBManager.FieldConditionValue> fcvlist = new List<DBManager.FieldConditionValue>();
        Guid lastSearchFcvListID, fcvListId;
        Font font = new Font("Arial", 8);
        bool notPrintPreviewed, notPrinted;

        public PregledForm()
        {
            InitializeComponent();



            itemsForList = DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(DateTime.Now);
            font = new Font("Courier New", 8);



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
            lastSearchFcvListID = Guid.NewGuid();
            fcvListId = lastSearchFcvListID;
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
                    else if (props.ElementAt(i).Name.StartsWith("vrijednostNaDatum"))
                    {
                        if (props.ElementAt(i).GetValue(item, null).ToString() == "-1")
                            row.Add("");
                        else
                            row.Add(props.ElementAt(i).GetValue(item, null).ToString());
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
            try
            {
                PrintDialog pd = new PrintDialog();
                PrintDocument pdoc = new PrintDocument();

                PrinterSettings ps = new PrinterSettings();

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
                    PrintDocument pdocDuplicate = pdoc;

                    PrintPreviewDialog prv = new PrintPreviewDialog();

                    prv.Document = pdoc;

                    res = prv.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        pdocDuplicate.Print();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            PrintDocument doc = (PrintDocument)sender;
            doc.InitializeLifetimeService();
            Graphics graphics = e.Graphics;
            //Font font = new Font("Courier New", 10);
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
            string[] columns = { "#", "inventurniBroj", "brojPoNabavci", "naziv", "kolicina", "datumNabavke", "datumAmortizacije", "nabavnaVrijednost", "ispravkaVrijednosti", "sadasnjaVrijednost", "konto" };

            SizeF maxStringSize2 = new SizeF();
            maxStringSize2 = e.Graphics.MeasureString("Bootta11", font);

            Dictionary<string, int> lengthsList = new Dictionary<string, int>()
            {
                {"#", 41 },
                {"inventurniBroj", 114 },
                {"brojPoNabavci", 73 },
                {"naziv", 120 },
                {"kolicina", 65 },
                {"datumNabavke", 93 },
                {"datumAmortizacije", 135 },
                { "vrijednostNaDatumAmortizacije", 135 },
                {"nabavnaVrijednost", 128 },
                {"ispravkaVrijednosti", 142 },
                {"sadasnjaVrijednost", 135 },
                {"konto", 60 },
            };
            int rowHeightCorrectionFactor = 14;

            foreach (string cname in columns)
            {
                SizeF maxStringSize = new SizeF();

                if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], font); maxStringSize.Width += 2; }
                else maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], font);

                Console.WriteLine("{\" " + cname + "\", " + maxStringSize.Width + " },");


            }

            foreach (string cname in columns)
            {
                SizeF maxStringSize = new SizeF();
                if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], font); maxStringSize.Width += 2; }
                else maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], font);

                graphics.DrawRectangle(Pens.Black, offsetX, offsetY, lengthsList[cname], dgvPregled.Rows[0].Height);
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(offsetX + 1, offsetY + 1, (int)lengthsList[cname] - 1, dgvPregled.Rows[0].Height));
                RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)lengthsList[cname], (float)dgvPregled.Rows[0].Height);
                StringFormat stringDrawFormat = new StringFormat();
                stringDrawFormat.Alignment = StringAlignment.Center;
                stringDrawFormat.LineAlignment = StringAlignment.Center;

                graphics.DrawString(OSUtil.columnNames[cname], font, Brushes.Black, rectf, stringDrawFormat);

                offsetX += (int)lengthsList[cname];
            }

            StringFormat stringDrawFormat2 = new StringFormat();
            stringDrawFormat2.Alignment = StringAlignment.Center;
            stringDrawFormat2.LineAlignment = StringAlignment.Center;

            offsetY += dgvPregled.Rows[0].Height;

            int a = itemsForList.items.Count;
            int c = dgvPregled.Columns.Count;
            Type ositemType = typeof(OSItem);
            int rb = 1;

            while (itemPrintNum < a)
            {
                rowHeightCorrectionFactor = 14;
                offsetX = startX;
                if ((offsetY) > (e.PageBounds.Height - startY - 10) && (notPrintPreviewed || notPrinted))
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

                //Calculate row height correction factor
                foreach (string cname in columns)
                {
                    SizeF maxStringSize = new SizeF();
                    PropertyInfo prop = ositemType.GetProperty(cname);


                    if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], font); maxStringSize.Width += 2; }
                    else if (cname.StartsWith("datum")) maxStringSize = e.Graphics.MeasureString(DateTime.ParseExact(prop.GetValue(itemsForList.items[itemPrintNum]).ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy") + "", font);
                    else maxStringSize = e.Graphics.MeasureString(prop.GetValue(itemsForList.items[itemPrintNum]).ToString(), font);

                    if ((maxStringSize.Width - lengthsList[cname]) > 2)
                    {
                        rowHeightCorrectionFactor = (int)(rowHeightCorrectionFactor + ((int)(maxStringSize.Width / lengthsList[cname])) * rowHeightCorrectionFactor);
                    }
                }

                foreach (string cname in columns)
                {
                    PropertyInfo prop = ositemType.GetProperty(cname);
                    SizeF maxStringSize = new SizeF();

                    if (cname.Equals("#")) { maxStringSize = e.Graphics.MeasureString((itemsForList.items.Count).ToString().Length > OSUtil.columnNames[cname].Length ? (itemsForList.items.Count).ToString() : OSUtil.columnNames[cname], font); maxStringSize.Width += 2; }
                    else maxStringSize = e.Graphics.MeasureString(itemsForList.fieldMaxLength[cname], font);

                    //graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawRectangle(Pens.Black, offsetX, offsetY, lengthsList[cname], rowHeightCorrectionFactor);
                    //graphics.FillRectangle(Brushes.White, new Rectangle(offsetX + 1, offsetY + 1,(int)maxStringSize.Width - 1, dgvPregled.Rows[i].Height));

                    graphics.FillRectangle(Brushes.White, new Rectangle(offsetX + 1, offsetY + 1, (int)lengthsList[cname] - 1, rowHeightCorrectionFactor - 2));
                    RectangleF rectf = new RectangleF((float)offsetX, ((float)(offsetY)), (float)lengthsList[cname], (float)rowHeightCorrectionFactor);
                    StringFormat stringDrawFormat = new StringFormat();
                    stringDrawFormat.Alignment = StringAlignment.Center;
                    stringDrawFormat.LineAlignment = StringAlignment.Center;
                    //Convert.ToString(dataGridView1.Rows[i].Cells[k].Value);
                    if (cname.Equals("#"))
                    {
                        graphics.DrawString((itemPrintNum + 1) + "", font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    else if (cname.StartsWith("datum"))
                    {
                        graphics.DrawString(DateTime.ParseExact(prop.GetValue(itemsForList.items[itemPrintNum]).ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy") + "", font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    else
                    {
                        graphics.DrawString(prop.GetValue(itemsForList.items[itemPrintNum]) + "", font, Brushes.Black, rectf, stringDrawFormat);
                    }
                    //graphics.DrawString()

                    //graphics.DrawString("\t" + Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    //offsetX += dgvPregled.Columns[cname].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells,true);
                    offsetX += (int)lengthsList[cname];
                }

                offsetY += rowHeightCorrectionFactor;
                itemPrintNum++;
            }
            if (itemPrintNum >= a)
            {
                itemPrintNum = 0;
                if (notPrintPreviewed == false)
                    notPrintPreviewed = false;
                if (notPrintPreviewed)
                {
                    notPrintPreviewed = false;

                }
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            itemPrintNum = 0;
            notPrintPreviewed = true;
            notPrinted = true;
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
            {
                DBManager.UpdateItem(item);
                if (fcvlist.Count > 0)
                    FillGridView(DBManager.GetAllWithFilter(fcvlist, dtAmortizacije.Value));
                else
                    FillGridView(DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(dtAmortizacije.Value));
            }
        }

        private void dgvPregled_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Console.WriteLine(e.Row.Cells["id"].Value);

            DBManager.deleteOS((string)e.Row.Cells["id"].Value);

            if (fcvlist.Count > 0)
                FillGridView(DBManager.GetAllWithFilter(fcvlist, dtAmortizacije.Value));
            else
                FillGridView(DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(dtAmortizacije.Value));
        }







        private void brnKreirajCSV_Click(object sender, EventArgs e)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"items.csv", false);
            System.IO.StreamWriter file;
            HSSFWorkbook wb;
            HSSFSheet sh;

            //System.Web.HttpContext.Current.Response.Write("Some Text");
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Comma separated values|*.csv";
            saveFileDialog1.Title = "Save an CSV File";
            saveFileDialog1.DefaultExt = "csv";
            DialogResult? result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Console.WriteLine(saveFileDialog1.);
                System.IO.Stream dlgstream;
                if ((dlgstream = saveFileDialog1.OpenFile()) != null)
                {

                    file = new System.IO.StreamWriter(dlgstream);

                    //System.IO.StreamWriter file = new System.IO.StreamWriter(Response.OutputStream, Encoding.UTF8);

                    //Console.WriteLine(OSUtil.ispravkaVrijednosti(double.Parse(inputNabavnaVrednost.Text), DateTime.ParseExact("2017-05-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), double.Parse(inputStopaAmortizacije.Text)));
                    List<OSItem> itemsForListCSV = itemsForList.items;



                    Type myType = typeof(OSItem);
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    Console.WriteLine(props.Count);
                    //dgvPregled.ColumnCount = props.Count;
                    for (int i = 0; i < props.Count; i++)
                    {
                        //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                        //object propValue = prop.GetValue(myObject, null);
                        //dgvPregled.Columns[i].Name = props.ElementAt(i).Name;
                        //file.Write( props.ElementAt(i).Name + (i < props.Count - 1 ? ";" : Environment.NewLine));
                        file.Write(OSUtil.columnNames[props.ElementAt(i).Name] + (i < props.Count - 1 ? ";" : Environment.NewLine));
                        // Do something with propValue
                    }

                    foreach (OSItem item in itemsForListCSV)
                    {
                        List<string> row = new List<string>();
                        for (int i = 0; i < props.Count; i++)
                        {
                            if (props.ElementAt(i).Name.StartsWith("datum"))
                            {
                                //row.Add(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy."));
                                file.Write(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy.") + (i < props.Count - 1 ? ";" : Environment.NewLine));
                            }
                            else
                            {
                                //row.Add(props.ElementAt(i).GetValue(item, null).ToString());
                                file.Write(props.ElementAt(i).GetValue(item, null).ToString() + (i < props.Count - 1 ? ";" : Environment.NewLine));
                            }
                            //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                            //object propValue = prop.GetValue(myObject, null);

                            // Do something with propValue
                        }
                        //dgvPregled.Rows.Add(row.ToArray());
                    }
                    file.Close();
                }
            }
        }

        private void cbFieldName_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void btnDodajFilter_Click_1(object sender, EventArgs e)
        {
            DBManager.FieldConditionValue fcv = new DBManager.FieldConditionValue();
            string itemfieldName = "";
            string selectedItem = "";

            if (cbFieldName.SelectedIndex > 0)
            {
                if (cbCondition.SelectedIndex > 0)
                {
                    selectedItem = cbFieldName.Items[cbFieldName.SelectedIndex].ToString().Trim();
                    if (selectedItem.ToLower().StartsWith("datum"))
                    {

                        DBManager.Condition cond = DBManager.Condition.contain;
                        string conditionValue = cbCondition.Items[cbCondition.SelectedIndex].ToString();
                        if (conditionValue.Equals("Manje")) cond = DBManager.Condition.lower;
                        else if (conditionValue.Equals("Vece")) cond = DBManager.Condition.greater;
                        else if (conditionValue.Equals("Jednako")) cond = DBManager.Condition.equal;
                        else cond = DBManager.Condition.none;

                        dictFields.TryGetValue(cbFieldName.SelectedIndex, out itemfieldName);
                        string databaseFieldName = (cond == DBManager.Condition.none ? "" : OSUtil.columnNamesToTableMapping[itemfieldName]);
                        //FillGridView(DBManager.GetAllWithFilter(databaseFieldName, cond, inputDatumZaPretragu.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                        fcv.condition = cond;
                        fcv.field = databaseFieldName;
                        fcv.value = inputDatumZaPretragu.Value.ToString("yyyy-MM-dd");


                    }
                    else
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
                        //FillGridView(DBManager.GetAllWithFilter(databaseFieldName, cond, inputSearchValue.Text.Trim()));
                        fcv.condition = cond;
                        fcv.field = databaseFieldName;
                        fcv.value = inputSearchValue.Text.Trim();




                    }

                    cbActiveFilters.Enabled = true;

                    cbActiveFilters.Items.Clear();
                    fcvlist.Add(fcv);

                    for (int i = 0; i < fcvlist.Count; i++)
                    {
                        string condName = "NN";
                        if (fcv.condition == DBManager.Condition.contain)
                        {
                            condName = "Sadrži";
                        }
                        else if (fcv.condition == DBManager.Condition.lower)
                        {
                            condName = "Manje";
                        }
                        else if (fcv.condition == DBManager.Condition.greater)
                        {
                            condName = "Veće";
                        }
                        else if (fcv.condition == DBManager.Condition.equal)
                        {
                            condName = "Jednako";
                        }
                        cbActiveFilters.Items.Insert(i, fcvlist[i].field + " " + condName + " " + fcvlist[i].value);
                    }
                    cbActiveFilters.SelectedIndex = cbActiveFilters.Items.Count - 1;
                    cbFieldName.SelectedIndex = 0;
                    inputSearchValue.Text = "";
                    fcvListId = Guid.NewGuid();
                }
            }
        }

        private void dtAmortizacije_ValueChanged(object sender, EventArgs e)
        {
            fcvListId = Guid.NewGuid();
        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"items.csv", false);
            System.IO.StreamWriter file;
            HSSFWorkbook wb;
            HSSFSheet sh;

            //System.Web.HttpContext.Current.Response.Write("Some Text");
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Exel file|*.xls";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.DefaultExt = "csv";
            DialogResult? result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Console.WriteLine(saveFileDialog1.);
                System.IO.Stream dlgstream;


                if ((dlgstream = saveFileDialog1.OpenFile()) != null)
                {

                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        int rowcount = 0;
                        wb = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());
                        sh = (HSSFSheet)wb.CreateSheet("Lista amortizacije za datum " + dtAmortizacije.Value.ToString("dd.MM.yyyy"));
                        //file = new System.IO.StreamWriter(dlgstream);

                        //System.IO.StreamWriter file = new System.IO.StreamWriter(Response.OutputStream, Encoding.UTF8);

                        //Console.WriteLine(OSUtil.ispravkaVrijednosti(double.Parse(inputNabavnaVrednost.Text), DateTime.ParseExact("2017-05-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(inputDatumAmortizacije.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), double.Parse(inputStopaAmortizacije.Text)));
                        List<OSItem> itemsForListCSV = itemsForList.items;



                        Type myType = typeof(OSItem);
                        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                        Console.WriteLine(props.Count);
                        //dgvPregled.ColumnCount = props.Count;
                        NPOI.SS.UserModel.IRow row = sh.CreateRow(rowcount);
                        for (int i = 0; i < props.Count; i++)
                        {

                            //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                            //object propValue = prop.GetValue(myObject, null);
                            //dgvPregled.Columns[i].Name = props.ElementAt(i).Name;
                            //file.Write( props.ElementAt(i).Name + (i < props.Count - 1 ? ";" : Environment.NewLine));
                            //file.Write(OSUtil.columnNames[props.ElementAt(i).Name] + (i < props.Count - 1 ? ";" : Environment.NewLine));
                            // Do something with propValue
                            var cell = row.CreateCell(i);
                            cell.SetCellValue(OSUtil.columnNames[props.ElementAt(i).Name]);
                        }
                        rowcount++;
                        foreach (OSItem item in itemsForListCSV)
                        {
                            row = sh.CreateRow(rowcount);
                            for (int i = 0; i < props.Count; i++)
                            {
                                var cell = row.CreateCell(i);
                                if (props.ElementAt(i).Name.StartsWith("datum"))
                                {
                                    //row.Add(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy."));
                                    //file.Write(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy.") + (i < props.Count - 1 ? ";" : Environment.NewLine));
                                    cell.SetCellValue(DateTime.ParseExact(props.ElementAt(i).GetValue(item, null).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy."));
                                }
                                else
                                {
                                    //row.Add(props.ElementAt(i).GetValue(item, null).ToString());
                                    //file.Write(props.ElementAt(i).GetValue(item, null).ToString() + (i < props.Count - 1 ? ";" : Environment.NewLine));
                                    cell.SetCellValue(props.ElementAt(i).GetValue(item, null).ToString());
                                }
                                //Console.WriteLine(prop.Name + " = " + prop.GetValue(itemsForList[0], null));
                                //object propValue = prop.GetValue(myObject, null);

                                // Do something with propValue
                            }
                            //dgvPregled.Rows.Add(row.ToArray());
                            rowcount++;
                        }
                        wb.Write(dlgstream);
                        dlgstream.Close();
                    }
                }
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (fcvListId != lastSearchFcvListID)
            {
                if (fcvlist.Count > 0)
                    FillGridView(DBManager.GetAllWithFilter(fcvlist, dtAmortizacije.Value));
                else
                    FillGridView(DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(dtAmortizacije.Value));

                lastSearchFcvListID = fcvListId;
            }
        }

        private void btnIzbrisiFiltere_Click_1(object sender, EventArgs e)
        {
            fcvlist.Clear();
            cbActiveFilters.Items.Clear();
            cbActiveFilters.Enabled = false;
            FillGridView(DBManager.GetAllSaIspravkaVrijednostiISadasnjaVrijednost(dtAmortizacije.Value));

            lastSearchFcvListID = fcvListId;
        }


    }
}
