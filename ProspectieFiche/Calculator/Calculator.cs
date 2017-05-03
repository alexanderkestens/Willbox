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
    public partial class Calculator : MetroForm
    {
        private Main main;
        private int codeUser;
        private string kwaliteit;
        private double totaal, totaalPerPlaat;
        MySqlConnection conn;
        private DataGridView dgvLijsten;
        private BindingSource bindingSource1;
        private int prijsPerM;

        public Calculator()
        {
            InitializeComponent();
        }

        public Calculator(Main main, int codeUser)
        {
            this.main = main;
            this.codeUser = codeUser;
            kwaliteit = "BC";
            dgvLijsten = new DataGridView();
            InitializeComponent();
            btnLeegmaken.Visible = false;
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

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
            conn.Close();
            return dgvLijsten;
        }

        public void dataRefresh(string kwaliteitcode, string kwaliteit)
        {
            txtKwaliteitcode.Text = kwaliteitcode;
            this.kwaliteit = kwaliteit;
        }

        private double berekenen()
        {
            double breedtePlaat = 0;
            double lengtePlaat = 0;
            double totaal = 0;
            double breedte = double.Parse(txtBreedte.Text);
            double hoogte = double.Parse(txtHoogte.Text);
            double lengte = double.Parse(txtLengte.Text);
            double aantal = double.Parse(txtAantal.Text);

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

            //zelfde als B
            if (kwaliteit == "EE")
            {
                breedtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 6) + ((0.5 * breedte) + 2));
            }

            //zelfde als C
            if (kwaliteit == "BE")
            {
                breedtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 8) + ((0.5 * breedte) + 2));
            }

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

            if (kwaliteit == "EE")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 12 + 35 + 15);
            }

            if (kwaliteit == "BE")
            {
                lengtePlaat = ((2 * lengte) + (2 * breedte) + 16 + 35 + 15);
            }

            txtLengtePlaat.Text = lengtePlaat.ToString();
            txtBreedtePlaat.Text = breedtePlaat.ToString();

            totaal = (((breedtePlaat / 1000) * (lengtePlaat / 1000)) * aantal);
            totaalPerPlaat = ((breedtePlaat / 1000) * (lengtePlaat / 1000));

            //TotaalPerPlaat
            txtAantal200.Text = Math.Round(((200 / totaalPerPlaat) + 1), 0).ToString();
            txtAantal500.Text = Math.Round(((500 / totaalPerPlaat) + 1), 0).ToString();
            txtAantal3000.Text = Math.Round(((3000 / totaalPerPlaat) + 1), 0).ToString();
            txtAantal10000.Text = Math.Round(((10000 / totaalPerPlaat) + 1), 0).ToString();
            return totaal;
        }

        private int dataPrijsOpvragen()
        {
            string zoeken = "";
            prijsPerM = 0;
            if (totaal > 200 && totaal < 500)
            {
                zoeken = "200m";
                panel200.BackColor = Color.LightBlue;
                lblAantal200m.BackColor = Color.LightBlue;
                lbl200m.BackColor = Color.LightBlue;
            }
            else if (totaal > 500 && totaal < 3000)
            {
                zoeken = "500m";
                panel500.BackColor = Color.LightBlue;
                lblAantal500m.BackColor = Color.LightBlue;
                lbl500m.BackColor = Color.LightBlue;
            }
            else if (totaal > 3000 && totaal < 10000)
            {
                zoeken = "3000m";
                panel3000.BackColor = Color.LightBlue;
                lblAantal3000m.BackColor = Color.LightBlue;
                lbl3000m.BackColor = Color.LightBlue;
            }
            else if (totaal > 10000)
            {
                zoeken = "10000m";
                panel10000.BackColor = Color.LightBlue;
                lblAantal10000m.BackColor = Color.LightBlue;
                lbl10000m.BackColor = Color.LightBlue;
            }

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            //string sql = "SELECT " + zoeken + " FROM gondardennes WHERE kwaliteitcode=@kwaliteitcode;";
            string sql = "SELECT 200m, 500m, 3000m, 10000m FROM gondardennes WHERE kwaliteitcode=@kwaliteitcode;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@kwaliteitcode", txtKwaliteitcode.Text);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                prijsPerM = (int)rdr[zoeken];
                //double prijs200 = double.Parse(((int)rdr["200m"]).ToString());
                //txtPrijs200.Text = ( ((200 / 1000) * prijs200) / double.Parse(txtAantal200.Text) ).ToString();
                txtPrijs200.Text = ((int)rdr["200m"]).ToString();
                txtPrijs500.Text = ((int)rdr["500m"]).ToString();
                txtPrijs3000.Text = ((int)rdr["3000m"]).ToString();
                txtPrijs10000.Text = ((int)rdr["10000m"]).ToString();
            }

            return prijsPerM;
        }

        private void dataUitvoeren()
        {
            if (rbDoosformaat.Checked == true)
            {
                totaal = berekenen();
            }
            else
            {
                totaal = (((double.Parse(txtLengte.Text) / 1000) * (double.Parse(txtBreedte.Text) / 1000)) * double.Parse(txtAantal.Text));
                txtLengtePlaat.Text = txtLengte.Text;
                txtBreedtePlaat.Text = txtBreedte.Text;
            }

            if (totaal < 200)
            {
                txtPrijs.Text = "Niet mogelijk, te weinig m²";
            }
            else
            {
                double prijsM = dataPrijsOpvragen();
                double prijs = ((prijsM / 1000) * totaal);
                txtPrijs.Text = Math.Round(prijs, 2).ToString();
                if (txtPrijs.Text == "0")
                {
                    txtPrijs.Text = "Kwaliteit kan pas vanaf 500m²";
                }
                else
                {
                    txtStukPrijs.Text = Math.Round((double.Parse(txtPrijs.Text) / double.Parse(txtAantal.Text)), 3).ToString();
                    if (totaal > 200 && totaal < 500)
                    {
                        lblAantal200m.Text = Math.Round(totaal, 1).ToString() + " m²";
                    }
                    else if (totaal > 500 && totaal < 3000)
                    {
                        lblAantal500m.Text = Math.Round(totaal, 1).ToString() + " m²";
                    }
                    else if (totaal > 3000 && totaal < 10000)
                    {
                        lblAantal3000m.Text = Math.Round(totaal, 1).ToString() + " m²";
                    }
                    else if (totaal > 10000)
                    {
                        lblAantal10000m.Text = Math.Round(totaal, 1).ToString() + " m²";
                    }

                    txtVerkoopprijs.Visible = true;
                    txtWinst.Visible = true;
                    nupVerkoopprijs.Visible = true;
                    lblPercentage.Visible = true;
                    lblVerkoopprijs.Visible = true;
                    lblWinst.Visible = true;
                }
            }
            txtAantal.ReadOnly = true;
            txtHoogte.ReadOnly = true;
            txtBreedte.ReadOnly = true;
            txtLengte.ReadOnly = true;
            btnBereken.Visible = false;
            btnLeegmaken.Visible = true;
            rbDoosformaat.Visible = false;
            rbPlaatformaat.Visible = false;
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
            //zelfde als B
            if (kwaliteit == "EE")
            {
                rillen1 = (0.5 * breedte) + 2;
            }
            //zelfde als C
            if (kwaliteit == "BE")
            {
                rillen1 = (0.5 * breedte) + 2;
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
            //zelfde als B
            if (kwaliteit == "EE")
            {
                rillen1 = (1 * hoogte) + 6;
            }
            //zelfde als C
            if (kwaliteit == "BE")
            {
                rillen1 = (1 * hoogte) + 8;
            }
            return rillen1;
        }

        //knoppen

        private void txtKwaliteit_Click(object sender, EventArgs e)
        {
            if (btnBereken.Visible == true)
            {
                Laden.ShowSplashScreen();
                Lijsten lijsten = new Lijsten(main, dataKwaliteiten(), "gondardennes");
                lijsten.MdiParent = this.main;
                Laden.CloseForm();
                lijsten.Show();
            }
        }

        private void btnBereken_Click(object sender, EventArgs e)
        {
            dataUitvoeren();
            if (rbDoosformaat.Checked == true)
            {
                txtRillen1.Text = berekenRillenFlap(int.Parse(txtBreedte.Text), kwaliteit).ToString();
                txtRillen2.Text = berekenRillenHoogte(int.Parse(txtHoogte.Text), kwaliteit).ToString();
                txtRillen3.Text = berekenRillenFlap(int.Parse(txtBreedte.Text), kwaliteit).ToString();
            }
            else
            {
                txtRillen1.Text = "0";
                txtRillen2.Text = "0";
                txtRillen3.Text = "0";
            }

        }

        private void btnLeegmaken_Click(object sender, EventArgs e)
        {
            txtAantal.ReadOnly = false;
            txtHoogte.ReadOnly = false;
            txtBreedte.ReadOnly = false;
            txtLengte.ReadOnly = false;
            btnBereken.Visible = true;
            //txtAantalM.Text = "0";
            txtPrijs.Text = "";
            btnLeegmaken.Visible = false;
            txtBreedtePlaat.Text = "0";
            txtLengtePlaat.Text = "0";
            txtStukPrijs.Text = "0";
            txtRillen1.Text = "0";
            txtRillen2.Text = "0";
            txtRillen3.Text = "0";
            rbPlaatformaat.Visible = true;
            rbDoosformaat.Visible = true;
            txtAantal200.Text = "";
            txtAantal500.Text = "";
            txtAantal3000.Text = "";
            txtAantal10000.Text = "";
            lblAantal200m.Text = "";
            lblAantal500m.Text = "";
            lblAantal3000m.Text = "";
            lblAantal10000m.Text = "";
            txtPrijs200.Text = "";
            txtPrijs500.Text = "";
            txtPrijs3000.Text = "";
            txtPrijs10000.Text = "";
            panel200.BackColor = Color.White;
            lblAantal200m.BackColor = Color.White;
            lbl200m.BackColor = Color.White;
            panel500.BackColor = Color.White;
            lblAantal500m.BackColor = Color.White;
            lbl500m.BackColor = Color.White;
            panel3000.BackColor = Color.White;
            lblAantal3000m.BackColor = Color.White;
            lbl3000m.BackColor = Color.White;
            panel10000.BackColor = Color.White;
            lblAantal10000m.BackColor = Color.White;
            lbl10000m.BackColor = Color.White;

            txtVerkoopprijs.Visible = false;
            txtWinst.Visible = false;
            nupVerkoopprijs.Visible = false;
            lblPercentage.Visible = false;
            lblVerkoopprijs.Visible = false;
            lblWinst.Visible = false;
            txtWinst.Text = "";
            txtVerkoopprijs.Text = "";
            nupVerkoopprijs.Value = 0;
        }

        private void txtLengte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtBreedte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtHoogte_KeyPress(object sender, KeyPressEventArgs e)
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

        private void rbPlaatformaat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPlaatformaat.Checked == true)
            {
                txtHoogte.Visible = false;
                lblHoogte.Visible = false;
                txtLengte.Text = "0";
                txtBreedte.Text = "0";
                txtHoogte.Text = "0";
                txtAantal.Text = "0";
            }
        }

        private void rbDoosformaat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDoosformaat.Checked == true)
            {
                txtHoogte.Visible = true;
                lblHoogte.Visible = true;
                txtLengte.Text = "0";
                txtBreedte.Text = "0";
                txtHoogte.Text = "0";
                txtAantal.Text = "0";
            }
        }

        private void nupVerkoopprijs_ValueChanged(object sender, EventArgs e)
        {
            double percentage = 0;
            if (nupVerkoopprijs.Value != 0)
            {
                if (nupVerkoopprijs.Value < 10)
                {
                    percentage = double.Parse("0,0" + nupVerkoopprijs.Value);
                }
                else if (nupVerkoopprijs.Value < 100)
                {
                    percentage = double.Parse("0," + nupVerkoopprijs.Value);
                }
                else if (nupVerkoopprijs.Value < 110)
                {
                    percentage = double.Parse("1,0" + nupVerkoopprijs.Value);
                }
                else if (nupVerkoopprijs.Value < 200)
                {
                    percentage = double.Parse("1," + nupVerkoopprijs.Value);
                }
                txtWinst.Text = (double.Parse(txtPrijs.Text) * percentage).ToString();
                txtVerkoopprijs.Text = ((double.Parse(txtPrijs.Text) + double.Parse(txtWinst.Text))).ToString();
            } else
            {
                txtWinst.Text = "";
                txtVerkoopprijs.Text = "";
            }
        }

        private void Calculator_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("calculator");
            }
        }
    }
}
