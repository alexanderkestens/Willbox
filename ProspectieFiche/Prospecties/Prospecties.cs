using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ProspectieFiche
{
    public partial class Prospecties : Form
    {
        private int codeUser;
        MySqlConnection conn;
        private int klantCode, contactCode, klantnrOpzoeken;
        private String firmanaam;
        private Main main;
        BindingSource bindingSource;

        public Prospecties(Main main, int codeUser)
        {
            this.codeUser = codeUser;
            klantCode = -1;
            contactCode = -1;
            this.main = main;
            InitializeComponent();
            dataOpvragenProspectie();
        }

        // Data opvragen

        private void dataOpvragenProspectie()
        {
            try
            {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klantnr, naam, postcode, aanmaakdatum FROM klant WHERE type=2 ORDER BY naam ASC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvProspecten.DataSource = bindingSource;
                for (int j = 0; j < 4; j++)
                {
                    dgvProspecten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvProspecten.CurrentCell = dgvProspecten.Rows[0].Cells[0];
                klantCode = int.Parse(dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmanaam = dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                lblContact.Text = "Contact: " + firmanaam;
            } catch
            {
                // error 2007
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2007", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataOpvragenProspectieFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klantnr, naam, postcode, aanmaakdatum FROM klant WHERE type=2 AND naam LIKE '" + txtZoekenFirma.Text.ToUpper() + "%' ORDER BY naam ASC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvProspecten.DataSource = bindingSource;
                for (int j = 0; j < 4; j++)
                {
                    dgvProspecten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvProspecten.CurrentCell = dgvProspecten.Rows[0].Cells[0];
                klantCode = int.Parse(dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmanaam = dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                lblContact.Text = "Contact: " + firmanaam;
            }
            catch
            {
                //error 1002
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // code 2008
        private void dataOpvragenDataGrid()
        {
            try
            {
            BindingSource bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT prospectienr, datum, contactpersoon, duur, type FROM prospectie WHERE klantnr=" + klantCode + " ORDER BY datum DESC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            dgvContacten.DataSource = bindingSource1;
                for (int j = 0; j < 5; j++)
                {
                    dgvContacten.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvContacten.CurrentCell = dgvContacten.Rows[0].Cells[0];
                var contactCode1 = dgvContacten.Rows[dgvContacten.CurrentCell.RowIndex].Cells["prospectienr"].Value.ToString();
                contactCode = int.Parse(contactCode1);
            } catch
            {
                // error 2008
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2008", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // code 2006
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
            }
            catch
            {
                // error 2006
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2006", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void dataRefresh()
        {
            dataOpvragenProspectie();
            dataOpvragenDataGrid();
            dataOpvragenCommentaar();
        }

        // code 2005
        private void dataKlantMaken()
        {
            try
            {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            //int code = int.Parse(txtCodeVerlopig.Text);
            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "UPDATE klant SET type=@type WHERE klantnr=@klantnr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@type", 1);
            cmd.Parameters.AddWithValue("@klantnr", this.klantCode);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("De nieuwe klant is aangemaakt!", "Klant aangemaakt!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch
            {
                // error 2005
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2005", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Knoppen

        private void iconNew_Click(object sender, EventArgs e)
        {
            Aanmaken aanmaken = new Aanmaken(2, codeUser);
            aanmaken.MdiParent = this.main;
            aanmaken.Show();
        }

        private void iconContact_Click(object sender, EventArgs e)
        {
            if (klantCode == -1)
            {
                MessageBox.Show("Zoek eerst een prospectie, aub! Of maak een nieuwe aan", "Geen prospect geselecteerd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Contact prospectie = new Contact(klantCode, codeUser, 2);
                prospectie.MdiParent = this.main;
                prospectie.Show();
            }
        }

        private void iconEdit_Click(object sender, EventArgs e)
        {
            Aanpassen aanpassen = new Aanpassen(klantCode, 2);
            aanpassen.MdiParent = this.main;
            aanpassen.Show();
        }

        private void iconInfo_Click(object sender, EventArgs e)
        {
            Info info = new Info(klantCode);
            info.MdiParent = this.main;
            info.Show();
        }

        private void iconMakeCustomer_Click(object sender, EventArgs e)
        {
            if (klantCode == -1)
            {
                MessageBox.Show("Zoek eerst een klant, aub! Of maak een nieuwe aan", "Geen klant geselecteerd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ben u zeker dat u prospect " + dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["naam"].Value.ToString() +  " klant wilt maken?", "Klant maken?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (dr)
                {
                    case DialogResult.Yes:
                        dataKlantMaken();
                        dataRefresh();
                        break;
                    case DialogResult.No: break;
                }
            }
        }

        // cellClick

        // code 2003
        private void dgvProspecten_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                klantCode = int.Parse(dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmanaam = dgvProspecten.Rows[dgvProspecten.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                dataOpvragenDataGrid();
                dataOpvragenCommentaar();
                lblContact.Text = "Contact: " + firmanaam; 
            }
            catch
            {
                // error 2003
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2003", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        // code 2002
        private void dgvContacten_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var prospectienr = dgvContacten.Rows[dgvContacten.CurrentCell.RowIndex].Cells["prospectienr"].Value.ToString();
                contactCode = int.Parse(prospectienr);
                dataOpvragenCommentaar();
            }
            catch
            {
                // error 2002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2002", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Prospecties_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("prospecties");
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
                dataOpvragenProspectieFirma();
            }
        }

        private void btnLijst_Click(object sender, EventArgs e)
        {
            dataOpvragenProspectie();
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
                    dataOpvragenProspectieFirma();
                }
            }
        }

        // code 2001
        private void Prospecties_Load(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(dgvProspecten.Rows[0].Cells["klantnr"].Value.ToString()) == 0)
                {
                    MessageBox.Show("Er zijn geen prospecten beschikbaar", "Geen prospecten", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var klantnr = dgvProspecten.Rows[0].Cells["klantnr"].Value.ToString();
                    klantCode = int.Parse(klantnr);
                    dataOpvragenDataGrid();
                    dataOpvragenCommentaar();
                }
            }
            catch
            {
                // error 2001
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:2001", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
