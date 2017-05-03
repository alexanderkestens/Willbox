namespace ProspectieFiche
{
    partial class Klantenlijst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Klantenlijst));
            this.btnMaakLijst = new System.Windows.Forms.Button();
            this.clbvelden = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnMaakLijst
            // 
            this.btnMaakLijst.Location = new System.Drawing.Point(131, 143);
            this.btnMaakLijst.Name = "btnMaakLijst";
            this.btnMaakLijst.Size = new System.Drawing.Size(103, 38);
            this.btnMaakLijst.TabIndex = 13;
            this.btnMaakLijst.Text = "Maak lijst";
            this.btnMaakLijst.UseVisualStyleBackColor = true;
            this.btnMaakLijst.Click += new System.EventHandler(this.btnMaakLijst_Click);
            // 
            // clbvelden
            // 
            this.clbvelden.FormattingEnabled = true;
            this.clbvelden.Items.AddRange(new object[] {
            "Adres",
            "Gemeente",
            "Postcode",
            "Telefoonnummer1",
            "Telefoonnummer2",
            "Email1",
            "Email2",
            "Website",
            "Commentaar",
            "Aanmaakdatum"});
            this.clbvelden.Location = new System.Drawing.Point(12, 12);
            this.clbvelden.Name = "clbvelden";
            this.clbvelden.Size = new System.Drawing.Size(113, 169);
            this.clbvelden.TabIndex = 14;
            // 
            // Klantenlijst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 190);
            this.Controls.Add(this.clbvelden);
            this.Controls.Add(this.btnMaakLijst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Klantenlijst";
            this.Text = "Klantenlijst";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnMaakLijst;
        private System.Windows.Forms.CheckedListBox clbvelden;
    }
}