namespace ProspectieFiche
{
    partial class LeveringExcel
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
            this.dgvDataOrders = new System.Windows.Forms.DataGridView();
            this.btnLeegmaken = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.txtZoekenFirma = new System.Windows.Forms.TextBox();
            this.lblBedrijf = new System.Windows.Forms.Label();
            this.iconSearch = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDataOrders
            // 
            this.dgvDataOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataOrders.Location = new System.Drawing.Point(1, 3);
            this.dgvDataOrders.Name = "dgvDataOrders";
            this.dgvDataOrders.Size = new System.Drawing.Size(10, 10);
            this.dgvDataOrders.TabIndex = 101;
            this.dgvDataOrders.Visible = false;
            // 
            // btnLeegmaken
            // 
            this.btnLeegmaken.Location = new System.Drawing.Point(366, 15);
            this.btnLeegmaken.Name = "btnLeegmaken";
            this.btnLeegmaken.Size = new System.Drawing.Size(119, 30);
            this.btnLeegmaken.TabIndex = 100;
            this.btnLeegmaken.Text = "Leegmaken";
            this.btnLeegmaken.UseVisualStyleBackColor = true;
            this.btnLeegmaken.Click += new System.EventHandler(this.btnLeegmaken_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExcel.Location = new System.Drawing.Point(174, 283);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 25);
            this.btnExcel.TabIndex = 99;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(93, 283);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 98;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 283);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 97;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(1, 62);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(932, 215);
            this.dgvOrders.TabIndex = 96;
            // 
            // txtZoekenFirma
            // 
            this.txtZoekenFirma.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoekenFirma.Location = new System.Drawing.Point(71, 16);
            this.txtZoekenFirma.Name = "txtZoekenFirma";
            this.txtZoekenFirma.Size = new System.Drawing.Size(252, 29);
            this.txtZoekenFirma.TabIndex = 95;
            this.txtZoekenFirma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZoekenFirma_KeyPress);
            // 
            // lblBedrijf
            // 
            this.lblBedrijf.AutoSize = true;
            this.lblBedrijf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBedrijf.Location = new System.Drawing.Point(12, 25);
            this.lblBedrijf.Name = "lblBedrijf";
            this.lblBedrijf.Size = new System.Drawing.Size(53, 16);
            this.lblBedrijf.TabIndex = 93;
            this.lblBedrijf.Text = "Bedrijf";
            // 
            // iconSearch
            // 
            this.iconSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconSearch.Image = global::ProspectieFiche.Properties.Resources.iconSearch;
            this.iconSearch.Location = new System.Drawing.Point(329, 16);
            this.iconSearch.Name = "iconSearch";
            this.iconSearch.Size = new System.Drawing.Size(31, 29);
            this.iconSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconSearch.TabIndex = 94;
            this.iconSearch.TabStop = false;
            this.iconSearch.Click += new System.EventHandler(this.iconSearch_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblInfo.Location = new System.Drawing.Point(290, 286);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 102;
            // 
            // LeveringExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 311);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvDataOrders);
            this.Controls.Add(this.btnLeegmaken);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.txtZoekenFirma);
            this.Controls.Add(this.iconSearch);
            this.Controls.Add(this.lblBedrijf);
            this.Name = "LeveringExcel";
            this.Text = "Leveringsnota maken";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LeveringExcel_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataOrders;
        private System.Windows.Forms.Button btnLeegmaken;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.TextBox txtZoekenFirma;
        private System.Windows.Forms.PictureBox iconSearch;
        private System.Windows.Forms.Label lblBedrijf;
        private System.Windows.Forms.Label lblInfo;
    }
}