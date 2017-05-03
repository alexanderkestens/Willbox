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
    public partial class OrdersToFacturen : Form
    {
        private int ordernr, klantnr, postcode, aantal, lengte, breedte, hoogte, stansmeskosten, clichekost;
        MySqlConnection conn;
        private String firmaNaam, btwnummer, tav, adres, gemeente, land, omschrijving, referentie, fefco, kwaliteit, bedrukking;
        double prijs, totaalExclBtw, btw21, totaalAlles;

        public OrdersToFacturen()
        {
            InitializeComponent();
        }

        public OrdersToFacturen(int ordernr)
        {
            this.ordernr = ordernr;
            
            InitializeComponent();
            dataKlantOpzoeken();
            txtFactuurnr.Text = codeOpzoekenFacturen().ToString();
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
            cmd.Parameters.Add("@ordernr", MySqlDbType.Int64).Value = ordernr;
            cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = txtFirma.Text;
            cmd.Parameters.Add("@factuurdatum", MySqlDbType.DateTime).Value = dtpDatum.Value;
            cmd.Parameters.Add("@exclusiefbtw", MySqlDbType.Int64).Value = totaalExclBtw;
            cmd.Parameters.Add("@btw", MySqlDbType.Int64).Value = btw21;
            cmd.Parameters.Add("@inclusiefbtw", MySqlDbType.Int64).Value = totaalAlles;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void dataKlantOpzoeken()
        {
            //try
            //{
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klant.klantnr, klant.naam, klant.adres, klant.postcode, klant.gemeente, klant.land, klant.btwnummer, orders.tav, orders.stansmeskosten, orders.clichekost, orderArtikel.aantalgeproduceerd, orderArtikel.prijs, orderArtikel.omschrijving, orderArtikel.ref, orderArtikel.fefco, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.kwaliteit, orderArtikel.bedrukking FROM ((klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr) WHERE orders.ordernr=@ordernr;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ordernr", ordernr);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    firmaNaam = (string)rdr["naam"];
                    klantnr = (int)rdr["klantnr"];
                    btwnummer = (string)rdr["btwnummer"];
                    tav = (string)rdr["tav"];
                    adres = (string)rdr["adres"];
                    gemeente = (string)rdr["gemeente"];
                    land = (string)rdr["land"];
                    postcode = (int)rdr["postcode"];
                    aantal = (int)rdr["aantalgeproduceerd"];
                    prijs = (int)rdr["prijs"];
                    omschrijving = (string)rdr["omschrijving"];
                    referentie = (string)rdr["ref"];
                    fefco = (string)rdr["fefco"];
                    lengte = (int)rdr["lengte"];
                    breedte = (int)rdr["breedte"];
                    hoogte = (int)rdr["hoogte"];
                    kwaliteit = (string)rdr["kwaliteit"];
                    bedrukking = (string)rdr["bedrukking"];
                    stansmeskosten = (int)rdr["stansmeskosten"];
                    clichekost = (int)rdr["clichekost"];
                }
                else
                {
                    MessageBox.Show("Er werd geen Firma gevonden", "Error");
                }
                txtFirma.Text = firmaNaam;
                txtBTWnummer.Text = btwnummer;
                cmd.Connection.Close();

            /*}
            catch
            {

            }*/

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

        private void makeExcellFactuur()
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
            xlWorkSheet.Columns["D"].ColumnWidth = 27;
            xlWorkSheet.Columns["E"].ColumnWidth = 10;
            xlWorkSheet.Columns["F"].ColumnWidth = 10;
            xlWorkSheet.Columns["G"].ColumnWidth = 15;

            xlWorkSheet.get_Range("B33", "F40").RowHeight = 15;
            xlWorkSheet.get_Range("A4", "A15").RowHeight = 13;
            xlWorkSheet.get_Range("A46", "A49").RowHeight = 13;

            xlWorkSheet.Columns["B"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.Columns["D"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            xlWorkSheet.Cells[4, 6] = "Factuur";
            xlWorkSheet.get_Range("F4").Font.Italic = true;
            xlWorkSheet.get_Range("F4").Font.Bold = true;
            xlWorkSheet.get_Range("F4", "G4").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("F4", "G4").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("F4", "G4").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("F4", "G4").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
            xlWorkSheet.Cells[5, 7] = firmaNaam;
            if (tav != "Geen")
            {
                xlWorkSheet.Cells[6, 7] = "T.a.v. " + tav;
            }
            xlWorkSheet.Cells[7, 7] = adres;
            xlWorkSheet.Cells[8, 7] = postcode + " " + gemeente;
            xlWorkSheet.Cells[9, 7] = land;
            xlWorkSheet.Cells[8, 2] = "Factuurnummer: " + txtFactuurnr.Text;
            xlWorkSheet.get_Range("B8").Font.Bold = true;
            xlWorkSheet.Cells[11, 2] = "Klantnummer: " + klantnr;           
            xlWorkSheet.Cells[12, 2] = "BTWnummer: " + btwnummer;

            xlWorkSheet.Cells[14, 2] = "Datum";
            xlWorkSheet.Cells[15, 2] = dtpDatum.Value.ToString("dd-MM-yyyy");
            xlWorkSheet.Cells[14, 4] = "Vervaldatum";
            xlWorkSheet.Cells[15, 4] = dtpDatum.Value.AddDays(30).ToString("dd-MM-yyyy");

            xlWorkSheet.get_Range("B8").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B11").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B12").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B15").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("D14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("D15").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 5; i < 10; i++)
            {
                xlWorkSheet.get_Range("G" + i).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            xlWorkSheet.get_Range("B5", "G15").Font.Size = 10;

            xlWorkSheet.Cells[17, 1] = "Ordernr/Omschrijving";
            xlWorkSheet.Cells[17, 5] = "Aantal";
            xlWorkSheet.Cells[17, 6] = "Prijs/E.";
            xlWorkSheet.Cells[17, 7] = "Euro";

            xlWorkSheet.get_Range("A17", "G17").Font.Size = 14;
            xlWorkSheet.get_Range("A17", "G17").Font.Bold = true;
            xlWorkSheet.get_Range("A17", "G17").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A17", "G17").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A17", "G17").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A17", "G17").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

            //OfferteGegevens
            int j = 19;

            xlWorkSheet.Cells[j, 1] = "# Ordernr " + ordernr;
            xlWorkSheet.get_Range("A" + j).Font.Bold = true;

            xlWorkSheet.Cells[j, 5] = aantal;
            double stukprijs = (prijs / 1000);
            xlWorkSheet.Cells[j, 6] = stukprijs.ToString();
            xlWorkSheet.get_Range("E" + j, "G" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[j, 7] = (aantal * stukprijs).ToString();
            j++;
            if (Regex.Replace(omschrijving, @"\s+", "") != "")
            {
                xlWorkSheet.Cells[j, 2] = "Omschrijving";
                xlWorkSheet.Cells[j, 4] = omschrijving;
                j++;
            }
            if (referentie != "")
            {
                xlWorkSheet.Cells[j, 2] = "Uw referentie";
                xlWorkSheet.Cells[j, 4] = referentie;
                j++;
            }
            xlWorkSheet.Cells[j, 2] = "Fefco";
            xlWorkSheet.Cells[j, 4] = fefco;
            j++;
            xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
            if (fefco == "F110")
            {
                xlWorkSheet.Cells[j, 4] = lengte + " X " + breedte;
            }
            else
            {
                xlWorkSheet.Cells[j, 4] = lengte + " X " + breedte + " X " + hoogte;
            }
            j++;
            if (kwaliteit == "" || kwaliteit == "0")
            {
            }
            else
            {
                xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                xlWorkSheet.Cells[j, 4] = kwaliteit;
                j++;
            }
            xlWorkSheet.Cells[j, 2] = "Bedrukking";
            xlWorkSheet.Cells[j, 4] = bedrukking;
            j++;
            if (Regex.Replace(stansmeskosten.ToString(), @"\s+", "") != "0")
            {
                xlWorkSheet.Cells[j, 2] = "Eenmalige stansmeskost";
                xlWorkSheet.Cells[j, 6] = stansmeskosten;
                xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                j++;
            }
            if (Regex.Replace(clichekost.ToString(), @"\s+", "") != "0")
            {
                xlWorkSheet.Cells[j, 2] = "Eenmalige clichekost";
                xlWorkSheet.Cells[j, 6] = clichekost;
                xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                j++;
            }
            j++;

            xlWorkSheet.get_Range("D35", "G35").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("D43", "G43").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("D43", "G43").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("D43", "G43").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("D43", "G43").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

            double totaal = Math.Round((aantal * stukprijs) + clichekost + stansmeskosten, 2);
            double kmHeffing = Math.Round(totaal * 0.008, 2);
            totaalExclBtw = 0;
            if ((totaal + kmHeffing) > 500)
            {
                totaalExclBtw = Math.Round(totaal + kmHeffing, 2);
            }
            else
            {
                totaalExclBtw = Math.Round(totaal + kmHeffing + 45, 2);
            }
            btw21 = Math.Round(totaalExclBtw * 0.21, 2);
            totaalAlles = Math.Round(btw21 + totaalExclBtw, 2);

            xlWorkSheet.Cells[35, 5] = "Totaal";
            xlWorkSheet.get_Range("E35").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[35, 6] = "€";
            xlWorkSheet.get_Range("F35").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[35, 7] = totaal;
            xlWorkSheet.get_Range("E35").Font.Bold = true;
            xlWorkSheet.Cells[37, 5] = "Km heffing (0,8%)";
            xlWorkSheet.get_Range("E37").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[37, 6] = "€";
            xlWorkSheet.get_Range("F37").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[37, 7] = kmHeffing;
            xlWorkSheet.Cells[38, 5] = "Transportkosten";
            xlWorkSheet.get_Range("E38").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[38, 6] = "€";
            xlWorkSheet.get_Range("F38").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            if ((totaal + kmHeffing) > 500)
            {
                xlWorkSheet.Cells[38, 7] = "0";
            } else
            {
                xlWorkSheet.Cells[38, 7] = "45";
            }
            xlWorkSheet.Cells[40, 5] = "Totaal exclusief BTW";
            xlWorkSheet.get_Range("E40").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[40, 6] = "€";
            xlWorkSheet.get_Range("F40").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[40, 7] = totaalExclBtw;
            xlWorkSheet.get_Range("E40").Font.Bold = true;
            xlWorkSheet.Cells[41, 5] = "21% BTW";
            xlWorkSheet.get_Range("E41").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[41, 6] = "€";
            xlWorkSheet.get_Range("F41").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[41, 7] = btw21;
            xlWorkSheet.Cells[43, 5] = "TOTAAL";
            xlWorkSheet.get_Range("E43").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[43, 6] = "€";
            xlWorkSheet.get_Range("F43").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[43, 7] = totaalAlles;
            xlWorkSheet.get_Range("E43").Font.Bold = true;
            xlWorkSheet.get_Range("E43").Font.Size = 12;

            //xlWorkSheet.Cells[34, 2] = "Leveringstermijn: " + txtLeveringsTermijn.Text;
            xlWorkSheet.get_Range("A46", "G46").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A46", "G46").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A51", "G51").MergeCells = true;
            xlWorkSheet.get_Range("A52", "G52").MergeCells = true;
            xlWorkSheet.Cells[46, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
            xlWorkSheet.Cells[47, 1] = "Betaling : 30 dagen netto";
            xlWorkSheet.Cells[48, 1] = "Alle prijzen in EUR";
            xlWorkSheet.Cells[49, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
            xlWorkSheet.get_Range("A46", "A49").Font.Italic = true;
            xlWorkSheet.get_Range("A46", "A49").Font.Size = 9;

            xlWorkSheet.get_Range("A51", "G52").Font.Size = 7;
            xlWorkSheet.Cells[51, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
            xlWorkSheet.Cells[52, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
            xlWorkSheet.get_Range("A51", "G52").WrapText = true;
            xlWorkSheet.get_Range("A51", "G52").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // afbeelding toevoegen
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 180, 62);
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 45, 101, 18);

            string path = @"c:/Willbox/Facturen/" + firmaNaam.ToLower() + "/";

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
            xlWorkBook.SaveAs("c:\\Willbox\\Facturen\\" + firmaNaam.ToLower() + "\\Factuur " + firmaNaam.ToLower() + " " + txtFactuurnr.Text + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            Laden.CloseForm();
            MessageBox.Show("Excel bestand gecreërd met de als naam: Factuur " + firmaNaam.ToLower() + " " + txtFactuurnr.Text + ".xls");
        }


        //knoppen

        private void btnMaken_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            makeExcellFactuur();
            dataToevoegenFacturen();
            this.Close();
        }

        private void btnAnnuleren_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
