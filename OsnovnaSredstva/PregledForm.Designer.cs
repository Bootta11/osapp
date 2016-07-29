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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inputSearchValue = new System.Windows.Forms.TextBox();
            this.cbCondition = new System.Windows.Forms.ComboBox();
            this.cbFieldName = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvPregled = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.inputDatumZaPretragu = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPregled)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inputDatumZaPretragu);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.inputSearchValue);
            this.splitContainer1.Panel1.Controls.Add(this.cbCondition);
            this.splitContainer1.Panel1.Controls.Add(this.cbFieldName);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvPregled);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(894, 431);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 0;
            // 
            // inputSearchValue
            // 
            this.inputSearchValue.Location = new System.Drawing.Point(266, 53);
            this.inputSearchValue.Name = "inputSearchValue";
            this.inputSearchValue.Size = new System.Drawing.Size(100, 20);
            this.inputSearchValue.TabIndex = 3;
            // 
            // cbCondition
            // 
            this.cbCondition.FormattingEnabled = true;
            this.cbCondition.Location = new System.Drawing.Point(139, 53);
            this.cbCondition.Name = "cbCondition";
            this.cbCondition.Size = new System.Drawing.Size(121, 21);
            this.cbCondition.TabIndex = 2;
            // 
            // cbFieldName
            // 
            this.cbFieldName.FormattingEnabled = true;
            this.cbFieldName.Location = new System.Drawing.Point(12, 53);
            this.cbFieldName.Name = "cbFieldName";
            this.cbFieldName.Size = new System.Drawing.Size(121, 21);
            this.cbFieldName.TabIndex = 1;
            this.cbFieldName.SelectedIndexChanged += new System.EventHandler(this.cbFieldName_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(12, 12);
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
            this.dgvPregled.Size = new System.Drawing.Size(894, 350);
            this.dgvPregled.TabIndex = 1;
            this.dgvPregled.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPregled_CellValueChanged);
            this.dgvPregled.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvPregled_UserDeletingRow);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(372, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Pretrazi";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // inputDatumZaPretragu
            // 
            this.inputDatumZaPretragu.CustomFormat = "dd.MM.yyyy.";
            this.inputDatumZaPretragu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inputDatumZaPretragu.Location = new System.Drawing.Point(266, 54);
            this.inputDatumZaPretragu.Name = "inputDatumZaPretragu";
            this.inputDatumZaPretragu.Size = new System.Drawing.Size(100, 20);
            this.inputDatumZaPretragu.TabIndex = 5;
            this.inputDatumZaPretragu.Visible = false;
            // 
            // PregledForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 431);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PregledForm";
            this.Text = "PregledForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPregled)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvPregled;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox inputSearchValue;
        private System.Windows.Forms.ComboBox cbCondition;
        private System.Windows.Forms.ComboBox cbFieldName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker inputDatumZaPretragu;
    }
}