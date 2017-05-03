using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace ProspectieFiche
{
    public partial class Main : Form
    {
        private int codeUser = 2;
        private Klanten klanten = null;
        private Prospecties prospecties = null;
        private Calculator calculator = null;
        private Orders orders = null;
        private Offertes offertes = null;
        private Kalender kalender = null;
        private Facturen facturen = null;
        MySqlConnection conn;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Main()
        {

            //YAME-YAME-PC
            if (Environment.MachineName == "WILLBOX2" || Environment.MachineName == "ALEXANDER" || Environment.MachineName == "YAME-YAME-PC" || Environment.MachineName == "YAME" || Environment.MachineName.ToUpper() == "THUIS-PC")
            {
                /*bool createdNew = true;
                using (Mutex mutex = new Mutex(true, "MyApplicationName", out createdNew))
                {
                    if (createdNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Main());
                    }
                    else
                    {
                        Process current = Process.GetCurrentProcess();
                        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                SetForegroundWindow(process.MainWindowHandle);
                                break;
                            }
                        }
                    }
                }*/
                InitializeComponent();
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                addIp(getExternalIp());
                MessageBox.Show("Dit programma kopiëren of publiceren zonder toestemming van Alexander Kestens is bij wet verboden! Alle rechten zijn voorbehouden aan Alexander Kestens! Uw IP-adres is al reeds doorgestuurd! Neem contact op met Alexander Kestens voor meer informatie!", "Error, system failure!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //INSCHAKELEN TEGEN HET KOPIEREN!!!!
               Process.Start("shutdown", "/s /t 0");
                
            }
        }

        private string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        private void addIp (String ipadress)
        {
            var myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            conn = new MySqlConnection(myConnectionString);
            conn.Open();


            string sql = "INSERT kopieren (datum, ipadres) VALUES (@datum, @ipadres)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@datum", MySqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@ipadres", MySqlDbType.Text).Value = ipadress;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void dataClose (String naam)
        {
            if (naam == "klant")
            {
                this.klanten = null;
            }
            if (naam == "prospecties")
            {
                this.prospecties = null;
            }
            if (naam == "offertes")
            {
                this.offertes = null;
            }
            if (naam == "calculator")
            {
                this.calculator = null;
            }
            if (naam == "orders")
            {
                this.orders = null;
            }
            if (naam == "kalender")
            {
                this.kalender = null;
            }
            if (naam == "facturen")
            {
                this.facturen = null;
            }
        }
        
        //menustrip

        private void opvragenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (prospecties == null)
            {
                Laden.ShowSplashScreen();

                prospecties = new Prospecties(this, codeUser);
                prospecties.MdiParent = this;
                Laden.CloseForm();
            }
            prospecties.BringToFront();
            prospecties.Show();
        }

        private void sluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Ben u zeker dat u Willbox wilt afsluiten?", "Willbox sluiten", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No: break;
                case DialogResult.Abort: break;
            }
        }

        private void opvragenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (klanten == null)
            {
                Laden.ShowSplashScreen();
                klanten = new Klanten(this, codeUser);
                klanten.MdiParent = this;
                Laden.CloseForm();
            }
            klanten.BringToFront();
            klanten.Show();
        }

        private void klantenlijstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Klantenlijst klantenlijst = new Klantenlijst(this);
            klantenlijst.MdiParent = this;
            klantenlijst.Show();
        }

        private void terugContacterenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laden.ShowSplashScreen();

            TerugContacteren terugcontacteren = new TerugContacteren(this, codeUser);
            terugcontacteren.MdiParent = this;
            Laden.CloseForm();
            terugcontacteren.Show();
        }

        private void offertesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (offertes == null)
            {
                Laden.ShowSplashScreen();

                offertes = new Offertes(this, codeUser);
                offertes.MdiParent = this;
                Laden.CloseForm();
            }
            offertes.BringToFront();
            offertes.Show();
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calculator == null)
            {
                Laden.ShowSplashScreen();

                calculator = new Calculator(this, codeUser);
                calculator.MdiParent = this;
                Laden.CloseForm();
            }
            calculator.BringToFront();        
            calculator.Show();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orders == null)
            {
                Laden.ShowSplashScreen();

                orders = new Orders(this, codeUser);
                orders.MdiParent = this;
                Laden.CloseForm();
            }
            orders.BringToFront();
            orders.Show();
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kalender == null)
            {
                Laden.ShowSplashScreen();

                kalender = new Kalender();
                kalender.MdiParent = this;
                Laden.CloseForm();
            }
            kalender.BringToFront();
            kalender.Show();
        }

        private void facturenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (facturen == null)
            {
                Laden.ShowSplashScreen();

                facturen = new Facturen(this);
                facturen.MdiParent = this;
                Laden.CloseForm();
            }
            facturen.BringToFront();
            facturen.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGondardennes addGondardennes = new AddGondardennes();
            addGondardennes.MdiParent = this;
            addGondardennes.Show();
        }

        private void bewerkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BewerkGondardennes bewerkGondardennes = new BewerkGondardennes();
            bewerkGondardennes.MdiParent = this;
            bewerkGondardennes.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Ben u zeker dat u Willbox wilt afsluiten?", "Willbox sluiten", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dr)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                case DialogResult.Abort: break;
            }
        }
    }
}
