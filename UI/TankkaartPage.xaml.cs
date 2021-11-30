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

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour TankkaartPage.xaml
    /// </summary>
    public partial class TankkaartPage : Page
    {
        private VoertuigPage VoertuigPage;
        public TankkaartPage(VoertuigPage vp = null)
        {
            InitializeComponent();
            VoertuigPage = vp;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(VoertuigPage);
        }
    }
}
