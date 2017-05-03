namespace ProspectieFiche
{
    partial class OfferteExcell
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
            this.lblBedrijf = new System.Windows.Forms.Label();
            this.txtZoekenFirma = new System.Windows.Forms.TextBox();
            this.iconSearch = new System.Windows.Forms.PictureBox();
            this.dgvOffertes = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnLeegmaken = new System.Windows.Forms.Button();
            this.dgvDataOffertes = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffertes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOffertes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBedrijf
            // 
            this.lblBedrijf.AutoSize = true;
            this.lblBedrijf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBedrijf.Location = new System.Drawing.Point(12, 22);
            this.lblBedrijf.Name = "lblBedrijf";
            this.lblBedrijf.Size = new System.Drawing.Size(53, 16);
            this.lblBedrijf.TabIndex = 0;
            this.lblBedrijf.Text = "Bedrijf";
            // 
            // txtZoekenFirma
            // 
            this.txtZoekenFirma.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoekenFirma.Location = new System.Drawing.Point(71, 13);
            this.txtZoekenFirma.Name = "txtZoekenFirma";
            this.txtZoekenFirma.Size = new System.Drawing.Size(252, 29);
            this.txtZoekenFirma.TabIndex = 85;
            this.txtZoekenFirma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZoekenFirma_KeyPress);
            // 
            // iconSearch
            // 
            this.iconSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconSearch.Image = global::ProspectieFiche.Properties.Resources.iconSearch;
            this.iconSearch.Location = new System.Drawing.Point(329, 13);
            this.iconSearch.Name = "iconSearch";
            this.iconSearch.Size = new System.Drawing.Size(31, 29);
            this.iconSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconSearch.TabIndex = 84;
            this.iconSearch.TabStop = false;
            this.iconSearch.Click += new System.EventHandler(this.iconSearch_Click);
            // 
            // dgvOffertes
            // 
            this.dgvOffertes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOffertes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOffertes.Location = new System.Drawing.Point(1, 59);
            this.dgvOffertes.Name = "dgvOffertes";
            this.dgvOffertes.Size = new System.Drawing.Size(932, 215);
            this.dgvOffertes.TabIndex = 86;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 280);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 87;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(93, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 89;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExcel.Location = new System.Drawing.Point(174, 280);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 25);
            this.btnExcel.TabIndex = 90;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnLeegmaken
            // 
            this.btnLeegmaken.Location = new System.Drawing.Point(366, 12);
            this.btnLeegmaken.Name = "btnLeegmaken";
            this.btnLeegmaken.Size = new System.Drawing.Size(119, 30);
            this.btnLeegmaken.TabIndex = 91;
            this.btnLeegmaken.Text = "Leegmaken";
            this.btnLeegmaken.UseVisualStyleBackColor = true;
            this.btnLeegmaken.Click += new System.EventHandler(this.btnLeegmaken_Click);
            // 
            // dgvDataOffertes
            // 
            this.dgvDataOffertes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataOffertes.Location = new System.Drawing.Point(1, 0);
            this.dgvDataOffertes.Name = "dgvDataOffertes";
            this.dgvDataOffertes.Size = new System.Drawing.Size(10, 10);
            this.dgvDataOffertes.TabIndex = 92;
            this.dgvDataOffertes.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblInfo.Location = new System.Drawing.Point(265, 287);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 93;
            // 
            // OfferteExcell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 311);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvDataOffertes);
            this.Controls.Add(this.btnLeegmaken);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvOffertes);
            this.Controls.Add(this.txtZoekenFirma);
            this.Controls.Add(this.iconSearch);
            this.Controls.Add(this.lblBedrijf);
            this.Name = "OfferteExcell";
            this.Text = "Offerte maken";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OfferteExcell_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffertes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOffertes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBedrijf;
        private System.Windows.Forms.TextBox txtZoekenFirma;
        private System.Windows.Forms.PictureBox iconSearch;
        private System.Windows.Forms.DataGridView dgvOffertes;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnLeegmaken;
        private System.Windows.Forms.DataGridView dgvDataOffertes;
        private System.Windows.Forms.Label lblInfo;
    }
}