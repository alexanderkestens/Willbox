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
    public partial class AddFactuur : Form
    {
        MySqlConnection conn;
        private String firmaNaam;
        private int klantnr;

        public AddFactuur()
        {
            InitializeComponent();
            txtFactuurnr.Text = codeOpzoekenFacturen().ToString();
        }

        private int codeOpzoekenFacturen()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM facturen;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["factuurnr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void dataToevoegenFacturen()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT facturen (factuurnr, klantnr, ordernr, factuurdatum, exclusiefbtw, btw, inclusiefbtw, naam) VALUES (@factuurnr, @klantnr, @ordernr, @factuurdatum, @exclusiefbtw, @btw, @inclusiefbtw, @naam)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@factuurnr", MySqlDbType.Int64).Value = txtFactuurnr.Text;
            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantnr;
            cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = txtFirma.Text;
            cmd.Parameters.Add("@ordernr", MySqlDbType.Int64).Value = 0;
            cmd.Parameters.Add("@factuurdatum", MySqlDbType.DateTime).Value = dtpDatum.Value;
            cmd.Parameters.Add("@exclusiefbtw", MySqlDbType.Float).Value = 0.0;
            cmd.Parameters.Add("@btw", MySqlDbType.Float).Value = 0.0;
            cmd.Parameters.Add("@inclusiefbtw", MySqlDbType.Float).Value = double.Parse(txtTotaal.Text);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
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

                cmd.Parameters.AddWithValue("@tags", txtFirma.Text.ToUpper() + "%");
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
                txtFirma.Text = firmaNaam;
                cmd.Connection.Close();
            }
            catch
            {

            }
        }

        //knoppen

        private void AddFactuur_Load(object sender, EventArgs e)
        {

        }

        private void btnToevoegen_Click(object sender, EventArgs e)
        {
            dataToevoegenFacturen();
            this.Close();
        }

        private void txtFirma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Regex.Replace(txtFirma.Text, @"\s+", "") == "")
                {
                    MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
                }
                else
                {
                    dataKlantOpzoeken();
                }
            }
        }
    }
}
