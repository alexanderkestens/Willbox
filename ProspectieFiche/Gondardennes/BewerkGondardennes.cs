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
    public partial class BewerkGondardennes : Form
    {
        private Main main;
        MySqlConnection conn;
        private DataGridView dgvLijsten;
        private BindingSource bindingSource1;
        private int gonnr;

        public BewerkGondardennes()
        {
            dgvLijsten = new DataGridView();
            InitializeComponent();
        }

        public BewerkGondardennes(Main main)
        {
            this.main = main;
            InitializeComponent();
        }
        public void dataRefresh(int gondardennesnr, string kwaliteitcode, string kwaliteit, int gewicht, string buiten, string golf1, string papier, string golf2, string binnen, int m200, int m500, int m3000, int m10000)
        {
            gonnr = gondardennesnr;
            txtKwaliteitcode.Text = kwaliteitcode;
            txtKwaliteit.Text = kwaliteit;
            txtGewicht.Text = gewicht.ToString();
            txtBuiten.Text = buiten;
            txtGolf1.Text = golf1;
            txtPapier.Text = papier;
            txtGolf2.Text = golf2;
            txtBinnen.Text = binnen;
            txt200m.Text = m200.ToString();
            txt500m.Text = m500.ToString();
            txt3000m.Text = m3000.ToString();
            txt10000m.Text = m10000.ToString();
        }

        private DataGridView dataKwaliteiten()
        {
            bindingSource1 = new BindingSource();

            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT gondardennesnr AS 'nr', kwaliteit, kwaliteitcode, gewicht, buiten, golf1, papier, golf2, binnen, 200m, 500m, 3000m, 10000m FROM gondardennes";

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

        private void updateData()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            //int code = int.Parse(txtCodeVerlopig.Text);
            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "UPDATE gondardennes SET 200m=@200m, 500m=@500m, 3000m=@3000m, 10000m=@10000m WHERE gondardennesnr=@gondardennesnr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@200m", txt200m.Text);
            cmd.Parameters.AddWithValue("@500m", txt500m.Text);
            cmd.Parameters.AddWithValue("@3000m", txt3000m.Text);
            cmd.Parameters.AddWithValue("@10000m", txt10000m.Text);
            cmd.Parameters.AddWithValue("@gondardennesnr", gonnr);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("De nieuwe data is toegevoegd!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txt10000m.Text = "";
            txt200m.Text = "";
            txt3000m.Text = "";
            txt500m.Text = "";
            txtBinnen.Text = "";
            txtBuiten.Text = "";
            txtGewicht.Text = "";
            txtGolf1.Text = "";
            txtGolf2.Text = "";
            txtKwaliteit.Text = "";
            txtKwaliteitcode.Text = "";
            txtPapier.Text = "";

        }

        private void btnLijst_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();
            Lijsten lijsten = new Lijsten(main, dataKwaliteiten(), "bewerkGondardennes");
            lijsten.MdiParent = this.main;
            Laden.CloseForm();
            lijsten.Show();
        }

        private void btnBewerken_Click(object sender, EventArgs e)
        {
            updateData();
        }
    }
}
