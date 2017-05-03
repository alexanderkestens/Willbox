namespace ProspectieFiche
{
    partial class Contact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contact));
            this.txtCommentaar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContactPersoon = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnVerstuur = new System.Windows.Forms.Button();
            this.cbDuurGesprek = new System.Windows.Forms.ComboBox();
            this.cbTypeGesprek = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTerugcontacteren = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.rbYes = new System.Windows.Forms.RadioButton();
            this.cbContacterenVia = new System.Windows.Forms.ComboBox();
            this.lblVia = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCommentaar
            // 
            this.txtCommentaar.Location = new System.Drawing.Point(110, 156);
            this.txtCommentaar.Multiline = true;
            this.txtCommentaar.Name = "txtCommentaar";
            this.txtCommentaar.Size = new System.Drawing.Size(298, 84);
            this.txtCommentaar.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Commentaar";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "Duur gesprek";
            // 
            // txtContactPersoon
            // 
            this.txtContactPersoon.Location = new System.Drawing.Point(110, 11);
            this.txtContactPersoon.Name = "txtContactPersoon";
            this.txtContactPersoon.Size = new System.Drawing.Size(157, 20);
            this.txtContactPersoon.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "ContactPersoon";
            // 
            // btnVerstuur
            // 
            this.btnVerstuur.Location = new System.Drawing.Point(110, 247);
            this.btnVerstuur.Name = "btnVerstuur";
            this.btnVerstuur.Size = new System.Drawing.Size(99, 25);
            this.btnVerstuur.TabIndex = 8;
            this.btnVerstuur.Text = "Verstuur";
            this.btnVerstuur.UseVisualStyleBackColor = true;
            this.btnVerstuur.Click += new System.EventHandler(this.btnVerstuur_Click);
            // 
            // cbDuurGesprek
            // 
            this.cbDuurGesprek.FormattingEnabled = true;
            this.cbDuurGesprek.Items.AddRange(new object[] {
            "< 10 min",
            "10 - 30 min",
            "> 30 min"});
            this.cbDuurGesprek.Location = new System.Drawing.Point(110, 38);
            this.cbDuurGesprek.Name = "cbDuurGesprek";
            this.cbDuurGesprek.Size = new System.Drawing.Size(121, 21);
            this.cbDuurGesprek.TabIndex = 2;
            this.cbDuurGesprek.Text = "< 10 min";
            // 
            // cbTypeGesprek
            // 
            this.cbTypeGesprek.FormattingEnabled = true;
            this.cbTypeGesprek.Items.AddRange(new object[] {
            "Telefonisch",
            "Email",
            "Persoonlijk"});
            this.cbTypeGesprek.Location = new System.Drawing.Point(110, 65);
            this.cbTypeGesprek.Name = "cbTypeGesprek";
            this.cbTypeGesprek.Size = new System.Drawing.Size(121, 21);
            this.cbTypeGesprek.TabIndex = 3;
            this.cbTypeGesprek.Text = "Telefonisch";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Type gesprek";
            // 
            // dtpTerugcontacteren
            // 
            this.dtpTerugcontacteren.Location = new System.Drawing.Point(211, 92);
            this.dtpTerugcontacteren.Name = "dtpTerugcontacteren";
            this.dtpTerugcontacteren.Size = new System.Drawing.Size(196, 20);
            this.dtpTerugcontacteren.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Terug contacteren";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbNo);
            this.panel1.Controls.Add(this.rbYes);
            this.panel1.Location = new System.Drawing.Point(114, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(78, 23);
            this.panel1.TabIndex = 57;
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(37, 3);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(33, 17);
            this.rbNo.TabIndex = 7;
            this.rbNo.Text = "N";
            this.rbNo.UseVisualStyleBackColor = true;
            this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Checked = true;
            this.rbYes.Location = new System.Drawing.Point(3, 3);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(32, 17);
            this.rbYes.TabIndex = 6;
            this.rbYes.TabStop = true;
            this.rbYes.Text = "Y";
            this.rbYes.UseVisualStyleBackColor = true;
            this.rbYes.CheckedChanged += new System.EventHandler(this.rbYes_CheckedChanged);
            // 
            // cbContacterenVia
            // 
            this.cbContacterenVia.FormattingEnabled = true;
            this.cbContacterenVia.Items.AddRange(new object[] {
            "Telefonisch",
            "Email",
            "Afspraak"});
            this.cbContacterenVia.Location = new System.Drawing.Point(142, 119);
            this.cbContacterenVia.Name = "cbContacterenVia";
            this.cbContacterenVia.Size = new System.Drawing.Size(121, 21);
            this.cbContacterenVia.TabIndex = 58;
            this.cbContacterenVia.Text = "Telefonisch";
            // 
            // lblVia
            // 
            this.lblVia.AutoSize = true;
            this.lblVia.Location = new System.Drawing.Point(114, 122);
            this.lblVia.Name = "lblVia";
            this.lblVia.Size = new System.Drawing.Size(22, 13);
            this.lblVia.TabIndex = 59;
            this.lblVia.Text = "Via";
            // 
            // Contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 280);
            this.Controls.Add(this.cbContacterenVia);
            this.Controls.Add(this.lblVia);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTerugcontacteren);
            this.Controls.Add(this.cbTypeGesprek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDuurGesprek);
            this.Controls.Add(this.btnVerstuur);
            this.Controls.Add(this.txtCommentaar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtContactPersoon);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Contact";
            this.Text = "Maak prospectie";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Contact_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCommentaar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContactPersoon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnVerstuur;
        private System.Windows.Forms.ComboBox cbDuurGesprek;
        private System.Windows.Forms.ComboBox cbTypeGesprek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTerugcontacteren;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.ComboBox cbContacterenVia;
        private System.Windows.Forms.Label lblVia;
    }
}