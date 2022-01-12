using BusinessLayer.Entities;
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
using UI.tankkaart;
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
            detailview.Visibility = Visibility.Hidden;
            details.Text = "";
            var vs = Connection.Voertuig();
            var voertuigen = vs.GetVoertuigen(merk.Text,model.Text,nummerplaat.Text);
            lv.ItemsSource = voertuigen;
        }

        private void detailview_Click(object sender, RoutedEventArgs e) {
            Voertuig v = (Voertuig)lv.SelectedItem;
            VoertuigDetails vd = new VoertuigDetails(v);
            vd.ShowDialog();
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv.SelectedIndex > -1) {
                Voertuig v = (Voertuig)lv.SelectedItem;
                FillDetails(v);
                detailview.Visibility = Visibility.Visible;
            }
        }

        private void FillDetails(Voertuig v) {
            details.Text = $"Chassisnummer: {v.ChassisNummer}\nNummerplaat: {v.Nummerplaat}\nMerk: {v.Merk} Model: {v.Model}\nBestuurder: {v.BestuurderId}";
        }

        private void lv_Click(object sender, RoutedEventArgs e) {

        }
    }
}
