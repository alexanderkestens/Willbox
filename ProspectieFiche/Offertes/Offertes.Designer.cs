namespace ProspectieFiche
{
    partial class Offertes
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
            this.txtZoekenFirma = new System.Windows.Forms.TextBox();
            this.btnReorder = new System.Windows.Forms.Button();
            this.iconExcel = new System.Windows.Forms.PictureBox();
            this.iconGreen = new System.Windows.Forms.PictureBox();
            this.iconDelete = new System.Windows.Forms.PictureBox();
            this.iconSearch = new System.Windows.Forms.PictureBox();
            this.iconEdit = new System.Windows.Forms.PictureBox();
            this.iconNew = new System.Windows.Forms.PictureBox();
            this.dgvOffertes = new System.Windows.Forms.DataGridView();
            this.btnLijst = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffertes)).BeginInit();
            this.SuspendLayout();
            // 
            // txtZoekenFirma
            // 
            this.txtZoekenFirma.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoekenFirma.Location = new System.Drawing.Point(196, 12);
            this.txtZoekenFirma.Name = "txtZoekenFirma";
            this.txtZoekenFirma.Size = new System.Drawing.Size(252, 29);
            this.txtZoekenFirma.TabIndex = 83;
            this.txtZoekenFirma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnter);
            // 
            // btnReorder
            // 
            this.btnReorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReorder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReorder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReorder.Location = new System.Drawing.Point(624, 12);
            this.btnReorder.Name = "btnReorder";
            this.btnReorder.Size = new System.Drawing.Size(144, 29);
            this.btnReorder.TabIndex = 97;
            this.btnReorder.Text = "Re-order";
            this.btnReorder.UseVisualStyleBackColor = false;
            this.btnReorder.Click += new System.EventHandler(this.btnReorder_Click);
            // 
            // iconExcel
            // 
            this.iconExcel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconExcel.Image = global::ProspectieFiche.Properties.Resources.iconExcell;
            this.iconExcel.Location = new System.Drawing.Point(122, 12);
            this.iconExcel.Name = "iconExcel";
            this.iconExcel.Size = new System.Drawing.Size(31, 29);
            this.iconExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconExcel.TabIndex = 98;
            this.iconExcel.TabStop = false;
            this.iconExcel.Click += new System.EventHandler(this.iconExcel_Click);
            // 
            // iconGreen
            // 
            this.iconGreen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconGreen.Image = global::ProspectieFiche.Properties.Resources.iconGreen;
            this.iconGreen.Location = new System.Drawing.Point(159, 12);
            this.iconGreen.Name = "iconGreen";
            this.iconGreen.Size = new System.Drawing.Size(31, 29);
            this.iconGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconGreen.TabIndex = 84;
            this.iconGreen.TabStop = false;
            this.iconGreen.Click += new System.EventHandler(this.iconGreen_Click);
            // 
            // iconDelete
            // 
            this.iconDelete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconDelete.Image = global::ProspectieFiche.Properties.Resources.iconDelete;
            this.iconDelete.Location = new System.Drawing.Point(86, 12);
            this.iconDelete.Name = "iconDelete";
            this.iconDelete.Size = new System.Drawing.Size(31, 29);
            this.iconDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconDelete.TabIndex = 81;
            this.iconDelete.TabStop = false;
            this.iconDelete.Click += new System.EventHandler(this.iconDelete_Click);
            // 
            // iconSearch
            // 
            this.iconSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconSearch.Image = global::ProspectieFiche.Properties.Resources.iconSearch;
            this.iconSearch.Location = new System.Drawing.Point(454, 12);
            this.iconSearch.Name = "iconSearch";
            this.iconSearch.Size = new System.Drawing.Size(31, 29);
            this.iconSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconSearch.TabIndex = 80;
            this.iconSearch.TabStop = false;
            this.iconSearch.Click += new System.EventHandler(this.iconSearch_Click);
            // 
            // iconEdit
            // 
            this.iconEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconEdit.Image = global::ProspectieFiche.Properties.Resources.iconEdit;
            this.iconEdit.Location = new System.Drawing.Point(49, 12);
            this.iconEdit.Name = "iconEdit";
            this.iconEdit.Size = new System.Drawing.Size(31, 29);
            this.iconEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconEdit.TabIndex = 78;
            this.iconEdit.TabStop = false;
            this.iconEdit.Click += new System.EventHandler(this.iconEdit_Click);
            // 
            // iconNew
            // 
            this.iconNew.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconNew.Image = global::ProspectieFiche.Properties.Resources.iconAdd2;
            this.iconNew.Location = new System.Drawing.Point(12, 12);
            this.iconNew.Name = "iconNew";
            this.iconNew.Size = new System.Drawing.Size(31, 29);
            this.iconNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconNew.TabIndex = 77;
            this.iconNew.TabStop = false;
            this.iconNew.Click += new System.EventHandler(this.iconNew_Click);
            // 
            // dgvOffertes
            // 
            this.dgvOffertes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOffertes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOffertes.Location = new System.Drawing.Point(0, 47);
            this.dgvOffertes.Name = "dgvOffertes";
            this.dgvOffertes.Size = new System.Drawing.Size(1150, 498);
            this.dgvOffertes.TabIndex = 82;
            this.dgvOffertes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOffertes_CellClick);
            this.dgvOffertes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOffertes_DataBindingComplete);
            // 
            // btnLijst
            // 
            this.btnLijst.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLijst.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLijst.Location = new System.Drawing.Point(491, 12);
            this.btnLijst.Name = "btnLijst";
            this.btnLijst.Size = new System.Drawing.Size(55, 29);
            this.btnLijst.TabIndex = 99;
            this.btnLijst.Text = "Lijst";
            this.btnLijst.UseVisualStyleBackColor = false;
            this.btnLijst.Click += new System.EventHandler(this.btnLijst_Click);
            // 
            // Offertes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 545);
            this.Controls.Add(this.btnLijst);
            this.Controls.Add(this.iconExcel);
            this.Controls.Add(this.btnReorder);
            this.Controls.Add(this.iconGreen);
            this.Controls.Add(this.txtZoekenFirma);
            this.Controls.Add(this.dgvOffertes);
            this.Controls.Add(this.iconDelete);
            this.Controls.Add(this.iconSearch);
            this.Controls.Add(this.iconEdit);
            this.Controls.Add(this.iconNew);
            this.Name = "Offertes";
            this.Text = "Offertes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Offertes_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.iconExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffertes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconDelete;
        private System.Windows.Forms.PictureBox iconSearch;
        private System.Windows.Forms.PictureBox iconEdit;
        private System.Windows.Forms.PictureBox iconNew;
        private System.Windows.Forms.TextBox txtZoekenFirma;
        private System.Windows.Forms.PictureBox iconGreen;
        private System.Windows.Forms.Button btnReorder;
        private System.Windows.Forms.PictureBox iconExcel;
        private System.Windows.Forms.DataGridView dgvOffertes;
        private System.Windows.Forms.Button btnLijst;
    }
}