using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamsoft.DotNet.TWAIN.Enums;
using MySql.Data.MySqlClient;

namespace prison_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Global variables to be used...
        #region declarations
        public static ImageSource _img,_ci;
        public static System.Drawing.Image bio_img;
        public static string _index, _temp;
        public static List<String> _pri_names = new List<string>();
        public static List<String> _inmate_names = new List<string>();
        public static String _cn, _cp, _ct, _pn, _pno;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\temp_data") == true)
            {
                // Program Temporary folder exists, so we continue execution...
            }
            else
            {
                // create a new Temporary Folder for the Program...
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\temp_data");
            }
            // Make sure these license keys are given to avoid any SDK_RELATED error...
            this.dynamicDotNetTwain1.LicenseKeys = "3A81B0A193CC694A58736359BAFC2C9C;3A81B0A193CC694A10ABD9F01B81D5DA;3A81B0A193CC694A68D5E9CCC589BDC1;3A81B0A193CC694A85288E31666B404C;3A81B0A193CC694A20B242EC8D2D91B6;3A81B0A193CC694ABDC1C217C3B432EE";
            this.dynamicDotNetTwain1.SupportedDeviceType = EnumSupportedDeviceType.SDT_WEBCAM;
            this.dynamicDotNetTwain1.IfShowUI = true;
            // Set the control for previewing input from camera...
            dynamicDotNetTwain1.SetVideoContainer(inmate_image);
            // Timer to handle our Time/Date updates...
            Timer t = new Timer();
            t.Interval = 100;
            t.Tick += t_Tick;
            t.Start();
            panel_enroll.Margin = new Thickness(10, 10, 0, 0);
            panel_enroll.Height = 639;
            panel_enroll.Width = 1056;
            panel_enroll.Visibility = System.Windows.Visibility.Visible;
            panel_check_out.Visibility = System.Windows.Visibility.Hidden;
            panel_statistics.Visibility = System.Windows.Visibility.Hidden;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            // Load States from our Enumaeration
            foreach (Enum i in Enum.GetValues(typeof(States.stats)))
            {
                cbostate.Items.Add(i);
            }
            // Connect to DB_Server and Load Content...
            _my_sql_db db = new _my_sql_db();
            db._verify_db_exists();
            db._load_db();
            //System.Threading.Thread th = new System.Threading.Thread(db._verify_db_exists);
            //th.Start();
            //db._load_db();     
           
        }
        /// <summary>
        /// Handles our convict registration via the _reg_convict function in _my_sql_db.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void enroller(object sender, RoutedEventArgs args)
        {
            if (txt_prison_name.Text != null && txt_con_name.Text != null)
            {
                _my_sql_db db = new _my_sql_db();
                id_gen io = new id_gen();
                _app_brain ap = new _app_brain();
                System.Drawing.Image _i = System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\temp_data\\in_use.jpg");
                db._verify_db_exists();
                db.id = io._trial().ToString();
                db.convict_name = txt_con_name.Text;
                db.dob = txt_dob.Text;
                db.home_address = txt_home_add.Text;
                db.city = txt_city.Text;
                db.mobile = txt_mobile.Text;
                db.gender = cbo_gender.Text;
                db.marital = cbo_marital.Text;
                db.state = cbostate.Text;
                db.lga = cbolga.Text;
                db.home_town = txt_hometown.Text;
                db.template = "0000SJIDDIAADHFSJKK99793FUU";
                db.findex = "4";
                db.picture = ap._to_string(_i);
                db.crime_type = txt_crime_type.Text;
                db.date_of_crime = txt_date_of_crime.Text;
                db.crime_location = txt_crime_loca.Text;
                db.prison_name = txt_prison_name.Text;
                db.prison_number = txt_prison_number.Text;
                db.jail_term = txt_jail_term.Text;
                db.crime_details = txt_details.Text;
                db.next_of_kin_name = txt_next_kin_name.Text;
                db.kin_phone = txt_kin_phone.Text;
                db.relationship = txt_relationship.Text;
                db.kin_address = txt_kin_address.Text;
                bool v = db._reg_convict();
                if (v == true)
                {
                   //System.Windows.MessageBox.Show("Convict successfully added !!!", "Enroll Convict");
                    pop_up pop = new pop_up();
                    pop.lbl_title.Content = "Enroll Convict";
                    pop.lbl_msg.Text = "\nCoonvict successfully added !!!";
                    pop.ShowDialog();
                    txt_con_name.Text = ""; txt_dob.Text = ""; txt_home_add.Text = ""; txt_city.Text = "";
                    txt_mobile.Text = ""; cbo_gender.SelectedIndex = -1; cbo_marital.SelectedIndex = -1;
                    txt_relationship.SelectedIndex = -1; cbolga.SelectedIndex = -1;
                    cbostate.SelectedIndex = -1; cbolga.SelectedIndex = -1; txt_hometown.Text = "";
                    inmate_image.Source = null; inmate_bio.Source = null; txt_crime_type.Text = "";
                    txt_date_of_crime.Text = ""; txt_crime_loca.Text = ""; txt_prison_name.Text = "";
                    txt_prison_number.Text = ""; txt_jail_term.Text = ""; txt_details.Text = "";
                    txt_next_kin_name.Text = ""; txt_kin_phone.Text = ""; txt_relationship.Text = "";
                    txt_kin_address.Text = "";
                    //clear fields
                }
                else
                { //do nothing for now, coz there was an error
                }

                db._load_db();
            } else
            {
                System.Windows.Forms.MessageBox.Show("Please fill in the blanks ...","Error");
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            lbltime.Content = global::System.DateTime.Now.ToLongDateString();
            lbltimer.Content = global::System.DateTime.Now.ToLongTimeString();
        }

        private void enroll_select(object sender, RoutedEventArgs e)
        {
            panel_enroll.Margin = new Thickness(10, 10, 0, 0);
            panel_enroll.Height = 639;
            panel_enroll.Width = 1056;
            panel_enroll.Visibility = System.Windows.Visibility.Visible;
            panel_check_out.Visibility = System.Windows.Visibility.Hidden;
            panel_statistics.Visibility = System.Windows.Visibility.Hidden;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
        }
        private void checkout_select(object sender, RoutedEventArgs e)
        {
            panel_check_out.Margin = new Thickness(10, 10, 0, 0);
            panel_check_out.Height = 639;
            panel_check_out.Width = 1056;
            panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            panel_statistics.Visibility = System.Windows.Visibility.Hidden;
            panel_check_out.Visibility = System.Windows.Visibility.Visible;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            //    panel_enroll.Visibility = System.Windows.Visibility.Hidden;
        }

        private void b1(object sender, RoutedEventArgs e)
        {
            try
            {
                cam ca = new cam();
                ca.ShowDialog();
                inmate_image.Source = _img;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void exiter(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) { Console.Out.WriteLine("__EXIT__ERR__AT__" + ex.Message); }
        }
        private void minim(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void mw_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.MessageBoxResult m;
            m = (System.Windows.MessageBoxResult)System.Windows.Forms.MessageBox.Show("Do you want to quit?", "Exit", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
            if (m == MessageBoxResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
                GC.Collect(); // collect the garbage after the deletion
            }
            else { e.Cancel = true; GC.Collect(); /* collect the garbage
                                                   * after the deletion */}
        }

        private void stat_Selected(object sender, RoutedEventArgs e)
        {
            panel_statistics.Margin = new Thickness(10, 10, 0, 0);
            panel_statistics.Height = 639;
            panel_statistics.Width = 1056;
            panel_statistics.Visibility = System.Windows.Visibility.Visible;
            panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            panel_check_out.Visibility = System.Windows.Visibility.Hidden;
            //panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            //panel_enroll.Visibility = System.Windows.Visibility.Hidden;
            _my_sql_db m = new _my_sql_db();
            m._stat_load(); 
            list_prison.ItemsSource = _pri_names;
            lbl_pri_count.Text = list_prison.Items.Count.ToString();
            //
        }

        #region lga
        void _reg_combo()
        {
	try {
		if (cbostate.SelectedItem.ToString() == "Abia") {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.Abia))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Adamawa")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.adamawa))) {
			    cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "AkwaIbom")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.AkwaIbom))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Anambra")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.anambra))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Bauchi")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.bauchi))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Bayelsa")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.Bayelsa))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Benue")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.benue))) {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Borno")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.Borno))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "CrossRiver")
        {
			cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.CrossRiver))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Delta")
        {
        	cbolga.Items.Clear();
			foreach (var i in Enum.GetValues(typeof(States.Delta))) {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Ebonyi")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.ebonyi)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Edo")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Edo)))
            {
    		cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Ekiti")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.ekiti)))
            {
			cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Enugu")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.enugu)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Gombe")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.gombe)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Imo")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.imo)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Jigawa")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.jigawa)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Kaduna")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.kaduna)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Kano")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Kano)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Katsina")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Katsina)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Kebbi")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.kebbi)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Kogi")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.kogi)))
            {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Kwara")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.kwara)))
           {		
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Lagos")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.lagos)))
            {		
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Nasarawa")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Nasarawa)))
            {
					cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Niger")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Niger)))
            {		
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Ogun")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Ogun)))
            {		
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Ondo")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Ondo)))
            {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Osun")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Osun)))
            {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Oyo")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.oyo)))
            {		
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Plateau")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.plateau)))
            {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Rivers")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Rivers)))
            {	 
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Sokoto")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.sokoto)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Taraba")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Taraba)))
            {
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Yobe")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Yobe)))
            {	
				cbolga.Items.Add(i);
			}
        }
        else if (cbostate.SelectedItem.ToString() == "Zamfara")
        {
			cbolga.Items.Clear();
            foreach (var i in Enum.GetValues(typeof(States.Zamfara)))
            {		
				cbolga.Items.Add(i);
			}
		}
	} catch (Exception ex) {
        System.Windows.MessageBox.Show(ex.Message);
		//Interaction.MsgBox(Err.Description);
	}
}
        #endregion

        private void cbostate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _reg_combo();
        }

        private void btn_bio(object sender, RoutedEventArgs e)
        {
            try
            {
                bio b = new bio();
                b.ShowDialog();
              
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message,"Error");
            }
        }

        private void list_prison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _my_sql_db m = new _my_sql_db();
            list_inmates.ItemsSource = null; 
            try
            {
                m._populate_inmates(list_prison.SelectedItem.ToString());
                list_inmates.ItemsSource = _inmate_names;
                lbl_in_count.Text = list_inmates.Items.Count.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ERR_AT_LST_PRI_SEL_CHGD +++ \n" + ex.Message);
            }
        }

        private void list_inmates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _my_sql_db m = new _my_sql_db();
          
            try
            {
                m._view_inmate_data(list_inmates.SelectedItem.ToString());
                sel_inmate_name.Text = _cn;
                sel_inmate_pri.Text = _pn;
                sel_inmate_term.Text = _pno;
                sel_inmate_crime.Text = _ct;
                _app_brain ap = new _app_brain();
                System.Drawing.Image i = ap._fun(_cp);

               // sel_inmate_img.Source = (BitmapSource)i;
                //MemoryStream mem = new MemoryStream();
                //Byte[] buffer = Convert.FromBase64String(_cp);
                //mem.Position = 0;
                //mem.Write(buffer, 0, buffer.Count());
                //if (mem != null)
                //{
                //    System.Windows.Media.Imaging.BitmapImage bi = new BitmapImage();
                //    bi.StreamSource = mem;
                //    ImageSource k = (ImageSource)bi;
                //    sel_inmate_img.Source = k;
                //}

            }

            //try
            //{
            //   
            //    ImageSourceConverter con = new ImageSourceConverter();
            //    var s = con.ConvertFrom(i);
            //    Stream ss;
            //    ss.Position = 0;
            //    JpegBitmapDecoder d = new JpegBitmapDecoder(BitmapCreateOptions.None, BitmapCacheOption.Default);
            //    sel_inmate_img.Source = (ImageSource)s;
            //}
            //catch (Exception u)
            //{
            //    System.Windows.Forms.MessageBox.Show("Test" + u.Message);
            //}

            //using (FileStream stream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\temp_data\\in_use.jpg", FileMode.Create)) ;

            //System.Windows.Forms.MessageBox.Show(_cp);
            //db.picture = ap._to_string(_i);

            //sel_inmate_img.Source = System.Windows.Forms.Application.StartupPath + "\\temp_data\\in_img.jpg";
            //= (ImageSource)isc.ConvertFromString(_cp);

            catch (Exception ex)
            {
               Console.WriteLine("ERR_AT_LST_INM_SEL_CHGD +++ \n" + ex.Message);
            }
        }
    
    }
}