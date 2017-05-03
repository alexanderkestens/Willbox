namespace ProspectieFiche
{
    partial class Facturen
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
            this.dgvFacturen = new System.Windows.Forms.DataGridView();
            this.btnAddFactuur = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFacturen
            // 
            this.dgvFacturen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacturen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturen.Location = new System.Drawing.Point(1, 47);
            this.dgvFacturen.Name = "dgvFacturen";
            this.dgvFacturen.Size = new System.Drawing.Size(774, 358);
            this.dgvFacturen.TabIndex = 0;
            // 
            // btnAddFactuur
            // 
            this.btnAddFactuur.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddFactuur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFactuur.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddFactuur.Location = new System.Drawing.Point(12, 12);
            this.btnAddFactuur.Name = "btnAddFactuur";
            this.btnAddFactuur.Size = new System.Drawing.Size(107, 29);
            this.btnAddFactuur.TabIndex = 100;
            this.btnAddFactuur.Text = "Toevoegen";
            this.btnAddFactuur.UseVisualStyleBackColor = false;
            this.btnAddFactuur.Click += new System.EventHandler(this.btnAddFactuur_Click);
            // 
            // Facturen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 405);
            this.Controls.Add(this.btnAddFactuur);
            this.Controls.Add(this.dgvFacturen);
            this.Name = "Facturen";
            this.Text = "Facturen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Facturen_FormClosed);
            this.Load += new System.EventHandler(this.Facturen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFacturen;
        private System.Windows.Forms.Button btnAddFactuur;
    }
}