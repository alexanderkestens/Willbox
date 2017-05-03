using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProspectieFiche
{
    public partial class Contact : Form
    {
        private String terugcontacterenYN = "Y";
        private int codeUser;
        private int typeAanmaken;
        MySqlConnection conn;
        private int klantCode;

        public Contact()
        {
            InitializeComponent();
        }

        public Contact(int klantCode, int codeUser, int typeAanmaken)
        {
            this.codeUser = codeUser;
            this.klantCode = klantCode;
            this.typeAanmaken = typeAanmaken;
            InitializeComponent();
        }

        private void dataToevoegen()
        {
            string theDate = dtpTerugcontacteren.Value.ToString("dd-MM-yyyy");
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            var code = codeOpzoeken().ToString();

            //int code = int.Parse(txtCodeVerlopig.Text);
            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT prospectie (prospectienr, klantnr, contactpersoon, duur, type, commentaar, datum, code, terugcontacteren, terugcontacterenYN, terugcontacterenvia) VALUES (@prospectienr, @klantnr, @contactpersoon, @duur, @type, @commentaar, @datum, @code, @terugcontacteren, @terugcontacterenYN, @terugcontacterenvia)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@prospectienr", MySqlDbType.Int64).Value = Int64.Parse(code);
            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantCode;
            cmd.Parameters.Add("@contactpersoon", MySqlDbType.Text).Value = txtContactPersoon.Text.ToUpper();
            cmd.Parameters.Add("@duur", MySqlDbType.Text).Value = cbDuurGesprek.SelectedItem;
            cmd.Parameters.Add("@type", MySqlDbType.Text).Value = cbTypeGesprek.SelectedItem;
            cmd.Parameters.Add("@commentaar", MySqlDbType.Text).Value = txtCommentaar.Text;
            cmd.Parameters.Add("@datum", MySqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@code", MySqlDbType.Int64).Value = codeUser;
            cmd.Parameters.Add("@terugcontacteren", MySqlDbType.Text).Value = theDate;
            cmd.Parameters.Add("@terugcontacterenYN", MySqlDbType.Text).Value = terugcontacterenYN;
            cmd.Parameters.Add("@terugcontacterenvia", MySqlDbType.Text).Value = cbContacterenVia.SelectedItem;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("De data is verstuurd!", "Verstuurd", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (typeAanmaken == 1)
            {
                if (Application.OpenForms["Klanten"] != null)
                {
                    (Application.OpenForms["Klanten"] as Klanten).dataRefresh(klantCode);
                }
            }
            else
            {
                if (Application.OpenForms["Prospecties"] != null)
                {
                    (Application.OpenForms["Prospecties"] as Prospecties).dataRefresh();
                }
            }
        }

        private object codeOpzoeken()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM prospectie;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["prospectienr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void btnVerstuur_Click(object sender, EventArgs e)
        {
            if (txtContactPersoon.Text == "")
            {
                MessageBox.Show("Gelieven het veld contactpersoon in te vullen!", "Veld contactpersoon");
            }
            else
            {
            dataToevoegen();
            this.Close();
            }          
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked == true)
            {
                dtpTerugcontacteren.Visible = false;
                lblVia.Visible = false;
                cbContacterenVia.Visible = false;
                terugcontacterenYN = "N";
            }
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYes.Checked == true)
            {
                dtpTerugcontacteren.Visible = true;
                lblVia.Visible = true;
                cbContacterenVia.Visible = true;
                terugcontacterenYN = "Y";
            }
        }

        private void Contact_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Klanten"] != null)
            {
                (Application.OpenForms["Klanten"] as Klanten).dataClose("ContactKlanten");
            }
        }
    }
}
