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
    public partial class LeveringExcel : Form
    {
        BindingSource bindingSource;
        MySqlConnection conn;
        private int klantnr, ordernr;
        private String firmaNaam, statusCode;

        public LeveringExcel()
        {
            InitializeComponent();
        }

        private void dataKlantOpzoeken()
        {
            //try
            //{
                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT klantnr, naam FROM klant WHERE naam LIKE @tags;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tags", txtZoekenFirma.Text.ToUpper() + "%");
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
                txtZoekenFirma.Text = firmaNaam;
                cmd.Connection.Close();

                dataOpvragenOrdersFirma();
            /*}
            catch
            {

            }*/
        }

        private void dataOpvragenOrdersFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT orders.ordernr, klant.naam, klant.klantnr, klant.adres, klant.gemeente, klant.postcode, klant.land, orders.datum, orderArtikel.ref, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.prijs, orderArtikel.kwaliteit, orderArtikel.status, orders.tav, orderArtikel.fefco, orderArtikel.bedrukking, orders.stansmeskosten, orders.clichekost, orders.leveringstermijn, orderArtikel.omschrijving, orderArtikel.aantalgeproduceerd, orderArtikel.aantalwwp, orderArtikel.aantalep, orderArtikel.bestelbonnummer FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE klant.klantnr=" + klantnr + " ORDER BY ordernr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOrders.DataSource = bindingSource;

                for (int j = 0; j < 26; j++)
                {
                    dgvOrders.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvOrders.Columns[3].Visible = false;
                dgvOrders.Columns[4].Visible = false;
                dgvOrders.Columns[5].Visible = false;
                dgvOrders.Columns[6].Visible = false;
                for (int i = 15; i < 24; i++)
                {
                    dgvOrders.Columns[i].Visible = false;
                }

                dgvOrders.CurrentCell = dgvOrders.Rows[0].Cells[0];
                ordernr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
                statusCode = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["status"].Value.ToString();

                txtZoekenFirma.Visible = false;
                iconSearch.Visible = false;
                lblBedrijf.Visible = false;
            }
            catch
            {
                //error dataOpvragenOffertesFirma
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code: dataOpvragenOffertesFirma", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            xlWorkSheet.Columns["D"].ColumnWidth = 26;
            xlWorkSheet.Columns["E"].ColumnWidth = 10;
            xlWorkSheet.Columns["F"].ColumnWidth = 11;
            xlWorkSheet.Columns["G"].ColumnWidth = 15;

            xlWorkSheet.get_Range("B33", "F40").RowHeight = 15;
            xlWorkSheet.get_Range("A7", "A12").RowHeight = 13;
            xlWorkSheet.get_Range("A45", "A48").RowHeight = 13;
            xlWorkSheet.get_Range("A37", "A39").RowHeight = 18;
            //pagina 2
            xlWorkSheet.get_Range("A57", "A62").RowHeight = 13;
            xlWorkSheet.get_Range("A95", "A97").RowHeight = 13;
            xlWorkSheet.get_Range("A86", "A89").RowHeight = 18;

            xlWorkSheet.Columns["B"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.Columns["D"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            xlWorkSheet.Cells[7, 6] = "Leveringsnota";
            xlWorkSheet.get_Range("F7").Font.Italic = true;
            xlWorkSheet.get_Range("F7").Font.Bold = true;
            xlWorkSheet.get_Range("F7", "G7").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("F7", "G7").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("F7", "G7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("F7", "G7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
            xlWorkSheet.Cells[8, 7] = dgvOrders.Rows[0].Cells["naam"].Value.ToString();
            if (dgvOrders.Rows[0].Cells["tav"].Value.ToString() != "Geen")
            {
                xlWorkSheet.Cells[9, 7] = "T.a.v. " + dgvOrders.Rows[0].Cells["tav"].Value.ToString();
            }
            string adres = dgvOrders.Rows[0].Cells["adres"].Value.ToString().ToLower();
            if (dgvOrders.Rows[0].Cells["adres"].Value.ToString() != "")
            {
                adres = adres.First().ToString().ToUpper() + String.Join("", adres.Skip(1));
            }
            xlWorkSheet.Cells[10, 7] = adres;
            xlWorkSheet.Cells[11, 7] = dgvOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvOrders.Rows[0].Cells["gemeente"].Value.ToString();
            xlWorkSheet.Cells[12, 7] = dgvOrders.Rows[0].Cells["land"].Value.ToString();
            xlWorkSheet.Cells[11, 2] = "Klantnummer: " + klantnr;
            xlWorkSheet.Cells[12, 2] = "Datum: " + DateTime.Now.ToString("dd MMM yyyy");
            xlWorkSheet.get_Range("B11").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B12").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 8; i < 13; i++)
            {
                xlWorkSheet.get_Range("G" + i).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            xlWorkSheet.get_Range("B7", "G12").Font.Size = 10;

            xlWorkSheet.Cells[14, 1] = "Ordernr/Omschrijving";
            xlWorkSheet.Cells[14, 7] = "Aantal";

            xlWorkSheet.get_Range("A14", "G14").Font.Size = 14;
            xlWorkSheet.get_Range("A14", "G14").Font.Bold = true;
            xlWorkSheet.get_Range("A14", "G14").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A14", "G14").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A14", "G14").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A14", "G14").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

            //OfferteGegevens
            int j = 16;
            int laatste = 16;
            int aantalwwp = 0;
            int aantalep = 0;
            for (int i = 0; i < dgvOrders.RowCount - 1; i++)
            {
            LOOP:
                aantalwwp += int.Parse(dgvOrders.Rows[i].Cells["aantalwwp"].Value.ToString());
                aantalep += int.Parse(dgvOrders.Rows[i].Cells["aantalep"].Value.ToString());
                xlWorkSheet.Cells[j, 1] = "# Ordernr " + dgvOrders.Rows[i].Cells["ordernr"].Value.ToString();
                xlWorkSheet.get_Range("A" + j).Font.Bold = true;
                if (dgvOrders.Rows[i].Cells["aantalgeproduceerd"].Value.ToString() != "")
                {
                    xlWorkSheet.Cells[j, 7] = dgvOrders.Rows[i].Cells["aantalgeproduceerd"].Value.ToString();
                } else
                {
                    xlWorkSheet.Cells[j, 7] = dgvOrders.Rows[i].Cells["aantal"].Value.ToString();
                }
                xlWorkSheet.get_Range("E" + j, "G" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                j++;
                if (dgvOrders.Rows[i].Cells["bestelbonnummer"].Value.ToString() != "")
                {
                    xlWorkSheet.Cells[j, 2] = "Bestelbonnummer";
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
                    j++;
                }
                if (Regex.Replace(dgvOrders.Rows[i].Cells["omschrijving"].Value.ToString(), @"\s+", "") != "")
                {
                    xlWorkSheet.Cells[j, 2] = "Omschrijving";
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["omschrijving"].Value.ToString();
                    j++;
                }
                if (dgvOrders.Rows[i].Cells["ref"].Value.ToString() != "")
                {
                    xlWorkSheet.Cells[j, 2] = "Uw referentie";
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["ref"].Value.ToString();
                    j++;
                }
                xlWorkSheet.Cells[j, 2] = "Fefco";
                xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["fefco"].Value.ToString();
                j++;
                xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
                if (dgvOrders.Rows[i].Cells["fefco"].Value.ToString() == "F110")
                {
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOrders.Rows[i].Cells["breedte"].Value.ToString();
                }
                else
                {
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOrders.Rows[i].Cells["breedte"].Value.ToString() + " X " + dgvOrders.Rows[i].Cells["hoogte"].Value.ToString();
                }
                j++;
                if (dgvOrders.Rows[i].Cells["kwaliteit"].Value.ToString() == "" || dgvOrders.Rows[i].Cells["kwaliteit"].Value.ToString() == "0")
                {
                }
                else
                {
                    xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["kwaliteit"].Value.ToString();
                    j++;
                }
                if (dgvOrders.Rows[i].Cells["Bedrukking"].Value.ToString() != "Geen")
                {
                    xlWorkSheet.Cells[j, 2] = "Bedrukking";
                    xlWorkSheet.Cells[j, 4] = dgvOrders.Rows[i].Cells["Bedrukking"].Value.ToString();
                    j++;
                }
                j++;
                if (j > 37 && j < 66)
                {
                    xlWorkSheet.get_Range("A" + laatste, "G" + j).Cells.Clear();
                    xlWorkSheet.Cells[36, 7] = "Pagina 1 van 2";
                    j = 66;
                    goto LOOP;
                }
                else
                {
                    laatste = j;
                }
            }

            //Extra pagina
            if (j > 41)
            {
                xlWorkSheet.Cells[87, 1] = "Aantal paletten:";
                xlWorkSheet.Cells[89, 1] = "Teruggave:";
                xlWorkSheet.Cells[87, 4] = aantalwwp + " wegwerppaletten";
                xlWorkSheet.Cells[88, 4] = aantalep + " europaletten";
                xlWorkSheet.Cells[89, 4] = "Europaletten: ...........";
                xlWorkSheet.Cells[90, 1] = "Voor akkoord ontvangst";
                xlWorkSheet.Cells[90, 5] = "Naam in drukletters";
                xlWorkSheet.get_Range("A87", "E90").Font.Bold = true;
                xlWorkSheet.get_Range("A90", "G90").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A87", "G87").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A90", "G90").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("D90", "D94").Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 745, 180, 62);
                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 790, 101, 18);
                xlWorkSheet.Cells[57, 6] = "Leveringsnota";
                xlWorkSheet.get_Range("F57").Font.Italic = true;
                xlWorkSheet.get_Range("F57").Font.Bold = true;
                xlWorkSheet.get_Range("F57", "G57").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("F57", "G57").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("F57", "G57").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("F57", "G57").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
                xlWorkSheet.Cells[58, 7] = dgvOrders.Rows[0].Cells["naam"].Value.ToString();
                if (dgvOrders.Rows[0].Cells["tav"].Value.ToString() != "Geen")
                {
                    xlWorkSheet.Cells[59, 7] = "T.a.v. " + dgvOrders.Rows[0].Cells["tav"].Value.ToString();
                }
                xlWorkSheet.Cells[60, 7] = dgvOrders.Rows[0].Cells["adres"].Value.ToString().ToLower();
                xlWorkSheet.Cells[61, 7] = dgvOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvOrders.Rows[0].Cells["gemeente"].Value.ToString();
                xlWorkSheet.Cells[62, 7] = dgvOrders.Rows[0].Cells["land"].Value.ToString();
                xlWorkSheet.Cells[61, 2] = "Klantnummer: " + klantnr;
                xlWorkSheet.Cells[62, 2] = "OfferteDatum: " + DateTime.Now.ToString("dd MMM yyyy");
                xlWorkSheet.get_Range("B61").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.get_Range("B62").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                for (int k = 58; k < 63; k++)
                {
                    xlWorkSheet.get_Range("G" + k).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                }
                xlWorkSheet.get_Range("B57", "G63").Font.Size = 10;

                xlWorkSheet.Cells[64, 1] = "Offertenr/Omschrijving";
                xlWorkSheet.Cells[64, 7] = "Aantal";

                xlWorkSheet.Cells[86, 7] = "Pagina 2 van 2";

                xlWorkSheet.get_Range("A64", "G64").Font.Size = 14;
                xlWorkSheet.get_Range("A64", "G64").Font.Bold = true;
                xlWorkSheet.get_Range("A64", "G64").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A64", "G64").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A64", "G64").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A64", "G64").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                xlWorkSheet.get_Range("A95", "G95").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A95", "G95").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A99", "G99").MergeCells = true;
                xlWorkSheet.get_Range("A100", "G100").MergeCells = true;
                xlWorkSheet.Cells[95, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
                xlWorkSheet.Cells[96, 1] = "Km heffing: 0,8%";
                xlWorkSheet.Cells[97, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
                xlWorkSheet.get_Range("A95", "A97").Font.Italic = true;
                xlWorkSheet.get_Range("A95", "A97").Font.Size = 9;

                xlWorkSheet.get_Range("A99", "G100").Font.Size = 7;
                xlWorkSheet.Cells[99, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
                xlWorkSheet.Cells[100, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
                xlWorkSheet.get_Range("A99", "G100").WrapText = true;
                xlWorkSheet.get_Range("A99", "G100").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            }

            xlWorkSheet.Cells[37, 1] = "Aantal paletten:";
            xlWorkSheet.Cells[39, 1] = "Teruggave:";
            xlWorkSheet.Cells[37, 4] = aantalwwp + " wegwerppaletten";
            xlWorkSheet.Cells[38, 4] = aantalep + " europaletten";
            xlWorkSheet.Cells[39, 4] = "Europaletten: ...........";
            xlWorkSheet.Cells[40, 1] = "Voor akkoord ontvangst";
            xlWorkSheet.Cells[40, 5] = "Naam in drukletters";
            xlWorkSheet.get_Range("A37", "E40").Font.Bold = true;
            xlWorkSheet.get_Range("A40", "G40").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A37", "G37").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A40", "G40").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("D40", "D44").Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

            xlWorkSheet.get_Range("A45", "G45").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("A45", "G45").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
            xlWorkSheet.get_Range("A49", "G49").MergeCells = true;
            xlWorkSheet.get_Range("A50", "G50").MergeCells = true;
            xlWorkSheet.Cells[45, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
            xlWorkSheet.Cells[46, 1] = "Km heffing: 0,8%";
            xlWorkSheet.Cells[47, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
            xlWorkSheet.get_Range("A43", "A48").Font.Italic = true;
            xlWorkSheet.get_Range("A43", "A48").Font.Size = 9;

            xlWorkSheet.get_Range("A49", "G50").Font.Size = 7;
            xlWorkSheet.Cells[49, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
            xlWorkSheet.Cells[50, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
            xlWorkSheet.get_Range("A49", "G50").WrapText = true;
            xlWorkSheet.get_Range("A49", "G50").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // afbeelding toevoegen
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 180, 62);
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 45, 101, 18);

            string path = @"c:/Willbox/Zendnota's/" + firmaNaam.ToLower() + "/";

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
            lblInfo.Text = "Excel bestand gecreërd met de als naam: Zendnota " + DateTime.Now.ToString("yyyy-MM-dd HH mm") + " - " + firmaNaam.ToLower() + ".xls";
            Laden.CloseForm();
            xlWorkBook.SaveAs("c:\\Willbox\\Zendnota's\\" + firmaNaam.ToLower() + "\\Zendnota " + DateTime.Now.ToString("yyyy-MM-dd HH mm") + " - " + firmaNaam.ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            //MessageBox.Show("Excel bestand gecreërd met de als naam: Zendnota " + DateTime.Now.ToString("yyyy-MM-dd HH mm") + " - " + firmaNaam.ToLower() + ".xls");
        }

        //knoppen

        private void txtZoekenFirma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Regex.Replace(txtZoekenFirma.Text, @"\s+", "") == "")
                {
                    MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
                }
                else
                {
                    dataKlantOpzoeken();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(97, 107, 253);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            btnAdd.Visible = false;
            btnLeegmaken.Visible = false;
            btnDelete.Visible = false;
            btnExcel.Visible = false;
            int blauw = 0;
            int count = dgvOrders.RowCount - 1;
            for (int m = 0; m < count; m++)
            {
                if (dgvOrders.Rows[blauw].DefaultCellStyle.BackColor != Color.FromArgb(97, 107, 253))
                {
                    dgvOrders.Rows.Remove(dgvOrders.Rows[blauw]);
                }
                else
                {
                    blauw++;
                }
            }
            makeExcell();
            btnAdd.Visible = true;
            btnLeegmaken.Visible = true;
            btnDelete.Visible = true;
            btnExcel.Visible = true;
        }

        private void LeveringExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms["Orders"] != null)
            {
                (Application.OpenForms["Orders"] as Orders).dataClose("leveringExcel");
            }
        }

        private void iconSearch_Click(object sender, EventArgs e)
        {
            if (Regex.Replace(txtZoekenFirma.Text, @"\s+", "") == "")
            {
                MessageBox.Show("Gelieve een Geldige Firma op te geven aub!");
            }
            else
            {
                dataKlantOpzoeken();
            }
        }

        private void btnLeegmaken_Click(object sender, EventArgs e)
        {
            dgvOrders.DataSource = null;
            txtZoekenFirma.Text = "";
            txtZoekenFirma.Visible = true;
            iconSearch.Visible = true;
            lblBedrijf.Visible = true;
            lblInfo.Text = "";
        }
    }
}
