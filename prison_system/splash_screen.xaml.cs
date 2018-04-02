using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prison_system
{
    /// <summary>
    /// Interaction logic for splash_screen.xaml
    /// </summary>
    public partial class splash_screen : Window
    {
      public  System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();
      public System.Windows.Forms.Timer tim = new System.Windows.Forms.Timer();
    public  _my_sql_db db = new _my_sql_db();

        public splash_screen()
        {
            InitializeComponent();
         ///   System.Windows.Forms.Timer ti = new System.Windows.Forms.Timer();
          //  ti.Interval = 100; tt.Interval = 1200; tim.Interval = 20;
          //  ti.Tick += ti_Tick; tt.Tick += tt_Tick;tim.Tick +=tim_Tick;
          //  ti.Start(); tt.Start();
            _initially();
      
        }

        private void tim_Tick(object sender, EventArgs e)
        {
            txt_status.AppendText(db.n);
            lbl_progress.Content = "50%";
            tim.Enabled = false;
        }

        void tt_Tick(object sender, EventArgs e)
        {
            _initially();
            tt.Enabled = false;
        }
        void _initially()
        {
            // Connect to DB_Server and Load Content...
            //   System.Threading.Thread th = new System.Threading.Thread(db._verify_db_exists);
            // th.Start();
            db._verify_db_exists();
            if (db.n == "Application Connected to server . . .\nEstablished . . . . .")
            {
           //     tim.Enabled = true;
            }
            else
            {
                txt_status.AppendText( db.n);
            }
            db._load_db();
            if (db.m == "\nRecords Loaded, ready ...")
            {
                txt_status.AppendText( db.m);
                lbl_progress.Content = "99%";
                MainWindow mw = new MainWindow();
                mw.InitializeComponent();
                mw.Show();
                this.Close();
                  GC.Collect(); // collect the garbage after the deletion
            }
            else
            {
                txt_status.AppendText( db.m + "\nClick on Application Image to reload !");
            }
        }
        void ti_Tick(object sender, EventArgs e)
        {
            lblct.Content = DateTime.Now.ToLongTimeString();
        }
      
        private void minim_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void clos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //System.Windows.MessageBoxResult m;
            //m = (System.Windows.MessageBoxResult)System.Windows.Forms.MessageBox.Show("Do you want to quit?", "Exit", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
            //if (m == MessageBoxResult.Yes)
            //{
            //    System.Windows.Forms.Application.Exit();
            //}
            //else { e.Cancel = true; }
            GC.Collect(); // collect the garbage after the deletion
        }

        private void spl_ico_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _initially();
            GC.Collect();
        }
    }
}
