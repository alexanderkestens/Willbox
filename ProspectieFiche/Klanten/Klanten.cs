using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProspectieFiche
{
    public partial class Klanten : MetroForm
    {
        private int codeUser;
        MySqlConnection conn;
        private int klantCode, contactCode, klantnrOpzoeken;
        private Main main;
        BindingSource bindingSource;
        private Aanmaken aanmaken = null;
        private Info info = null;
        private Aanpassen aanpassen = null;
        private Contact prospectie = null;
        private String firmaNaam;

        public Klanten(Main main, int codeUser)
        {
            klantCode = -1;
            contactCode = -1;
            this.codeUser = codeUser;
            this.main = main;
            InitializeComponent();
            dataOpvragenKlanten();
            
        }

        //Data opvragen

        //error 1002
        private void dataOpvragenKlanten()
        {
            try
            {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klantnr, naam, postcode, aanmaakdatum FROM klant WHERE type=1 ORDER BY naam ASC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvKlanten.DataSource = bindingSource;
                for (int j = 0; j < 4; j++)
                {
                    dgvKlanten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvKlanten.CurrentCell = dgvKlanten.Rows[0].Cells[0];
                firmaNaam = dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                klantCode = int.Parse(dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                lblContact.Text = "Contact: " + firmaNaam;
            } catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dataOpvragenKlantenFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klantnr, naam, postcode, aanmaakdatum FROM klant WHERE type=1 AND naam LIKE '" + txtZoekenFirma.Text.ToUpper() + "%' ORDER BY naam ASC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvKlanten.DataSource = bindingSource;

                dgvKlanten.DataSource = bindingSource;
                for (int j = 0; j < 4; j++)
                {
                    dgvKlanten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvKlanten.CurrentCell = dgvKlanten.Rows[0].Cells[0];
                firmaNaam = dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                klantCode = int.Parse(dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                lblContact.Text = "Contact: " + firmaNaam;
            }
            catch
            {
                //error 1002
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //error 1000
        private void dataOpvragenDataGrid()
        {
            try {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT prospectienr, datum, contactpersoon, duur, type FROM prospectie WHERE klantnr=" + klantCode + " ORDER BY datum DESC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvContacten.DataSource = bindingSource;
                for (int j = 0; j < 5; j++)
                {
                    dgvContacten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvContacten.CurrentCell = dgvContacten.Rows[0].Cells[0];
                var contactCode1 = dgvContacten.Rows[dgvContacten.CurrentCell.RowIndex].Cells["prospectienr"].Value.ToString();
                contactCode = int.Parse(contactCode1);

                /*var prospectienr = dgvContacten.Rows[dgvContacten.CurrentCell.RowIndex].Cells["prospectienr"].Value.ToString();
            contactCode = int.Parse(prospectienr);
            dataOpvragenCommentaar();*/
            } catch
            {
                //error 1000
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        // error 1001
        private void dataOpvragenCommentaar()
        {
            try
            {
            txtCommentaar.Text = "";

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT commentaar FROM prospectie WHERE klantnr=" + klantCode + " AND prospectienr=" + contactCode + " ORDER BY datum DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            txtCommentaar.Text = cmd.ExecuteScalar().ToString();
            } catch
            {
                // error 1001
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1001", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void dataRefresh(int klantnrSelectRow)
        {
            dataOpvragenKlanten();
            dataOpvragenDataGrid();
            dataOpvragenCommentaar();
            /*int rowIndex = -1;
            foreach (DataGridViewRow row in dgvKlanten.Rows)
            {
                if (row.Cells["klantnr"].Value.ToString().Equals(klantnrSelectRow))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            dgvKlanten.Rows[rowIndex].Selected = true;*/
        }

        //knoppen

        public void dataClose(String naam)
        {
            if (naam == "AanmakenKlanten")
            {
                this.aanmaken = null;
            }
            if (naam == "InfoKlanten")
            {
                this.info = null;
            }
            if (naam == "EditKlanten")
            {
                this.aanpassen = null;
            }
            if (naam == "ContactKlanten")
            {
                this.prospectie = null;
            }
            
        }

        private void iconNew_Click(object sender, EventArgs e)
        {
            if (aanmaken == null)
            {
                aanmaken = new Aanmaken(1, codeUser);
                aanmaken.MdiParent = this.main;
            }
            aanmaken.BringToFront();           
            aanmaken.Show();
        }

        private void iconInfo_Click(object sender, EventArgs e)
        {
            if (info == null)
            {
                info = new Info(klantCode);
                info.MdiParent = this.main;
            }
            info.BringToFront();
            info.Show();
        }

        private void iconEdit_Click(object sender, EventArgs e)
        {
            if (aanpassen == null)
            {
                aanpassen = new Aanpassen(klantCode, 1);
                aanpassen.MdiParent = this.main;
            }
            aanpassen.BringToFront();
            aanpassen.Show();
        }

        private void iconContact_Click(object sender, EventArgs e)
        {
            if (klantCode == -1)
            {
                MessageBox.Show("Zoek eerst een klant, aub! Of maak een nieuwe aan", "Geen klant geselecteerd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (prospectie == null)
                {
                    prospectie = new Contact(klantCode, codeUser, 1);
                    prospectie.MdiParent = this.main;
                }
                prospectie.BringToFront();
                prospectie.Show();
            }
        }

        //cellClick

        // error 1003
        private void dgvKlanten_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                klantCode = int.Parse(dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmaNaam = dgvKlanten.Rows[dgvKlanten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                dataOpvragenDataGrid();
                dataOpvragenCommentaar();
                lblContact.Text = "Contact: " + firmaNaam;
            }
            catch
            {
                // error 1003
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1003", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // error 1005
        private void dgvContacten_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var prospectienr = dgvContacten.Rows[dgvContacten.CurrentCell.RowIndex].Cells["prospectienr"].Value.ToString();
                contactCode = int.Parse(prospectienr);

                dataOpvragenCommentaar();
            }
            catch
            {
                // error 1005
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1005", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    dataOpvragenKlantenFirma();
                }
        }

        private void Klanten_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("klant");
            }
            /* (Application.OpenForms["Aanmaken"] != null)
            {
                (Application.OpenForms["Aanmaken"] as Aanmaken).Close();
            }
            if (Application.OpenForms["Aanpassen"] != null)
            {
                (Application.OpenForms["Aanpassen"] as Aanpassen).Close();
            }
            if (Application.OpenForms["Info"] != null)
            {
                (Application.OpenForms["Info"] as Info).Close();
            }
            if (Application.OpenForms["Contact"] != null)
            {
                (Application.OpenForms["Contact"] as Contact).Close();
            }*/
        }

        private void btnLijst_Click(object sender, EventArgs e)
        {
            dataOpvragenKlanten();
            txtZoekenFirma.Text = "";
        }

        private void txtZoekenFirma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Regex.Replace(txtZoekenFirma.Text, @"\s+", "") == "")
                {
                    MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
                }
                else
                {
                    dataOpvragenKlantenFirma();
                }
            }
        }

        // error 1004
        private void Klanten_Load(object sender, EventArgs e)
        {
            try
            {               
                    var klantnr = dgvKlanten.Rows[0].Cells["klantnr"].Value.ToString();
                    klantCode = int.Parse(klantnr);
                    dataOpvragenDataGrid();
                    dataOpvragenCommentaar();
            } catch
            {
                // error 1004
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1004", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
