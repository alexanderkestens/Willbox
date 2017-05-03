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
    public partial class Facturen : Form
    {
        BindingSource bindingSource;
        MySqlConnection conn;
        private Main main;

        public Facturen()
        {
            InitializeComponent();
        }

        public Facturen(Main main)
        {
            this.main = main;
            InitializeComponent();
            dataOpvragenOffertes();
        }

        private void Facturen_Load(object sender, EventArgs e)
        {

        }

        private void dataOpvragenOffertes()
        {
            try
            {
                bindingSource = new BindingSource();

                var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT facturen.factuurnr, facturen.naam, facturen.factuurdatum, facturen.inclusiefbtw AS 'totaal' FROM facturen ORDER BY factuurnr DESC";

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
                MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                dgvFacturen.DataSource = bindingSource;

                for (int j = 0; j < 4; j++)
                {
                    dgvFacturen.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }


                /*dgvOffertes.CurrentCell = dgvOffertes.Rows[0].Cells[0];
                klantnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString());
                offertenr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["offertenr"].Value.ToString());
                offerteartikelnr = int.Parse(dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["artikelnr"].Value.ToString());
                firmaNaam = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["naam"].Value.ToString();
                statusCode = dgvOffertes.Rows[dgvOffertes.CurrentCell.RowIndex].Cells["status"].Value.ToString();*/
            }
            catch
            {
                //error 1002
                MessageBox.Show("Er is iets fout gelopen bij het opvragen van data, neem contact op met Alexander", "Error code:1002", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Facturen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("facturen");
            }
        }

        private void btnAddFactuur_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            AddFactuur addFactuur = new AddFactuur();
            addFactuur.MdiParent = main;
            Laden.CloseForm();
            addFactuur.Show();
        }
    }
}
