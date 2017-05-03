using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProspectieFiche
{
    public partial class OrderDone : Form
    {
        MySqlConnection conn;
        private int productionOrdernr;
        private String orderSoort;

        public OrderDone()
        {
            InitializeComponent();
        }

        public OrderDone(int productionOrdernr, string orderSoort)
        {
            this.productionOrdernr = productionOrdernr;
            this.orderSoort = orderSoort;
            
            InitializeComponent();
            if (orderSoort == "NoProduction")
            {
                this.Text = "Order afgerond";
                lblDozenGeproduceerd.Text = "Aantal dozen";
            }
        }

        private void dataUpdateOrderStatusProductionDone()
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            string sql = "UPDATE orderArtikel SET status='AFGEWERKT', aantalgeproduceerd=@aantalgeproduceerd, aantalwwp=@aantalwwp, aantalep=@aantalep, nudaantal1=@nudaantal1, aantal1=@aantal1, nudaantal2=@nudaantal2, aantal2=@aantal2, nudaantal3=@nudaantal3, aantal3=@aantal3 WHERE ordernr=@ordernr";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@ordernr", MySqlDbType.Int64).Value = productionOrdernr;
            cmd.Parameters.Add("@aantalgeproduceerd", MySqlDbType.Int64).Value = Int64.Parse(txtAantalDozen.Text);
            cmd.Parameters.Add("@aantalwwp", MySqlDbType.Int64).Value = Int64.Parse(nudWegwerp.Text);
            cmd.Parameters.Add("@aantalep", MySqlDbType.Int64).Value = Int64.Parse(nudEuro.Text);
            cmd.Parameters.Add("@nudaantal1", MySqlDbType.Int64).Value = Int64.Parse(nudAantal1.Value.ToString());
            cmd.Parameters.Add("@aantal1", MySqlDbType.Int64).Value = Int64.Parse(txtAantal1.Text);
            cmd.Parameters.Add("@nudaantal2", MySqlDbType.Int64).Value = Int64.Parse(nudAantal2.Value.ToString());
            cmd.Parameters.Add("@aantal2", MySqlDbType.Int64).Value = Int64.Parse(txtAantal2.Text);
            cmd.Parameters.Add("@nudaantal3", MySqlDbType.Int64).Value = Int64.Parse(nudAantal3.Value.ToString());
            cmd.Parameters.Add("@aantal3", MySqlDbType.Int64).Value = Int64.Parse(txtAantal3.Text);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            if (orderSoort == "NoProduction")
            {
                if (Application.OpenForms["Orders"] != null)
                {
                    (Application.OpenForms["Orders"] as Orders).dataOpvragenOrderbevestigingExcel("NoProduction");
                    (Application.OpenForms["Orders"] as Orders).makeExcellPalKaart("NoProduction");
                }
            } else
            {
                if (Application.OpenForms["Orders"] != null)
                {
                    (Application.OpenForms["Orders"] as Orders).dataOpvragenOrderbevestigingExcel("Production");
                    (Application.OpenForms["Orders"] as Orders).makeExcellPalKaart("Production");
                }
            }
        }

        private void btnIngeven_Click(object sender, EventArgs e)
        {
            if (Regex.Replace(txtAantal1.Text, @"\s+", "") == "")
            {
                txtAantal1.Text = "0";
            }
            if (Regex.Replace(txtAantal2.Text, @"\s+", "") == "")
            {
                txtAantal2.Text = "0";
            }
            if (Regex.Replace(txtAantal3.Text, @"\s+", "") == "")
            {
                txtAantal3.Text = "0";
            }
            dataUpdateOrderStatusProductionDone();
            this.Close();
        }

        private void btnAnnuleren_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
