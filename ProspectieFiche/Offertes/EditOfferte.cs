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
    public partial class EditOfferte : Form
    {
        private int klantnr = 0;
        private int codeUser;
        private string status;
        private Main main;
        private String firmaNaam;
        private int offertenr, offerteartikelnr;
        MySqlConnection conn;
        private DataGridView dgvLijsten;
        private BindingSource bindingSource1;
        private Lijsten lijsten = null;
        private Lijsten lijstenkwal = null;

        public EditOfferte()
        {
            InitializeComponent();
        }

        public EditOfferte(int klantnr, int offertenr, int offerteartikelnr, Main main)
        {
            this.klantnr = klantnr;
            this.offertenr = offertenr;
            this.offerteartikelnr = offerteartikelnr;
            this.main = main;
            InitializeComponent();

            dgvLijsten = new DataGridView();

            setDatagridView();
            dataOfferteOpvragen();
            dataLeveringsTermijn();
            dataOpvragenOfferteArtikels();
        }

        public void dataRefresh(String naam, int klantnr)
        {
            txtFirma.Text = naam;
            this.klantnr = klantnr;
            txtKlantnr.Text = klantnr.ToString();
            dataKlantOpzoeken();
            this.lijsten = null;
        }

        //error 3001
        private void dataOfferteOpvragen()
        {
            try
            {
                string postCode;
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT naam, adres, postcode, gemeente FROM klant WHERE klantnr=@klantnr;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantnr;
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtFirma.Text = (String)rdr["naam"];
                    txtAdres.Text = (String)rdr["adres"];
                    postCode = (String)rdr["postcode"];
                    txtGemeente.Text = postCode + " " + (String)rdr["gemeente"];
                    dataTavOpzoeken();
                }
                conn.Close();
                txtKlantnr.Text = klantnr.ToString();
                firmaNaam = txtFirma.Text;
            }
            catch
            {
                MessageBox.Show("Er is iets fout gelopen! Contacteer de beheerder aub!", "Error 3001", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataLeveringsTermijn()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT leveringstermijn, stansmeskost, clichekost, tav FROM offertes WHERE offertenr=" + offertenr;
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                txtLeveringsTermijn.Text = (String)rdr["leveringstermijn"];
                txtStansmeskost.Text = ((int)rdr["stansmeskost"]).ToString();
                txtClichekost.Text = ((int)rdr["clichekost"]).ToString();   
                cbTav.Text = (String)rdr["tav"];
            }
            conn.Close();
            if (txtStansmeskost.Text == "0")
            {
                txtStansmeskost.Text = "";
            }
            if (txtClichekost.Text == "0")
            {
                txtClichekost.Text = "";
            }
        }

        private void dataOpvragenOfferteArtikels()
        {
            try
            {
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT ref, fefco, lengte, breedte, hoogte, kwaliteit, kwaliteit2, kwaliteit3, bedrukking, aantal, aantal2, aantal3, prijs, prijs2, prijs3, status, soortorder, omschrijving FROM offerteArtikel WHERE offerteartikelnr=" + offerteartikelnr + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtRef.Text = (String)rdr["ref"];
                    cbFefco.Text = (String)rdr["fefco"];
                    txtLengte.Text = ((int)rdr["lengte"]).ToString();
                    txtBreedte.Text = ((int)rdr["breedte"]).ToString();
                    txtHoogte.Text = ((int)rdr["hoogte"]).ToString();
                    txtKwaliteit.Text = (String)rdr["kwaliteit"];
                    txtKwaliteit2.Text = (String)rdr["kwaliteit2"];
                    txtKwaliteit3.Text = (String)rdr["kwaliteit3"];
                    cmbBedrukking.Text = (String)rdr["bedrukking"];
                    txtAantal.Text = ((int)rdr["aantal"]).ToString();
                    txtAantal2.Text = ((int)rdr["aantal2"]).ToString();
                    txtAantal3.Text = ((int)rdr["aantal3"]).ToString();
                    txtVerkoopPrijs.Text = (String)rdr["prijs"];
                    txtVerkoopPrijs2.Text = (String)rdr["prijs2"];
                    txtVerkoopPrijs3.Text = (String)rdr["prijs3"];
                    cbSoortOrder.Text = (String)rdr["soortorder"];
                    status = (String)rdr["status"];
                    txtOmschrijving.Text = (String)rdr["omschrijving"];
                }
                conn.Close();

                if (cbFefco.Text.ToString() == "F110")
                {
                    txtHoogte.Visible = false;
                    txtHoogteX.Visible = false;
                    txtHoogte.Text = "0";
                }
            }
            catch
            {
                //error 3002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code: 3002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataToevoegenOfferte()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "UPDATE offertes SET code=@code, leveringstermijn=@leveringstermijn, stansmeskost=@stansmeskost, clichekost=@clichekost, tav=@tav WHERE offertenr=" + offertenr;
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@code", MySqlDbType.Int64).Value = codeUser;
            cmd.Parameters.Add("@leveringstermijn", MySqlDbType.Text).Value = txtLeveringsTermijn.Text;
            cmd.Parameters.Add("@tav", MySqlDbType.Text).Value = cbTav.Text;
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
            
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void dataToevoegenOfferteArtikel()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            String sql = "UPDATE offerteArtikel SET fefco=@fefco, lengte=@lengte, breedte=@breedte, hoogte=@hoogte, kwaliteit=@kwaliteit, kwaliteit2=@kwaliteit2, kwaliteit3=@kwaliteit3, bedrukking=@bedrukking, aantal=@aantal, aantal2=@aantal2, aantal3=@aantal3, prijs=@prijs, prijs2=@prijs2, prijs3=@prijs3, ref=@ref, status=@status, soortorder=@soortorder, omschrijving=@omschrijving WHERE offerteartikelnr=@offerteartikelnr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@offerteartikelnr", MySqlDbType.Int64).Value = offerteartikelnr;
            //cmd.Parameters.Add("@offertenr", MySqlDbType.Int64).Value = offertenr;
            cmd.Parameters.Add("@fefco", MySqlDbType.Text).Value = cbFefco.Text;
            cmd.Parameters.Add("@lengte", MySqlDbType.Text).Value = txtLengte.Text;
            cmd.Parameters.Add("@breedte", MySqlDbType.Text).Value = txtBreedte.Text;
            cmd.Parameters.Add("@hoogte", MySqlDbType.Text).Value = txtHoogte.Text;
            cmd.Parameters.Add("@bedrukking", MySqlDbType.Text).Value = cmbBedrukking.Text;
            cmd.Parameters.Add("@ref", MySqlDbType.Text).Value = txtRef.Text;
            cmd.Parameters.Add("@soortorder", MySqlDbType.Text).Value = cbSoortOrder.Text;
            cmd.Parameters.Add("@kwaliteit", MySqlDbType.Text).Value = txtKwaliteit.Text;
            cmd.Parameters.Add("@kwaliteit2", MySqlDbType.Text).Value = txtKwaliteit2.Text;
            cmd.Parameters.Add("@kwaliteit3", MySqlDbType.Text).Value = txtKwaliteit3.Text;
            cmd.Parameters.Add("@omschrijving", MySqlDbType.Text).Value = txtOmschrijving.Text;

            //prijs 1
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
                cmd.Parameters.Add("@prijs", MySqlDbType.Text).Value = "";
            }
            else
            {
                cmd.Parameters.Add("@prijs", MySqlDbType.Text).Value = txtVerkoopPrijs.Text;
            }
            //prijs 2
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
                cmd.Parameters.Add("@prijs2", MySqlDbType.Text).Value = "";
            }
            else
            {
                cmd.Parameters.Add("@prijs2", MySqlDbType.Text).Value = txtVerkoopPrijs2.Text;
            }
            //prijs 3
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
                cmd.Parameters.Add("@prijs3", MySqlDbType.Text).Value = "";
            }
            else
            {
                cmd.Parameters.Add("@prijs3", MySqlDbType.Text).Value = txtVerkoopPrijs3.Text;
            }
            //Status
            if (status == "GROEN")
            {
                cmd.Parameters.Add("@status", MySqlDbType.Text).Value = "GROEN";
            } else
            {
                cmd.Parameters.Add("@status", MySqlDbType.Text).Value = "ROOD";
            }         
                

            cmd.ExecuteNonQuery();

            conn.Close();
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

        private void setDatagridView()
        {
            /*dgvHistory.ColumnCount = 2;
            dgvHistory.Columns[0].Name = "datum";
            dgvHistory.Columns[1].Name = "info";*/

            bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT datum, commentaar AS 'info' FROM history WHERE offertenr=" + offertenr + " ORDER BY datum ASC";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            dgvHistory.DataSource = bindingSource1;

            for (int j = 0; j < 2; j++)
            {
                dgvHistory.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dataKlantOpzoeken()
        {
            try
            {
                int postcode = 0;
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
                    postcode = (int)rdr["postcode"];
                    txtGemeente.Text = postcode.ToString() + " " + (string)rdr["gemeente"];
                    klantnr = (int)rdr["klantnr"];
                    dataTavOpzoeken();
                }
                else
                {
                    MessageBox.Show("Er werd geen Firma gevonden", "Error");
                }
                txtKlantnr.Text = klantnr.ToString();
                txtFirma.Text = firmaNaam;           
                cmd.Connection.Close();
            }
            catch
            {

            }

        }

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

        private void iconSave_Click(object sender, EventArgs e)
        {
            if (klantnr == 0)
            {
                MessageBox.Show("Gelieve eerst een klant toe te voegen!", "Klant toevoegen");
            }
            else
            {
                iconSave.Visible = false;
                //dataVerwijderenArtikels();
                dataToevoegenOfferteArtikel();
                dataToevoegenOfferte();
                if (Application.OpenForms["Offertes"] != null)
                {
                    (Application.OpenForms["Offertes"] as Offertes).dataRefresh();
                }
                MessageBox.Show("De nieuwe data is toegevoegd!", "Toegevoegd");

                this.Close();
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

        private void iconDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgvHistory.SelectedCells)
            {
                if (oneCell.Selected)
                    dgvHistory.Rows.RemoveAt(oneCell.RowIndex);
            }
        }

        private void EditOfferte_Load(object sender, EventArgs e)
        {
            
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
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

        private void iconSearch_Click(object sender, EventArgs e)
        {
            if (lijsten == null)
            {
                Laden.ShowSplashScreen();
                lijsten = new Lijsten(main, dataKlanten(), "klantenEdit");
                lijsten.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijsten.BringToFront();
            lijsten.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtLeveringsTermijn.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void EditOfferte_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Offertes"] != null)
            {
                (Application.OpenForms["Offertes"] as Offertes).dataClose("editOfferte");
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
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesEditOfferte");
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
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesEditOfferte2");
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
                lijstenkwal = new Lijsten(main, dataKwaliteiten(), "gondardennesEditOfferte3");
                lijstenkwal.MdiParent = this.main;
                Laden.CloseForm();
            }
            lijstenkwal.BringToFront();
            lijstenkwal.Show();
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

        private void txtClichekost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


    }
}
