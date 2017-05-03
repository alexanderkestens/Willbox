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
    public partial class Kalender : Form
    {
        private DateTime datum;
        public Kalender()
        {
            InitializeComponent();
            LoadDate();
        }

        private void LoadDate ()
        {
            String vandaag = System.DateTime.Now.ToString("dddd").ToString();
            datum = DateTime.Now;
            switch (vandaag)
            {
                case "maandag":
                    Maandag.Text = datum.ToString("dd'/'MM");
                    Dinsdag.Text = datum.AddDays(1).ToString("dd'/'MM");
                    Woensdag.Text = datum.AddDays(2).ToString("dd'/'MM");
                    Donderdag.Text = datum.AddDays(3).ToString("dd'/'MM");
                    Vrijdag.Text = datum.AddDays(4).ToString("dd'/'MM");
                    break;
                case "dinsdag":
                    Dinsdag.Text = datum.ToString("dd'/'MM");
                    Maandag.Text = datum.AddDays(-1).ToString("dd'/'MM");
                    Woensdag.Text = datum.AddDays(1).ToString("dd'/'MM");
                    Donderdag.Text = datum.AddDays(2).ToString("dd'/'MM");
                    Vrijdag.Text = datum.AddDays(3).ToString("dd'/'MM");
                    break;
                case "woensdag":
                    Woensdag.Text = datum.ToString("dd'/'MM");
                    Maandag.Text = datum.AddDays(-2).ToString("dd'/'MM");
                    Dinsdag.Text = datum.AddDays(-1).ToString("dd'/'MM");
                    Donderdag.Text =  datum.AddDays(1).ToString("dd'/'MM");
                    Vrijdag.Text = datum.AddDays(2).ToString("dd'/'MM");
                    break;
                case "donderdag":
                    Donderdag.Text = datum.ToString("dd'/'MM");
                    Maandag.Text = datum.AddDays(-3).ToString("dd'/'MM");
                    Dinsdag.Text = datum.AddDays(-2).ToString("dd'/'MM");
                    Woensdag.Text = datum.AddDays(-1).ToString("dd'/'MM");
                    Vrijdag.Text = datum.AddDays(1).ToString("dd'/'MM");
                    break;
                case "vrijdag":
                    Vrijdag.Text = datum.ToString("dd'/'MM");
                    Maandag.Text = datum.AddDays(-4).ToString("dd'/'MM");
                    Dinsdag.Text = datum.AddDays(-3).ToString("dd'/'MM");
                    Woensdag.Text = datum.AddDays(-2).ToString("dd'/'MM");
                    Donderdag.Text = datum.AddDays(-1).ToString("dd'/'MM");
                    break;
            }
        }

        private void Kalender_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Main"] != null)
            {
                (Application.OpenForms["Main"] as Main).dataClose("kalender");
            }
        }
    }
}
