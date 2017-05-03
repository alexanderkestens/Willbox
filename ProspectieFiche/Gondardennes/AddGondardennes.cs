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
    public partial class AddGondardennes : Form
    {
        MySqlConnection conn;
        public AddGondardennes()
        {
            InitializeComponent();
        }

        private void btnMaken_Click(object sender, EventArgs e)
        {
            int gondardennesnr = codeOpzoekenGondardennes();
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT gondardennes (gondardennesnr, 200m, 500m, 3000m, kwaliteit, kwaliteitcode, gewicht, buiten, golf1, papier, golf2, binnen) VALUES (@gondardennesnr, @200m, @500m, @3000m, @kwaliteit, @kwaliteitcode, @gewicht, @buiten, @golf1, @papier, @golf2, @binnen)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@gondardennesnr", MySqlDbType.Int64).Value = gondardennesnr;
            cmd.Parameters.Add("@200m", MySqlDbType.Int64).Value = txt200m.Text;
            cmd.Parameters.Add("@500m", MySqlDbType.Int64).Value = txt500m.Text;
            cmd.Parameters.Add("@3000m", MySqlDbType.Int64).Value = txt3000m.Text;
            cmd.Parameters.Add("@kwaliteit", MySqlDbType.Text).Value = txtKwaliteit.Text;
            cmd.Parameters.Add("@kwaliteitcode", MySqlDbType.Text).Value = txtKwaliteitcode.Text;
            cmd.Parameters.Add("@gewicht", MySqlDbType.Int64).Value = txtGewicht.Text;
            cmd.Parameters.Add("@buiten", MySqlDbType.Text).Value = txtBuiten.Text;
            cmd.Parameters.Add("@golf1", MySqlDbType.Text).Value = txtGolf1.Text;
            cmd.Parameters.Add("@papier", MySqlDbType.Text).Value = txtPapier.Text;
            cmd.Parameters.Add("@golf2", MySqlDbType.Text).Value = txtGolf2.Text;
            cmd.Parameters.Add("@binnen", MySqlDbType.Text).Value = txtBinnen.Text;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            MessageBox.Show("Data toegevoegd");
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

        private int codeOpzoekenGondardennes()
        {
            int hoogste = 0;
            int tussenstap;
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            string sql = "SELECT * FROM gondardennes;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tussenstap = (int)rdr["gondardennesnr"];
                if (tussenstap > hoogste)
                {
                    hoogste = tussenstap;
                }
            }

            cmd.Connection.Close();

            return hoogste + 1;
        }
    }
}
