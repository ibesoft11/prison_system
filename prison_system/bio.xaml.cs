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
//using FlexCodeSDK;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace prison_system
{
    /// <summary>
    /// Interaction logic for bio.xaml
    /// </summary>
    public partial class bio : Window
    {
        bool ItIsUniqueTemplate;
    //    FlexCodeSDK.FinFPVer FpVer;
     //   FlexCodeSDK.FinFPReg FPReg;
      //  FlexCodeSDK.FinFPReg FPRe = new FinFPReg();
        String EmpID;
        Byte FpIndex;
        String template, c;
        MySqlConnection con = new MySqlConnection("server=127.0.0.1;user id=root;password=;database=auth_sys");
        
        public bio()
        {
        InitializeComponent();
        //FPReg.FPRegistrationStatus += FPReg_FPRegistrationStatus;
        //FPReg.FPRegistrationTemplate += FPReg_FPRegistrationTemplate;
        //FPReg.FPSamplesNeeded += FPReg_FPSamplesNeeded;
        //FPReg.FPRegistrationImage += FPReg_FPRegistrationImage;
        //FpVer.FPVerificationID += FpVer_FPVerificationID;
        //FpVer.FPVerificationStatus += FpVer_FPVerificationStatus;
        }
        
        void FPReg_FPSamplesNeeded(short Samples)
        {
            lblsample.Content = Samples.ToString() + "x";
        }

        void FPReg_FPRegistrationTemplate(string FPTemplate)
        {
            template = FPTemplate;
        }

        //void FPReg_FPRegistrationStatus(RegistrationStatus Status)
        //{
        //    if (Status == FlexCodeSDK.RegistrationStatus.r_OK)
        //    {
        //        btn_verify.IsEnabled = true;
        //    }
        //}

        //private void FpVer_FPVerificationStatus(VerificationStatus Status)
        //{
        //    if (Status == FlexCodeSDK.VerificationStatus.v_OK)
        //    {
        //        MessageBox.Show("This finger already registered as ID: " + EmpID + " and Finger Index:" + (FpIndex) + "\nPlease register other finger");
        //        btn_save.IsEnabled = false;
        //        btn_start.IsEnabled = true;
        //    }
        //    else if (Status == FlexCodeSDK.VerificationStatus.v_NotMatch || Status == FlexCodeSDK.VerificationStatus.v_FPListEmpty)
        //    {
        //        MessageBox.Show ("Your finger is unique! Now you can save the template to database.");
        //        ItIsUniqueTemplate = true;
        //    }
        //    else if (Status == FlexCodeSDK.VerificationStatus.v_VerifyCaptureFingerTouch)
        //    {
        //        MessageBox.Show("\nFinger touch !");
        //    }
        //    else
        //    {
        //        MessageBox.Show(Status.ToString());
        //    }
        //}

        //private void FpVer_FPVerificationID(String ID,FingerNumber FingerNr)
        //{
        //   EmpID = ID;
        //   FpIndex = (byte)FingerNr;
        //}

        //private void FPReg_FPRegistrationImage()
        //{
        //   System.IO.FileStream imgFile = new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + "\\FPTemp.BMP",System.IO.FileMode.Open,System.IO.FileAccess.Read,System.IO.FileShare.ReadWrite);
        //   Byte[] fileBytes = new Byte[imgFile.Length];
        //   imgFile.Read(fileBytes, 0, fileBytes.Length);
        //   imgFile.Close();
        //   System.IO.MemoryStream[] ms = new System.IO.MemoryStream[fileBytes.Length];
        //  // image1.Source = System.Drawing.Image.FromStream(ms);

        //}

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
           
            id_gen id = new id_gen();
            c = id._trial().ToString();
            //FPReg = new FlexCodeSDK.FinFPReg();
            //FPReg.AddDeviceInfo("C700F001339", "1SX-J98-067-81L-40X", "EAA0675F-3AC1-F649-8F07-A46D58C5ED6D");
            //FPReg.FPRegistrationStart("YourSecretKey" + c);
            //FPReg.PictureSamplePath = System.IO.Directory.GetCurrentDirectory() + "\\FPTemp.BMP";
            //FPReg.PictureSampleHeight = (short)image1.Height;
            //FPReg.PictureSampleWidth = (short)image1.Width;
            //ItIsUniqueTemplate = false;
        }

        private void btn_verify_Click(object sender, RoutedEventArgs e)
        {
           // try
            //{
            //    FpVer = new FlexCodeSDK.FinFPVer();
            //    FPReg.AddDeviceInfo("C700F001339", "1SX-J98-067-81L-40X", "C2BE52B7-BF7D-5D40-A8E6-BA6A47B1BD43");
            //    MySqlCommand sqlcommand = new MySqlCommand();
            //    con.Open();
            //    sqlcommand.Connection = con;
            //    sqlcommand.CommandText = "SELECT * FROM records";
            //    MySqlDataReader rd;
            //    rd = sqlcommand.ExecuteReader();
                
            //    do{
            //         FpVer.FPLoad(rd.GetString(0), (FingerNumber)rd.GetInt64(5), rd.GetString(6), "YourSecretKey" + rd.GetString(0));
            //    } while (rd.Read());
            //    FpVer.FPVerificationStart();
            //    rd.Close();
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (ItIsUniqueTemplate)
            {
                MessageBox.Show("It worked !!!");
            }
            this.Close();
        }
    }
}
