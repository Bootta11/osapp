namespace OsnovnaSredstva
{
    partial class Podesavanja
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
            this.tblOptions = new System.Windows.Forms.TableLayoutPanel();
            this.tblOption1 = new System.Windows.Forms.TableLayoutPanel();
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSnimiti = new System.Windows.Forms.Button();
            this.btnZatvoriti = new System.Windows.Forms.Button();
            this.tblOption2 = new System.Windows.Forms.TableLayoutPanel();
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tblOptions.SuspendLayout();
            this.tblOption1.SuspendLayout();
            this.tblOption2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblOptions
            // 
            this.tblOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblOptions.ColumnCount = 1;
            this.tblOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.59282F));
            this.tblOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.40718F));
            this.tblOptions.Controls.Add(this.tblOption2, 0, 1);
            this.tblOptions.Controls.Add(this.tblOption1, 0, 0);
            this.tblOptions.Location = new System.Drawing.Point(12, 12);
            this.tblOptions.Name = "tblOptions";
            this.tblOptions.RowCount = 2;
            this.tblOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOptions.Size = new System.Drawing.Size(338, 80);
            this.tblOptions.TabIndex = 0;
            // 
            // tblOption1
            // 
            this.tblOption1.ColumnCount = 2;
            this.tblOption1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.32927F));
            this.tblOption1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.67073F));
            this.tblOption1.Controls.Add(this.inputOmogucitiAutomatskoZavrsavanjeRijeci, 0, 0);
            this.tblOption1.Controls.Add(this.label1, 0, 0);
            this.tblOption1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOption1.Location = new System.Drawing.Point(3, 3);
            this.tblOption1.Name = "tblOption1";
            this.tblOption1.RowCount = 1;
            this.tblOption1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOption1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOption1.Size = new System.Drawing.Size(332, 34);
            this.tblOption1.TabIndex = 0;
            // 
            // inputOmogucitiAutomatskoZavrsavanjeRijeci
            // 
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.AutoSize = true;
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.Location = new System.Drawing.Point(306, 10);
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.Name = "inputOmogucitiAutomatskoZavrsavanjeRijeci";
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.Size = new System.Drawing.Size(15, 14);
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.TabIndex = 4;
            this.inputOmogucitiAutomatskoZavrsavanjeRijeci.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Omogućiti automatsko završavanje riječi pri unosu: ";
            // 
            // btnSnimiti
            // 
            this.btnSnimiti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSnimiti.Location = new System.Drawing.Point(12, 103);
            this.btnSnimiti.Name = "btnSnimiti";
            this.btnSnimiti.Size = new System.Drawing.Size(120, 23);
            this.btnSnimiti.TabIndex = 1;
            this.btnSnimiti.Text = "Snimiti podešavanja";
            this.btnSnimiti.UseVisualStyleBackColor = true;
            this.btnSnimiti.Click += new System.EventHandler(this.btnSnimiti_Click);
            // 
            // btnZatvoriti
            // 
            this.btnZatvoriti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZatvoriti.Location = new System.Drawing.Point(138, 103);
            this.btnZatvoriti.Name = "btnZatvoriti";
            this.btnZatvoriti.Size = new System.Drawing.Size(75, 23);
            this.btnZatvoriti.TabIndex = 2;
            this.btnZatvoriti.Text = "Zatvoriti";
            this.btnZatvoriti.UseVisualStyleBackColor = true;
            this.btnZatvoriti.Click += new System.EventHandler(this.btnZatvoriti_Click);
            // 
            // tblOption2
            // 
            this.tblOption2.ColumnCount = 2;
            this.tblOption2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.32927F));
            this.tblOption2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.67073F));
            this.tblOption2.Controls.Add(this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa, 0, 0);
            this.tblOption2.Controls.Add(this.label2, 0, 0);
            this.tblOption2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOption2.Location = new System.Drawing.Point(3, 43);
            this.tblOption2.Name = "tblOption2";
            this.tblOption2.RowCount = 1;
            this.tblOption2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOption2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOption2.Size = new System.Drawing.Size(332, 34);
            this.tblOption2.TabIndex = 1;
            // 
            // inputObrisatiUnoseNakonUspjesnoUnijetogUnosa
            // 
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.AutoSize = true;
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.Location = new System.Drawing.Point(306, 10);
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.Name = "inputObrisatiUnoseNakonUspjesnoUnijetogUnosa";
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.Size = new System.Drawing.Size(15, 14);
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.TabIndex = 4;
            this.inputObrisatiUnoseNakonUspjesnoUnijetogUnosa.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Obrisati unose nako uspješno sačuvanog unosa:";
            // 
            // Podesavanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 134);
            this.Controls.Add(this.btnZatvoriti);
            this.Controls.Add(this.btnSnimiti);
            this.Controls.Add(this.tblOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Podesavanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Podesavanja";
            this.Shown += new System.EventHandler(this.Podesavanja_Shown);
            this.tblOptions.ResumeLayout(false);
            this.tblOption1.ResumeLayout(false);
            this.tblOption1.PerformLayout();
            this.tblOption2.ResumeLayout(false);
            this.tblOption2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblOptions;
        private System.Windows.Forms.Button btnSnimiti;
        private System.Windows.Forms.Button btnZatvoriti;
        private System.Windows.Forms.TableLayoutPanel tblOption1;
        private System.Windows.Forms.CheckBox inputOmogucitiAutomatskoZavrsavanjeRijeci;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblOption2;
        private System.Windows.Forms.CheckBox inputObrisatiUnoseNakonUspjesnoUnijetogUnosa;
        private System.Windows.Forms.Label label2;
    }
}