using System;

namespace ProspectieFiche
{
    partial class Aanmaken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aanmaken));
            this.btnMaken = new MetroFramework.Controls.MetroButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFacturen = new MetroFramework.Controls.MetroTextBox();
            this.gbBestand = new System.Windows.Forms.GroupBox();
            this.cbGemeente = new MetroFramework.Controls.MetroComboBox();
            this.txtBTWCode = new MetroFramework.Controls.MetroTextBox();
            this.cbLand = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.txtGemeente = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.txtCommentaar = new MetroFramework.Controls.MetroTextBox();
            this.txtBTW = new MetroFramework.Controls.MetroTextBox();
            this.txtWebsite = new MetroFramework.Controls.MetroTextBox();
            this.txtEmail2 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.txtEmail1 = new MetroFramework.Controls.MetroTextBox();
            this.txtTelefoon2 = new MetroFramework.Controls.MetroTextBox();
            this.txtTelefoon1 = new MetroFramework.Controls.MetroTextBox();
            this.txtPostcode = new MetroFramework.Controls.MetroTextBox();
            this.txtAdres = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtFirma = new MetroFramework.Controls.MetroTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProductie = new MetroFramework.Controls.MetroTextBox();
            this.lblError = new MetroFramework.Controls.MetroLabel();
            this.groupBox2.SuspendLayout();
            this.gbBestand.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMaken
            // 
            this.btnMaken.Location = new System.Drawing.Point(11, 418);
            this.btnMaken.Name = "btnMaken";
            this.btnMaken.Size = new System.Drawing.Size(102, 30);
            this.btnMaken.TabIndex = 86;
            this.btnMaken.Text = "Aanmaken";
            this.btnMaken.UseSelectable = true;
            this.btnMaken.Click += new System.EventHandler(this.btnMaken_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFacturen);
            this.groupBox2.Location = new System.Drawing.Point(565, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 201);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Facturen";
            // 
            // txtFacturen
            // 
            // 
            // 
            // 
            this.txtFacturen.CustomButton.Image = null;
            this.txtFacturen.CustomButton.Location = new System.Drawing.Point(58, 2);
            this.txtFacturen.CustomButton.Name = "";
            this.txtFacturen.CustomButton.Size = new System.Drawing.Size(171, 171);
            this.txtFacturen.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtFacturen.CustomButton.TabIndex = 1;
            this.txtFacturen.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtFacturen.CustomButton.UseSelectable = true;
            this.txtFacturen.CustomButton.Visible = false;
            this.txtFacturen.Lines = new string[0];
            this.txtFacturen.Location = new System.Drawing.Point(6, 19);
            this.txtFacturen.MaxLength = 32767;
            this.txtFacturen.Multiline = true;
            this.txtFacturen.Name = "txtFacturen";
            this.txtFacturen.PasswordChar = '\0';
            this.txtFacturen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFacturen.SelectedText = "";
            this.txtFacturen.SelectionLength = 0;
            this.txtFacturen.SelectionStart = 0;
            this.txtFacturen.ShortcutsEnabled = true;
            this.txtFacturen.Size = new System.Drawing.Size(232, 176);
            this.txtFacturen.TabIndex = 104;
            this.txtFacturen.UseSelectable = true;
            this.txtFacturen.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFacturen.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // gbBestand
            // 
            this.gbBestand.Controls.Add(this.cbGemeente);
            this.gbBestand.Controls.Add(this.txtBTWCode);
            this.gbBestand.Controls.Add(this.cbLand);
            this.gbBestand.Controls.Add(this.metroLabel9);
            this.gbBestand.Controls.Add(this.metroLabel10);
            this.gbBestand.Controls.Add(this.metroLabel3);
            this.gbBestand.Controls.Add(this.metroLabel11);
            this.gbBestand.Controls.Add(this.txtGemeente);
            this.gbBestand.Controls.Add(this.metroLabel12);
            this.gbBestand.Controls.Add(this.txtCommentaar);
            this.gbBestand.Controls.Add(this.txtBTW);
            this.gbBestand.Controls.Add(this.txtWebsite);
            this.gbBestand.Controls.Add(this.txtEmail2);
            this.gbBestand.Controls.Add(this.metroLabel8);
            this.gbBestand.Controls.Add(this.metroLabel7);
            this.gbBestand.Controls.Add(this.metroLabel5);
            this.gbBestand.Controls.Add(this.metroLabel6);
            this.gbBestand.Controls.Add(this.metroLabel4);
            this.gbBestand.Controls.Add(this.txtEmail1);
            this.gbBestand.Controls.Add(this.txtTelefoon2);
            this.gbBestand.Controls.Add(this.txtTelefoon1);
            this.gbBestand.Controls.Add(this.txtPostcode);
            this.gbBestand.Controls.Add(this.txtAdres);
            this.gbBestand.Controls.Add(this.metroLabel2);
            this.gbBestand.Controls.Add(this.metroLabel1);
            this.gbBestand.Controls.Add(this.txtFirma);
            this.gbBestand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBestand.Location = new System.Drawing.Point(11, 29);
            this.gbBestand.Name = "gbBestand";
            this.gbBestand.Size = new System.Drawing.Size(548, 383);
            this.gbBestand.TabIndex = 83;
            this.gbBestand.TabStop = false;
            this.gbBestand.Text = "Information";
            // 
            // cbGemeente
            // 
            this.cbGemeente.FormattingEnabled = true;
            this.cbGemeente.ItemHeight = 23;
            this.cbGemeente.Location = new System.Drawing.Point(279, 106);
            this.cbGemeente.Name = "cbGemeente";
            this.cbGemeente.Size = new System.Drawing.Size(240, 29);
            this.cbGemeente.TabIndex = 105;
            this.cbGemeente.UseSelectable = true;
            // 
            // txtBTWCode
            // 
            // 
            // 
            // 
            this.txtBTWCode.CustomButton.Image = null;
            this.txtBTWCode.CustomButton.Location = new System.Drawing.Point(11, 1);
            this.txtBTWCode.CustomButton.Name = "";
            this.txtBTWCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBTWCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBTWCode.CustomButton.TabIndex = 1;
            this.txtBTWCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBTWCode.CustomButton.UseSelectable = true;
            this.txtBTWCode.CustomButton.Visible = false;
            this.txtBTWCode.Lines = new string[] {
        "BE"};
            this.txtBTWCode.Location = new System.Drawing.Point(105, 258);
            this.txtBTWCode.MaxLength = 32767;
            this.txtBTWCode.Name = "txtBTWCode";
            this.txtBTWCode.PasswordChar = '\0';
            this.txtBTWCode.ReadOnly = true;
            this.txtBTWCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBTWCode.SelectedText = "";
            this.txtBTWCode.SelectionLength = 0;
            this.txtBTWCode.SelectionStart = 0;
            this.txtBTWCode.ShortcutsEnabled = true;
            this.txtBTWCode.Size = new System.Drawing.Size(33, 23);
            this.txtBTWCode.TabIndex = 104;
            this.txtBTWCode.Text = "BE";
            this.txtBTWCode.UseSelectable = true;
            this.txtBTWCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBTWCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // cbLand
            // 
            this.cbLand.FormattingEnabled = true;
            this.cbLand.ItemHeight = 23;
            this.cbLand.Items.AddRange(new object[] {
            "Belgie",
            "Nederland",
            "Frankrijk"});
            this.cbLand.Location = new System.Drawing.Point(105, 45);
            this.cbLand.Name = "cbLand";
            this.cbLand.Size = new System.Drawing.Size(157, 29);
            this.cbLand.TabIndex = 103;
            this.cbLand.UseSelectable = true;
            this.cbLand.SelectedIndexChanged += new System.EventHandler(this.cbLand_SelectedIndexChanged);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel9.Location = new System.Drawing.Point(6, 287);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(96, 19);
            this.metroLabel9.TabIndex = 102;
            this.metroLabel9.Text = "Commentaar";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel10.Location = new System.Drawing.Point(6, 258);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(94, 19);
            this.metroLabel10.TabIndex = 101;
            this.metroLabel10.Text = "Btw nummer";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(197, 113);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(77, 19);
            this.metroLabel3.TabIndex = 90;
            this.metroLabel3.Text = "Gemeente";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel11.Location = new System.Drawing.Point(6, 229);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(63, 19);
            this.metroLabel11.TabIndex = 100;
            this.metroLabel11.Text = "Website";
            // 
            // txtGemeente
            // 
            // 
            // 
            // 
            this.txtGemeente.CustomButton.Image = null;
            this.txtGemeente.CustomButton.Location = new System.Drawing.Point(217, 1);
            this.txtGemeente.CustomButton.Name = "";
            this.txtGemeente.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtGemeente.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGemeente.CustomButton.TabIndex = 1;
            this.txtGemeente.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGemeente.CustomButton.UseSelectable = true;
            this.txtGemeente.CustomButton.Visible = false;
            this.txtGemeente.Lines = new string[0];
            this.txtGemeente.Location = new System.Drawing.Point(279, 109);
            this.txtGemeente.MaxLength = 32767;
            this.txtGemeente.Name = "txtGemeente";
            this.txtGemeente.PasswordChar = '\0';
            this.txtGemeente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGemeente.SelectedText = "";
            this.txtGemeente.SelectionLength = 0;
            this.txtGemeente.SelectionStart = 0;
            this.txtGemeente.ShortcutsEnabled = true;
            this.txtGemeente.Size = new System.Drawing.Size(239, 23);
            this.txtGemeente.TabIndex = 84;
            this.txtGemeente.UseSelectable = true;
            this.txtGemeente.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtGemeente.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel12.Location = new System.Drawing.Point(6, 200);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(57, 19);
            this.metroLabel12.TabIndex = 99;
            this.metroLabel12.Text = "Email 2";
            // 
            // txtCommentaar
            // 
            // 
            // 
            // 
            this.txtCommentaar.CustomButton.Image = null;
            this.txtCommentaar.CustomButton.Location = new System.Drawing.Point(333, 1);
            this.txtCommentaar.CustomButton.Name = "";
            this.txtCommentaar.CustomButton.Size = new System.Drawing.Size(81, 81);
            this.txtCommentaar.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCommentaar.CustomButton.TabIndex = 1;
            this.txtCommentaar.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCommentaar.CustomButton.UseSelectable = true;
            this.txtCommentaar.CustomButton.Visible = false;
            this.txtCommentaar.Lines = new string[0];
            this.txtCommentaar.Location = new System.Drawing.Point(105, 285);
            this.txtCommentaar.MaxLength = 32767;
            this.txtCommentaar.Multiline = true;
            this.txtCommentaar.Name = "txtCommentaar";
            this.txtCommentaar.PasswordChar = '\0';
            this.txtCommentaar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommentaar.SelectedText = "";
            this.txtCommentaar.SelectionLength = 0;
            this.txtCommentaar.SelectionStart = 0;
            this.txtCommentaar.ShortcutsEnabled = true;
            this.txtCommentaar.ShowClearButton = true;
            this.txtCommentaar.Size = new System.Drawing.Size(415, 83);
            this.txtCommentaar.TabIndex = 98;
            this.txtCommentaar.UseSelectable = true;
            this.txtCommentaar.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCommentaar.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtBTW
            // 
            // 
            // 
            // 
            this.txtBTW.CustomButton.Image = null;
            this.txtBTW.CustomButton.Location = new System.Drawing.Point(135, 1);
            this.txtBTW.CustomButton.Name = "";
            this.txtBTW.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBTW.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBTW.CustomButton.TabIndex = 1;
            this.txtBTW.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBTW.CustomButton.UseSelectable = true;
            this.txtBTW.CustomButton.Visible = false;
            this.txtBTW.Lines = new string[0];
            this.txtBTW.Location = new System.Drawing.Point(139, 258);
            this.txtBTW.MaxLength = 32767;
            this.txtBTW.Name = "txtBTW";
            this.txtBTW.PasswordChar = '\0';
            this.txtBTW.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBTW.SelectedText = "";
            this.txtBTW.SelectionLength = 0;
            this.txtBTW.SelectionStart = 0;
            this.txtBTW.ShortcutsEnabled = true;
            this.txtBTW.ShowClearButton = true;
            this.txtBTW.Size = new System.Drawing.Size(157, 23);
            this.txtBTW.TabIndex = 97;
            this.txtBTW.UseSelectable = true;
            this.txtBTW.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBTW.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtBTW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBTW_KeyPress);
            // 
            // txtWebsite
            // 
            // 
            // 
            // 
            this.txtWebsite.CustomButton.Image = null;
            this.txtWebsite.CustomButton.Location = new System.Drawing.Point(393, 1);
            this.txtWebsite.CustomButton.Name = "";
            this.txtWebsite.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtWebsite.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtWebsite.CustomButton.TabIndex = 1;
            this.txtWebsite.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtWebsite.CustomButton.UseSelectable = true;
            this.txtWebsite.CustomButton.Visible = false;
            this.txtWebsite.Lines = new string[0];
            this.txtWebsite.Location = new System.Drawing.Point(105, 227);
            this.txtWebsite.MaxLength = 32767;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.PasswordChar = '\0';
            this.txtWebsite.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtWebsite.SelectedText = "";
            this.txtWebsite.SelectionLength = 0;
            this.txtWebsite.SelectionStart = 0;
            this.txtWebsite.ShortcutsEnabled = true;
            this.txtWebsite.ShowClearButton = true;
            this.txtWebsite.Size = new System.Drawing.Size(415, 23);
            this.txtWebsite.TabIndex = 96;
            this.txtWebsite.UseSelectable = true;
            this.txtWebsite.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtWebsite.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtEmail2
            // 
            // 
            // 
            // 
            this.txtEmail2.CustomButton.Image = null;
            this.txtEmail2.CustomButton.Location = new System.Drawing.Point(393, 1);
            this.txtEmail2.CustomButton.Name = "";
            this.txtEmail2.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEmail2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEmail2.CustomButton.TabIndex = 1;
            this.txtEmail2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmail2.CustomButton.UseSelectable = true;
            this.txtEmail2.CustomButton.Visible = false;
            this.txtEmail2.Lines = new string[0];
            this.txtEmail2.Location = new System.Drawing.Point(105, 196);
            this.txtEmail2.MaxLength = 32767;
            this.txtEmail2.Name = "txtEmail2";
            this.txtEmail2.PasswordChar = '\0';
            this.txtEmail2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail2.SelectedText = "";
            this.txtEmail2.SelectionLength = 0;
            this.txtEmail2.SelectionStart = 0;
            this.txtEmail2.ShortcutsEnabled = true;
            this.txtEmail2.ShowClearButton = true;
            this.txtEmail2.Size = new System.Drawing.Size(415, 23);
            this.txtEmail2.TabIndex = 95;
            this.txtEmail2.UseSelectable = true;
            this.txtEmail2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEmail2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel8.Location = new System.Drawing.Point(6, 113);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(71, 19);
            this.metroLabel8.TabIndex = 94;
            this.metroLabel8.Text = "Postcode";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel7.Location = new System.Drawing.Point(268, 142);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(93, 19);
            this.metroLabel7.TabIndex = 93;
            this.metroLabel7.Text = "Telefoonnr 2";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(6, 171);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(57, 19);
            this.metroLabel5.TabIndex = 92;
            this.metroLabel5.Text = "Email 1";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel6.Location = new System.Drawing.Point(6, 142);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(93, 19);
            this.metroLabel6.TabIndex = 91;
            this.metroLabel6.Text = "Telefoonnr 1";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.Location = new System.Drawing.Point(6, 84);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(48, 19);
            this.metroLabel4.TabIndex = 89;
            this.metroLabel4.Text = "Adres";
            // 
            // txtEmail1
            // 
            // 
            // 
            // 
            this.txtEmail1.CustomButton.Image = null;
            this.txtEmail1.CustomButton.Location = new System.Drawing.Point(393, 1);
            this.txtEmail1.CustomButton.Name = "";
            this.txtEmail1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEmail1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEmail1.CustomButton.TabIndex = 1;
            this.txtEmail1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmail1.CustomButton.UseSelectable = true;
            this.txtEmail1.CustomButton.Visible = false;
            this.txtEmail1.Lines = new string[0];
            this.txtEmail1.Location = new System.Drawing.Point(105, 167);
            this.txtEmail1.MaxLength = 32767;
            this.txtEmail1.Name = "txtEmail1";
            this.txtEmail1.PasswordChar = '\0';
            this.txtEmail1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail1.SelectedText = "";
            this.txtEmail1.SelectionLength = 0;
            this.txtEmail1.SelectionStart = 0;
            this.txtEmail1.ShortcutsEnabled = true;
            this.txtEmail1.ShowClearButton = true;
            this.txtEmail1.Size = new System.Drawing.Size(415, 23);
            this.txtEmail1.TabIndex = 88;
            this.txtEmail1.UseSelectable = true;
            this.txtEmail1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEmail1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtTelefoon2
            // 
            // 
            // 
            // 
            this.txtTelefoon2.CustomButton.Image = null;
            this.txtTelefoon2.CustomButton.Location = new System.Drawing.Point(135, 1);
            this.txtTelefoon2.CustomButton.Name = "";
            this.txtTelefoon2.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtTelefoon2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTelefoon2.CustomButton.TabIndex = 1;
            this.txtTelefoon2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTelefoon2.CustomButton.UseSelectable = true;
            this.txtTelefoon2.CustomButton.Visible = false;
            this.txtTelefoon2.Lines = new string[0];
            this.txtTelefoon2.Location = new System.Drawing.Point(363, 138);
            this.txtTelefoon2.MaxLength = 32767;
            this.txtTelefoon2.Name = "txtTelefoon2";
            this.txtTelefoon2.PasswordChar = '\0';
            this.txtTelefoon2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefoon2.SelectedText = "";
            this.txtTelefoon2.SelectionLength = 0;
            this.txtTelefoon2.SelectionStart = 0;
            this.txtTelefoon2.ShortcutsEnabled = true;
            this.txtTelefoon2.ShowClearButton = true;
            this.txtTelefoon2.Size = new System.Drawing.Size(157, 23);
            this.txtTelefoon2.TabIndex = 87;
            this.txtTelefoon2.UseSelectable = true;
            this.txtTelefoon2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTelefoon2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtTelefoon1
            // 
            // 
            // 
            // 
            this.txtTelefoon1.CustomButton.Image = null;
            this.txtTelefoon1.CustomButton.Location = new System.Drawing.Point(135, 1);
            this.txtTelefoon1.CustomButton.Name = "";
            this.txtTelefoon1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtTelefoon1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTelefoon1.CustomButton.TabIndex = 1;
            this.txtTelefoon1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTelefoon1.CustomButton.UseSelectable = true;
            this.txtTelefoon1.CustomButton.Visible = false;
            this.txtTelefoon1.Lines = new string[0];
            this.txtTelefoon1.Location = new System.Drawing.Point(105, 138);
            this.txtTelefoon1.MaxLength = 32767;
            this.txtTelefoon1.Name = "txtTelefoon1";
            this.txtTelefoon1.PasswordChar = '\0';
            this.txtTelefoon1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefoon1.SelectedText = "";
            this.txtTelefoon1.SelectionLength = 0;
            this.txtTelefoon1.SelectionStart = 0;
            this.txtTelefoon1.ShortcutsEnabled = true;
            this.txtTelefoon1.ShowClearButton = true;
            this.txtTelefoon1.Size = new System.Drawing.Size(157, 23);
            this.txtTelefoon1.TabIndex = 86;
            this.txtTelefoon1.UseSelectable = true;
            this.txtTelefoon1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTelefoon1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtPostcode
            // 
            // 
            // 
            // 
            this.txtPostcode.CustomButton.Image = null;
            this.txtPostcode.CustomButton.Location = new System.Drawing.Point(64, 1);
            this.txtPostcode.CustomButton.Name = "";
            this.txtPostcode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtPostcode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPostcode.CustomButton.TabIndex = 1;
            this.txtPostcode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPostcode.CustomButton.UseSelectable = true;
            this.txtPostcode.CustomButton.Visible = false;
            this.txtPostcode.Lines = new string[0];
            this.txtPostcode.Location = new System.Drawing.Point(105, 109);
            this.txtPostcode.MaxLength = 32767;
            this.txtPostcode.Name = "txtPostcode";
            this.txtPostcode.PasswordChar = '\0';
            this.txtPostcode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPostcode.SelectedText = "";
            this.txtPostcode.SelectionLength = 0;
            this.txtPostcode.SelectionStart = 0;
            this.txtPostcode.ShortcutsEnabled = true;
            this.txtPostcode.ShowClearButton = true;
            this.txtPostcode.Size = new System.Drawing.Size(86, 23);
            this.txtPostcode.TabIndex = 85;
            this.txtPostcode.UseSelectable = true;
            this.txtPostcode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPostcode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPostcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPostcode_KeyPress);
            this.txtPostcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPostcode_KeyUp);
            // 
            // txtAdres
            // 
            // 
            // 
            // 
            this.txtAdres.CustomButton.Image = null;
            this.txtAdres.CustomButton.Location = new System.Drawing.Point(393, 1);
            this.txtAdres.CustomButton.Name = "";
            this.txtAdres.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtAdres.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAdres.CustomButton.TabIndex = 1;
            this.txtAdres.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAdres.CustomButton.UseSelectable = true;
            this.txtAdres.CustomButton.Visible = false;
            this.txtAdres.Lines = new string[0];
            this.txtAdres.Location = new System.Drawing.Point(105, 80);
            this.txtAdres.MaxLength = 32767;
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.PasswordChar = '\0';
            this.txtAdres.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAdres.SelectedText = "";
            this.txtAdres.SelectionLength = 0;
            this.txtAdres.SelectionStart = 0;
            this.txtAdres.ShortcutsEnabled = true;
            this.txtAdres.ShowClearButton = true;
            this.txtAdres.Size = new System.Drawing.Size(415, 23);
            this.txtAdres.TabIndex = 83;
            this.txtAdres.UseSelectable = true;
            this.txtAdres.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAdres.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(6, 49);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(41, 19);
            this.metroLabel2.TabIndex = 81;
            this.metroLabel2.Text = "Land";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(6, 20);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(47, 19);
            this.metroLabel1.TabIndex = 80;
            this.metroLabel1.Text = "Firma";
            // 
            // txtFirma
            // 
            // 
            // 
            // 
            this.txtFirma.CustomButton.Image = null;
            this.txtFirma.CustomButton.Location = new System.Drawing.Point(393, 1);
            this.txtFirma.CustomButton.Name = "";
            this.txtFirma.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtFirma.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtFirma.CustomButton.TabIndex = 1;
            this.txtFirma.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtFirma.CustomButton.UseSelectable = true;
            this.txtFirma.CustomButton.Visible = false;
            this.txtFirma.Lines = new string[0];
            this.txtFirma.Location = new System.Drawing.Point(105, 16);
            this.txtFirma.MaxLength = 32767;
            this.txtFirma.Name = "txtFirma";
            this.txtFirma.PasswordChar = '\0';
            this.txtFirma.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFirma.SelectedText = "";
            this.txtFirma.SelectionLength = 0;
            this.txtFirma.SelectionStart = 0;
            this.txtFirma.ShortcutsEnabled = true;
            this.txtFirma.ShowClearButton = true;
            this.txtFirma.Size = new System.Drawing.Size(415, 23);
            this.txtFirma.TabIndex = 79;
            this.txtFirma.UseSelectable = true;
            this.txtFirma.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFirma.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProductie);
            this.groupBox1.Location = new System.Drawing.Point(565, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 176);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productie";
            // 
            // txtProductie
            // 
            // 
            // 
            // 
            this.txtProductie.CustomButton.Image = null;
            this.txtProductie.CustomButton.Location = new System.Drawing.Point(80, 2);
            this.txtProductie.CustomButton.Name = "";
            this.txtProductie.CustomButton.Size = new System.Drawing.Size(149, 149);
            this.txtProductie.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductie.CustomButton.TabIndex = 1;
            this.txtProductie.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductie.CustomButton.UseSelectable = true;
            this.txtProductie.CustomButton.Visible = false;
            this.txtProductie.Lines = new string[0];
            this.txtProductie.Location = new System.Drawing.Point(6, 16);
            this.txtProductie.MaxLength = 32767;
            this.txtProductie.Multiline = true;
            this.txtProductie.Name = "txtProductie";
            this.txtProductie.PasswordChar = '\0';
            this.txtProductie.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProductie.SelectedText = "";
            this.txtProductie.SelectionLength = 0;
            this.txtProductie.SelectionStart = 0;
            this.txtProductie.ShortcutsEnabled = true;
            this.txtProductie.Size = new System.Drawing.Size(232, 154);
            this.txtProductie.TabIndex = 103;
            this.txtProductie.UseSelectable = true;
            this.txtProductie.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProductie.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(119, 423);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 0);
            this.lblError.TabIndex = 87;
            // 
            // Aanmaken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(820, 457);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnMaken);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbBestand);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Aanmaken";
            this.Resizable = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Aanmaken_FormClosed);
            this.Load += new System.EventHandler(this.Aanmaken_Load);
            this.groupBox2.ResumeLayout(false);
            this.gbBestand.ResumeLayout(false);
            this.gbBestand.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private MetroFramework.Controls.MetroButton btnMaken;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroTextBox txtFacturen;
        private System.Windows.Forms.GroupBox gbBestand;
        private MetroFramework.Controls.MetroTextBox txtBTWCode;
        private MetroFramework.Controls.MetroComboBox cbLand;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroTextBox txtCommentaar;
        private MetroFramework.Controls.MetroTextBox txtBTW;
        private MetroFramework.Controls.MetroTextBox txtWebsite;
        private MetroFramework.Controls.MetroTextBox txtEmail2;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox txtEmail1;
        private MetroFramework.Controls.MetroTextBox txtTelefoon2;
        private MetroFramework.Controls.MetroTextBox txtTelefoon1;
        private MetroFramework.Controls.MetroTextBox txtPostcode;
        private MetroFramework.Controls.MetroTextBox txtGemeente;
        private MetroFramework.Controls.MetroTextBox txtAdres;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtFirma;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox txtProductie;
        private MetroFramework.Controls.MetroComboBox cbGemeente;
        private MetroFramework.Controls.MetroLabel lblError;
    }
}