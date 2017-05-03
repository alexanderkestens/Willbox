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
    public partial class Gondardennes : Form
    {
        MySqlConnection conn;
        private string kwaliteitcode;
        private int ordernr, orderartikelnr;

        public Gondardennes()
        {
            InitializeComponent();
        }

        public Gondardennes(int ordernr)
        {
            this.ordernr = ordernr;
            InitializeComponent();
            dataOpvragenGondardennes();
            if (lblBesteld.Text == "Y")
            {
                lblBesteld.Text = "Besteld";
                lblBesteld.ForeColor = Color.Green;
            }
            else
            {
                lblBesteld.Text = "Niet Besteld";
                lblBesteld.ForeColor = Color.Red;
            }
            kwaliteitcode = Regex.Replace(txtKwaliteit.Text, @"[\d-]", string.Empty).ToUpper();
            kwaliteitcode = Regex.Replace(kwaliteitcode, @"\s+", "");
            txtBreedteplaat.Text = berekenBreedtePlaat(int.Parse(txtLengte.Text), int.Parse(txtBreedte.Text), int.Parse(txtHoogte.Text), kwaliteitcode).ToString();
            txtLengteplaat.Text = berekenLengtePlaat(int.Parse(txtLengte.Text), int.Parse(txtBreedte.Text), int.Parse(txtHoogte.Text), kwaliteitcode).ToString();
            if (txtHoogte.Text != "0")
            {
                txtRillen1.Text = berekenRillenFlap(int.Parse(txtBreedte.Text), kwaliteitcode).ToString();
                txtRillen2.Text = berekenRillenHoogte(int.Parse(txtHoogte.Text), kwaliteitcode).ToString();
                txtRillen3.Text = berekenRillenFlap(int.Parse(txtBreedte.Text), kwaliteitcode).ToString();
            }
        }

        private double berekenBreedtePlaat(int lengte, int breedte, int hoogte, string kwaliteit)
        {
            double breedtePlaat = 0;

            if (kwaliteit == "B")
            {
                breedtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 6) + ((0.5 * breedte) + 2));
            }

            if (kwaliteit == "BC")
            {
                breedtePlaat = (((0.5 * breedte) + 4) + ((1 * hoogte) + 14) + ((0.5 * breedte) + 4));
            }

            if (kwaliteit == "C")
            {
                breedtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 8) + ((0.5 * breedte) + 2));
            }

            if (kwaliteit == "E")
            {
                breedtePlaat = (((0.5 * breedte) + 1) + ((1 * hoogte) + 3) + ((0.5 * breedte) + 1));
            }

            return breedtePlaat;
        }

        private double berekenLengtePlaat(int lengte, int breedte, int hoogte, string kwaliteit)
        {
            int lengtePlaat = 0;

            if (kwaliteit == "B")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 12 + 35 + 15);
            }

            if (kwaliteit == "BC")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 28 + 35 + 20);
            }

            if (kwaliteit == "C")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 16 + 35 + 15);
            }

            if (kwaliteit == "E")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 8 + 35 + 15);
            }

            return lengtePlaat;
        }

        private double berekenRillenFlap(int breedte, string kwaliteit)
        {
            double rillen1 = 0;
            if (kwaliteit == "B")
            {
                rillen1 = (0.5 * breedte) + 2;
            }
            if (kwaliteit == "BC")
            {
                rillen1 = (0.5 * breedte) + 4;
            }
            if (kwaliteit == "C")
            {
                rillen1 = (0.5 * breedte) + 2;
            }
            if (kwaliteit == "E")
            {
                rillen1 = (0.5 * breedte) + 1;
            }
            return rillen1;
        }

        private double berekenRillenHoogte(int hoogte, string kwaliteit)
        {
            double rillen1 = 0;
            if (kwaliteit == "B")
            {
                rillen1 = (1 * hoogte) + 6;
            }
            if (kwaliteit == "BC")
            {
                rillen1 = (1 * hoogte) + 14;
            }
            if (kwaliteit == "C")
            {
                rillen1 = (1 * hoogte) + 8;
            }
            if (kwaliteit == "E")
            {
                rillen1 = (1 * hoogte) + 3;
            }
            return rillen1;
        }

        private void dataOpvragenGondardennes()
        {
            /*try
            {*/
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT orders.ordernr, klant.naam, klant.klantnr, orders.datum, orders.leveringstermijn, orderArtikel.orderartikelnr, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.ref, orderArtikel.fefco, orderArtikel.kwaliteit, orderArtikel.gonbesteld FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orders.ordernr=" + ordernr + " ORDER BY ordernr ASC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string kwal= "";
            while (rdr.Read())
            {
                txtOrdernr.Text = ((int)rdr["ordernr"]).ToString();
                txtKlantnr.Text = ((int)rdr["klantnr"]).ToString();
                txtFirma.Text = (String)rdr["naam"];
                txtRef.Text = (String)rdr["ref"];
                txtLengte.Text = ((int)rdr["lengte"]).ToString();
                txtBreedte.Text = ((int)rdr["breedte"]).ToString();
                txtHoogte.Text = ((int)rdr["hoogte"]).ToString();
                txtAantal.Text = ((int)rdr["aantal"]).ToString();
                kwal = (String)rdr["kwaliteit"];
                txtFefco.Text = (String)rdr["fefco"];
                lblBesteld.Text = (String)rdr["gonbesteld"];
                orderartikelnr = (int)rdr["orderartikelnr"];
            }
            txtKwaliteit.Text = kwal;
            conn.Close();
            /*}
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void dataUpdateGondardennesStatus()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = "UPDATE orderArtikel SET gonbesteld='Y' WHERE orderartikelnr=@orderartikelnr";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@orderartikelnr", MySqlDbType.Int64).Value = orderartikelnr;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        //knoppen

        private void btnBesteld_Click(object sender, EventArgs e)
        {
            dataUpdateGondardennesStatus();
            if (Application.OpenForms["Orders"] != null)
            {
                (Application.OpenForms["Orders"] as Orders).dataRefresh();
            }
            this.Close();
        }

        private void btnAnnuleren_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
