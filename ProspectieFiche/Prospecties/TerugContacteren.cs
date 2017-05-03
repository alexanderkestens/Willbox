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
    public partial class TerugContacteren : Form
    {
        private Main main;
        private int codeUser;
        MySqlConnection conn;
        BindingSource bindingSource;

        public TerugContacteren()
        {
            InitializeComponent();
        }

        public TerugContacteren(Main main, int codeUser)
        {
            this.main = main;
            this.codeUser = codeUser;
            InitializeComponent();
            dataOpvragenAlles();
        }

        private void dataOpvragenAlles()
        {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klant.klantnr, klant.naam, prospectie.contactpersoon, prospectie.terugcontacteren FROM klant JOIN prospectie ON klant.klantnr=prospectie.klantnr WHERE terugcontacterenYN='Y';";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvContacteren.DataSource = bindingSource;
            for (int j = 0; j < 3; j++)
            {
                dgvContacteren.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

        }

        private void dataOpvragenVandaag()
        {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klant.klantnr, klant.naam, prospectie.contactpersoon, prospectie.terugcontacteren FROM klant JOIN prospectie ON klant.klantnr=prospectie.klantnr WHERE terugcontacterenYN='Y' AND terugcontacteren='" + DateTime.Now.ToString("dd-MM-yyyy") + "';";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvContacteren.DataSource = bindingSource;
            for (int j = 0; j < 3; j++)
            {
                dgvContacteren.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

        }

        private void dataOpvragenWeek()
        {
            bindingSource = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT klant.klantnr, klant.naam, prospectie.contactpersoon, prospectie.terugcontacteren FROM klant JOIN prospectie ON klant.klantnr=prospectie.klantnr WHERE terugcontacterenYN='Y' AND (terugcontacteren BETWEEN '" + DateTime.Now.ToString("dd-MM-yyyy") + "' AND '" + DateTime.Now.AddDays(7).ToString("dd-MM-yyyy") + "');";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, myConnectionString);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;

            dgvContacteren.DataSource = bindingSource;
            for (int j = 0; j < 3; j++)
            {
                dgvContacteren.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

        }

        private void btnVandaag_Click(object sender, EventArgs e)
        {
            dataOpvragenVandaag();
        }

        private void btnAlles_Click(object sender, EventArgs e)
        {
            dataOpvragenAlles();
        }

        private void iconInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnDezeWeek_Click(object sender, EventArgs e)
        {
            dataOpvragenWeek();
        }
    }
}
