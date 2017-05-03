using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProspectieFiche
{
    public partial class Offertes : Form
    {
        private Main main;
        private int codeUser;
        private int klantnr, offertenr, ordernr, offerteartikelnr;
        BindingSource bindingSource;
        MySqlConnection conn;
        private String firmaNaam, statusCode;
        private AddOfferte addOfferte;
        private EditOfferte editOfferte;
        private OfferteExcell offerteExcell;

        public Offertes()
        {
            InitializeComponent();
        }

        public Offertes(Main main, int codeUser)
        {
            this.main = main;
            this.codeUser = codeUser;
            InitializeComponent();
            dataOpvragenOffertes();
        }

        private void dataOpvragenOffertes()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT offertes.offertenr, offerteArtikel.offerteartikelnr AS 'artikelnr', klant.naam, klant.klantnr, offertes.datum, offerteArtikel.ref, offerteArtikel.lengte, offerteArtikel.breedte, offerteArtikel.hoogte, offerteArtikel.aantal, offerteArtikel.prijs, offerteArtikel.kwaliteit, offerteArtikel.status FROM (klant JOIN offertes ON klant.klantnr=offertes.klantnr) JOIN offerteArtikel ON offertes.offertenr=offerteArtikel.offertenr ORDER BY offertenr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOffertes.DataSource = bindingSource;

                for (int j = 0; j < 12; j++)
                {
                    dgvOffertes.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                dgvOffertes.Columns[1].Visible = false;
                dgvOffertes.Columns[12].Visible = false;

                dgvOffertes.CurrentCell = dgvOffertes.Rows[0].Cells[0];
                klantnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                offertenr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["offertenr"].Value.ToString());
                offerteartikelnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["artikelnr"].Value.ToString());
                firmaNaam = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                statusCode = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["status"].Value.ToString();
            }
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataOpvragenOffertesFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT offertes.offertenr, offerteArtikel.offerteartikelnr AS 'artikelnr', klant.naam, klant.klantnr, offertes.datum, offerteArtikel.ref, offerteArtikel.lengte, offerteArtikel.breedte, offerteArtikel.hoogte, offerteArtikel.aantal, offerteArtikel.prijs, offerteArtikel.kwaliteit, offerteArtikel.status FROM (klant JOIN offertes ON klant.klantnr=offertes.klantnr) JOIN offerteArtikel ON offertes.offertenr=offerteArtikel.offertenr WHERE klant.klantnr=" + klantnr + " ORDER BY offertenr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOffertes.DataSource = bindingSource;

                for (int j = 0; j < 12; j++)
                {
                    dgvOffertes.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                dgvOffertes.Columns[1].Visible = false;
                dgvOffertes.Columns[12].Visible = false;

                dgvOffertes.CurrentCell = dgvOffertes.Rows[0].Cells[0];
                klantnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                offertenr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["offertenr"].Value.ToString());
                firmaNaam = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                statusCode = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["status"].Value.ToString();
            }
            catch
            {
                //error dataOpvragenOffertesFirma
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code: dataOpvragenOffertesFirma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOffertes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvOffertes.Rows.Count - 1; i++)
            {
                string status = dgvOffertes.Rows[i].Cells["status"].Value.ToString();
                if (status == "ROOD")
                {
                    DataGridViewRow row = dgvOffertes.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 128, 128);
                }
                else
                {
                    DataGridViewRow row = dgvOffertes.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                }
            }
        }

        public void dataRefresh()
        {
            dataOpvragenOffertes();
        }

        private void dataKlantOpzoeken()
        {
            try
            {
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klantnr, naam FROM klant WHERE naam LIKE @tags;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tags", txtZoekenFirma.Text.ToUpper() + "%");
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    firmaNaam = (string)rdr["naam"];
                    klantnr = (int)rdr["klantnr"];
                }
                else
                {
                    MessageBox.Show("Er werd geen Firma gevonden", "Error");
                }
                txtZoekenFirma.Text = firmaNaam;
                cmd.Connection.Close();

                dataOpvragenOffertesFirma();
            }
            catch
            {

            }

        }

        //data naar orders

        private void dataUpdateOfferteStatus()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = "UPDATE offerteArtikel SET status='GROEN' WHERE offerteartikelnr=@offerteartikelnr";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@offerteartikelnr", MySqlDbType.Int64).Value = offerteartikelnr;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            dataRefresh();
        }

        //knoppen

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Regex.Replace(txtZoekenFirma.Text, @"\s+", "") == "")
                {
                    MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
                }
                else
                {
                    dataKlantOpzoeken();
                }
            }
        }

        public void dataClose(String naam)
        {
            if (naam == "addOfferte")
            {
                this.addOfferte = null;
            }
            if (naam == "editOfferte")
            {
                this.editOfferte = null;
            }
            if (naam == "offerteExcell")
            {
                this.offerteExcell = null;
            }
        }

        private void iconNew_Click(object sender, EventArgs e)
        {
            if (addOfferte == null)
            {
                addOfferte = new AddOfferte(main, codeUser);
                addOfferte.MdiParent = main;
            }
            addOfferte.BringToFront();
            addOfferte.Show();
        }

        private void iconEdit_Click(object sender, EventArgs e)
        {
            if (editOfferte == null)
            {
                editOfferte = new EditOfferte(klantnr, offertenr, offerteartikelnr, main);
                editOfferte.MdiParent = this.main;
            }
            editOfferte.BringToFront();
            editOfferte.Show();
        }

        private void iconExcel_Click(object sender, EventArgs e)
        {
            if (offerteExcell == null)
            {
                offerteExcell = new OfferteExcell();
                offerteExcell.MdiParent = this.main;
            }
            offerteExcell.BringToFront();
            offerteExcell.Show();
        }

        private void btnLijst_Click(object sender, EventArgs e)
        {
            dataOpvragenOffertes();
            txtZoekenFirma.Text = "";
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            if (statusCode == "ROOD")
            {
                MessageBox.Show("Deze offerte is nog nooit geplaatst als order. Als u deze wens te plaatsen als order, klik dan op het groene vinkje", "Nog geen order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ben u zeker dat u offerte " + offertenr + " van klant " + firmaNaam + " wilt toevoegen als een nieuw order?", "Order toevoegen", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Yes:
                        iconGreen.Visible = false;
                        iconEdit.Visible = false;
                        iconDelete.Visible = false;
                        iconNew.Visible = false;

                        Laden.ShowSplashScreen();
                        OfferteToOrder offerteToOrder = new OfferteToOrder(offertenr, offerteartikelnr, firmaNaam, klantnr, "Yes");
                        offerteToOrder.MdiParent = this.main;
                        Laden.CloseForm();
                        offerteToOrder.Show();

                        iconGreen.Visible = true;
                        iconEdit.Visible = true;
                        iconDelete.Visible = true;
                        iconNew.Visible = true;
                        break;
                    case DialogResult.No: break;
                    case DialogResult.Abort: break;
                }
            }
        }

        private void iconDelete_Click(object sender, EventArgs e)
        {

        }

        private void Offertes_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("offertes");
            }
            if (Application.OpenForms["AddOfferte"] != null)
            {
                (Application.OpenForms["AddOfferte"] as AddOfferte).Close();
            }
            if (Application.OpenForms["EditOfferte"] != null)
            {
                (Application.OpenForms["EditOfferte"] as EditOfferte).Close();
            }
        }

        private void iconSearch_Click(object sender, EventArgs e)
        {
            if (Regex.Replace(txtZoekenFirma.Text, @"\s+", "") == "")
            {
                MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
            }
            else
            {
                dataKlantOpzoeken();
            }
        }

        private void iconGreen_Click(object sender, EventArgs e)
        {
            if (statusCode == "GROEN")
            {
                MessageBox.Show("Deze offerte is al geplaatst als order", "Reeds order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ben u zeker dat u offerte " + offertenr + " van klant " + firmaNaam + " wilt toevoegen als order?", "Order toevoegen", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Yes:
                        iconGreen.Visible = false;
                        iconEdit.Visible = false;
                        iconDelete.Visible = false;
                        iconNew.Visible = false;

                        Laden.ShowSplashScreen();
                        OfferteToOrder offerteToOrder = new OfferteToOrder(offertenr, offerteartikelnr, firmaNaam, klantnr, "No");
                        offerteToOrder.MdiParent = this.main;
                        Laden.CloseForm();
                        offerteToOrder.Show();

                        iconGreen.Visible = true;
                        iconEdit.Visible = true;
                        iconDelete.Visible = true;
                        iconNew.Visible = true;
                        break;
                    case DialogResult.No: break;
                    case DialogResult.Abort: break;
                }
            }
        }

        private void dgvOffertes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                klantnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                offertenr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["offertenr"].Value.ToString());
                offerteartikelnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["artikelnr"].Value.ToString());
                firmaNaam = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString();
                statusCode = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["status"].Value.ToString();
            }
            catch
            {
                // error 1003
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1003", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
