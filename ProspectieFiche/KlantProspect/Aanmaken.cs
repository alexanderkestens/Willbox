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
    public partial class Aanmaken : MetroForm
    {
        private int codeUser;
        MySqlConnection conn;
        private string BTW;
        private int typeAanmaken;
        ErrorProvider errorProvider1;

        public Aanmaken()
        {
            InitializeComponent();
        }

        public Aanmaken(int typeAanmaken, int codeUser)
        {
            InitializeComponent();
            this.typeAanmaken = typeAanmaken;
            this.codeUser = codeUser;
            errorProvider1 = new ErrorProvider();
            if (typeAanmaken == 1)
            {
                this.Text = "Nieuwe klant aanmaken";
            }
            else
            {
                this.Text = "Nieuwe prospect aanmaken";
            }
            cbLand.Text = "Belgie";
        }

        private void dataToevoegen()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            var code = codeOpzoeken().ToString();

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT klant (klantnr, naam, adres, gemeente, postcode, telefoonnummer1, telefoonnummer2, email1, email2, website, commentaar, type, aanmaakdatum, code, land, btwnummer, commentaarproductie, commentaarfacturen) VALUES (@klantnr, @naam, @adres, @gemeente, @postcode, @telefoonnummer1, @telefoonnummer2, @email1, @email2, @website, @commentaar, @type, @aanmaakdatum, @code, @land, @btwnummer, @commentaarproductie, @commentaarfacturen)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = Int64.Parse(code);
            cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = txtFirma.Text.ToUpper().Trim();
            cmd.Parameters.Add("@adres", MySqlDbType.Text).Value = txtAdres.Text.ToUpper();
            if (cbLand.Text == "Belgie")
            {
                cmd.Parameters.Add("@gemeente", MySqlDbType.Text).Value = cbGemeente.Text.ToUpper();
            }
            else
            {
                cmd.Parameters.Add("@gemeente", MySqlDbType.Text).Value = txtGemeente.Text.ToUpper();
            }
            cmd.Parameters.Add("@postcode", MySqlDbType.Text).Value = txtPostcode.Text.Trim();
            cmd.Parameters.Add("@telefoonnummer1", MySqlDbType.Text).Value = txtTelefoon1.Text;
            cmd.Parameters.Add("@telefoonnummer2", MySqlDbType.Text).Value = txtTelefoon2.Text;
            cmd.Parameters.Add("@email1", MySqlDbType.Text).Value = txtEmail1.Text;
            cmd.Parameters.Add("@email2", MySqlDbType.Text).Value = txtEmail2.Text;
            cmd.Parameters.Add("@website", MySqlDbType.Text).Value = txtWebsite.Text;
            cmd.Parameters.Add("@commentaar", MySqlDbType.Text).Value = txtCommentaar.Text;
            cmd.Parameters.Add("@type", MySqlDbType.Int64).Value = typeAanmaken;
            cmd.Parameters.Add("@aanmaakdatum", MySqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@code", MySqlDbType.Int64).Value = codeUser;
            cmd.Parameters.Add("@land", MySqlDbType.Text).Value = cbLand.SelectedItem.ToString();
            cmd.Parameters.Add("@btwnummer", MySqlDbType.Text).Value = BTW;
            cmd.Parameters.Add("@commentaarproductie", MySqlDbType.Text).Value = txtProductie.Text;
            cmd.Parameters.Add("@commentaarfacturen", MySqlDbType.Text).Value = txtFacturen.Text;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (typeAanmaken == 1)
            {
                MessageBox.Show("Klant " + txtFirma.Text + " is toegevoegd!", "Toegevoegd!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Prospect " + txtFirma.Text + " is toegevoegd!", "Toegevoegd!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();

            if (typeAanmaken == 1)
            {
                if (Application.OpenForms["Klanten"] != null)
                {
                    (Application.OpenForms["Klanten"] as Klanten).dataRefresh(int.Parse(code));
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

        private bool checkKlant()
        {
            bool trueFalse = true;

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT naam FROM klant WHERE naam=@naam";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = txtFirma.Text.ToUpper();

            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                trueFalse = false;
            }

            cmd.Connection.Close();
            return trueFalse;
        }

        private int codeOpzoeken()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM klant;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["klantnr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void dataGemeenteOpzoeken()
        {
            //try
            //{
            cbGemeente.Items.Clear();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT gemeente FROM gemeentes WHERE postcode=@postcode;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@postcode", txtPostcode.Text);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                cbGemeente.Items.Add((string)rdr["gemeente"].ToString().ToUpper());
                cbGemeente.Text = cbGemeente.Items[0].ToString();
            }

            cmd.Connection.Close();
            /*}
            catch
            {

            }*/

        }

        //knoppen

        private void btnMaken_Click(object sender, EventArgs e)
        {
            //int distance;
            errorProvider1.Clear();
            lblError.Text = "";
            bool truefalse = true;
            bool checkBestaandeKLant = checkKlant();

            BTW = txtBTWCode.Text + Regex.Replace(txtBTW.Text, @"\s+", "");

            if (txtBTW.Text == "")
            {
                BTW = "";
            }
            else
            {
                switch (txtBTWCode.Text)
                {
                    case "BE":
                        Regex pattern = new Regex("^BE[0-9]{10,10}$");
                        if (!(pattern.IsMatch(BTW)))
                        {
                            truefalse = false;
                            lblError.Text = "De BTW code voldoet niet aan de validatie";
                        }
                        break;
                    case "NL":
                        Regex pattern1 = new Regex("^NL[A-Z0-9]{9,9}B[A-Z0-9]{2,2}$");
                        if (!(pattern1.IsMatch(BTW)))
                        {
                            truefalse = false;
                            lblError.Text = "De BTW code voldoet niet aan de validatie";
                        }
                        break;
                    case "FR":
                        Regex pattern2 = new Regex("^FR[A-Z0-9]{2,2}[0-9]{9,9}$");
                        if (!(pattern2.IsMatch(BTW)))
                        {
                            truefalse = false;
                            lblError.Text = "De BTW code voldoet niet aan de validatie";
                        }
                        break;
                }
            }


            if (checkBestaandeKLant == false)
            {
                lblError.Text = "Deze firma bestaat al reeds";
                truefalse = false;
            }
            if (txtFirma.Text == "")
            {
                errorProvider1.SetError(txtFirma, "Gelieve het veld in te vullen met een geldige waarde");
                truefalse = false;
            }
            if (txtAdres.Text == "")
            {
                errorProvider1.SetError(txtAdres, "Gelieve het veld in te vullen met een geldige waarde");
                truefalse = false;
            }
            if (txtPostcode.Text == "" || txtPostcode.Text.Length < 4)
            {
                errorProvider1.SetError(txtPostcode, "Gelieve het veld in te vullen met een geldige waarde");
                truefalse = false;
            }
            if (truefalse != false)
            {
                dataToevoegen();
            }
        }

        private void cbLand_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLand.SelectedItem.ToString())
            {
                case "Belgie":
                    txtBTWCode.Text = "BE";
                    cbGemeente.Visible = true;
                    txtGemeente.Visible = false;
                    break;
                case "Nederland":
                    txtBTWCode.Text = "NL";
                    cbGemeente.Visible = false;
                    txtGemeente.Visible = true;
                    break;
                case "Frankrijk":
                    txtBTWCode.Text = "FR";
                    cbGemeente.Visible = false;
                    txtGemeente.Visible = true;
                    break;
            }
        }

        private void txtPostcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbLand.Text == "Belgie")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtBTW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Aanmaken_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Klanten"] != null)
            {
                (Application.OpenForms["Klanten"] as Klanten).dataClose("AanmakenKlanten");
            }
        }

        private void txtPostcode_KeyUp(object sender, KeyEventArgs e)
        {
            int lengte = txtPostcode.Text.Length;
            if (lengte == 4)
            {
                dataGemeenteOpzoeken();
            }
            if (lengte < 4 || lengte > 4)
            {
                cbGemeente.Text = "";
                cbGemeente.Items.Clear();
            }
        }

        private void Aanmaken_Load(object sender, EventArgs e)
        {

        }
    }
}

/* BTW nummer
at       ^ATU[A-Z0-9]{8,8}$
be       ^BE[0-9]{10,10}$
cy       ^CY[0-9]{9,9}$
cz       ^CZ[0-9]{8,10}$
de       ^DE[0-9]{9,9}$
dk       ^DK[0-9]{8,8}$
ee       ^EE[0-9]{9,9}$
es       ^ES[A-Z0-9]{1,1}[0-9]{7,7}[A-Z0-9]{1,1}$
fi       ^FI[0-9]{8,8}$
fr       ^FR[A-Z0-9]{2,2}[0-9]{9,9}$
gb       ^GB[0-9]{9,9}$|^GB[0-9]{12,12}$|^GBGD[0-9]{3,3}$
hu       ^HU[0-9]{8,8}$
ie       ^IE[A-Z0-9]{8,8}$
it       ^IT[0-9]{11,11}$
lt       ^LT[0-9]{9,9}$|^LT[0-9]{12,12}$
lu       ^LU[0-9]{8,8}$
lv       ^LV[0-9]{11,11}$
mt       ^MT[0-9]{8,8}$
nl       ^NL[A-Z0-9]{9,9}B[A-Z0-9]{2,2}$
pl       ^PL[0-9]{10,10}$
pt       ^PT[0-9]{9,9}$
se       ^SE[0-9]{12,12}$
si       ^SI[0-9]{8,8}$
sk       ^SK[0-9]{10,10}$
*/
