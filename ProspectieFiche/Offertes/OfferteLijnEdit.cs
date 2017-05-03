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
    public partial class OfferteLijnEdit : Form
    {
        MySqlConnection conn;
        private int offerteartikelnr;

        public OfferteLijnEdit()
        {
            InitializeComponent();
        }

        public OfferteLijnEdit(int offerteartikelnr)
        {
            InitializeComponent();
            this.offerteartikelnr = offerteartikelnr;
        }

        private void dataOfferteLijnOpvragen()
        {
            try
            {
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT fefco, kwaliteit, aantal, prijs, ref, bedrukking, lengte, breedte, hoogte FROM offerteArtikel WHERE offerteartikelnr=@offerteartikelnr;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@offerteartikelnr", offerteartikelnr);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtRef.Text = (string)rdr["ref"];
                    cbFefco.Text = (string)rdr["fefco"];
                    txtLengte.Text = ((int)rdr["lengte"]).ToString();
                    txtBreedte.Text = ((int)rdr["breedte"]).ToString();
                    txtHoogte.Text = ((int)rdr["hoogte"]).ToString();
                    txtKwaliteit.Text = (string)rdr["kwaliteit"];
                    txtAantal.Text = ((int)rdr["aantal"]).ToString();
                    txtVerkoopPrijs.Text = ((int)rdr["prijs"]).ToString();
                }
                else
                {
                    MessageBox.Show("Er werd geen artikel gevonden", "Error");
                }
                cmd.Connection.Close();
            }
            catch
            {

            }
        }
    }
}
