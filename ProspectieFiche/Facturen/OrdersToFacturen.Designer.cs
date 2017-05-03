namespace ProspectieFiche
{
    partial class OrdersToFacturen
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirma = new System.Windows.Forms.TextBox();
            this.txtBTWnummer = new System.Windows.Forms.TextBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.txtFactuurnr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnMaken = new System.Windows.Forms.Button();
            this.btnAnnuleren = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Firma";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BTW nummer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Datum";
            // 
            // txtFirma
            // 
            this.txtFirma.Location = new System.Drawing.Point(119, 10);
            this.txtFirma.Name = "txtFirma";
            this.txtFirma.ReadOnly = true;
            this.txtFirma.Size = new System.Drawing.Size(200, 20);
            this.txtFirma.TabIndex = 3;
            // 
            // txtBTWnummer
            // 
            this.txtBTWnummer.Location = new System.Drawing.Point(119, 38);
            this.txtBTWnummer.Name = "txtBTWnummer";
            this.txtBTWnummer.Size = new System.Drawing.Size(200, 20);
            this.txtBTWnummer.TabIndex = 4;
            // 
            // dtpDatum
            // 
            this.dtpDatum.Location = new System.Drawing.Point(119, 64);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(200, 20);
            this.dtpDatum.TabIndex = 6;
            // 
            // txtFactuurnr
            // 
            this.txtFactuurnr.Location = new System.Drawing.Point(119, 90);
            this.txtFactuurnr.Name = "txtFactuurnr";
            this.txtFactuurnr.Size = new System.Drawing.Size(200, 20);
            this.txtFactuurnr.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Factuur nummer";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 124);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Transportkosten";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(16, 147);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(76, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "Km heffing";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // btnMaken
            // 
            this.btnMaken.Location = new System.Drawing.Point(16, 184);
            this.btnMaken.Name = "btnMaken";
            this.btnMaken.Size = new System.Drawing.Size(135, 29);
            this.btnMaken.TabIndex = 11;
            this.btnMaken.Text = "Factuur maken";
            this.btnMaken.UseVisualStyleBackColor = true;
            this.btnMaken.Click += new System.EventHandler(this.btnMaken_Click);
            // 
            // btnAnnuleren
            // 
            this.btnAnnuleren.Location = new System.Drawing.Point(157, 184);
            this.btnAnnuleren.Name = "btnAnnuleren";
            this.btnAnnuleren.Size = new System.Drawing.Size(135, 29);
            this.btnAnnuleren.TabIndex = 12;
            this.btnAnnuleren.Text = "Annuleren";
            this.btnAnnuleren.UseVisualStyleBackColor = true;
            this.btnAnnuleren.Click += new System.EventHandler(this.btnAnnuleren_Click);
            // 
            // OrdersToFacturen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 226);
            this.Controls.Add(this.btnAnnuleren);
            this.Controls.Add(this.btnMaken);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtFactuurnr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.txtBTWnummer);
            this.Controls.Add(this.txtFirma);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OrdersToFacturen";
            this.Text = "OrdersToFacturen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirma;
        private System.Windows.Forms.TextBox txtBTWnummer;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.TextBox txtFactuurnr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnMaken;
        private System.Windows.Forms.Button btnAnnuleren;
    }
}