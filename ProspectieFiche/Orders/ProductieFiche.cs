using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using SelectPdf;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace ProspectieFiche
{
    public partial class ProductieFiche : Form
    {
        MySqlConnection conn;
        private Main main;

        public int codeMachine, klantnr;
        private String klant, fefco, kwaliteit, kwaliteit1, plaatformaat, commentaarproductie;
        private int lengtedoos, breedtedoos, hoogtedoos, aantal, ordernr;

        public ProductieFiche()
        {
            InitializeComponent();
        }

        public ProductieFiche(Main main, string klant, int lengte, int breedte, int hoogte, string fefco, string kwaliteit, int aantal, int ordernr, int klantnr)
        {
            this.klant = klant;
            this.lengtedoos = lengte;
            this.breedtedoos = breedte;
            this.hoogtedoos = hoogte;
            this.fefco = fefco;
            this.aantal = aantal;
            this.ordernr = ordernr;
            this.kwaliteit = kwaliteit;
            this.klantnr = klantnr;
            this.main = main;
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            txtKlant.Text = klant;
            txtAdres.Text = "";
            txtOrdernr.Text = ordernr.ToString();
            txtLengte.Text = lengtedoos.ToString();
            txtBreedte.Text = breedtedoos.ToString();
            txtHoogte.Text = hoogtedoos.ToString();
            txtFefco.Text = fefco;
            txtKwaliteit.Text = kwaliteit;
            txtTeProduceren.Text = aantal.ToString();

            kwaliteit1 = Regex.Replace(kwaliteit, @"[\d-]", string.Empty).ToUpper();
            txtLengtePlaat.Text = berekenLengtePlaat(lengtedoos, breedtedoos, hoogtedoos, kwaliteit1).ToString();
            txtBreedtePlaat.Text = berekenBreedtePlaat(lengtedoos, breedtedoos, hoogtedoos, kwaliteit1).ToString();
            txtRillen1.Text = berekenRillenFlap(breedtedoos, kwaliteit1).ToString();
            txtRillen2.Text = berekenRillenHoogte(hoogtedoos, kwaliteit1).ToString();
            txtRillen3.Text = berekenRillenFlap(breedtedoos, kwaliteit1).ToString();

            if (checkInfo() == false)
            {
                codeMachine = codeOpzoeken();
                txtMachineCode.Text = codeMachine.ToString();

            } else
            {
                txtMachineCode.Text = codeMachine.ToString();
            }
            plaatformaat = "B " + txtLengtePlaat.Text + " X L " + txtBreedtePlaat.Text;
        }

        private bool checkInfo()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT code, model, lengtePlaat, breedtePlaat, commentaar FROM productiefiche WHERE lengteDoos=@lengteDoos AND breedteDoos=@breedteDoos AND hoogteDoos=@hoogteDoos AND kwaliteit=@kwaliteit";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@lengteDoos", MySqlDbType.Int64).Value = Int32.Parse(txtLengte.Text);
            cmd.Parameters.Add("@breedteDoos", MySqlDbType.Int64).Value = Int32.Parse(txtBreedte.Text);
            cmd.Parameters.Add("@hoogteDoos", MySqlDbType.Int64).Value = Int32.Parse(txtHoogte.Text);
            cmd.Parameters.Add("@kwaliteit", MySqlDbType.String).Value = kwaliteit1;
            MySqlDataReader rdr = cmd.ExecuteReader();

            var intLengtePlaat = 0;
            var intBreedtePlaat = 0;
            var code = 0;
            var commentaar = "";
            var model = "";
            bool trueFalse = false;

            while (rdr.Read())
            {
                trueFalse = true;
                try
                {
                    intLengtePlaat = (int)rdr["lengtePlaat"];
                    intBreedtePlaat = (int)rdr["breedtePlaat"];
                    commentaar = (String)rdr["commentaar"];
                    model = (String)rdr["model"];
                    code = (int)rdr["code"];
                }
                catch (Exception)
                {
                }
            }

            cmd.Connection.Close();
            codeMachine = code;

            return trueFalse;
        }

        private void voegDataToe()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT productiefiche (code, model, lengteDoos, breedteDoos, hoogteDoos, kwaliteit, lengtePlaat, breedtePlaat, rillen1, rillen2, rillen3, commentaar) VALUES (@boxcode, @model, @lengteDoos, @breedteDoos, @hoogteDoos, @kwaliteit, @lengtePlaat, @breedtePlaat, @rillen1, @rillen2, @rillen3, @commentaar)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@boxcode", MySqlDbType.Text).Value = txtMachineCode.Text;
            cmd.Parameters.Add("@model", MySqlDbType.Text).Value = txtFefco.Text;
            cmd.Parameters.Add("@lengteDoos", MySqlDbType.Int64).Value = Int64.Parse(txtLengte.Text);
            cmd.Parameters.Add("@breedteDoos", MySqlDbType.Int64).Value = Int64.Parse(txtBreedte.Text);
            cmd.Parameters.Add("@hoogteDoos", MySqlDbType.Int64).Value = Int64.Parse(txtHoogte.Text);
            cmd.Parameters.Add("@kwaliteit", MySqlDbType.Text).Value = kwaliteit1;
            cmd.Parameters.Add("@lengtePlaat", MySqlDbType.Int64).Value = txtLengtePlaat.Text;
            cmd.Parameters.Add("@breedtePlaat", MySqlDbType.Int64).Value = txtBreedte.Text;

            cmd.Parameters.Add("@rillen1", MySqlDbType.Double).Value = Double.Parse(txtRillen1.Text);
            cmd.Parameters.Add("@rillen2", MySqlDbType.Double).Value = Double.Parse(txtRillen2.Text);
            cmd.Parameters.Add("@rillen3", MySqlDbType.Double).Value = Double.Parse(txtRillen3.Text);
            cmd.Parameters.Add("@commentaar", MySqlDbType.Text).Value = txtCommentaar.Text;
            //cmd.Parameters.Add("@kwaliteitcode", MySqlDbType.Text).Value = txtKwaliteit.Text;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void UpdateDataOrderArtikel()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "UPDATE orderArtikel SET aantalplaten=@aantalplaten WHERE ordernr=@ordernr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@aantalplaten", MySqlDbType.Int64).Value = int.Parse(txtPlatenFabrikant.Text);
            cmd.Parameters.Add("@ordernr", MySqlDbType.Int64).Value = ordernr;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private int codeOpzoeken()
        {
            int legePlaats = 0;
            int teller = 0;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT code FROM productiefiche";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetInt32(0) != teller)
                {
                    legePlaats = teller;
                    break;
                }
                teller++;
            }
            cmd.Connection.Close();
            if (legePlaats == 0)
            {
                legePlaats = teller + 1;
            }
            return legePlaats;
        }

        private void dataUpdateOrderStatusProduction(string status)
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = "UPDATE orderArtikel SET status=@status WHERE ordernr=@ordernr";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@status", MySqlDbType.Text).Value = status;
            cmd.Parameters.Add("@ordernr", MySqlDbType.Int64).Value = ordernr;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void getDataCommentaarProductie()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT commentaarproductie FROM klant WHERE klantnr=@klantnr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@klantnr", MySqlDbType.Int64).Value = klantnr;
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                commentaarproductie = (String)rdr["commentaarproductie"];
            }

            cmd.Connection.Close();
        }

        private double berekenLengtePlaat(int lengte, int breedte, int hoogte, string kwaliteit)
        {
            double lengtePlaat = 0;

            if (kwaliteit == "B")
            {
                lengtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 6) + ((0.5 * breedte) + 2));
            }

            if (kwaliteit == "BC")
            {
                lengtePlaat = (((0.5 * breedte) + 4) + ((1 * hoogte) + 14) + ((0.5 * breedte) + 4));
            }

            if (kwaliteit == "C")
            {
                lengtePlaat = (((0.5 * breedte) + 2) + ((1 * hoogte) + 8) + ((0.5 * breedte) + 2));
            }

            if (kwaliteit == "E")
            {
                lengtePlaat = (((0.5 * breedte) + 1) + ((1 * hoogte) + 3) + ((0.5 * breedte) + 1));
            }

            return lengtePlaat;
        }

        private double berekenBreedtePlaat(int lengte, int breedte, int hoogte, string kwaliteit)
        {
            int breedtePlaat = 0;

            if (kwaliteit == "B")
            {
                breedtePlaat = ((2 * lengte) + (2 * breedte) + 12 + 35 + 15);
            }

            if (kwaliteit == "BC")
            {
                breedtePlaat = ((2 * lengte) + (2 * breedte) + 28 + 35 + 20);
            }

            if (kwaliteit == "C")
            {
                breedtePlaat = ((2 * lengte) + (2 * breedte) + 16 + 35 + 15);
            }

            if (kwaliteit == "E")
            {
                breedtePlaat = ((2 * lengte) + (2 * breedte) + 8 + 35 + 15);
            }

            return breedtePlaat;
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

        private int toegifteFlappen(string kwaliteit)
        {
            int extraToegifte = 0;
            if (kwaliteit == "B")
            {
                extraToegifte = 3;
            }

            if (kwaliteit == "BC")
            {
                extraToegifte = 7;
            }

            if (kwaliteit == "C")
            {
                extraToegifte = 4;
            }

            if (kwaliteit == "E")
            {
                extraToegifte = 1;
            }
            return extraToegifte;
        }

        private void btnProductie_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            bool truefalse = true;
            if (Regex.Replace(txtPlatenFabrikant.Text, @"\s+", "") == "0")
            {
                //MessageBox.Show("Gelieve het aantal platen in te geven dat de fabrikant heeft afgeleverd!", "Aantal platen?");
                txtPlatenFabrikant.BackColor = Color.Red;
                lblError.Text = "Gelieve het aantal platen in te geven dat de fabrikant heeft afgeleverd!";
                truefalse = false;
            }
            if (Regex.Replace(txtTeProduceren.Text, @"\s+", "") == "0")
            {
                //MessageBox.Show("Gelieve het aantal dozen in te geven dat er geproduceerd moet worden!", "Aantal dozen?");
                txtTeProduceren.BackColor = Color.Red;
                lblError.Text = "Gelieve het aantal dozen in te geven dat er geproduceerd moet worden!";
                truefalse = false;
            }

            if (checkInfo() == false)
            {
                voegDataToe();       
            }
            if (truefalse == true)
            {
                UpdateDataOrderArtikel();
                PDF();
                dataUpdateOrderStatusProduction("IN PRODUCTIE");
                if (Application.OpenForms["Orders"] != null)
                {
                    (Application.OpenForms["Orders"] as Orders).dataRefresh();
                }
            }
            
        }

        private void PDF()
        {
            // create a new pdf document
            PdfDocument doc = new PdfDocument();

            // add a new page to the document
            PdfPage page = doc.AddPage();

            // create a new pdf font
            PdfFont font1 = doc.AddFont(PdfStandardFont.Helvetica);
            PdfFont font2 = doc.AddFont(PdfStandardFont.Helvetica);
            PdfFont font3 = doc.AddFont(PdfStandardFont.Helvetica);
            font1.Size = 20;
            font2.Size = 16;
            font3.Size = 12;

            // create a new text element and add it to the page

            PdfTextElement klant = new PdfTextElement(50, 50, "Klant: " + txtKlant.Text.ToLower(), font1);
            PdfTextElement code = new PdfTextElement(320, 50, "Code machine: " + txtMachineCode.Text, font1);
            PdfTextElement ordernr1 = new PdfTextElement(50, 80, "Ordernr: " + txtOrdernr.Text, font1);

            PdfTextElement lengteDoos2 = new PdfTextElement(50, 170, "" + txtLengte.Text, font2);
            PdfTextElement breedteDoos2 = new PdfTextElement(150, 170, "" + txtBreedte.Text, font2);
            PdfTextElement hoogteDoos2 = new PdfTextElement(250, 170, "" + txtHoogte.Text, font2);
            PdfTextElement modelDoos2 = new PdfTextElement(350, 170, "" + txtFefco.Text, font2);
            PdfTextElement aantal = new PdfTextElement(450, 170, "" + txtTeProduceren.Text, font2);

            PdfTextElement kwaliteit2 = new PdfTextElement(50, 250, "Kwaliteit: " + kwaliteit, font1);
            PdfTextElement plaatformaat1 = new PdfTextElement(50, 290, "Plaatformaat: " + plaatformaat, font1);
            PdfTextElement aantalPlaten = new PdfTextElement(50, 330, "Aantal platen: " + txtPlatenFabrikant.Text, font1);

            PdfTextElement page1 = new PdfTextElement(50, 130, "Lengte           Breedte          Hoogte           Model            Aantal", font2);

            //extra toegifte flappen
            int toegifte = toegifteFlappen(kwaliteit1.ToString().ToUpper());
            PdfTextElement page2 = new PdfTextElement(130, 425, (lengtedoos + toegifte) + "         " + (breedtedoos + toegifte) + "     " + (lengtedoos + toegifte) + "         " + (breedtedoos + toegifte), font2);
            PdfTextElement rillen1 = new PdfTextElement(65, 460, "" + txtRillen1.Text, font2);
            PdfTextElement rillen2 = new PdfTextElement(65, 495, "" + txtRillen2.Text, font2);
            PdfTextElement rillen3 = new PdfTextElement(65, 535, "" + txtRillen3.Text, font2);

            PdfTextElement text1 = new PdfTextElement(65, 580, "Aantal paletten: ..........", font2);
            PdfTextElement text2 = new PdfTextElement(65, 610, "Aantal wwp: ..........   Aantal europal: ..........", font2);
            PdfTextElement text3 = new PdfTextElement(65, 640, "Aantal per pallet: ..........", font2);
            PdfTextElement text4 = new PdfTextElement(65, 670, "AANTAL GEPRODUCEERD: ..........", font1);
            PdfTextElement text5 = new PdfTextElement(65, 710, "Extra commentaar: " + txtCommentaar.Text + " " + commentaarproductie, font3);

            System.Drawing.Bitmap bitmap1 = ProspectieFiche.Properties.Resources.doos;
            //string imgFile = Path.Combine(Environment.CurrentDirectory, @"Data\", "doos.png");
            //"C:/Users/alexander/Documents/Visual Studio 2015/Projects/Productiefiche2/Productiefiche2/doos.png";
            PdfRenderingResult result;

            // create image element from file path with real image size
            PdfImageElement img1 = new PdfImageElement(100, 450, bitmap1);

            result = page.Add(img1);

            page.Add(klant);
            page.Add(code);
            page.Add(ordernr1);
            page.Add(lengteDoos2);
            page.Add(breedteDoos2);
            page.Add(hoogteDoos2);
            page.Add(modelDoos2);
            page.Add(aantal);
            page.Add(page1);
            page.Add(kwaliteit2);
            page.Add(plaatformaat1);
            page.Add(aantalPlaten);

            page.Add(rillen1);
            page.Add(rillen2);
            page.Add(rillen3);

            page.Add(page2);
            page.Add(text1);
            page.Add(text2);
            page.Add(text3);
            page.Add(text4);
            page.Add(text5);

            // save pdf document
            string path = @"c:/willbox/Orders/" + txtKlant.Text.ToLower() + "/" + "Ordernr " + ordernr + "/";
            /*
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {

                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }*/

            String padname = "ProductieFiche " + txtKlant.Text.ToLower();
            doc.Save(path + padname + ".pdf");

            // close pdf document
            doc.Close();

            MessageBox.Show("Het PDF-document is aangemaakt met als naam " + padname + ".pdf");

            this.Close();

        }

    }
}
