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
    public partial class Aanpassen : MetroForm
    {
        private int klantcode;
        private int typeAanmaken;
        private String gemeente;
        private String BTW;
        MySqlConnection conn;
        ErrorProvider errorProvider1;

        public Aanpassen(int klantcode, int typeAanmaken)
        {
            InitializeComponent();
            this.klantcode = klantcode;
            this.typeAanmaken = typeAanmaken;
            errorProvider1 = new ErrorProvider();
            dataOpvragen();
            
            if (cbLand.Text == "Belgie")
            {
                cbGemeente.Visible = true;
                txtGemeente.Visible = false;
                dataGemeenteOpzoeken();
            }
            else
            {
                cbGemeente.Visible = false;
                txtGemeente.Visible = true;
            }
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
            }

            if (cbLand.Text == "Belgie")
            {
                cbGemeente.Text = gemeente;
            }
            else
            {
                txtGemeente.Text = gemeente;
            }
            cmd.Connection.Close();
            /*}
            catch
            {

            }*/
        }

        private void dataOpvragen()
        {
            //int postcode = 0;
            //try
            //{
                bool truefalse = false;
                string BTW = "";

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT * FROM klant WHERE klantnr=@klantnr;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantcode;
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtFirma.Text = (String)rdr["naam"];
                    txtAdres.Text = (String)rdr["adres"];
                    txtEmail1.Text = (String)rdr["email1"];
                    txtEmail2.Text = (String)rdr["email2"];
                    txtPostcode.Text = (String)rdr["postcode"];
                    txtWebsite.Text = (String)rdr["website"];                    
                    txtTelefoon1.Text = (String)rdr["telefoonnummer1"];
                    txtTelefoon2.Text = (String)rdr["telefoonnummer2"];
                    txtCommentaar.Text = (String)rdr["commentaar"];
                    BTW = (String)rdr["btwnummer"];
                    cbLand.Text = (String)rdr["land"];
                    gemeente = (String)rdr["gemeente"];
                    txtProductie.Text = (String)rdr["commentaarproductie"];
                    txtFacturen.Text = (String)rdr["commentaarfacturen"];
                    truefalse = true;
                }

                if (!(BTW == ""))
                {
                    txtBTW.Text = BTW.Remove(0, 2);
                }

                cmd.Connection.Close();
                //txtPostcode.Text = postcode.ToString();

                if (truefalse == false)
                {
                    MessageBox.Show("Deze klant bestaat niet!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            /*}
            catch
            {
                MessageBox.Show("Er is iets fout gelopen! Contacteer de beheerder aub!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void updateData()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            //int code = int.Parse(txtCodeVerlopig.Text);
            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "UPDATE klant SET naam=@naam, adres=@adres, gemeente=@gemeente, postcode=@postcode, telefoonnummer1=@telefoonnummer1, telefoonnummer2=@telefoonnummer2, email1=@email1, email2=@email2, website=@website, land=@land, btwnummer=@btwnummer, commentaar=@commentaar, commentaarproductie=@commentaarproductie, commentaarfacturen=@commentaarfacturen WHERE klantnr=@klantnr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@naam", txtFirma.Text.Trim());
            cmd.Parameters.AddWithValue("@klantnr", this.klantcode);
            cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
            if (cbLand.Text == "Belgie")
            {
                cmd.Parameters.AddWithValue("@gemeente", cbGemeente.Text);
            } else
            {
                cmd.Parameters.AddWithValue("@gemeente", txtGemeente.Text);
            }             
            cmd.Parameters.AddWithValue("@postcode", txtPostcode.Text.Trim());
            cmd.Parameters.AddWithValue("@telefoonnummer1", txtTelefoon1.Text);
            cmd.Parameters.AddWithValue("@telefoonnummer2", txtTelefoon2.Text);
            cmd.Parameters.AddWithValue("@email1", txtEmail1.Text);
            cmd.Parameters.AddWithValue("@email2", txtEmail2.Text);
            cmd.Parameters.AddWithValue("@website", txtWebsite.Text);
            cmd.Parameters.AddWithValue("@land", cbLand.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@btwnummer", BTW);
            cmd.Parameters.AddWithValue("@commentaar", txtCommentaar.Text);
            cmd.Parameters.AddWithValue("@commentaarproductie", txtProductie.Text);
            cmd.Parameters.AddWithValue("@commentaarfacturen", txtFacturen.Text);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("De nieuwe data is toegevoegd!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            if (typeAanmaken == 1)
            {
                if (Application.OpenForms["Klanten"] != null)
                {
                    (Application.OpenForms["Klanten"] as Klanten).dataRefresh(klantcode);
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

        //knoppen

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool truefalse = true;
            errorProvider1.Clear();

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
                            MessageBox.Show("De BTW code voldoet niet aan de validatie", "Error BTW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case "NL":
                        Regex pattern1 = new Regex("^NL[A-Z0-9]{9,9}B[A-Z0-9]{2,2}$");
                        if (!(pattern1.IsMatch(BTW)))
                        {
                            truefalse = false;
                            MessageBox.Show("De BTW code voldoet niet aan de validatie", "Error BTW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case "FR":
                        Regex pattern2 = new Regex("^FR[A-Z0-9]{2,2}[0-9]{9,9}$");
                        if (!(pattern2.IsMatch(BTW)))
                        {
                            truefalse = false;
                            MessageBox.Show("De BTW code voldoet niet aan de validatie", "Error BTW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
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
                updateData();
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

        private void Aanpassen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Klanten"] != null)
            {
                (Application.OpenForms["Klanten"] as Klanten).dataClose("EditKlanten");
            }
        }

        private void Aanpassen_Load(object sender, EventArgs e)
        {

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
    }
}
