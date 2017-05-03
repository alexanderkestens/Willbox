using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProspectieFiche
{
    public partial class Laden : Form
    {
        private static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private static Thread thread;

        public Laden()
        {
            InitializeComponent();

            timer.Enabled = true;
            timer.Start();
            timer.Interval = 1000;
            pgbLaden.Maximum = 20;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (pgbLaden.Value != 20)
            {
                pgbLaden.Value++;
            }
            else
            {
                myTimer.Stop();
            }
        }

        private void Laden_Load(object sender, EventArgs e)
        {
            
        }

        private delegate void CloseDelegate();
        
        private static Laden splashForm;

        static public void ShowSplashScreen()
        {
            //if (splashForm != null)
              //  return;
            thread = new Thread(new ThreadStart(Laden.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            splashForm = new Laden();
            Application.Run(splashForm);
        }

        static public void CloseForm()
        {
            splashForm.Invoke(new CloseDelegate(Laden.CloseFormInternal));
            thread.Abort();
        }

        static private void CloseFormInternal()
        {
            splashForm.Close();
        }
    }
}
