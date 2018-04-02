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
using Dynamsoft.DotNet.TWAIN.Enums;
using System.IO;

namespace prison_system
{
    /// <summary>
    /// Interaction logic for cam.xaml
    /// </summary>
    public partial class cam : Window
    {
        public cam()
        {

            InitializeComponent();
            this.dynamicDotNetTwain1.LicenseKeys = "3A81B0A193CC694A58736359BAFC2C9C;3A81B0A193CC694A10ABD9F01B81D5DA;3A81B0A193CC694A68D5E9CCC589BDC1;3A81B0A193CC694A85288E31666B404C;3A81B0A193CC694A20B242EC8D2D91B6;3A81B0A193CC694ABDC1C217C3B432EE";
            this.dynamicDotNetTwain1.SupportedDeviceType = EnumSupportedDeviceType.SDT_WEBCAM;
            this.dynamicDotNetTwain1.IfShowUI = false;
            for (short i = 0; i < dynamicDotNetTwain1.SourceCount; i++)
            {
                string SourceCountName = dynamicDotNetTwain1.SourceNameItems(i);
                if (SourceCountName != null)
                {
                    this.cb.Items.Add(SourceCountName);
                }
                if (cb.Items.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            dynamicDotNetTwain1.SetVideoContainer(image1);
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamicDotNetTwain1.OpenSource();
            }
            catch (Exception ex) { }
        }

        private void btn_capturee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow._img = image1.Source;
                dynamicDotNetTwain1.CloseSource();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamicDotNetTwain1.CloseSource();
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image1.Source));
                using (FileStream stream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\temp_data\\in_use.jpg", FileMode.Create))
                    encoder.Save(stream);
              cammer.Close();
              //var decoder = new PngBitmapDecoder();
                
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (((ComboBox)sender).SelectedIndex >= 0 && ((ComboBox)sender).SelectedIndex < dynamicDotNetTwain1.SourceCount)
                {
                    dynamicDotNetTwain1.SelectSourceByIndex(cb.SelectedIndex);
                    dynamicDotNetTwain1.OpenSource();
                }
            }
        }
    }
}
