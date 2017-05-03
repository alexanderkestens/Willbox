using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProspectieFiche
{
    public partial class Orders : Form
    {
        private Main main;
        private int codeUser;
        BindingSource bindingSource;
        MySqlConnection conn;
        private String firmaNaam, productionFirmaNaam, productionAantalPlaten, soortorder;
        private int klantnr, klantnrOpzoeken, ordernr;
        private int productionKlantnr, productionOrdernr;
        LeveringExcel leveringExcel = null;

        public Orders()
        {
            InitializeComponent();
        }

        public Orders(Main main, int codeUser)
        {
            this.main = main;
            this.codeUser = codeUser;
            InitializeComponent();
            dataOpvragenOrders();
            dataOpvragenProduction();
        }

        private void dataOpvragenOrders()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT orders.ordernr, klant.naam, klant.klantnr, orders.datum, orderArtikel.ref, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.prijs, orderArtikel.kwaliteit, orderArtikel.status, orderArtikel.gonbesteld AS 'gon', orderArtikel.soortorder FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orderArtikel.status='AFGEWERKT' OR orderArtikel.status='ROOD' ORDER BY ordernr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOrders.DataSource = bindingSource;

                for (int j = 0; j < 14; j++)
                {
                    dgvOrders.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                dgvOrders.Columns[11].Visible = false;
                dgvOrders.Columns[13].Visible = false;

                dgvOrders.CurrentCell = dgvOrders.Rows[0].Cells[0];
                klantnr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmaNaam = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                ordernr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
                soortorder = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["soortorder"].Value.ToString();
            }
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataOpvragenOrdersFirma()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT orders.ordernr, klant.naam, klant.klantnr, orders.datum, orderArtikel.ref, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.prijs, orderArtikel.kwaliteit, orderArtikel.status, orderArtikel.gonbesteld AS 'gon', orderArtikel.soortorder FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE klant.klantnr=" + klantnrOpzoeken + " AND (orderArtikel.status='AFGEWERKT' OR orderArtikel.status='ROOD') ORDER BY ordernr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvOrders.DataSource = bindingSource;

                for (int j = 0; j < 14; j++)
                {
                    dgvOrders.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                dgvOrders.Columns[11].Visible = false;
                dgvOrders.Columns[13].Visible = false;

                dgvOrders.CurrentCell = dgvOrders.Rows[0].Cells[0];
                klantnr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmaNaam = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                ordernr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
                soortorder = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["soortorder"].Value.ToString();
            }
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataOpvragenProduction()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT orders.ordernr, klant.naam, klant.klantnr, orders.datum, orderArtikel.ref, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.prijs, orderArtikel.kwaliteit, orderArtikel.status FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orderArtikel.status='IN PRODUCTIE' ORDER BY ordernr ASC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvProductie.DataSource = bindingSource;

                for (int j = 0; j < 11; j++)
                {
                    dgvProductie.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvProductie.CurrentCell = dgvProductie.Rows[0].Cells[0];
                productionOrdernr = int.Parse(dgvProductie.Rows[dgvProductie.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
                productionKlantnr = int.Parse(dgvProductie.Rows[dgvProductie.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                productionFirmaNaam = dgvProductie.Rows[dgvOrders.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
            }
            catch
            {
                //error 1002
                //MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvOrders.Rows.Count - 1; i++)
            {
                string status = dgvOrders.Rows[i].Cells["status"].Value.ToString();
                if (status == "ROOD")
                {
                    DataGridViewRow row = dgvOrders.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 128, 128);
                }
                else
                {
                    DataGridViewRow row = dgvOrders.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                }
            }

            for (int i = 0; i < dgvOrders.Rows.Count - 1; i++)
            {
                DataGridViewCellStyle styleGreen = new DataGridViewCellStyle();
                styleGreen.BackColor = Color.FromArgb(144, 238, 144);
                DataGridViewCellStyle styleRed = new DataGridViewCellStyle();
                styleRed.BackColor = Color.FromArgb(240, 128, 128);
                DataGridViewCellStyle styleBlack = new DataGridViewCellStyle();
                styleBlack.BackColor = Color.Black;
                //style.ForeColor = Color.Black;
                string gon = dgvOrders.Rows[i].Cells["gon"].Value.ToString();
                if (dgvOrders.Rows[i].Cells["soortorder"].Value.ToString() != "Eigen productie")
                {
                    dgvOrders.Rows[i].Cells[12].Style = styleBlack;
                }
                else if (gon == "Y")
                {
                    dgvOrders.Rows[i].Cells[12].Style = styleGreen;
                }
                else
                {
                    dgvOrders.Rows[i].Cells[12].Style = styleRed;
                }
            }
        }

        private void dgvProductie_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvProductie.Rows.Count - 1; i++)
            {
                string status = dgvProductie.Rows[i].Cells["status"].Value.ToString();
                if (status == "IN PRODUCTIE")
                {
                    DataGridViewRow row = dgvProductie.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 127, 80);
                }
                else
                {
                    DataGridViewRow row = dgvProductie.Rows[i];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                }
            }
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
                    klantnrOpzoeken = (int)rdr["klantnr"];
                }
                else
                {
                    MessageBox.Show("Er werd geen Firma gevonden", "Error");
                }
                txtZoekenFirma.Text = firmaNaam;
                cmd.Connection.Close();

                dataOpvragenOrdersFirma();
            }
            catch
            {

            }
        }

        public void dataRefresh()
        {
            dataOpvragenOrders();
            dataOpvragenProduction();
        }

        public void deleteOrder()
        {
            try
            {

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
                string sql;
                MySqlCommand cmd;
                sql = "DELETE FROM orders WHERE ordernr = @ordernr;";
                cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ordernr", ordernr);
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM orderArtikel WHERE ordernr = @ordernr;";
                cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ordernr", ordernr);
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
            catch
            {

            }
        }

        //Excel

        public void dataOpvragenOrderbevestigingExcel(string soort)
        {
            //try
            //{
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql;
            if (soort == "Production")
            {
                sql = "SELECT orders.ordernr, klant.naam, klant.adres, klant.gemeente, klant.postcode, klant.land, klant.klantnr, orders.datum, orders.leveringstermijn, orders.stansmeskosten, orders.clichekost, orders.tav, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.ref, orderArtikel.fefco, orderArtikel.kwaliteit, orderArtikel.bedrukking, orderArtikel.prijs, orderArtikel.aantalgeproduceerd, orderArtikel.aantalwwp, orderArtikel.aantalep, orderArtikel.bestelbonnummer, orderArtikel.nudaantal1, orderArtikel.aantal1, orderArtikel.nudaantal2, orderArtikel.aantal2, orderArtikel.nudaantal3, orderArtikel.aantal3, orderArtikel.omschrijving FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orders.ordernr=" + productionOrdernr + " ORDER BY ordernr ASC";
            }
            else
            {
                sql = "SELECT orders.ordernr, klant.naam, klant.adres, klant.gemeente, klant.postcode, klant.land, klant.klantnr, orders.datum, orders.leveringstermijn, orders.stansmeskosten, orders.clichekost, orders.tav, orderArtikel.lengte, orderArtikel.breedte, orderArtikel.hoogte, orderArtikel.aantal, orderArtikel.ref, orderArtikel.fefco, orderArtikel.kwaliteit, orderArtikel.bedrukking, orderArtikel.prijs, orderArtikel.aantalgeproduceerd, orderArtikel.aantalwwp, orderArtikel.aantalep, orderArtikel.bestelbonnummer, orderArtikel.nudaantal1, orderArtikel.aantal1, orderArtikel.nudaantal2, orderArtikel.aantal2, orderArtikel.nudaantal3, orderArtikel.aantal3, orderArtikel.omschrijving FROM (klant JOIN orders ON klant.klantnr=orders.klantnr) JOIN orderArtikel ON orders.ordernr=orderArtikel.ordernr WHERE orders.ordernr=" + ordernr + " ORDER BY ordernr ASC";
            }

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvDataOrders.DataSource = bindingSource;
            /*}
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        public void makeExcellPalKaart(string soort)
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

            int aantalPaletten = int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal1"].Value.ToString()) + int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal2"].Value.ToString()) + int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal3"].Value.ToString());
            int aantalPalettenCount = 1;
            int aantalcellen = 40;
            int i;
            int nudaantal1 = int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal1"].Value.ToString());
            for (i = 0; i < nudaantal1; i++)
            {
                xlWorkSheet.Columns["E"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.get_Range("A" + (28 + (aantalcellen * i)), "I" + (28 + (aantalcellen * i))).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (28 + (aantalcellen * i)), "I" + (28 + (aantalcellen * i))).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A" + (31 + (aantalcellen * i)), "I" + (31 + (aantalcellen * i))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (31 + (aantalcellen * i)), "I" + (31 + (aantalcellen * i))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                xlWorkSheet.Cells[28 + (aantalcellen * i), 5] = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
                xlWorkSheet.Cells[29 + (aantalcellen * i), 5] = dgvDataOrders.Rows[0].Cells["adres"].Value.ToString();
                xlWorkSheet.Cells[30 + (aantalcellen * i), 5] = dgvDataOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvDataOrders.Rows[0].Cells["gemeente"].Value.ToString();
                xlWorkSheet.Cells[31 + (aantalcellen * i), 5] = dgvDataOrders.Rows[0].Cells["land"].Value.ToString();
                xlWorkSheet.get_Range("E" + (28 + (aantalcellen * i))).Font.Size = 20;
                xlWorkSheet.get_Range("E" + (29 + (aantalcellen * i))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (30 + (aantalcellen * i))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (31 + (aantalcellen * i))).Font.Size = 16;

                if (soort == "Production")
                {
                    xlWorkSheet.Cells[33 + (aantalcellen * i), 1] = "OrderNr: " + productionOrdernr;
                }
                else
                {
                    xlWorkSheet.Cells[33 + (aantalcellen * i), 1] = "OrderNr: " + ordernr;
                }
                if (dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[34 + (aantalcellen * i), 1] = "BestelbonNr klant: " + dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
                }
                if (dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[35 + (aantalcellen * i), 1] = "Ref klant: " + dgvDataOrders.Rows[0].Cells["ref"].Value.ToString();
                }
                xlWorkSheet.get_Range("A" + (33 + (aantalcellen * i)), "A" + (35 + (aantalcellen * i))).Font.Size = 18;

                xlWorkSheet.Cells[37 + (aantalcellen * i), 1] = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString() + " mm";             
                xlWorkSheet.get_Range("A" + (37 + (aantalcellen * i))).Font.Size = 36;

                xlWorkSheet.Cells[38 + (aantalcellen * i), 1] = "Aantal: " + dgvDataOrders.Rows[0].Cells["aantal1"].Value.ToString();
                xlWorkSheet.get_Range("A" + (38 + (aantalcellen * i))).Font.Size = 48;

                xlWorkSheet.Cells[40 + (aantalcellen * i), 1] = "Paletten: " + aantalPalettenCount + " / " + aantalPaletten;
                xlWorkSheet.get_Range("A" + (40 + (aantalcellen * i))).Font.Size = 22;

                /*Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
                System.Drawing.Image img = barcode39.Draw("Hello World", 40);
                pictureBox1.Image = img;

                pictureBox1.Image.Save(@"C:\willbox\data\barcodes\img1.jpg", ImageFormat.Jpeg);
                img.Save("c:\\willbox\\data\\barcodes\\img1.png", ImageFormat.Png);*/

                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 25, 270 + ((810 * i) - (63 * i)), 360, 124);
                aantalPalettenCount++;
            }
            int j;
            int totaalLogo = 270 + ((810 * i - 1) - (63 * i - 1));
            int aantalcellen2 = 1 + (aantalcellen * i - 1);
            int nudaantal2 = int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal2"].Value.ToString());
            for (j = 0; j < nudaantal2; j++)
            {
                xlWorkSheet.Columns["E"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.get_Range("A" + (28 + aantalcellen2 + (aantalcellen * j)), "I" + (28 + aantalcellen2 + (aantalcellen * j))).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (28 + aantalcellen2 + (aantalcellen * j)), "I" + (28 + aantalcellen2 + (aantalcellen * j))).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A" + (31 + aantalcellen2 + (aantalcellen * j)), "I" + (31 + aantalcellen2 + (aantalcellen * j))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (31 + aantalcellen2 + (aantalcellen * j)), "I" + (31 + aantalcellen2 + (aantalcellen * j))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                xlWorkSheet.Cells[28 + aantalcellen2 + (aantalcellen * j), 5] = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
                xlWorkSheet.Cells[29 + aantalcellen2 + (aantalcellen * j), 5] = dgvDataOrders.Rows[0].Cells["adres"].Value.ToString();
                xlWorkSheet.Cells[30 + aantalcellen2 + (aantalcellen * j), 5] = dgvDataOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvDataOrders.Rows[0].Cells["gemeente"].Value.ToString();
                xlWorkSheet.Cells[31 + aantalcellen2 + (aantalcellen * j), 5] = dgvDataOrders.Rows[0].Cells["land"].Value.ToString();
                xlWorkSheet.get_Range("E" + (28 + aantalcellen2 + (aantalcellen * j))).Font.Size = 20;
                xlWorkSheet.get_Range("E" + (29 + aantalcellen2 + (aantalcellen * j))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (30 + aantalcellen2 + (aantalcellen * j))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (31 + aantalcellen2 + (aantalcellen * j))).Font.Size = 16;

                if (soort == "Production")
                {
                    xlWorkSheet.Cells[33 + aantalcellen2 + (aantalcellen * j), 1] = "OrderNr: " + productionOrdernr;
                }
                else
                {
                    xlWorkSheet.Cells[33 + aantalcellen2 + (aantalcellen * j), 1] = "OrderNr: " + ordernr;
                }
                if (dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[34 + aantalcellen2 + (aantalcellen * j), 1] = "BestelbonNr klant: " + dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
                }
                if (dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[35 + aantalcellen2 + (aantalcellen * j), 1] = "Ref klant: " + dgvDataOrders.Rows[0].Cells["ref"].Value.ToString();
                }
                xlWorkSheet.get_Range("A" + (33 + aantalcellen2 + (aantalcellen * j)), "A" + (35 + aantalcellen2 + (aantalcellen * j))).Font.Size = 18;

                xlWorkSheet.Cells[37 + aantalcellen2 + (aantalcellen * j), 1] = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString() + " mm";
                xlWorkSheet.get_Range("A" + (37 + aantalcellen2 + (aantalcellen * j))).Font.Size = 36;

                xlWorkSheet.Cells[38 + aantalcellen2 + (aantalcellen * j), 1] = "Aantal: " + dgvDataOrders.Rows[0].Cells["aantal2"].Value.ToString();
                xlWorkSheet.get_Range("A" + (38 + aantalcellen2 + (aantalcellen * j))).Font.Size = 48;

                xlWorkSheet.Cells[40 + aantalcellen2 + (aantalcellen * j), 1] = "Paletten: " + aantalPalettenCount + " / " + aantalPaletten;
                xlWorkSheet.get_Range("A" + (40 + aantalcellen2 + (aantalcellen * j))).Font.Size = 22;

                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 25, totaalLogo + ((810 * j) - (63 * j)), 360, 124);
                aantalPalettenCount++;
            }
            int k;
            int totaalLogo2 = 270 + ((810 * i - 1) - (63 * i - 1)) + ((810 * j) - (63 * j));
            int aantalcellen3 = 2 + ((aantalcellen * i - 1) + (aantalcellen * j - 1));
            int nudaantal3 = int.Parse(dgvDataOrders.Rows[0].Cells["nudaantal3"].Value.ToString());
            for (k = 0; k < nudaantal3; k++)
            {
                xlWorkSheet.Columns["E"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.get_Range("A" + (28 + aantalcellen3 + (aantalcellen * k)), "I" + (28 + aantalcellen3 + (aantalcellen * k))).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (28 + aantalcellen3 + (aantalcellen * k)), "I" + (28 + aantalcellen3 + (aantalcellen * k))).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.get_Range("A" + (31 + aantalcellen3 + (aantalcellen * k)), "I" + (31 + aantalcellen3 + (aantalcellen * k))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWorkSheet.get_Range("A" + (31 + aantalcellen3 + (aantalcellen * k)), "I" + (31 + aantalcellen3 + (aantalcellen * k))).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                xlWorkSheet.Cells[28 + aantalcellen3 + (aantalcellen * k), 5] = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
                xlWorkSheet.Cells[29 + aantalcellen3 + (aantalcellen * k), 5] = dgvDataOrders.Rows[0].Cells["adres"].Value.ToString();
                xlWorkSheet.Cells[30 + aantalcellen3 + (aantalcellen * k), 5] = dgvDataOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvDataOrders.Rows[0].Cells["gemeente"].Value.ToString();
                xlWorkSheet.Cells[31 + aantalcellen3 + (aantalcellen * k), 5] = dgvDataOrders.Rows[0].Cells["land"].Value.ToString();
                xlWorkSheet.get_Range("E" + (28 + aantalcellen3 + (aantalcellen * k))).Font.Size = 20;
                xlWorkSheet.get_Range("E" + (29 + aantalcellen3 + (aantalcellen * k))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (30 + aantalcellen3 + (aantalcellen * k))).Font.Size = 16;
                xlWorkSheet.get_Range("E" + (31 + aantalcellen3 + (aantalcellen * k))).Font.Size = 16;

                if (soort == "Production")
                {
                    xlWorkSheet.Cells[33 + aantalcellen3 + (aantalcellen * k), 1] = "OrderNr: " + productionOrdernr;
                }
                else
                {
                    xlWorkSheet.Cells[33 + aantalcellen3 + (aantalcellen * k), 1] = "OrderNr: " + ordernr;
                }
                if (dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[34 + aantalcellen3 + (aantalcellen * k), 1] = "BestelbonNr klant: " + dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
                }
                if (dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "0" || dgvDataOrders.Rows[0].Cells["ref"].Value.ToString() == "")
                {
                }
                else
                {
                    xlWorkSheet.Cells[35 + aantalcellen3 + (aantalcellen * k), 1] = "Ref klant: " + dgvDataOrders.Rows[0].Cells["ref"].Value.ToString();
                }
                xlWorkSheet.get_Range("A" + (33 + aantalcellen3 + (aantalcellen * k)), "A" + (35 + aantalcellen3 + (aantalcellen * k))).Font.Size = 18;

                xlWorkSheet.Cells[37 + aantalcellen3 + (aantalcellen * k), 1] = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString() + " mm";
                xlWorkSheet.get_Range("A" + (37 + aantalcellen3 + (aantalcellen * k))).Font.Size = 36;

                xlWorkSheet.Cells[38 + aantalcellen3 + (aantalcellen * k), 1] = "Aantal: " + dgvDataOrders.Rows[0].Cells["aantal3"].Value.ToString();
                xlWorkSheet.get_Range("A" + (38 + aantalcellen3 + (aantalcellen * k))).Font.Size = 48;

                xlWorkSheet.Cells[40 + aantalcellen3 + (aantalcellen * k), 1] = "Paletten: " + aantalPalettenCount + " / " + aantalPaletten;
                xlWorkSheet.get_Range("A" + (40 + aantalcellen3 + (aantalcellen * k))).Font.Size = 22;

                xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 25, totaalLogo2 + ((810 * k) - (63 * k)), 360, 124);
                aantalPalettenCount++;
            }
            string path;
            if (soort == "Production")
            {
                path = @"c:/willbox/Orders/" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "/Ordernr " + productionOrdernr + "/";
            }
            else
            {
                path = @"c:/willbox/Orders/" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "/Ordernr " + ordernr + "/";
            }

            //try
            //{
            if (Directory.Exists(path))
            {

            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            if (soort == "Production")
            {
                xlWorkBook.SaveAs("c:\\Willbox\\Orders\\" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "\\" + "Ordernr " + productionOrdernr + "\\Paletkaart " + productionOrdernr + " " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            else
            {
                xlWorkBook.SaveAs("c:\\Willbox\\Orders\\" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "\\" + "Ordernr " + ordernr + "\\Paletkaart " + ordernr + " " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            if (soort == "Production")
            {
                MessageBox.Show("Excel bestand gecreërd met de als naam 'Paletkaart " + productionOrdernr + " " + productionFirmaNaam.ToLower() + "'.xls");
            }
            else
            {
                MessageBox.Show("Excel bestand gecreërd met de als naam 'Paletkaart " + ordernr + " " + firmaNaam.ToLower() + "'.xls");
            }
            dataRefresh();
            /*}
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                MessageBox.Show("Er is iets fout gelopen bij de aanmaak van het Bestand, probeer het later opnieuw.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }*/
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
            xlWorkSheet.Cells[j, 6] = double.Parse(dgvDataOrders.Rows[0].Cells["prijs"].Value.ToString()) / 1000;
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

        /*private void makeExcellZendnota()
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

            xlWorkSheet.Columns["A"].ColumnWidth = 0;
            xlWorkSheet.Columns["B"].ColumnWidth = 14;
            xlWorkSheet.Columns["C"].ColumnWidth = 8;
            xlWorkSheet.Columns["D"].ColumnWidth = 18;
            xlWorkSheet.Columns["E"].ColumnWidth = 12;
            xlWorkSheet.Columns["F"].ColumnWidth = 10;
            xlWorkSheet.Columns["G"].ColumnWidth = 14;
            xlWorkSheet.Columns["H"].ColumnWidth = 2;
            xlWorkSheet.Columns["I"].ColumnWidth = 10;

            xlWorkSheet.get_Range("B20", "G30").RowHeight = 23;
            xlWorkSheet.get_Range("B33", "F40").RowHeight = 15;

            xlWorkSheet.get_Range("B33", "H33").MergeCells = true;
            xlWorkSheet.get_Range("B34", "H34").MergeCells = true;
            xlWorkSheet.get_Range("B35", "H35").MergeCells = true;
            xlWorkSheet.get_Range("B36", "H36").MergeCells = true;
            xlWorkSheet.get_Range("B37", "H37").MergeCells = true;
            xlWorkSheet.get_Range("B38", "H38").MergeCells = true;
            xlWorkSheet.get_Range("B39", "H39").MergeCells = true;
            xlWorkSheet.get_Range("B40", "H40").MergeCells = true;

            xlWorkSheet.get_Range("B42", "H42").MergeCells = true;
            xlWorkSheet.get_Range("B43", "H43").MergeCells = true;

            xlWorkSheet.Cells[9, 7] = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
            if (dgvDataOrders.Rows[0].Cells["tav"].Value.ToString() != "")
            {
                xlWorkSheet.Cells[11, 7] = "tav " + dgvDataOrders.Rows[0].Cells["tav"].Value.ToString();
            }        
            xlWorkSheet.Cells[12, 7] = dgvDataOrders.Rows[0].Cells["adres"].Value.ToString();
            string gemeente = dgvDataOrders.Rows[0].Cells["postcode"].Value.ToString() + " " + dgvDataOrders.Rows[0].Cells["gemeente"].Value.ToString(); ;
            xlWorkSheet.Cells[13, 7] = gemeente;
            xlWorkSheet.get_Range("G9", "G13").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            xlWorkSheet.Cells[14, 2] = "ZendNota";
            xlWorkSheet.get_Range("B14").Font.Size = 18;
            xlWorkSheet.get_Range("B14", "B18").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            if (dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString() != "")
            {
                xlWorkSheet.Cells[16, 2] = "Bestelbonnummer klant: " + dgvDataOrders.Rows[0].Cells["bestelbonnummer"].Value.ToString();
            }
            xlWorkSheet.Cells[17, 2] = "Ordernummer: " + ordernr;
            xlWorkSheet.Cells[18, 2] = "Klantnummer: " + klantnr;
            xlWorkSheet.get_Range("B16", "B18").Font.Size = 14;

            xlWorkSheet.Cells[18, 7] = DateTime.Now.ToString("dd MMM yyyy");
            xlWorkSheet.get_Range("G18").Font.Size = 14;
            xlWorkSheet.Cells[18, 7].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            xlWorkSheet.Cells[20, 2] = "REF";
            xlWorkSheet.Cells[20, 3] = "FEFCO";
            xlWorkSheet.Cells[20, 4] = "AFMETINGEN mm";
            xlWorkSheet.Cells[20, 5] = "KWALITEIT";
            xlWorkSheet.Cells[20, 6] = "DRUK";
            xlWorkSheet.Cells[20, 7] = "AANTAL";
            //xlWorkSheet.Cells[20, 8] = "PRIJS";

            string afmetingen = dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString() + " X " + dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString();
            xlWorkSheet.Cells[21, 2] = dgvDataOrders.Rows[0].Cells["ref"].Value.ToString();
            xlWorkSheet.Cells[21, 3] = dgvDataOrders.Rows[0].Cells["fefco"].Value.ToString();
            xlWorkSheet.Cells[21, 4] = afmetingen;
            xlWorkSheet.Cells[21, 5] = dgvDataOrders.Rows[0].Cells["kwaliteit"].Value.ToString();
            xlWorkSheet.Cells[21, 6] = dgvDataOrders.Rows[0].Cells["bedrukking"].Value.ToString();
            xlWorkSheet.Cells[21, 7] = dgvDataOrders.Rows[0].Cells["aantalgeproduceerd"].Value.ToString();

            if (Regex.Replace(dgvDataOrders.Rows[0].Cells["stansmeskosten"].Value.ToString(), @"\s+", "") != "0")
            {
                xlWorkSheet.Cells[23, 4] = "Eenmalige stansmeskost: €" + dgvDataOrders.Rows[0].Cells["stansmeskosten"].Value.ToString();
                xlWorkSheet.Cells[23, 4].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }

            if (Regex.Replace(dgvDataOrders.Rows[0].Cells["clichekost"].Value.ToString(), @"\s+", "") != "0")
            {
                if (Regex.Replace(dgvDataOrders.Rows[0].Cells["stansmeskosten"].Value.ToString(), @"\s+", "") != "0")
                {
                    xlWorkSheet.Cells[24, 4] = "Eenmalige clichékost: €" + dgvDataOrders.Rows[0].Cells["clichekost"].Value.ToString();
                    xlWorkSheet.Cells[24, 4].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                }
                else
                {
                    xlWorkSheet.Cells[23, 4] = "Eenmalige clichékost: €" + dgvDataOrders.Rows[0].Cells["clichekost"].Value.ToString();
                    xlWorkSheet.Cells[23, 4].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                }
            }

            xlWorkSheet.Cells[28, 4] = "Aantal paletten:";
            xlWorkSheet.Cells[28, 5] = "wwp " + dgvDataOrders.Rows[0].Cells["aantalwwp"].Value.ToString(); 
            xlWorkSheet.Cells[29, 5] = "euro pal " + dgvDataOrders.Rows[0].Cells["aantalep"].Value.ToString();
            xlWorkSheet.Cells[30, 4] = "Teruggave:";
            xlWorkSheet.Cells[30, 5] = "euro pal ........";
            xlWorkSheet.get_Range("D28", "E30").EntireRow.Font.Bold = true;

            xlWorkSheet.get_Range("D28", "E30").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.get_Range("B30", "G30").Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("B28", "G28").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("B28", "B30").Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("G28", "G30").Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            
            xlWorkSheet.get_Range("B20", "G20").Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            xlWorkSheet.get_Range("B20", "H30").Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.get_Range("B33", "G33").Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
           
            xlWorkSheet.Cells[37, 2] = "Voor akkoord ontvangst: ..............................................";
            xlWorkSheet.Cells[39, 2] = "Naam in drukletters: ..............................................";
            xlWorkSheet.get_Range("B37", "B39").EntireRow.Font.Bold = true;

            xlWorkSheet.get_Range("B42", "H43").Font.Size = 7;
            xlWorkSheet.Cells[42, 2] = "WillBox bvba  |  Zavelstraat 21a  |  9190 Stekene  |  Mail: info@willbox.be  |  Tel. +32 (0)3 293 52 50  |  Gsm: + 32 (0)472 97 49 46  | ";
            xlWorkSheet.Cells[43, 2] = "Btw: BE 0655 906 872  |  Fortis Bank: BE31 0017 8705 5955  |  BIC GEBABEBB";

            xlWorkSheet.get_Range("B42", "H43").WrapText = true;
            xlWorkSheet.get_Range("B42", "H43").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            // afbeelding toevoegen
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\willboxlogo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 30, 5, 180, 62);
            xlWorkSheet.Shapes.AddPicture("C:\\willbox\\data\\more.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 210, 45, 101, 18);

            xlWorkSheet.get_Range("A1", "D1").Font.Bold = true;
            xlWorkSheet.get_Range("A1", "D1").MergeCells = true;


            string path = @"c:/willbox/Zendnota's/" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "/Ordernr " + ordernr + "/";

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
                xlWorkBook.SaveAs("c:\\Willbox\\Zendnota's\\" + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + "\\" + "Ordernr " + ordernr + "\\Zendnota " + ordernr + " " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                MessageBox.Show("Excel bestand gecreërd met de als naam 'Zendnota " + dgvDataOrders.Rows[0].Cells["naam"].Value.ToString().ToLower() + ".xls'");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                MessageBox.Show("Er is iets fout gelopen bij de aanmaak van het Bestand, probeer het later opnieuw.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }
        }*/

        //knoppen

        public void dataClose(String naam)
        {
            if (naam == "leveringExcel")
            {
                this.leveringExcel = null;
            }
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

        private void Orders_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("orders");
            }
        }

        private void btnOrderBevestiging_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            dataOpvragenOrderbevestigingExcel("NoProduction");
            makeExcell();
            Laden.CloseForm();
        }

        private void iconDelivery_Click(object sender, EventArgs e)
        {
            if (leveringExcel == null)
            {
                Laden.ShowSplashScreen();
                leveringExcel = new LeveringExcel();
                leveringExcel.MdiParent = main;
                Laden.CloseForm();
            }
            leveringExcel.BringToFront();
            leveringExcel.Show();
        }

        private void btnGondardennes_Click(object sender, EventArgs e)
        {
            if (soortorder != "Eigen productie")
            {
                MessageBox.Show("Dit is een transit order en moet niet besteld worden bij Gondardennes", "Transit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Laden.ShowSplashScreen();
                Gondardennes gondardennes = new Gondardennes(ordernr);
                gondardennes.MdiParent = main;
                gondardennes.Show();
                Laden.CloseForm();
            }
        }

        private void btnLijst_Click(object sender, EventArgs e)
        {
            dataOpvragenOrders();
            txtZoekenFirma.Text = "";
        }

        private void btnFactuur_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Wenst u een factuur van ordernr " + ordernr + "?", "Factuur", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:
                    Laden.ShowSplashScreen();
                    OrdersToFacturen ordersToFacturen = new OrdersToFacturen(ordernr);
                    ordersToFacturen.MdiParent = main;
                    ordersToFacturen.Show();
                    Laden.CloseForm();
                    break;
                case DialogResult.No: break;
            }
        }

        private void iconDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bent u zeker dat u order: " + ordernr + " definitief wilt verwijderen?", "Order verwijderen?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:

                    DialogResult dr2 = MessageBox.Show("Bent u 100% zeker dat u order: " + ordernr + " definitief wilt verwijderen? Dit kan niet ongedaan gemaakt worden!", "Order verwijderen?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    switch (dr2)
                    {
                        case DialogResult.Yes:
                            deleteOrder();
                            dataRefresh();
                            break;
                        case DialogResult.No: break;
                    }

                    break;
                case DialogResult.No: break;
            }          
        }

        private void iconDoneOrder_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Order met ordernr " + ordernr + " afgewerkt?", "Order afgewerkt", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:
                    OrderDone orderDone = new OrderDone(ordernr, "NoProduction");
                    orderDone.MdiParent = main;
                    orderDone.Show();
                    //UpdateDataOrderArtikel();
                    //dataUpdateOrderStatusProduction("AFGEWERKT");
                    break;
                case DialogResult.No: break;
            }
        }

        private void iconProduction_Click(object sender, EventArgs e)
        {
            if (soortorder != "Eigen productie")
            {
                MessageBox.Show("Dit is een transit order en moet niet productie gaan", "Transit order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ben u zeker dat u order " + ordernr + " van klant " + firmaNaam + " wilt toevoegen aan productie?", "Productie toevoegen", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Yes:
                        Laden.ShowSplashScreen();
                        iconProduction.Visible = false;
                        iconDelivery.Visible = false;
                        iconDelete.Visible = false;
                        btnOrderBevestiging.Visible = false;
                        iconProductionDone.Visible = false;
                        dataOpvragenOrderbevestigingExcel("NoProduction");

                        productieficheMaken();
                        //makeExcellPalKaart();
                        Laden.CloseForm();


                        break;
                    case DialogResult.No: break;
                }
            }
        }

        private void productieficheMaken()
        {
            string klant = dgvDataOrders.Rows[0].Cells["naam"].Value.ToString();
            int lengte = int.Parse(dgvDataOrders.Rows[0].Cells["lengte"].Value.ToString());
            int breedte = int.Parse(dgvDataOrders.Rows[0].Cells["breedte"].Value.ToString());
            int hoogte = int.Parse(dgvDataOrders.Rows[0].Cells["hoogte"].Value.ToString());
            int aantal = int.Parse(dgvDataOrders.Rows[0].Cells["aantal"].Value.ToString());
            string fefco = dgvDataOrders.Rows[0].Cells["fefco"].Value.ToString();
            string kwaliteit = dgvDataOrders.Rows[0].Cells["kwaliteit"].Value.ToString();
            ProductieFiche productieFiche = new ProductieFiche(main, klant, lengte, breedte, hoogte, fefco, kwaliteit, aantal, ordernr, klantnr);
            productieFiche.MdiParent = main;
            productieFiche.Show();

            dataRefresh();
            iconProduction.Visible = true;
            iconDelivery.Visible = true;
            iconDelete.Visible = true;
            btnOrderBevestiging.Visible = true;
            iconProductionDone.Visible = true;
        }

        private void iconProductionDone_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Productie met ordernr " + productionOrdernr + " afgewerkt?", "Productie afgewerkt", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:
                    //productionAantalPlaten = Microsoft.VisualBasic.Interaction.InputBox("Hoeveel dozen zijn er geproduceerd", "Aantal geproduceerd", "", -1, -1);
                    OrderDone orderDone = new OrderDone(productionOrdernr, "Production");
                    orderDone.MdiParent = main;
                    orderDone.Show();
                    break;
                case DialogResult.No: break;
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                klantnr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                firmaNaam = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                ordernr = int.Parse(dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
                soortorder = dgvOrders.Rows[dgvOrders.CurrentCell.RowIndex].Cells["soortorder"].Value.ToString();
            }
            catch
            {
                // error 1003
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1003", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                productionKlantnr = int.Parse(dgvProductie.Rows[dgvProductie.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                productionFirmaNaam = dgvProductie.Rows[dgvProductie.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                productionOrdernr = int.Parse(dgvProductie.Rows[dgvProductie.CurrentCell.RowIndex].Cells["ordernr"].Value.ToString());
            }
            catch
            {
                // error 1003
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1003", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
