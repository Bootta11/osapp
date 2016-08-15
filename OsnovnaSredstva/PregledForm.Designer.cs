namespace OsnovnaSredstva
{
    partial class PregledForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PregledForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnIzbrisati = new System.Windows.Forms.Button();
            this.lblUkupnoUnosa = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSetpageSize = new System.Windows.Forms.Button();
            this.topPage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.inputPageSize = new System.Windows.Forms.TextBox();
            this.inputStrana = new System.Windows.Forms.TextBox();
            this.cbEnablePocetniDatum = new System.Windows.Forms.CheckBox();
            this.dtPocetniDatumAmortizacije = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIzmijeniti = new System.Windows.Forms.Button();
            this.btnXLS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDodajFilter = new System.Windows.Forms.Button();
            this.btnIzbrisiFiltere = new System.Windows.Forms.Button();
            this.cbActiveFilters = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputDatumZaPretragu = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.inputSearchValue = new System.Windows.Forms.TextBox();
            this.cbCondition = new System.Windows.Forms.ComboBox();
            this.cbFieldName = new System.Windows.Forms.ComboBox();
            this.dtAmortizacije = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.brnKreirajCSV = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvNew = new Syncfusion.Windows.Forms.Grid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNew)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImage = global::OsnovnaSredstva.Properties.Resources.wov;
            this.splitContainer1.Panel1.Controls.Add(this.btnIzbrisati);
            this.splitContainer1.Panel1.Controls.Add(this.lblUkupnoUnosa);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnSetpageSize);
            this.splitContainer1.Panel1.Controls.Add(this.topPage);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.btnNextPage);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrevPage);
            this.splitContainer1.Panel1.Controls.Add(this.inputPageSize);
            this.splitContainer1.Panel1.Controls.Add(this.inputStrana);
            this.splitContainer1.Panel1.Controls.Add(this.cbEnablePocetniDatum);
            this.splitContainer1.Panel1.Controls.Add(this.dtPocetniDatumAmortizacije);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnIzmijeniti);
            this.splitContainer1.Panel1.Controls.Add(this.btnXLS);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.dtAmortizacije);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.brnKreirajCSV);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvNew);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(904, 464);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // btnIzbrisati
            // 
            this.btnIzbrisati.Enabled = false;
            this.btnIzbrisati.Location = new System.Drawing.Point(92, 113);
            this.btnIzbrisati.Name = "btnIzbrisati";
            this.btnIzbrisati.Size = new System.Drawing.Size(75, 23);
            this.btnIzbrisati.TabIndex = 27;
            this.btnIzbrisati.Text = "Izbrisati";
            this.btnIzbrisati.UseVisualStyleBackColor = true;
            this.btnIzbrisati.Click += new System.EventHandler(this.btnObrisati_Click);
            // 
            // lblUkupnoUnosa
            // 
            this.lblUkupnoUnosa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUkupnoUnosa.AutoSize = true;
            this.lblUkupnoUnosa.BackColor = System.Drawing.Color.Transparent;
            this.lblUkupnoUnosa.Location = new System.Drawing.Point(772, 125);
            this.lblUkupnoUnosa.Name = "lblUkupnoUnosa";
            this.lblUkupnoUnosa.Size = new System.Drawing.Size(89, 13);
            this.lblUkupnoUnosa.TabIndex = 26;
            this.lblUkupnoUnosa.Text = "Ukupno unosa: 0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(730, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Strana";
            // 
            // btnSetpageSize
            // 
            this.btnSetpageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetpageSize.Location = new System.Drawing.Point(846, 71);
            this.btnSetpageSize.Name = "btnSetpageSize";
            this.btnSetpageSize.Size = new System.Drawing.Size(51, 23);
            this.btnSetpageSize.TabIndex = 24;
            this.btnSetpageSize.Text = "Izaberi";
            this.btnSetpageSize.UseVisualStyleBackColor = true;
            this.btnSetpageSize.Click += new System.EventHandler(this.btnSetpageSize_Click);
            // 
            // topPage
            // 
            this.topPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topPage.Enabled = false;
            this.topPage.Location = new System.Drawing.Point(839, 101);
            this.topPage.Name = "topPage";
            this.topPage.Size = new System.Drawing.Size(29, 20);
            this.topPage.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(710, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Br. redova po str";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.Location = new System.Drawing.Point(874, 99);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(24, 23);
            this.btnNextPage.TabIndex = 21;
            this.btnNextPage.Text = "<";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevPage.Location = new System.Drawing.Point(774, 99);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(24, 23);
            this.btnPrevPage.TabIndex = 20;
            this.btnPrevPage.Text = ">";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // inputPageSize
            // 
            this.inputPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputPageSize.Location = new System.Drawing.Point(801, 73);
            this.inputPageSize.Name = "inputPageSize";
            this.inputPageSize.Size = new System.Drawing.Size(39, 20);
            this.inputPageSize.TabIndex = 19;
            // 
            // inputStrana
            // 
            this.inputStrana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputStrana.Location = new System.Drawing.Point(804, 101);
            this.inputStrana.Name = "inputStrana";
            this.inputStrana.Size = new System.Drawing.Size(29, 20);
            this.inputStrana.TabIndex = 18;
            // 
            // cbEnablePocetniDatum
            // 
            this.cbEnablePocetniDatum.AutoSize = true;
            this.cbEnablePocetniDatum.BackColor = System.Drawing.Color.Transparent;
            this.cbEnablePocetniDatum.Location = new System.Drawing.Point(16, 9);
            this.cbEnablePocetniDatum.Name = "cbEnablePocetniDatum";
            this.cbEnablePocetniDatum.Size = new System.Drawing.Size(15, 14);
            this.cbEnablePocetniDatum.TabIndex = 17;
            this.cbEnablePocetniDatum.UseVisualStyleBackColor = false;
            this.cbEnablePocetniDatum.CheckedChanged += new System.EventHandler(this.cbEnablePocetniDatum_CheckedChanged);
            // 
            // dtPocetniDatumAmortizacije
            // 
            this.dtPocetniDatumAmortizacije.CustomFormat = "dd.MM.yyyy.";
            this.dtPocetniDatumAmortizacije.Enabled = false;
            this.dtPocetniDatumAmortizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPocetniDatumAmortizacije.Location = new System.Drawing.Point(172, 7);
            this.dtPocetniDatumAmortizacije.Name = "dtPocetniDatumAmortizacije";
            this.dtPocetniDatumAmortizacije.Size = new System.Drawing.Size(90, 20);
            this.dtPocetniDatumAmortizacije.TabIndex = 16;
            this.dtPocetniDatumAmortizacije.ValueChanged += new System.EventHandler(this.dtPocetniDatumAmortizacije_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(37, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Pocetni datum amortizacije";
            // 
            // btnIzmijeniti
            // 
            this.btnIzmijeniti.Enabled = false;
            this.btnIzmijeniti.Location = new System.Drawing.Point(12, 113);
            this.btnIzmijeniti.Name = "btnIzmijeniti";
            this.btnIzmijeniti.Size = new System.Drawing.Size(75, 23);
            this.btnIzmijeniti.TabIndex = 2;
            this.btnIzmijeniti.Text = "Izmijeniti";
            this.btnIzmijeniti.UseVisualStyleBackColor = true;
            this.btnIzmijeniti.Click += new System.EventHandler(this.btnIzmijeniti_Click);
            // 
            // btnXLS
            // 
            this.btnXLS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXLS.Location = new System.Drawing.Point(823, 4);
            this.btnXLS.Name = "btnXLS";
            this.btnXLS.Size = new System.Drawing.Size(75, 23);
            this.btnXLS.TabIndex = 14;
            this.btnXLS.Text = "Kreiraj Excel";
            this.btnXLS.UseVisualStyleBackColor = true;
            this.btnXLS.Click += new System.EventHandler(this.btnXLS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::OsnovnaSredstva.Properties.Resources.wov;
            this.groupBox1.Controls.Add(this.btnDodajFilter);
            this.groupBox1.Controls.Add(this.btnIzbrisiFiltere);
            this.groupBox1.Controls.Add(this.cbActiveFilters);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.inputDatumZaPretragu);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.inputSearchValue);
            this.groupBox1.Controls.Add(this.cbCondition);
            this.groupBox1.Controls.Add(this.cbFieldName);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 72);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraga";
            // 
            // btnDodajFilter
            // 
            this.btnDodajFilter.Location = new System.Drawing.Point(366, 20);
            this.btnDodajFilter.Name = "btnDodajFilter";
            this.btnDodajFilter.Size = new System.Drawing.Size(75, 23);
            this.btnDodajFilter.TabIndex = 18;
            this.btnDodajFilter.Text = "Dodaj filter";
            this.btnDodajFilter.UseVisualStyleBackColor = true;
            this.btnDodajFilter.Click += new System.EventHandler(this.btnDodajFilter_Click_1);
            // 
            // btnIzbrisiFiltere
            // 
            this.btnIzbrisiFiltere.Location = new System.Drawing.Point(366, 43);
            this.btnIzbrisiFiltere.Name = "btnIzbrisiFiltere";
            this.btnIzbrisiFiltere.Size = new System.Drawing.Size(75, 23);
            this.btnIzbrisiFiltere.TabIndex = 17;
            this.btnIzbrisiFiltere.Text = "Izbrsi filtere";
            this.btnIzbrisiFiltere.UseVisualStyleBackColor = true;
            this.btnIzbrisiFiltere.Click += new System.EventHandler(this.btnIzbrisiFiltere_Click_1);
            // 
            // cbActiveFilters
            // 
            this.cbActiveFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActiveFilters.DropDownWidth = 121;
            this.cbActiveFilters.Enabled = false;
            this.cbActiveFilters.FormattingEnabled = true;
            this.cbActiveFilters.Location = new System.Drawing.Point(133, 46);
            this.cbActiveFilters.Name = "cbActiveFilters";
            this.cbActiveFilters.Size = new System.Drawing.Size(225, 21);
            this.cbActiveFilters.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(30, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Lista aktivnih filtera";
            // 
            // inputDatumZaPretragu
            // 
            this.inputDatumZaPretragu.CustomFormat = "dd.MM.yyyy.";
            this.inputDatumZaPretragu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inputDatumZaPretragu.Location = new System.Drawing.Point(260, 20);
            this.inputDatumZaPretragu.Name = "inputDatumZaPretragu";
            this.inputDatumZaPretragu.Size = new System.Drawing.Size(100, 20);
            this.inputDatumZaPretragu.TabIndex = 14;
            this.inputDatumZaPretragu.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(447, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Pretrazi";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // inputSearchValue
            // 
            this.inputSearchValue.Location = new System.Drawing.Point(260, 20);
            this.inputSearchValue.Name = "inputSearchValue";
            this.inputSearchValue.Size = new System.Drawing.Size(100, 20);
            this.inputSearchValue.TabIndex = 12;
            // 
            // cbCondition
            // 
            this.cbCondition.FormattingEnabled = true;
            this.cbCondition.Location = new System.Drawing.Point(133, 19);
            this.cbCondition.Name = "cbCondition";
            this.cbCondition.Size = new System.Drawing.Size(121, 21);
            this.cbCondition.TabIndex = 11;
            // 
            // cbFieldName
            // 
            this.cbFieldName.FormattingEnabled = true;
            this.cbFieldName.Location = new System.Drawing.Point(6, 19);
            this.cbFieldName.Name = "cbFieldName";
            this.cbFieldName.Size = new System.Drawing.Size(121, 21);
            this.cbFieldName.TabIndex = 10;
            this.cbFieldName.SelectedIndexChanged += new System.EventHandler(this.cbFieldName_SelectedIndexChanged_1);
            // 
            // dtAmortizacije
            // 
            this.dtAmortizacije.CustomFormat = "dd.MM.yyyy.";
            this.dtAmortizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAmortizacije.Location = new System.Drawing.Point(395, 7);
            this.dtAmortizacije.Name = "dtAmortizacije";
            this.dtAmortizacije.Size = new System.Drawing.Size(90, 20);
            this.dtAmortizacije.TabIndex = 12;
            this.dtAmortizacije.ValueChanged += new System.EventHandler(this.dtAmortizacije_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(268, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Krajnji datum amortizacije";
            // 
            // brnKreirajCSV
            // 
            this.brnKreirajCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brnKreirajCSV.Location = new System.Drawing.Point(742, 4);
            this.brnKreirajCSV.Name = "brnKreirajCSV";
            this.brnKreirajCSV.Size = new System.Drawing.Size(75, 23);
            this.brnKreirajCSV.TabIndex = 10;
            this.brnKreirajCSV.Text = "Kreiraj CSV";
            this.brnKreirajCSV.UseVisualStyleBackColor = true;
            this.brnKreirajCSV.Click += new System.EventHandler(this.brnKreirajCSV_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(661, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvNew
            // 
            this.dgvNew.ColorStyles = Syncfusion.Windows.Forms.ColorStyles.Office2007Blue;
            this.dgvNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNew.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2007Blue;
            this.dgvNew.Location = new System.Drawing.Point(0, 0);
            this.dgvNew.Name = "dgvNew";
            this.dgvNew.Size = new System.Drawing.Size(904, 318);
            this.dgvNew.SmartSizeBox = false;
            this.dgvNew.TabIndex = 2;
            this.dgvNew.Text = "gridControl1";
            this.dgvNew.UseRightToLeftCompatibleTextBox = true;
            this.dgvNew.CellsChanged += new Syncfusion.Windows.Forms.Grid.GridCellsChangedEventHandler(this.dgvNew_CellsChanged);
            this.dgvNew.QueryCellInfo += new Syncfusion.Windows.Forms.Grid.GridQueryCellInfoEventHandler(this.dgvNew_QueryCellInfo);
            this.dgvNew.SelectionChanged += new Syncfusion.Windows.Forms.Grid.GridSelectionChangedEventHandler(this.dgvNew_SelectionChanged);
            this.dgvNew.CurrentCellActivated += new System.EventHandler(this.dgvNew_CurrentCellActivated);
            this.dgvNew.CurrentCellChanged += new System.EventHandler(this.dgvNew_CurrentCellChanged);
            this.dgvNew.CurrentCellAcceptedChanges += new System.ComponentModel.CancelEventHandler(this.dgvNew_CurrentCellAcceptedChanges);
            // 
            // PregledForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(904, 464);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PregledForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PregledForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtAmortizacije;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button brnKreirajCSV;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDodajFilter;
        private System.Windows.Forms.Button btnIzbrisiFiltere;
        private System.Windows.Forms.ComboBox cbActiveFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker inputDatumZaPretragu;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox inputSearchValue;
        private System.Windows.Forms.ComboBox cbCondition;
        private System.Windows.Forms.ComboBox cbFieldName;
        private System.Windows.Forms.Button btnXLS;
        private System.Windows.Forms.Button btnIzmijeniti;
        private System.Windows.Forms.CheckBox cbEnablePocetniDatum;
        private System.Windows.Forms.DateTimePicker dtPocetniDatumAmortizacije;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.TextBox inputPageSize;
        private System.Windows.Forms.TextBox inputStrana;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox topPage;
        private System.Windows.Forms.Button btnSetpageSize;
        private System.Windows.Forms.Label lblUkupnoUnosa;
        private System.Windows.Forms.Label label5;
        private Syncfusion.Windows.Forms.Grid.GridControl dgvNew;
        private System.Windows.Forms.Button btnIzbrisati;
    }
}