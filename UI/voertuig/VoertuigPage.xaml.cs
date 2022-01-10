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
using UI.bestuurder;
using UI.utils;

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigPage.xaml
    /// </summary>
    public partial class VoertuigPage : Page {
        private BestuurderPage BestuurderPage;
        private TankkaartPage TankkaartPage;
        public VoertuigPage(BestuurderPage bp = null) {
            InitializeComponent();
            BestuurderPage = bp;
        }
        private void btn_back_Click(object sender, RoutedEventArgs e) {
            if (BestuurderPage == null) {
                BestuurderPage = new BestuurderPage();
            }
            NavigationService.Navigate(BestuurderPage);
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e) {
            if (TankkaartPage == null) {
                TankkaartPage = new TankkaartPage(this);
            }
            NavigationService.Navigate(TankkaartPage);
        }

        private void Zoek_Click(object sender, RoutedEventArgs e) {
            var vs = Connection.Voertuig();
            vs.GetVoertuigen(merk.Text,model.Text,nummerplaat.Text);
            details.Visibility = Visibility.Visible;
        }

        private void details_Click(object sender, RoutedEventArgs e) {
            VoertuigDetails vd = new VoertuigDetails();
            vd.ShowDialog();
        }
    }
}
