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
    public partial class Lijsten : Form
    {
        private Main main;
        private string lijstinhoud;

        public Lijsten(Main main, DataGridView dgvLijsten, string lijstinhoud)
        {
            InitializeComponent();
            this.main = main;
            this.lijstinhoud = lijstinhoud;
            this.MinimizeBox = false;
            dgvLijstenView.DataSource = dgvLijsten.DataSource;
            if (lijstinhoud == "gondardennes")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesEditOfferte")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesEditOfferte2")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesEditOfferte3")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesAddOfferte")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesAddOfferte2")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "gondardennesAddOfferte3")
            {
                for (int j = 0; j < 8; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "klantenEdit")
            {
                for (int j = 0; j < 4; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "klantenEdit")
            {
                for (int j = 0; j < 4; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (lijstinhoud == "klantenAdd")
            {
                for (int j = 0; j < 4; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            if (lijstinhoud == "bewerkGondardennes")
            {
                for (int j = 0; j < 12; j++)
                {
                    dgvLijstenView.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                this.Size = new Size(1000, 500);
            }
        }

        public Lijsten()
        {
            InitializeComponent();
        }

        private void Lijsten_Load(object sender, EventArgs e)
        {

        }

        private void dgvLijstenView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lijstinhoud == "gondardennes")
            {
                if (Application.OpenForms["Calculator"] != null)
                {
                    (Application.OpenForms["Calculator"] as Calculator).dataRefresh(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString(), dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteit"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesEditOfferte")
            {
                if (Application.OpenForms["EditOfferte"] != null)
                {
                    (Application.OpenForms["EditOfferte"] as EditOfferte).dataRefresh(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesEditOfferte2")
            {
                if (Application.OpenForms["EditOfferte"] != null)
                {
                    (Application.OpenForms["EditOfferte"] as EditOfferte).dataRefresh2(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesEditOfferte3")
            {
                if (Application.OpenForms["EditOfferte"] != null)
                {
                    (Application.OpenForms["EditOfferte"] as EditOfferte).dataRefresh3(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesAddOfferte")
            {
                if (Application.OpenForms["AddOfferte"] != null)
                {
                    (Application.OpenForms["AddOfferte"] as AddOfferte).dataRefresh(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesAddOfferte2")
            {
                if (Application.OpenForms["AddOfferte"] != null)
                {
                    (Application.OpenForms["AddOfferte"] as AddOfferte).dataRefresh2(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "gondardennesAddOfferte3")
            {
                if (Application.OpenForms["AddOfferte"] != null)
                {
                    (Application.OpenForms["AddOfferte"] as AddOfferte).dataRefresh3(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString());
                }
                this.Close();
            }
            if (lijstinhoud == "klantenEdit")
            {
                if (Application.OpenForms["EditOfferte"] != null)
                {
                    (Application.OpenForms["EditOfferte"] as EditOfferte).dataRefresh(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["naam"].Value.ToString(), int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString()));
                }
                this.Close();
            }
            if (lijstinhoud == "klantenAdd")
            {
                if (Application.OpenForms["AddOfferte"] != null)
                {
                    (Application.OpenForms["AddOfferte"] as AddOfferte).dataRefresh(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["naam"].Value.ToString(), int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["klantnr"].Value.ToString()));
                }
                this.Close();
            }
            if (lijstinhoud == "bewerkGondardennes")
            {
                if (Application.OpenForms["BewerkGondardennes"] != null)
                {
                    int gondardennesnr = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["nr"].Value.ToString());
                    string kwaliteitcode = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteitcode"].Value.ToString();
                    string kwaliteit = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["kwaliteit"].Value.ToString();
                    int gewicht = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["gewicht"].Value.ToString());
                    string buiten = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["buiten"].Value.ToString();
                    string golf1 = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["golf1"].Value.ToString();
                    string papier = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["papier"].Value.ToString();
                    string golf2 = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["golf2"].Value.ToString();
                    string binnen = dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["binnen"].Value.ToString();
                    int m200 = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["200m"].Value.ToString());
                    int m500 = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["500m"].Value.ToString());
                    int m3000 = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["3000m"].Value.ToString());
                    int m10000 = int.Parse(dgvLijstenView.Rows[dgvLijstenView.CurrentCell.RowIndex].Cells["10000m"].Value.ToString());
                    (Application.OpenForms["BewerkGondardennes"] as BewerkGondardennes).dataRefresh(gondardennesnr, kwaliteitcode, kwaliteit, gewicht, buiten, golf1, papier, golf2, binnen, m200, m500, m3000, m10000);
                }
                this.Close();
            }
        }

        private void Lijsten_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms["AddOfferte"] != null)
            {
                (Application.OpenForms["AddOfferte"] as AddOfferte).dataClose("lijsten");
            }
            if (Application.OpenForms["EditOfferte"] != null)
            {
                (Application.OpenForms["EditOfferte"] as EditOfferte).dataClose("lijsten");
            }
            if (Application.OpenForms["AddOfferte"] != null)
            {
                (Application.OpenForms["AddOfferte"] as AddOfferte).dataClose("lijstenkwal");
            }
            if (Application.OpenForms["EditOfferte"] != null)
            {
                (Application.OpenForms["EditOfferte"] as EditOfferte).dataClose("lijstenkwal");
            }
        }
    }
}
