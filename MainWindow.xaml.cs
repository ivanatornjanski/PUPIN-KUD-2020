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

namespace Kudovi


{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MeniKudovi_Click(object sender, RoutedEventArgs e)
        {
            Kud objKudovi = new Kud();
            objKudovi.Show();
        }

        private void MeniIgraci_Click(object sender, RoutedEventArgs e)
        {
            Igrac objIgrac = new Igrac();
            objIgrac.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Kud objKudovi = new Kud();
            objKudovi.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Igrac objIgrac = new Igrac();
            objIgrac.Show();
        }
    }
}