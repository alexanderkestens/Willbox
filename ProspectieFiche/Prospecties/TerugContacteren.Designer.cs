namespace ProspectieFiche
{
    partial class TerugContacteren
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
            this.btnAlles = new System.Windows.Forms.Button();
            this.btnVandaag = new System.Windows.Forms.Button();
            this.btnDezeWeek = new System.Windows.Forms.Button();
            this.dgvContacteren = new System.Windows.Forms.DataGridView();
            this.iconInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacteren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAlles
            // 
            this.btnAlles.Location = new System.Drawing.Point(13, 13);
            this.btnAlles.Name = "btnAlles";
            this.btnAlles.Size = new System.Drawing.Size(75, 23);
            this.btnAlles.TabIndex = 0;
            this.btnAlles.Text = "Alles";
            this.btnAlles.UseVisualStyleBackColor = true;
            this.btnAlles.Click += new System.EventHandler(this.btnAlles_Click);
            // 
            // btnVandaag
            // 
            this.btnVandaag.Location = new System.Drawing.Point(94, 13);
            this.btnVandaag.Name = "btnVandaag";
            this.btnVandaag.Size = new System.Drawing.Size(75, 23);
            this.btnVandaag.TabIndex = 1;
            this.btnVandaag.Text = "Vandaag";
            this.btnVandaag.UseVisualStyleBackColor = true;
            this.btnVandaag.Click += new System.EventHandler(this.btnVandaag_Click);
            // 
            // btnDezeWeek
            // 
            this.btnDezeWeek.Location = new System.Drawing.Point(175, 13);
            this.btnDezeWeek.Name = "btnDezeWeek";
            this.btnDezeWeek.Size = new System.Drawing.Size(75, 23);
            this.btnDezeWeek.TabIndex = 2;
            this.btnDezeWeek.Text = "Deze week";
            this.btnDezeWeek.UseVisualStyleBackColor = true;
            this.btnDezeWeek.Click += new System.EventHandler(this.btnDezeWeek_Click);
            // 
            // dgvContacteren
            // 
            this.dgvContacteren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContacteren.Location = new System.Drawing.Point(1, 42);
            this.dgvContacteren.Name = "dgvContacteren";
            this.dgvContacteren.Size = new System.Drawing.Size(476, 391);
            this.dgvContacteren.TabIndex = 3;
            // 
            // iconInfo
            // 
            this.iconInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconInfo.Image = global::ProspectieFiche.Properties.Resources.iconInfo;
            this.iconInfo.Location = new System.Drawing.Point(436, 7);
            this.iconInfo.Name = "iconInfo";
            this.iconInfo.Size = new System.Drawing.Size(31, 29);
            this.iconInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconInfo.TabIndex = 62;
            this.iconInfo.TabStop = false;
            this.iconInfo.Click += new System.EventHandler(this.iconInfo_Click);
            // 
            // TerugContacteren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 433);
            this.Controls.Add(this.iconInfo);
            this.Controls.Add(this.dgvContacteren);
            this.Controls.Add(this.btnDezeWeek);
            this.Controls.Add(this.btnVandaag);
            this.Controls.Add(this.btnAlles);
            this.Name = "TerugContacteren";
            this.Text = "TerugContacteren";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacteren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAlles;
        private System.Windows.Forms.Button btnVandaag;
        private System.Windows.Forms.Button btnDezeWeek;
        private System.Windows.Forms.DataGridView dgvContacteren;
        private System.Windows.Forms.PictureBox iconInfo;
    }
}