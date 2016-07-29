﻿namespace OsnovnaSredstva
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.dgvPregled = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPregled)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.dtAmortizacije);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.brnKreirajCSV);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvPregled);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(904, 464);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDodajFilter);
            this.groupBox1.Controls.Add(this.btnIzbrisiFiltere);
            this.groupBox1.Controls.Add(this.cbActiveFilters);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.inputDatumZaPretragu);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.inputSearchValue);
            this.groupBox1.Controls.Add(this.cbCondition);
            this.groupBox1.Controls.Add(this.cbFieldName);
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
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
            this.btnIzbrisiFiltere.Location = new System.Drawing.Point(366, 44);
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
            this.dtAmortizacije.Location = new System.Drawing.Point(105, 6);
            this.dtAmortizacije.Name = "dtAmortizacije";
            this.dtAmortizacije.Size = new System.Drawing.Size(98, 20);
            this.dtAmortizacije.TabIndex = 12;
            this.dtAmortizacije.ValueChanged += new System.EventHandler(this.dtAmortizacije_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Datum amortizacije";
            // 
            // brnKreirajCSV
            // 
            this.brnKreirajCSV.Location = new System.Drawing.Point(290, 4);
            this.brnKreirajCSV.Name = "brnKreirajCSV";
            this.brnKreirajCSV.Size = new System.Drawing.Size(75, 23);
            this.brnKreirajCSV.TabIndex = 10;
            this.brnKreirajCSV.Text = "Kreiraj CSV";
            this.brnKreirajCSV.UseVisualStyleBackColor = true;
            this.brnKreirajCSV.Click += new System.EventHandler(this.brnKreirajCSV_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(209, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvPregled
            // 
            this.dgvPregled.AllowUserToOrderColumns = true;
            this.dgvPregled.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPregled.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPregled.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPregled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPregled.Location = new System.Drawing.Point(0, 0);
            this.dgvPregled.Name = "dgvPregled";
            this.dgvPregled.Size = new System.Drawing.Size(904, 346);
            this.dgvPregled.TabIndex = 1;
            this.dgvPregled.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPregled_CellValueChanged);
            this.dgvPregled.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvPregled_UserDeletingRow);
            // 
            // PregledForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 464);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PregledForm";
            this.Text = "PregledForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPregled)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvPregled;
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
    }
}