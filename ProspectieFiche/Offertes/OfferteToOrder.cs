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
    public partial class OfferteToOrder : Form
    {
        private int offertenr, ordernr, offerteartikelnr, klantnr;
        private String bestelbonnummer, reorder;
        MySqlConnection conn;
        BindingSource bindingSource;

        public OfferteToOrder()
        {
            InitializeComponent();
        }

        public OfferteToOrder(int offertenr, int offerteartikelnr, string firmaNaam, int klantnr, string reorder)
        {
            this.offertenr = offertenr;
            this.offerteartikelnr = offerteartikelnr;
            this.klantnr = klantnr;
            this.reorder = reorder;
            InitializeComponent();
            txtFirma.Text = firmaNaam;
            if (reorder == "Yes")
            {
                this.Text = "Offerte naar Order (Re-order)";
                txtAantal.ReadOnly = false;
                txtAantal2.ReadOnly = false;
                txtAantal3.ReadOnly = false;
            } else
            {
                txtLeveringsTermijn.Visible = false;
                lblLeveringstermijn.Visible = false;
                dateTimePicker1.Visible = false;
            }
            dataOpvragenOfferteArtikels();
        }

        private void dataOpvragenOfferteArtikels()
        {
            try
            {
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT kwaliteit, kwaliteit2, kwaliteit3, aantal, aantal2, aantal3, prijs, prijs2, prijs3, soortorder FROM offerteArtikel WHERE offerteartikelnr=" + offerteartikelnr + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    txtKwaliteit.Text = (String)rdr["kwaliteit"];
                    txtKwaliteit2.Text = (String)rdr["kwaliteit2"];
                    txtKwaliteit3.Text = (String)rdr["kwaliteit3"];
                    txtAantal.Text = ((int)rdr["aantal"]).ToString();
                    txtAantal2.Text = ((int)rdr["aantal2"]).ToString();
                    txtAantal3.Text = ((int)rdr["aantal3"]).ToString();
                    txtVerkoopPrijs.Text = (String)rdr["prijs"];
                    txtVerkoopPrijs2.Text = (String)rdr["prijs2"];
                    txtVerkoopPrijs3.Text = (String)rdr["prijs3"];
                    cbSoortOrder.Text = (String)rdr["soortorder"];
                }
                conn.Close();

            }
            catch
            {
                //error 3002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code: 3002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataNaarOrders()
        {
            ordernr = codeOpzoekenOrders();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql;
            if (reorder == "Yes")
            {
                sql = "INSERT orders (ordernr, klantnr, offertenr, datum, code, leveringstermijn, stansmeskosten, clichekost, tav) (SELECT + " + ordernr + ", klantnr, offertenr, @datum, code, @leveringstermijn, stansmeskost, clichekost, tav FROM offertes WHERE offertenr=@offertenr)";
            }
            else
            {
                sql = "INSERT orders (ordernr, klantnr, offertenr, datum, code, leveringstermijn, stansmeskosten, clichekost, tav) (SELECT + " + ordernr + ", klantnr, offertenr, @datum, code, leveringstermijn, stansmeskost, clichekost, tav FROM offertes WHERE offertenr=@offertenr)";
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@offertenr", MySqlDbType.Int64).Value = offertenr;
            if (reorder == "Yes")
            {
                cmd.Parameters.Add("@leveringstermijn", MySqlDbType.Text).Value = txtLeveringsTermijn.Text;
            }             
            cmd.Parameters.Add("@datum", MySqlDbType.DateTime).Value = DateTime.Now;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void dataNaarOrderArtikel()
        {
            int codeOfferteArtikel = codeOpzoekenOrderArtikel();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = null;
            MySqlCommand cmd = null;
            if (rbPrijs1.Checked == true)
            {
                sql = "INSERT orderArtikel (orderartikelnr, ordernr, fefco, kwaliteit, aantal, prijs, ref, bedrukking, lengte, breedte, hoogte, status, soortorder, omschrijving) (SELECT " + codeOfferteArtikel + ", " + ordernr + ", fefco, kwaliteit, @aantal, prijs, ref, bedrukking, lengte, breedte, hoogte, @status, soortorder, omschrijving FROM offerteArtikel WHERE offerteartikelnr=@offerteartikelnr)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@aantal", MySqlDbType.Int64).Value = Int64.Parse(txtAantal.Text);
            }
            if (rbPrijs2.Checked == true)
            {
                sql = "INSERT orderArtikel (orderartikelnr, ordernr, fefco, kwaliteit, aantal, prijs, ref, bedrukking, lengte, breedte, hoogte, status, soortorder, omschrijving) (SELECT " + codeOfferteArtikel + ", " + ordernr + ", fefco, kwaliteit2, @aantal2, prijs2, ref, bedrukking, lengte, breedte, hoogte, @status, soortorder, omschrijving FROM offerteArtikel WHERE offerteartikelnr=@offerteartikelnr)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@aantal", MySqlDbType.Int64).Value = Int64.Parse(txtAantal2.Text);
            }
            if (rbPrijs3.Checked == true)
            {
                sql = "INSERT orderArtikel (orderartikelnr, ordernr, fefco, kwaliteit, aantal, prijs, ref, bedrukking, lengte, breedte, hoogte, status, soortorder, omschrijving) (SELECT " + codeOfferteArtikel + ", " + ordernr + ", fefco, kwaliteit3, @aantal3, prijs3, ref, bedrukking, lengte, breedte, hoogte, @status, soortorder, omschrijving FROM offerteArtikel WHERE offerteartikelnr=@offerteartikelnr)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@aantal", MySqlDbType.Int64).Value = Int64.Parse(txtAantal3.Text);
            }

            cmd.Parameters.Add("@offerteartikelnr", MySqlDbType.Int64).Value = offerteartikelnr;
            cmd.Parameters.Add("@status", MySqlDbType.Text).Value = "ROOD";

            cmd.ExecuteNonQuery();

            sql = "UPDATE orderArtikel SET gonbesteld=@gonbesteld, bestelbonnummer=@bestelbonnummer WHERE orderartikelnr=@orderartikelnr;";

            cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@orderartikelnr", MySqlDbType.Int64).Value = codeOfferteArtikel;
            cmd.Parameters.Add("@bestelbonnummer", MySqlDbType.Text).Value = txtBestelbonnr.Text;
            cmd.Parameters.Add("@gonbesteld", MySqlDbType.Text).Value = "N";

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private int codeOpzoekenOrders()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM orders;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["ordernr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private int codeOpzoekenOrderArtikel()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM orderArtikel;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["ordernr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }

        private void dataUpdateOfferteStatus()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = "UPDATE offerteArtikel SET status='GROEN' WHERE offerteartikelnr=@offerteartikelnr";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@offerteartikelnr", MySqlDbType.Int64).Value = offerteartikelnr;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            if (Application.OpenForms["Offertes"] != null)
            {
                (Application.OpenForms["Offertes"] as Offertes).dataRefresh();
            }
        }

        private void btnAnnuleren_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Make Excel

        private void dataOpvragenOrderbevestigingExcel()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT orders.ordernr, klant.naam, klant.adres, klant.gemeente, klant.postcode, klant.klantnr, klant.land, orders.datum, orders.leveringstermijn, orders.stansmeskosten, orders.clichekost, orders.tav, orderArtikel.bestelbonnummer, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.ref, orderArtikel.fefco, orderArtikel.kwaliteit, orderArtikel.bedrukking, orderArtikel.prijs, orderArtikel.aantal, orderArtikel.omschrijving FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orders.ordernr=" + ordernr + " ORDER BY ordernr ASC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvDataOrders.DataSource = bindingSource;
            }
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeExcell()
        {
            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Columns["A"].ColumnWidth = 2;
            xlWorkSheet.Columns["B"].ColumnWidth = 12;
            xlWorkSheet.Columns["C"].ColumnWidth = 6;
            xlWorkSheet.Columns["D"].ColumnWidth = 37;
            xlWorkSheet.Columns["E"].ColumnWidth = 11;
            xlWorkSheet.Columns["F"].ColumnWidth = 15;

            xlWorkSheet.get_Range("B33", "F40").RowHeight = 15;
            xlWorkSheet.get_Range("A7", "A12").RowHeight = 13;
            xlWorkSheet.get_Range("A43", "A48").RowHeight = 13;
            //pagina 2
            xlWorkSheet.get_Range("A58", "A63").RowHeight = 13;
            xlWorkSheet.get_Range("A94", "A99").RowHeight = 13;

            xlWorkSheet.Columns["B"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.Columns["D"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            xlWorkSheet.Cells[7, 5] = "Orderbevestiging";
            xlWorkSheet.get_Range("E7").Font.Italic = true;
            xlWorkSheet.get_Range("E7").Font.Bold = true;
            xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
            xlWorkSheet.Cells[8, 6] = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
            if (dgvDataOrders.Rows[0].Cells["tav"].Value.ToString() != "Geen")
            {
                xlWorkSheet.Cells[9, 6] = "T.a.v. " + dgvDataOrders.Rows[0].Cells["tav"].Value.ToString();
            }
            string adres = dgvDataOrders.Rows[0].Cells["adres"].Value.ToString().ToLower();
            if (dgvDataOrders.Rows[0].Cells["adres"].Value.ToString() != "")
            {
                adres = adres.First().ToString().ToUpper() + String.Join("", adres.Skip(1));
            }
            xlWorkSheet.Cells[10, 6] = adres;
            xlWorkSheet.Cells[11, 6] = dgvDataOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvDataOrders.Rows[0].Cells["gemeente"].Value.ToString();
            xlWorkSheet.Cells[12, 6] = dgvDataOrders.Rows[0].Cells["land"].Value.ToString();
            xlWorkSheet.Cells[11, 2] = "Klantnummer: " + klantnr;
            DateTime orderdatum = DateTime.Parse(dgvDataOrders.Rows[0].Cells["datum"].Value.ToString());
            xlWorkSheet.Cells[12, 2] = "OrderDatum: " + orderdatum.ToString("dd-MM-yyyy");
            xlWorkSheet.get_Range("B11").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B12").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 8; i < 13; i++)
            {
                xlWorkSheet.get_Range("F" + i).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            xlWorkSheet.get_Range("B7", "F12").Font.Size = 10;

            xlWorkSheet.Cells[15, 1] = "Ordernr/Omschrijving";
            xlWorkSheet.Cells[15, 5] = "Aantal";
            xlWorkSheet.Cells[15, 6] = "Prijs/Eenheid";

            xlWorkSheet.get_Range("A15", "F15").Font.Size = 14;
            xlWorkSheet.get_Range("A15", "F15").Font.Bold = true;
            xlWorkSheet.get_Range("A15", "F15").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A15", "F15").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A15", "F15").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A15", "F15").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

            //OfferteGegevens
            int j = 17;

            xlWorkSheet.Cells[j, 1] = "# Ordernr " + ordernr;
            xlWorkSheet.get_Range("A" + j).Font.Bold = true;

            xlWorkSheet.Cells[j, 5] = dgvDataOrders.Rows[0].Cells["aantal"].Value.ToString();
            double prijsEenheid = double.Parse(dgvDataOrders.Rows[0].Cells["prijs"].Value.ToString()) / 1000;
            xlWorkSheet.Cells[j, 6] = prijsEenheid;
            xlWorkSheet.get_Range("E" + j, "F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            j++;
            if (dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() != "")
            {
                xlWorkSheet.Cells[j, 2] = "Bestelbonnummer";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
                j++;
            }
            if (Regex.Replace(dgvDataOrders.Rows[0].Cells["omschrijving"].Value.ToString(), @"\s+", "") != "")
            {
                xlWorkSheet.Cells[j, 2] = "Omschrijving";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["omschrijving"].Value.ToString();
                j++;
            }
            if (dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() != "")
            {
                xlWorkSheet.Cells[j, 2] = "Uw referentie";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["ref"].Value.ToString();
                j++;
            }
            xlWorkSheet.Cells[j, 2] = "Fefco";
            xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["fefco"].Value.ToString();
            j++;
            xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
            if (dgvDataOrders.Rows[0].Cells["fefco"].Value.ToString() == "F110")
            {
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString();
            }
            else
            {
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString();
            }
            j++;
            if (dgvDataOrders.Rows[0].Cells["kwaliteit"].Value.ToString() == "" || dgvDataOrders.Rows[0].Cells["kwaliteit"].Value.ToString() == "0")
            {
            }
            else
            {
                xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["kwaliteit"].Value.ToString();
                j++;
            }
            if (dgvDataOrders.Rows[0].Cells["Bedrukking"].Value.ToString() != "Geen")
            {
                xlWorkSheet.Cells[j, 2] = "Bedrukking";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["Bedrukking"].Value.ToString();
                j++;
            }            
            if (dgvDataOrders.Rows[0].Cells["leveringstermijn"].Value.ToString() != "")
            {
                xlWorkSheet.Cells[j, 2] = "Leveringstermijn";
                xlWorkSheet.Cells[j, 4] = dgvDataOrders.Rows[0].Cells["leveringstermijn"].Value.ToString();
                j++;
            }
            if (Regex.Replace(dgvDataOrders.Rows[0].Cells["stansmeskosten"].Value.ToString(), @"\s+", "") != "0")
            {
                xlWorkSheet.Cells[j, 2] = "Eenmalige stansmeskost";
                xlWorkSheet.Cells[j, 6] = dgvDataOrders.Rows[0].Cells["stansmeskosten"].Value.ToString();
                xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                j++;
            }
            if (Regex.Replace(dgvDataOrders.Rows[0].Cells["clichekost"].Value.ToString(), @"\s+", "") != "0")
            {
                xlWorkSheet.Cells[j, 2] = "Eenmalige clichekost";
                xlWorkSheet.Cells[j, 6] = dgvDataOrders.Rows[0].Cells["clichekost"].Value.ToString();
                xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                j++;
            }
            j++;


            //xlWorkSheet.Cells[34, 2] = "Leveringstermijn: " + txtLeveringsTermijn.Text;
            xlWorkSheet.get_Range("A44", "F44").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A44", "F44").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A50", "F50").MergeCells = true;
            xlWorkSheet.get_Range("A51", "F51").MergeCells = true;
            xlWorkSheet.Cells[44, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
            xlWorkSheet.Cells[45, 1] = "Km heffing: 0,8%";
            xlWorkSheet.Cells[46, 1] = "Betaling : 30 dagen netto";
            xlWorkSheet.Cells[47, 1] = "Alle prijzen in EUR ex. BTW";
            xlWorkSheet.Cells[48, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
            xlWorkSheet.get_Range("A44", "A48").Font.Italic = true;
            xlWorkSheet.get_Range("A44", "A48").Font.Size = 9;

            xlWorkSheet.get_Range("A50", "F51").Font.Size = 7;
            xlWorkSheet.Cells[50, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
            xlWorkSheet.Cells[51, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
            xlWorkSheet.get_Range("A50", "F51").WrapText = true;
            xlWorkSheet.get_Range("A50", "F51").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // afbeelding toevoegen
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 180, 62);
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 45, 101, 18);

            string path = @"c:/Willbox/Orders/" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "/Ordernr " + ordernr;

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
                MessageBox.Show("Er is iets fout gelopen bij de aanmaak van het Bestand, probeer het later opnieuw.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }
            //Laden.CloseForm();
            xlWorkBook.SaveAs("c:\\Willbox\\Orders\\" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "\\Ordernr " + ordernr + "\\Orderbevestiging " + ordernr + " - " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            MessageBox.Show("Excel bestand gecreërd met de als naam: Orderbevestiging " + ordernr + " - " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls");
        }

        //knoppen

        private void btnOrder_Click(object sender, EventArgs e)
        {
            bestelbonnummer = txtBestelbonnr.Text;
            btnAnnuleren.Visible = false;
            btnOrder.Visible = false;
            dataNaarOrders();
            dataNaarOrderArtikel();
            if (reorder == "No")
            {
                dataUpdateOfferteStatus();
            }           

            DialogResult dr1 = MessageBox.Show("Wilt u hiervan een orderbevestiging?", "Orderbevestiging", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr1)
            {
                case DialogResult.Yes:
                    Laden.ShowSplashScreen();
                    dataOpvragenOrderbevestigingExcel();
                    makeExcell();
                    Laden.CloseForm();
                    this.Close();
                    break;
                case DialogResult.No:
                    this.Close();
                    break;
                case DialogResult.Abort: break;
            }

            if (Application.OpenForms["Orders"] != null)
            {
                (Application.OpenForms["Orders"] as Orders).dataRefresh();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtLeveringsTermijn.Text = dateTimePicker1.Value.ToShortDateString();
        }
    }
}
