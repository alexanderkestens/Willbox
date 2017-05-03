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
    public partial class Klantenlijst : Form
    {
        private Main main;
        MySqlConnection conn;
        private DataGridView dgvLijsten;
        private BindingSource bindingSource1;

        public Klantenlijst()
        {
            InitializeComponent();
        }

        public Klantenlijst(Main main)
        {
            this.main = main;
            dgvLijsten = new DataGridView();
            InitializeComponent();
        }

        private void dataOpvragenDataGrid()
        {
            String velden = " klantnr, naam";
            if (clbvelden.CheckedItems.Count == 0)
            {

            }
            else
            {
                velden = velden + ", ";
                for (int x = 0; x <= clbvelden.CheckedItems.Count - 2; x++)
                {
                    velden = velden + clbvelden.CheckedItems[x].ToString().ToLower() + ", ";
                }
                velden = velden + clbvelden.CheckedItems[clbvelden.CheckedItems.Count - 1].ToString().ToLower();
            }

            bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT" + velden + " FROM klant WHERE type=1 ORDER BY klantnr";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            dgvLijsten.DataSource = bindingSource1;
            dgvLijsten.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void btnMaakLijst_Click(object sender, EventArgs e)
        {
            dataOpvragenDataGrid();
            Lijsten lijsten = new Lijsten(main, dgvLijsten, "klanten");
            lijsten.MdiParent = this.main;
            lijsten.Show();
            this.Close();
        }
    }
}
