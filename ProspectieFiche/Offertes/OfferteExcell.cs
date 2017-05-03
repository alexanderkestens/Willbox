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
    public partial class OfferteExcell : Form
    {
        BindingSource bindingSource;
        MySqlConnection conn;
        private int klantnr, offertenr;
        private String firmaNaam, statusCode;

        public OfferteExcell()
        {
            InitializeComponent();
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

                dataOpvragenOffertesFirma();
            }
            catch
            {

            }
        }

        private void dataOpvragenOffertesFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT offertes.offertenr, offerteArtikel.offerteartikelnr AS 'artikelnr', klant.naam, klant.klantnr, offertes.datum, offerteArtikel.ref, offerteArtikel.lengte, offerteArtikel.breedte, offerteArtikel.hoogte, offerteArtikel.aantal, offerteArtikel.prijs, offerteArtikel.kwaliteit, offerteArtikel.status, klant.adres, klant.gemeente, klant.postcode, klant.land, offertes.tav, offerteArtikel.fefco, offerteArtikel.bedrukking, offerteArtikel.aantal2, offerteArtikel.prijs2, offerteArtikel.kwaliteit2, offerteArtikel.aantal3, offerteArtikel.prijs3, offerteArtikel.kwaliteit3, offertes.stansmeskost, offertes.clichekost, offertes.leveringstermijn, offerteArtikel.omschrijving FROM (klant JOIN offertes ON klant.klantnr=offertes.klantnr) JOIN offerteArtikel ON offertes.offertenr=offerteArtikel.offertenr WHERE klant.klantnr=" + klantnr + " ORDER BY offertenr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOffertes.DataSource = bindingSource;

                for (int j = 0; j < 14; j++)
                {
                    dgvOffertes.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                dgvOffertes.Columns[1].Visible = false;
                for (int i = 12; i < 28; i++)
                {
                    dgvOffertes.Columns[i].Visible = false;
                }

                dgvOffertes.CurrentCell = dgvOffertes.Rows[0].Cells[0];
                offertenr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["offertenr"].Value.ToString());
                statusCode = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["status"].Value.ToString();

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

            try
            {
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

                xlWorkSheet.Cells[7, 5] = "Offerte";
                xlWorkSheet.get_Range("E7").Font.Italic = true;
                xlWorkSheet.get_Range("E7").Font.Bold = true;
                xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("E7", "F7").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
                xlWorkSheet.Cells[8, 6] = dgvOffertes.Rows[0].Cells["naam"].Value.ToString();
                if (dgvOffertes.Rows[0].Cells["tav"].Value.ToString() != "Geen")
                {
                    xlWorkSheet.Cells[9, 6] = "T.a.v. " + dgvOffertes.Rows[0].Cells["tav"].Value.ToString();
                }
                string adres = dgvOffertes.Rows[0].Cells["adres"].Value.ToString().ToLower();
                if (dgvOffertes.Rows[0].Cells["adres"].Value.ToString() != "")
                {
                    adres = adres.First().ToString().ToUpper() + String.Join("", adres.Skip(1));
                }
                xlWorkSheet.Cells[10, 6] = adres;
                xlWorkSheet.Cells[11, 6] = dgvOffertes.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvOffertes.Rows[0].Cells["gemeente"].Value.ToString();
                xlWorkSheet.Cells[12, 6] = dgvOffertes.Rows[0].Cells["land"].Value.ToString();
                xlWorkSheet.Cells[11, 2] = "Klantnummer: " + klantnr;
                xlWorkSheet.Cells[12, 2] = "OfferteDatum: " + DateTime.Now.ToString("dd MMM yyyy");
                xlWorkSheet.get_Range("B11").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.get_Range("B12").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                for (int i = 8; i < 13; i++)
                {
                    xlWorkSheet.get_Range("F" + i).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                }
                xlWorkSheet.get_Range("B7", "F12").Font.Size = 10;

                xlWorkSheet.Cells[15, 1] = "Offertenr/Omschrijving";
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
                int laatste = 17;

                for (int i = 0; i < dgvOffertes.RowCount - 1; i++)
                {
                LOOP:
                    xlWorkSheet.Cells[j, 1] = "# Offertenr " + dgvOffertes.Rows[i].Cells["offertenr"].Value.ToString();
                    xlWorkSheet.get_Range("A" + j).Font.Bold = true;

                    xlWorkSheet.Cells[j, 5] = dgvOffertes.Rows[i].Cells["aantal"].Value.ToString();
                    xlWorkSheet.Cells[j, 6] = (double.Parse(dgvOffertes.Rows[i].Cells["prijs"].Value.ToString()) / 1000);
                    xlWorkSheet.get_Range("E" + j, "F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    j++;
                    if (Regex.Replace(dgvOffertes.Rows[i].Cells["omschrijving"].Value.ToString(), @"\s+", "") != "")
                    {
                        xlWorkSheet.Cells[j, 2] = "Omschrijving";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["omschrijving"].Value.ToString();
                        j++;
                    }
                    if (dgvOffertes.Rows[i].Cells["ref"].Value.ToString() != "")
                    {
                        xlWorkSheet.Cells[j, 2] = "Uw referentie";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["ref"].Value.ToString();
                        j++;
                    }
                    xlWorkSheet.Cells[j, 2] = "Fefco";
                    xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["fefco"].Value.ToString();
                    j++;
                    xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
                    if (dgvOffertes.Rows[i].Cells["fefco"].Value.ToString() == "F110")
                    {
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString();
                    }
                    else
                    {
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["hoogte"].Value.ToString();
                    }
                    j++;
                    if (dgvOffertes.Rows[i].Cells["kwaliteit"].Value.ToString() == "" || dgvOffertes.Rows[i].Cells["kwaliteit"].Value.ToString() == "0")
                    {
                    }
                    else
                    {
                        xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["kwaliteit"].Value.ToString();
                        j++;
                    }
                    if (dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString() != "Geen")
                    {
                        xlWorkSheet.Cells[j, 2] = "Bedrukking";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString();
                        j++;
                    }
                    if (dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString() != "")
                    {
                        xlWorkSheet.Cells[j, 2] = "Leveringstermijn";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString();
                        j++;
                    }
                    if (Regex.Replace(dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString(), @"\s+", "") != "0")
                    {
                        xlWorkSheet.Cells[j, 2] = "Eenmalige stansmeskost";
                        xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString();
                        xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        j++;
                    }
                    if (Regex.Replace(dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString(), @"\s+", "") != "0")
                    {
                        xlWorkSheet.Cells[j, 2] = "Eenmalige clichekost";
                        xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString();
                        xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        j++;
                    }
                    j++;
                    if (j > 42 && j < 68)
                    {
                        xlWorkSheet.get_Range("A" + laatste, "F" + j).Cells.Clear();
                        xlWorkSheet.Cells[42, 6] = "Pagina 1 van 2";
                        j = 68;
                        goto LOOP;
                    }
                    else
                    {
                        laatste = j;
                    }

                    //Kwaliteit2, aantal2, prijs2
                    if (dgvOffertes.Rows[i].Cells["kwaliteit2"].Value.ToString() == "" || (dgvOffertes.Rows[i].Cells["aantal2"].Value.ToString() == "0" || dgvOffertes.Rows[i].Cells["aantal2"].Value.ToString() == "") || (dgvOffertes.Rows[i].Cells["prijs2"].Value.ToString() == "" || dgvOffertes.Rows[i].Cells["prijs2"].Value.ToString() == "0"))
                    {
                    }
                    else
                    {
                        xlWorkSheet.Cells[j, 1] = "# Offertenr " + dgvOffertes.Rows[i].Cells["offertenr"].Value.ToString();
                        xlWorkSheet.get_Range("A" + j).Font.Bold = true;
                        xlWorkSheet.Cells[j, 5] = dgvOffertes.Rows[i].Cells["aantal2"].Value.ToString();
                        xlWorkSheet.Cells[j, 6] = (double.Parse(dgvOffertes.Rows[i].Cells["prijs2"].Value.ToString()) / 1000);
                        xlWorkSheet.get_Range("E" + j, "F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        j++;
                        if (dgvOffertes.Rows[i].Cells["ref"].Value.ToString() != "")
                        {
                            xlWorkSheet.Cells[j, 2] = "Uw referentie";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["ref"].Value.ToString();
                            j++;
                        }
                        xlWorkSheet.Cells[j, 2] = "Fefco";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["fefco"].Value.ToString();
                        j++;
                        xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
                        if (dgvOffertes.Rows[i].Cells["fefco"].Value.ToString() == "F110")
                        {
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString();
                        }
                        else
                        {
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["hoogte"].Value.ToString();
                        }
                        j++;
                        if (dgvOffertes.Rows[i].Cells["kwaliteit2"].Value.ToString() == "" || dgvOffertes.Rows[i].Cells["kwaliteit2"].Value.ToString() == "0")
                        {
                        }
                        else
                        {
                            xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["kwaliteit2"].Value.ToString();
                            j++;
                        }
                        if (dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString() != "Geen")
                        {
                            xlWorkSheet.Cells[j, 2] = "Bedrukking";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString();
                            j++;
                        }
                        if (dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString() != "")
                        {
                            xlWorkSheet.Cells[j, 2] = "Leveringstermijn";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString();
                            j++;
                        }
                        if (Regex.Replace(dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString(), @"\s+", "") != "0")
                        {
                            xlWorkSheet.Cells[j, 2] = "Eenmalige stansmeskost";
                            xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString();
                            xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            j++;
                        }
                        if (Regex.Replace(dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString(), @"\s+", "") != "0")
                        {
                            xlWorkSheet.Cells[j, 2] = "Eenmalige clichekost";
                            xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString();
                            xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            j++;
                        }
                        j++;
                    }
                    if (j > 42 && j < 68)
                    {
                        xlWorkSheet.get_Range("A" + laatste, "F" + j).Cells.Clear();
                        j = 68;
                        goto LOOP;
                    }
                    else
                    {
                        laatste = j;
                    }

                    //Kwaliteit3, aantal3, prijs3
                    if (dgvOffertes.Rows[i].Cells["kwaliteit3"].Value.ToString() == "" || (dgvOffertes.Rows[i].Cells["aantal3"].Value.ToString() == "0" || dgvOffertes.Rows[i].Cells["aantal3"].Value.ToString() == "") || (dgvOffertes.Rows[i].Cells["prijs3"].Value.ToString() == "" || dgvOffertes.Rows[i].Cells["prijs3"].Value.ToString() == "0"))
                    {
                    }
                    else
                    {
                        xlWorkSheet.Cells[j, 1] = "# Offertenr " + dgvOffertes.Rows[i].Cells["offertenr"].Value.ToString();
                        xlWorkSheet.get_Range("A" + j).Font.Bold = true;
                        xlWorkSheet.Cells[j, 5] = dgvOffertes.Rows[i].Cells["aantal3"].Value.ToString();
                        xlWorkSheet.Cells[j, 6] = (double.Parse(dgvOffertes.Rows[i].Cells["prijs3"].Value.ToString()) / 1000);
                        xlWorkSheet.get_Range("E" + j, "F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        j++;
                        if (dgvOffertes.Rows[i].Cells["ref"].Value.ToString() != "")
                        {
                            xlWorkSheet.Cells[j, 2] = "Uw referentie";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["ref"].Value.ToString();
                            j++;
                        }
                        xlWorkSheet.Cells[j, 2] = "Fefco";
                        xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["fefco"].Value.ToString();
                        j++;
                        xlWorkSheet.Cells[j, 2] = "Afmetingen (in mm)";
                        if (dgvOffertes.Rows[i].Cells["fefco"].Value.ToString() == "F110")
                        {
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString();
                        }
                        else
                        {
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["lengte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["breedte"].Value.ToString() + " X " + dgvOffertes.Rows[i].Cells["hoogte"].Value.ToString();
                        }
                        j++;
                        if (dgvOffertes.Rows[i].Cells["kwaliteit3"].Value.ToString() == "" || dgvOffertes.Rows[i].Cells["kwaliteit3"].Value.ToString() == "0")
                        {
                        }
                        else
                        {
                            xlWorkSheet.Cells[j, 2] = "Kwaliteit";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["kwaliteit3"].Value.ToString();
                            j++;
                        }
                        if (dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString() != "Geen")
                        {
                            xlWorkSheet.Cells[j, 2] = "Bedrukking";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["Bedrukking"].Value.ToString();
                            j++;
                        }
                        if (dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString() != "")
                        {
                            xlWorkSheet.Cells[j, 2] = "Leveringstermijn";
                            xlWorkSheet.Cells[j, 4] = dgvOffertes.Rows[i].Cells["leveringstermijn"].Value.ToString();
                            j++;
                        }
                        if (Regex.Replace(dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString(), @"\s+", "") != "0")
                        {
                            xlWorkSheet.Cells[j, 2] = "Eenmalige stansmeskost";
                            xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["stansmeskost"].Value.ToString();
                            xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            j++;
                        }
                        if (Regex.Replace(dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString(), @"\s+", "") != "0")
                        {
                            xlWorkSheet.Cells[j, 2] = "Eenmalige clichekost";
                            xlWorkSheet.Cells[j, 6] = dgvOffertes.Rows[i].Cells["clichekost"].Value.ToString();
                            xlWorkSheet.get_Range("F" + j).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            j++;
                        }
                        j++;
                    }
                    if (j > 42 && j < 68)
                    {
                        xlWorkSheet.get_Range("A" + laatste, "F" + j).Cells.Clear();
                        xlWorkSheet.Cells[42, 6] = "Pagina 1 van 2";
                        j = 68;
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
                    xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 745, 180, 62);
                    xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 790, 101, 18);
                    xlWorkSheet.Cells[58, 5] = "Offerte";
                    xlWorkSheet.get_Range("E58").Font.Italic = true;
                    xlWorkSheet.get_Range("E58").Font.Bold = true;
                    xlWorkSheet.get_Range("E58", "F58").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWorkSheet.get_Range("E58", "F58").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                    xlWorkSheet.get_Range("E58", "F58").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWorkSheet.get_Range("E58", "F58").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
                    xlWorkSheet.Cells[59, 6] = dgvOffertes.Rows[0].Cells["naam"].Value.ToString();
                    if (dgvOffertes.Rows[0].Cells["tav"].Value.ToString() != "Geen")
                    {
                        xlWorkSheet.Cells[60, 6] = "T.a.v. " + dgvOffertes.Rows[0].Cells["tav"].Value.ToString();
                    }
                    xlWorkSheet.Cells[61, 6] = dgvOffertes.Rows[0].Cells["adres"].Value.ToString().ToLower();
                    xlWorkSheet.Cells[62, 6] = dgvOffertes.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvOffertes.Rows[0].Cells["gemeente"].Value.ToString();
                    xlWorkSheet.Cells[63, 6] = dgvOffertes.Rows[0].Cells["land"].Value.ToString();
                    xlWorkSheet.Cells[62, 2] = "Klantnummer: " + klantnr;
                    xlWorkSheet.Cells[63, 2] = "OfferteDatum: " + DateTime.Now.ToString("dd MMM yyyy");
                    xlWorkSheet.get_Range("B63").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.get_Range("B64").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    for (int k = 58; k < 64; k++)
                    {
                        xlWorkSheet.get_Range("F" + k).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    xlWorkSheet.get_Range("B58", "F64").Font.Size = 10;

                    xlWorkSheet.Cells[66, 1] = "Offertenr/Omschrijving";
                    xlWorkSheet.Cells[66, 5] = "Aantal";
                    xlWorkSheet.Cells[66, 6] = "Prijs/Eenheid";

                    xlWorkSheet.Cells[93, 6] = "Pagina 2 van 2";

                    xlWorkSheet.get_Range("A66", "F66").Font.Size = 14;
                    xlWorkSheet.get_Range("A66", "F66").Font.Bold = true;
                    xlWorkSheet.get_Range("A66", "F66").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWorkSheet.get_Range("A66", "F66").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                    xlWorkSheet.get_Range("A66", "F66").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWorkSheet.get_Range("A66", "F66").Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                    xlWorkSheet.get_Range("A94", "F94").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWorkSheet.get_Range("A94", "F94").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                    xlWorkSheet.get_Range("A101", "F101").MergeCells = true;
                    xlWorkSheet.get_Range("A102", "F102").MergeCells = true;
                    xlWorkSheet.Cells[94, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
                    xlWorkSheet.Cells[95, 1] = "Km heffing: 0,8%";
                    xlWorkSheet.Cells[96, 1] = "Betaling : 30 dagen netto";
                    xlWorkSheet.Cells[97, 1] = "Geldigheidsduur offerte : 30 dagen";
                    xlWorkSheet.Cells[98, 1] = "Alle prijzen in EUR ex. BTW";
                    xlWorkSheet.Cells[99, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
                    xlWorkSheet.get_Range("A94", "A99").Font.Italic = true;
                    xlWorkSheet.get_Range("A94", "A99").Font.Size = 9;

                    xlWorkSheet.get_Range("A101", "F102").Font.Size = 7;
                    xlWorkSheet.Cells[101, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
                    xlWorkSheet.Cells[102, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
                    xlWorkSheet.get_Range("A101", "F102").WrapText = true;
                    xlWorkSheet.get_Range("A101", "F102").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                }

                //xlWorkSheet.Cells[34, 2] = "Leveringstermijn: " + txtLeveringsTermijn.Text;
                xlWorkSheet.get_Range("A43", "F43").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A43", "F43").Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A50", "F50").MergeCells = true;
                xlWorkSheet.get_Range("A51", "F51").MergeCells = true;
                xlWorkSheet.Cells[43, 1] = "Leveringsvoorwaarden : Franco vanaf 500 euro (<45 euro)";
                xlWorkSheet.Cells[44, 1] = "Km heffing: 0,8%";
                xlWorkSheet.Cells[45, 1] = "Betaling : 30 dagen netto";
                xlWorkSheet.Cells[46, 1] = "Geldigheidsduur offerte : 30 dagen";
                xlWorkSheet.Cells[47, 1] = "Alle prijzen in EUR ex. BTW";
                xlWorkSheet.Cells[48, 1] = "Algemene verkoopsvoorwaarden : www.willbox.be";
                xlWorkSheet.get_Range("A43", "A48").Font.Italic = true;
                xlWorkSheet.get_Range("A43", "A48").Font.Size = 9;

                xlWorkSheet.get_Range("A50", "F51").Font.Size = 7;
                xlWorkSheet.Cells[50, 1] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
                xlWorkSheet.Cells[51, 1] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";
                xlWorkSheet.get_Range("A50", "F51").WrapText = true;
                xlWorkSheet.get_Range("A50", "F51").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // afbeelding toevoegen
                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 180, 62);
                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 45, 101, 18);

                string path = @"c:/Willbox/Offertes/" + firmaNaam.ToLower() + "/";

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
                Laden.CloseForm();
                xlWorkBook.SaveAs("c:\\Willbox\\Offertes\\" + firmaNaam.ToLower() + "\\Offerenr " + DateTime.Now.ToString("yyyy-MM-dd HH mm") + " - " + firmaNaam.ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                lblInfo.Text = "Excel bestand gecreërd met de als naam: Offerenr " + DateTime.Now.ToString("yyyy - MM - dd HH mm") + " - " + firmaNaam.ToLower() + ".xls";
                //MessageBox.Show("Excel bestand gecreërd met de als naam: Offerenr " + DateTime.Now.ToString("yyyy-MM-dd HH mm") + " - " + firmaNaam.ToLower() + ".xls");
            }
            catch
            {
                lblInfo.Text = "Er is iets fout gelopen bij de aanmaak van het Bestand, probeer het later opnieuw.";
                //MessageBox.Show("Er is iets fout gelopen bij de aanmaak van het Bestand, probeer het later opnieuw.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                Laden.CloseForm();
            }

        }

        //knoppen

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(97, 107, 253);
        }

        private void btnLeegmaken_Click(object sender, EventArgs e)
        {
            dgvOffertes.DataSource = null;
            txtZoekenFirma.Text = "";
            txtZoekenFirma.Visible = true;
            iconSearch.Visible = true;
            lblBedrijf.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            btnExcel.Visible = true;
            lblInfo.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            lblInfo.Text = "";
            btnAdd.Visible = false;
            btnLeegmaken.Visible = false;
            btnDelete.Visible = false;
            btnExcel.Visible = false;
            int blauw = 0;
            int count = dgvOffertes.RowCount - 1;
            for (int m = 0; m < count; m++)
            {
                if (dgvOffertes.Rows[blauw].DefaultCellStyle.BackColor != Color.FromArgb(97, 107, 253))
                {
                    dgvOffertes.Rows.Remove(dgvOffertes.Rows[blauw]);
                }
                else
                {
                    blauw++;
                }
            }
            makeExcell();
            btnLeegmaken.Visible = true;            
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

        private void OfferteExcell_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Offertes"] != null)
            {
                (Application.OpenForms["Offertes"] as Offertes).dataClose("offerteExcell");
            }
        }

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
    }
}
