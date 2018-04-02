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
    /// Interaction logic for pop_up.xaml
    /// </summary>
    public partial class pop_up : Window
    {
        public pop_up()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void lbl_close_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_close.Background = Brushes.Maroon;
            lbl_close.Foreground = Brushes.White;
        }

        private void lbl_close_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_close.Background = null;
            lbl_close.Foreground = Brushes.Black;
        }

        private void lbl_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
