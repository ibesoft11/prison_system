using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace prison_system
{
    /// <summary>
    /// Class for our Database related processes
    /// </summary>
    public class _my_sql_db
    {
        #region _public_declarations
        static string _url = "Server=localhost;user id=root;password=; database=prison_db";
        public static MySqlConnection _conn;
        public static MySqlCommand _cmd = new MySqlCommand();
        public static MySqlDataAdapter _ada = new MySqlDataAdapter();
        public static DataTable _datat = new DataTable();
        public String n,m;     
        #endregion

        // Variables for registering a new convict...
        #region needed_data_for_reg_convict
        public string id, convict_name, dob, home_address, city, mobile, gender, marital, state, lga, home_town, template,
            findex, picture, crime_type, date_of_crime, crime_location, prison_name, prison_number, jail_term, crime_details,
            next_of_kin_name, kin_phone, relationship, kin_address;
        #endregion

        /// <summary>
        /// Verifies if the prisoners table exist else it creates a new table
        /// </summary>
        public void _verify_db_exists()
        {
try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "create table if not exists prisoners_data(" +
                    "id varchar(10)," +
                  "convict_name varchar(150)," +
                  "dob varchar(20)," +
                  "home_address varchar(50)," +
                  "city varchar(30)," +
                  "mobile varchar(15)," +
                  "gender varchar(7)," +
                  "marital varchar(10)," +
                  "state varchar(20)," +
                  "lga varchar(35)," +
                  "home_town varchar(50)," +
                  "template varchar(20000)," +
                  "findex varchar(3)," +
                  "picture varchar(20000)," +
                  "crime_type varchar(20)," +
                  "date_of_crime varchar(25)," +
                  "crime_location varchar(55)," +
                  "prison_name varchar(50)," +
                  "prison_number varchar(3)," +
                  "jail_term varchar(25)," +
                  "crime_details varchar(2500)," +
                  "next_of_kin_name varchar(80)," +
                  "kin_phone varchar(15)," +
                  "relationship varchar(18)," +
                  "kin_address varchar(50)" +
              ")";
                _conn.Open();
                _cmd.ExecuteNonQuery();
                n = "Application Connected to server . . .\nEstablished . . . . .";
            }
            catch (Exception ex)
            {
              //  System.Windows.Forms.MessageBox.Show(ex.Message);
              n= "\nConnection Error ... { "+ex.Message +" }";
            }
            _conn.Close();
        }

        /// <summary>
        /// Loads data from server
        /// </summary>
        public void _load_db()
        {
            try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "Select * from `prisoners_data`";
                _conn.Open();
                _ada.SelectCommand = _cmd;
                _cmd.ExecuteNonQuery();
                _ada.Fill(_datat);
                m = "\nRecords Loaded, ready ...";
            }
            catch (Exception)
            {
                m=("\nError retrieving data from database ....");
            }
            _conn.Close();
        }

        public void _inmate_details(string _inmate)
        {
            MainWindow._inmate_names.Clear();
            try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "Select `convict_name` from `prisoners_data` where `prison_name` like '%" + _inmate + "'";
                _conn.Open();
                _ada.SelectCommand = _cmd;
                _cmd.ExecuteNonQuery();
                _ada.Fill(_datat);
                MySqlDataReader _dr;
                _dr = _cmd.ExecuteReader();
                _dr.Read();
                do
                {
                    for (int i = 0; i <= _dr.FieldCount - 1; i++)
                    {
                        MainWindow._inmate_names.Add(_dr.GetString(i).ToString());
                    }
                } while (_dr.Read() == true);

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("EEE ++" + ex.Message);

            }
        }

        public void _view_inmate_data(String _inmate)
        {
             try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "Select `convict_name`,`picture`,`crime_type`,`prison_name`,`jail_term` from `prisoners_data` where `convict_name` = '"+_inmate+"'";
                _conn.Open();
                _ada.SelectCommand = _cmd;
               _cmd.ExecuteNonQuery();
               _ada.Fill(_datat);
              
                MySqlDataReader dr;
              dr = _cmd.ExecuteReader();
            dr.Read();         
                do
                {
                   MainWindow._cn  = dr.GetString(0).ToString();
                    //pic = at index 1
                   MainWindow._ct = dr.GetString(2).ToString();
                   MainWindow._pn = dr.GetString(3).ToString();
                   MainWindow._pno = dr.GetString(4).ToString();
                   MainWindow._cp = dr.GetString(1).ToString();
                } while (dr.Read());
                dr.Close();
                    
            }
             catch (Exception ex)
             {
                 System.Windows.Forms.MessageBox.Show("Test" + ex.Message);
             }
        }

        public void _populate_inmates(string _prison)
        {
            MainWindow._inmate_names.Clear();
            try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "Select `convict_name` from `prisoners_data` where `prison_name` like '%"+_prison+"'";
                _conn.Open();
                _ada.SelectCommand = _cmd;
               _cmd.ExecuteNonQuery();
               _ada.Fill(_datat);
               MySqlDataReader _dr;
               _dr = _cmd.ExecuteReader();
               _dr.Read();
               do
               {
                   for (int i = 0; i <= _dr.FieldCount - 1; i++)
                   {
                        MainWindow._inmate_names.Add(_dr.GetString(i).ToString());
                   }
               } while (_dr.Read() == true);
          
            }
  
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("EEE ++" + ex.Message);

            }
        }

        /// <summary>
        /// Populate's our prison count list..... Ibe Ogele
        /// </summary>
        public  void _stat_load()
        {
            try
            {
                MainWindow._pri_names.Clear();
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "Select `prison_name` from `prisoners_data`";
                _conn.Open();
                _ada.SelectCommand = _cmd;
               _cmd.ExecuteNonQuery();
               _ada.Fill(_datat);
               MySqlDataReader _dr;
               _dr = _cmd.ExecuteReader();
               _dr.Read();
               do
               {
                   for (int i = 0; i <= _dr.FieldCount - 1; i++)
                   {
                        MainWindow._inmate_names = new List<string>();
                       if (!MainWindow._pri_names.Contains(_dr.GetString(i).ToString().ToLower()))
                       {
                           MainWindow._pri_names.Add(_dr.GetString(i).ToString().ToLower());
                       }
                   }
               } while (_dr.Read() == true);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            _conn.Close();
        }

        /// <summary>
        /// Used for registering a new convict
        /// </summary>
        /// <returns>true if operation was successful else false</returns>
        public bool _reg_convict()
        {
            bool rv;
            try
            {
                _conn = new MySqlConnection(_url);
                _cmd.Connection = _conn;
                _cmd.CommandText = "INSERT INTO `prisoners_data`(`id`,`convict_name`,`dob`,`home_address`,`city`,`mobile`,`gender`," +
                    "`marital`,`state`,`lga`,`home_town`,`template`,`findex`,`picture`,`crime_type`,`date_of_crime`," +
                    "`crime_location`,`prison_name`,`prison_number`,`jail_term`,`crime_details`,`next_of_kin_name`,`kin_phone`," +
                        "`relationship`,`kin_address`) VALUES ('" + id + "','" + convict_name + "','" + dob + "','" + home_address + "','" + city + "','" +
                        mobile + "','" + gender + "','" + marital + "','" + state + "','" + lga + "','" + home_town + "','" + template + "','" + findex + "','" +
                        picture + "','" + crime_type + "','" + date_of_crime + "','" + crime_location + "','" + prison_name + "','" + prison_number + "','" +
                        jail_term + "','" + crime_details + "','" + next_of_kin_name + "','" + kin_phone + "','" + relationship + "','" +
                        kin_address + "')";
                _conn.Open();
                _ada.InsertCommand = _cmd;
                _cmd.ExecuteNonQuery();
              //  System.Windows.Forms.MessageBox.Show("Convict successfully added");
                _conn.Close();
                rv = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error adding convict \n" + ex.Message);
                _conn.Close();
                rv = false;
            }
            return rv;
        }

    }
}
