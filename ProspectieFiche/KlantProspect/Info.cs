using MetroFramework.Forms;
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
    public partial class Info : MetroForm
    {
        private int klantcode;
        MySqlConnection conn;

        public Info(int klantcode)
        {
            InitializeComponent();
            this.klantcode = klantcode;
            dataOpvragen();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            
        }

        private void dataOpvragen()
        {
            //int postcode=0;
            try
            {
                bool truefalse = false;
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
                    txtGemeente.Text = (String)rdr["gemeente"];
                    txtTelefoon1.Text = (String)rdr["telefoonnummer1"];
                    txtTelefoon2.Text = (String)rdr["telefoonnummer2"];
                    txtCommentaar.Text = (String)rdr["commentaar"];
                    txtBTW.Text = (String)rdr["btwnummer"];
                    txtLand.Text = (String)rdr["land"];
                    txtProductie.Text = (String)rdr["commentaarproductie"];
                    txtFacturen.Text = (String)rdr["commentaarfacturen"];
                    truefalse = true;
                }
                cmd.Connection.Close();
                //txtPostcode.Text = postcode.ToString();

                if (truefalse == false)
                {
                    MessageBox.Show("Deze klant bestaat niet!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("Er is iets fout gelopen! Contacteer de beheerder aub!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Klanten"] != null)
            {
                (Application.OpenForms["Klanten"] as Klanten).dataClose("InfoKlanten");
            }
        }
    }
}
