using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace prison_system
{
    /// <summary>
    /// Class for generating a convict's Identification Number
    /// </summary>
   public class id_gen
    {
       /// <summary>
       /// Function to generate Convict's ID
       /// </summary>
       /// <returns>Convict's ID</returns>
       public int _trial()
       {
           string li = "0";
           MySqlConnection con = new MySqlConnection("Server=localhost;user id=root;password=; database=prison_db");
           MySqlCommand cmd = new MySqlCommand();
           MySqlDataReader reader;
           try
           {
               cmd.Connection = con;
               cmd.CommandText = "SELECT `id` FROM `prisoners_data` WHERE 1";
               con.Open();
               cmd.ExecuteNonQuery();
               reader = cmd.ExecuteReader();
               reader.Read();
               do
               {
                   li = reader.GetString(0);
               } while (reader.Read());
           }
           catch (Exception ex)
           {
               MessageBox.Show("error + " + ex.Message);
           }
           con.Close();
           int _rv = int.Parse(li) + 1;
           return _rv;
       }
    }
}
