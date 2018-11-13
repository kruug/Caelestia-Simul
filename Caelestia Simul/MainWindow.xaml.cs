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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caelestia_Simul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            clsTime time = new clsTime();

            lblUTC.Content = time.getUTC().ToString("yyyy-MM-dd HH\\:mm\\:ss");
            lblLocal.Content = time.getLocal().ToString("yyyy-MM-dd HH\\:mm\\:sszzz");
            lblUTCJulian.Content = time.getUTCJulianDate();
            lblLocalJulian.Content = time.getLocalJulianDate();
            //lblUTCGregorian.Content = time.getGegorianFromJulian(time.getUTCJulianDate());
            //lblLocalGregorian.Content = time.getGegorianFromJulian(time.getLocalJulianDate());
            lblGMST.Content = time.getGMST();
            lblGAST.Content = time.getGAST();
        }
    }
}
