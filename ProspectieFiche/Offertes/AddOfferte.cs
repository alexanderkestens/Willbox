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
    public partial class AddOfferte : Form
    {
        private int klantnr = 0;
        private int codeUser;
        private int offertenr;
        private String firmaNaam;
        private Main main;
        MySqlConnection conn;
        private BindingSource bindingSource1;
        private DataGridView dgvLijsten;
        private Lijsten lijsten = null;
        private Lijsten lijstenkwal = null;

        public AddOfferte()
        {
            InitializeComponent();
        }

        public AddOfferte(Main main, int codeUser)
        {
            this.main = main;
            this.codeUser = codeUser;
            InitializeComponent();
            dgvLijsten = new DataGridView();
        }

        private void dataToevoegenOfferte()
        {
            offertenr = codeOpzoekenOffertes();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT offertes (offertenr, klantnr, datum, code, leveringstermijn, stansmeskost, clichekost, tav) VALUES (@offertenr, @klantnr, @datum, @code, @leveringstermijn, @stansmeskost, @clichekost, @tav)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@offertenr", MySqlDbType.Int64).Value = offertenr;
            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantnr;
            cmd.Parameters.Add("@datum", MySqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@code", MySqlDbType.Int64).Value = codeUser;
            cmd.Parameters.Add("@leveringstermijn", MySqlDbType.Text).Value = txtLeveringsTermijn.Text;
            if (Regex.Replace(txtStansmeskost.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@stansmeskost", MySqlDbType.Int64).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@stansmeskost", MySqlDbType.Int64).Value = Int64.Parse(txtStansmeskost.Text);
            }
            if (Regex.Replace(txtClichekost.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@clichekost", MySqlDbType.Int64).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@clichekost", MySqlDbType.Int64).Value = Int64.Parse(txtClichekost.Text);
            }
            cmd.Parameters.Add("@tav", MySqlDbType.Text).Value = cbTav.Text;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void dataToevoegenOfferteArtikel()
        {
            int offerteartikelnr = codeOpzoekenOfferteArtikels();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            conn.Open();

            String sql;
            sql = "INSERT INTO offerteArtikel (offerteartikelnr, offertenr, fefco, lengte, breedte, hoogte, kwaliteit, aantal, prijs, kwaliteit2, aantal2, prijs2, kwaliteit3, aantal3, prijs3, ref, bedrukking, status, soortorder, omschrijving) VALUES (@offerteartikelnr, @offertenr, @fefco, @lengte, @breedte, @hoogte, @kwaliteit, @aantal, @prijs, @kwaliteit2, @aantal2, @prijs2, @kwaliteit3, @aantal3, @prijs3, @ref, @bedrukking, @status, @soortorder, @omschrijving)";
            cmd.Parameters.Add("@offerteartikelnr", MySqlDbType.Int64).Value = codeOpzoekenOfferteArtikels();
            cmd.Parameters.Add("@offertenr", MySqlDbType.Int64).Value = offertenr;
            cmd.Parameters.Add("@fefco", MySqlDbType.Text).Value = cbFefco.SelectedItem;
            cmd.Parameters.Add("@lengte", MySqlDbType.Text).Value = txtLengte.Text;
            cmd.Parameters.Add("@breedte", MySqlDbType.Text).Value = txtBreedte.Text;
            cmd.Parameters.Add("@hoogte", MySqlDbType.Text).Value = txtHoogte.Text;
            cmd.Parameters.Add("@ref", MySqlDbType.Text).Value = txtRef.Text;
            cmd.Parameters.Add("@bedrukking", MySqlDbType.Text).Value = cmbBedrukking.SelectedItem;
            cmd.Parameters.Add("@status", MySqlDbType.Text).Value = "ROOD";
            cmd.Parameters.Add("@soortorder", MySqlDbType.Text).Value = cbSoortOrder.SelectedItem;
            cmd.Parameters.Add("@kwaliteit", MySqlDbType.Text).Value = txtKwaliteit.Text;
            cmd.Parameters.Add("@kwaliteit2", MySqlDbType.Text).Value = txtKwaliteit2.Text;
            cmd.Parameters.Add("@kwaliteit3", MySqlDbType.Text).Value = txtKwaliteit3.Text;
            cmd.Parameters.Add("@omschrijving", MySqlDbType.Text).Value = txtOmschrijving.Text;

            if (Regex.Replace(txtAantal.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@aantal", MySqlDbType.Int64).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@aantal", MySqlDbType.Int64).Value = txtAantal.Text;
            }
            if (Regex.Replace(txtVerkoopPrijs.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@prijs", MySqlDbType.Text).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@prijs", MySqlDbType.Text).Value = txtVerkoopPrijs.Text;
            }

            if (Regex.Replace(txtAantal2.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@aantal2", MySqlDbType.Int64).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@aantal2", MySqlDbType.Int64).Value = txtAantal2.Text;
            }
            if (Regex.Replace(txtVerkoopPrijs2.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@prijs2", MySqlDbType.Text).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@prijs2", MySqlDbType.Text).Value = txtVerkoopPrijs2.Text;
            }

            if (Regex.Replace(txtAantal3.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@aantal3", MySqlDbType.Int64).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@aantal3", MySqlDbType.Int64).Value = txtAantal3.Text;
            }
            if (Regex.Replace(txtVerkoopPrijs3.Text, @"\s+", "") == "")
            {
                cmd.Parameters.Add("@prijs3", MySqlDbType.Text).Value = "0";
            }
            else
            {
                cmd.Parameters.Add("@prijs3", MySqlDbType.Text).Value = txtVerkoopPrijs3.Text;
            }

            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand();
            cmd.Connection = conn;

            conn.Open();

            MessageBox.Show("De data is toegevoegd!");
        }

        public void dataRefresh(String naam, int klantnr)
        {
            txtFirma.Text = naam;
            this.klantnr = klantnr;
            txtKlantnr.Text = klantnr.ToString();
            dataKlantOpzoeken();
            lijsten = null;
        }

        private int codeOpzoekenOffertes()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM offertes;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["offertenr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private int codeOpzoekenOfferteArtikels()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM offerteArtikel;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["offerteartikelnr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void dataKlantOpzoeken()
        {
            try
            {
                string postcode;
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klantnr, naam, adres, postcode, gemeente FROM klant WHERE naam LIKE @tags;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tags", txtFirma.Text.ToUpper() + "%");
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    firmaNaam = (string)rdr["naam"];
                    txtAdres.Text = (string)rdr["adres"];
                    postcode = (string)rdr["postcode"];
                    txtGemeente.Text = postcode + " " + (string)rdr["gemeente"];
                    klantnr = (int)rdr["klantnr"];
                    dataTavOpzoeken();
                }
                else
                {
                    MessageBox.Show("Er werd geen Firma gevonden", "Error");
                }
                cmd.Connection.Close();
                txtKlantnr.Text = klantnr.ToString();
                txtFirma.Text = firmaNaam;
            }
            catch
            {

            }
        }

        private void dataTavOpzoeken()
        {
            try
            {
                cbTav.Items.Clear();
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT persoon FROM tav WHERE klantnr=@klantnr;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@klantnr", klantnr);
                MySqlDataReader rdr = cmd.ExecuteReader();

                cbTav.Items.Add("Geen");
                if (cbTav.Text == "")
                {
                    cbTav.Text = "Geen";
                }

                while (rdr.Read())
                {
                    cbTav.Items.Add((string)rdr["persoon"]);
                }

            }
            catch
            {

            }
        }

        private int codeOpzoekenTav()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM tav;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["tavnr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void addTav()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT tav (tavnr, klantnr, persoon) VALUES (@tavnr, @klantnr, @persoon)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@tavnr", MySqlDbType.Int64).Value = codeOpzoekenTav();
            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantnr;
            cmd.Parameters.Add("@persoon", MySqlDbType.Text).Value = txtTav.Text;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("De nieuwe contactpersoon is toegevoegd");
        }

        //klantenlijst aanmaken

        private DataGridView dataKlanten()
        {
            bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klantnr, naam, adres, gemeente, postcode FROM klant ORDER BY naam ASC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            dgvLijsten.DataSource = bindingSource1;
            return dgvLijsten;
        }

        private DataGridView dataKwaliteiten()
        {
            bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT kwaliteit, kwaliteitcode, gewicht, buiten, golf1, papier, golf2, binnen FROM gondardennes";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            dgvLijsten.DataSource = bindingSource1;
            return dgvLijsten;
        }

        public void dataRefresh(string kwaliteitcode)
        {
            txtKwaliteit.Text = kwaliteitcode;
            this.lijstenkwal = null;
        }

        public void dataRefresh2(string kwaliteitcode)
        {
            txtKwaliteit2.Text = kwaliteitcode;
            this.lijstenkwal = null;
        }

        public void dataRefresh3(string kwaliteitcode)
        {
            txtKwaliteit3.Text = kwaliteitcode;
            this.lijstenkwal = null;
        }

        //knoppen

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

        public void dataClose(String naam)
        {
            if (naam == "lijsten")
            {
                this.lijsten = null;
            }
            if (naam == "lijstenkwal")
            {
                this.lijstenkwal = null;
            }
        }

        private void iconSave_Click(object sender, EventArgs e)
        {
            if (klantnr == 0)
            {
                MessageBox.Show("Gelieve eerst een klant toe te voegen!", "Klant toevoegen");
            }
            else
            {
                iconSave.Visible = false;
                dataToevoegenOfferte();
                dataToevoegenOfferteArtikel();
                if (Application.OpenForms["Offertes"] != null)
                {
                    (Application.OpenForms["Offertes"] as Offertes).dataRefresh();
                }
                this.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtLeveringsTermijn.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void iconSearch_Click(object sender, EventArgs e)
        {
            if (lijsten == null)
            {
                Laden.ShowSplashScreen();
                lijsten = new Lijsten(main, dataKlanten(), "klantenAdd");
                lijsten.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijsten.BringToFront();
            lijsten.Show();
        }

        private void AddOfferte_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Offertes"] != null)
            {
                (Application.OpenForms["Offertes"] as Offertes).dataClose("addOfferte");
            }
        }

        private void iconAddTav_Click(object sender, EventArgs e)
        {
            if (Regex.Replace(txtTav.Text, @"\s+", "") == "")
            {
                MessageBox.Show("Gelieve een contactpersoon op te geven aub!");
            }
            else
            {
                if (klantnr == 0)
                {
                    MessageBox.Show("Gelieve een firma op te geven aub!");
                }
                else
                {
                    addTav();
                    dataTavOpzoeken();
                    txtTav.Text = "";
                }
            }
        }

        private void cbFefco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFefco.SelectedItem.ToString() == "F110")
            {
                txtHoogte.Visible = false;
                txtHoogteX.Visible = false;
                txtHoogte.Text = "0";
            }
            if (cbFefco.SelectedItem.ToString() != "F110")
            {
                txtHoogte.Visible = true;
                txtHoogteX.Visible = true;
                txtHoogte.Text = "";
            }
        }

        private void iconInfo_Click(object sender, EventArgs e)
        {
            Fefco_s fefco_s = new Fefco_s();
            fefco_s.MdiParent = this.main;
            fefco_s.Show();
        }

        private void txtKwaliteitOpzoeken_Click(object sender, EventArgs e)
        {
            if (lijstenkwal == null)
            {
                Laden.ShowSplashScreen();
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesAddOfferte");
                lijstenkwal.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijstenkwal.BringToFront();
            lijstenkwal.Show();
        }

        private void txtKwaliteitOpzoeken2_Click(object sender, EventArgs e)
        {
            if (lijstenkwal == null)
            {
                Laden.ShowSplashScreen();
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesAddOfferte2");
                lijstenkwal.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijstenkwal.BringToFront();
            lijstenkwal.Show();
        }

        private void txtKwaliteitOpzoeken3_Click(object sender, EventArgs e)
        {
            if (lijstenkwal == null)
            {
                Laden.ShowSplashScreen();
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesAddOfferte3");
                lijstenkwal.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijstenkwal.BringToFront();
            lijstenkwal.Show();
        }

        private void txtClichekost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtStansmeskost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtAantal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtAantal2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtAantal3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cbSoortOrder_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSoortOrder.SelectedItem.ToString() == "Stansdozen")
            {
                cbFefco.Items.Clear();
                cbFefco.Text = "F210";
                cbFefco.Items.Add("F210");
                cbFefco.Items.Add("F211");
                cbFefco.Items.Add("F212");
                cbFefco.Items.Add("F214");
                cbFefco.Items.Add("F215");
                cbFefco.Items.Add("F216");
                cbFefco.Items.Add("F217");
                cbFefco.Items.Add("F218");
                cbFefco.Items.Add("F303");
                cbFefco.Items.Add("F304");
                cbFefco.Items.Add("F306");
                cbFefco.Items.Add("F321");
                cbFefco.Items.Add("F330");
                cbFefco.Items.Add("F331");
                cbFefco.Items.Add("F401");
                cbFefco.Items.Add("F402");
                cbFefco.Items.Add("F403");
                cbFefco.Items.Add("F420");
                cbFefco.Items.Add("F421");
                cbFefco.Items.Add("F422");
                cbFefco.Items.Add("F426");
                cbFefco.Items.Add("F427");
                cbFefco.Items.Add("F701");
            }
            else
            {
                cbFefco.Items.Clear();
                cbFefco.Text = "F201";
                cbFefco.Items.Add("F110");
                cbFefco.Items.Add("F200");
                cbFefco.Items.Add("F201");
                cbFefco.Items.Add("F202");
                cbFefco.Items.Add("F203");
                cbFefco.Items.Add("F205");
                cbFefco.Items.Add("F300");
                cbFefco.Items.Add("F301");
                cbFefco.Items.Add("F320");
                cbFefco.Items.Add("F404");
                cbFefco.Items.Add("F405");
                cbFefco.Items.Add("F409");
                cbFefco.Items.Add("F410");
                cbFefco.Items.Add("F452");
            }
        }
    }
}
