namespace ProspectieFiche
{
    partial class Orders
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
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.txtZoekenFirma = new System.Windows.Forms.TextBox();
            this.dgvProductie = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.iconSearch = new System.Windows.Forms.PictureBox();
            this.iconDelete = new System.Windows.Forms.PictureBox();
            this.iconProduction = new System.Windows.Forms.PictureBox();
            this.iconDelivery = new System.Windows.Forms.PictureBox();
            this.dgvDataOrders = new System.Windows.Forms.DataGridView();
            this.iconProductionDone = new System.Windows.Forms.PictureBox();
            this.btnOrderBevestiging = new System.Windows.Forms.Button();
            this.btnGondardennes = new System.Windows.Forms.Button();
            this.iconDoneOrder = new System.Windows.Forms.PictureBox();
            this.btnLijst = new System.Windows.Forms.Button();
            this.btnFactuur = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelivery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconProductionDone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDoneOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(0, 47);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(1182, 328);
            this.dgvOrders.TabIndex = 87;
            this.dgvOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellClick);
            this.dgvOrders.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrders_DataBindingComplete);
            // 
            // txtZoekenFirma
            // 
            this.txtZoekenFirma.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoekenFirma.Location = new System.Drawing.Point(122, 12);
            this.txtZoekenFirma.Name = "txtZoekenFirma";
            this.txtZoekenFirma.Size = new System.Drawing.Size(252, 29);
            this.txtZoekenFirma.TabIndex = 89;
            this.txtZoekenFirma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnter);
            // 
            // dgvProductie
            // 
            this.dgvProductie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductie.Location = new System.Drawing.Point(0, 417);
            this.dgvProductie.Name = "dgvProductie";
            this.dgvProductie.Size = new System.Drawing.Size(1182, 194);
            this.dgvProductie.TabIndex = 90;
            this.dgvProductie.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductie_CellClick);
            this.dgvProductie.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductie_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 91;
            this.label1.Text = "In productie:";
            // 
            // iconSearch
            // 
            this.iconSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconSearch.Image = global::ProspectieFiche.Properties.Resources.iconSearch;
            this.iconSearch.Location = new System.Drawing.Point(380, 12);
            this.iconSearch.Name = "iconSearch";
            this.iconSearch.Size = new System.Drawing.Size(31, 29);
            this.iconSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconSearch.TabIndex = 88;
            this.iconSearch.TabStop = false;
            this.iconSearch.Click += new System.EventHandler(this.iconSearch_Click);
            // 
            // iconDelete
            // 
            this.iconDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconDelete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconDelete.Image = global::ProspectieFiche.Properties.Resources.iconDelete;
            this.iconDelete.Location = new System.Drawing.Point(1145, 12);
            this.iconDelete.Name = "iconDelete";
            this.iconDelete.Size = new System.Drawing.Size(31, 29);
            this.iconDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconDelete.TabIndex = 86;
            this.iconDelete.TabStop = false;
            this.iconDelete.Click += new System.EventHandler(this.iconDelete_Click);
            // 
            // iconProduction
            // 
            this.iconProduction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconProduction.Image = global::ProspectieFiche.Properties.Resources.iconProduction;
            this.iconProduction.Location = new System.Drawing.Point(12, 12);
            this.iconProduction.Name = "iconProduction";
            this.iconProduction.Size = new System.Drawing.Size(31, 29);
            this.iconProduction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconProduction.TabIndex = 84;
            this.iconProduction.TabStop = false;
            this.iconProduction.Click += new System.EventHandler(this.iconProduction_Click);
            // 
            // iconDelivery
            // 
            this.iconDelivery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconDelivery.Image = global::ProspectieFiche.Properties.Resources.iconDelivery;
            this.iconDelivery.Location = new System.Drawing.Point(85, 12);
            this.iconDelivery.Name = "iconDelivery";
            this.iconDelivery.Size = new System.Drawing.Size(31, 29);
            this.iconDelivery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconDelivery.TabIndex = 92;
            this.iconDelivery.TabStop = false;
            this.iconDelivery.Click += new System.EventHandler(this.iconDelivery_Click);
            // 
            // dgvDataOrders
            // 
            this.dgvDataOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvDataOrders.Name = "dgvDataOrders";
            this.dgvDataOrders.Size = new System.Drawing.Size(10, 10);
            this.dgvDataOrders.TabIndex = 93;
            this.dgvDataOrders.Visible = false;
            // 
            // iconProductionDone
            // 
            this.iconProductionDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconProductionDone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconProductionDone.Image = global::ProspectieFiche.Properties.Resources.iconGreen;
            this.iconProductionDone.Location = new System.Drawing.Point(1141, 382);
            this.iconProductionDone.Name = "iconProductionDone";
            this.iconProductionDone.Size = new System.Drawing.Size(31, 29);
            this.iconProductionDone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconProductionDone.TabIndex = 94;
            this.iconProductionDone.TabStop = false;
            this.iconProductionDone.Click += new System.EventHandler(this.iconProductionDone_Click);
            // 
            // btnOrderBevestiging
            // 
            this.btnOrderBevestiging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderBevestiging.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOrderBevestiging.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrderBevestiging.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOrderBevestiging.Location = new System.Drawing.Point(995, 12);
            this.btnOrderBevestiging.Name = "btnOrderBevestiging";
            this.btnOrderBevestiging.Size = new System.Drawing.Size(144, 29);
            this.btnOrderBevestiging.TabIndex = 95;
            this.btnOrderBevestiging.Text = "Orderbevestiging";
            this.btnOrderBevestiging.UseVisualStyleBackColor = false;
            this.btnOrderBevestiging.Click += new System.EventHandler(this.btnOrderBevestiging_Click);
            // 
            // btnGondardennes
            // 
            this.btnGondardennes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGondardennes.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGondardennes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGondardennes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGondardennes.Location = new System.Drawing.Point(695, 12);
            this.btnGondardennes.Name = "btnGondardennes";
            this.btnGondardennes.Size = new System.Drawing.Size(144, 29);
            this.btnGondardennes.TabIndex = 96;
            this.btnGondardennes.Text = "Gondardennes";
            this.btnGondardennes.UseVisualStyleBackColor = false;
            this.btnGondardennes.Click += new System.EventHandler(this.btnGondardennes_Click);
            // 
            // iconDoneOrder
            // 
            this.iconDoneOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iconDoneOrder.Image = global::ProspectieFiche.Properties.Resources.iconGreen;
            this.iconDoneOrder.Location = new System.Drawing.Point(49, 12);
            this.iconDoneOrder.Name = "iconDoneOrder";
            this.iconDoneOrder.Size = new System.Drawing.Size(31, 29);
            this.iconDoneOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconDoneOrder.TabIndex = 97;
            this.iconDoneOrder.TabStop = false;
            this.iconDoneOrder.Click += new System.EventHandler(this.iconDoneOrder_Click);
            // 
            // btnLijst
            // 
            this.btnLijst.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLijst.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLijst.Location = new System.Drawing.Point(417, 12);
            this.btnLijst.Name = "btnLijst";
            this.btnLijst.Size = new System.Drawing.Size(55, 29);
            this.btnLijst.TabIndex = 98;
            this.btnLijst.Text = "Lijst";
            this.btnLijst.UseVisualStyleBackColor = false;
            this.btnLijst.Click += new System.EventHandler(this.btnLijst_Click);
            // 
            // btnFactuur
            // 
            this.btnFactuur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFactuur.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFactuur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFactuur.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFactuur.Location = new System.Drawing.Point(845, 12);
            this.btnFactuur.Name = "btnFactuur";
            this.btnFactuur.Size = new System.Drawing.Size(144, 29);
            this.btnFactuur.TabIndex = 99;
            this.btnFactuur.Text = "Factuur";
            this.btnFactuur.UseVisualStyleBackColor = false;
            this.btnFactuur.Click += new System.EventHandler(this.btnFactuur_Click);
            // 
            // Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.btnFactuur);
            this.Controls.Add(this.btnLijst);
            this.Controls.Add(this.iconDoneOrder);
            this.Controls.Add(this.btnGondardennes);
            this.Controls.Add(this.btnOrderBevestiging);
            this.Controls.Add(this.iconProductionDone);
            this.Controls.Add(this.dgvDataOrders);
            this.Controls.Add(this.iconDelivery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvProductie);
            this.Controls.Add(this.txtZoekenFirma);
            this.Controls.Add(this.iconSearch);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.iconDelete);
            this.Controls.Add(this.iconProduction);
            this.Name = "Orders";
            this.Text = "Orders";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Orders_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelivery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconProductionDone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDoneOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.PictureBox iconDelete;
        private System.Windows.Forms.PictureBox iconProduction;
        private System.Windows.Forms.TextBox txtZoekenFirma;
        private System.Windows.Forms.PictureBox iconSearch;
        private System.Windows.Forms.DataGridView dgvProductie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox iconDelivery;
        private System.Windows.Forms.DataGridView dgvDataOrders;
        private System.Windows.Forms.PictureBox iconProductionDone;
        private System.Windows.Forms.Button btnOrderBevestiging;
        private System.Windows.Forms.Button btnGondardennes;
        private System.Windows.Forms.PictureBox iconDoneOrder;
        private System.Windows.Forms.Button btnLijst;
        private System.Windows.Forms.Button btnFactuur;
    }
}