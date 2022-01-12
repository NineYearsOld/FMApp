using BusinessLayer.Services;
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
using UI.utils;
using BusinessLayer.StaticData;

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigMaken.xaml
    /// </summary>
    public partial class VoertuigMaken : Window {
        public VoertuigMaken() {
            InitializeComponent();
            LoadCBData();
        }
        int x;
        private void Maak_Click(object sender, RoutedEventArgs e) {
            result.Text = $"Merk: {merk.Text}\nModel: {model.Text}\nChassisnummer: {chassisnr.Text}\nNummerplaat: {nummerplaat.Text}\nBrandstof: {brandstof.Text}\nType wagen: {typewagen.Text}\nKleur: {kleur.Text}\nAantal deuren: {x}\nBestuurder: ";
            bevestig.Visibility = Visibility.Visible;
        }

        private void bevestig_Click(object sender, RoutedEventArgs e) {
            var vs = Connection.Voertuig();
            if (!string.IsNullOrWhiteSpace(kleur.Text) && x != 0) {
                vs.CreateVoertuig(merk.Text, model.Text, chassisnr.Text, nummerplaat.Text, brandstof.Text, typewagen.Text, kleur.Text, x, null);
            } else if (string.IsNullOrWhiteSpace(kleur.Text) && x == 0) {
                vs.CreateVoertuig(merk.Text, model.Text, chassisnr.Text, nummerplaat.Text, brandstof.Text, typewagen.Text, null, null, null);
            } else if (string.IsNullOrWhiteSpace(kleur.Text)) {
                vs.CreateVoertuig(merk.Text, model.Text, chassisnr.Text, nummerplaat.Text, brandstof.Text, typewagen.Text, null, x, null);
            } else if (x == 0) {
                vs.CreateVoertuig(merk.Text, model.Text, chassisnr.Text, nummerplaat.Text, brandstof.Text, typewagen.Text, kleur.Text, null, null);
            }
        }

        private void annuleer_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void up_Click(object sender, RoutedEventArgs e) {
            if (x < 20) {
                x++;
                deuren.Text = x.ToString();
            }
        }

        private void down_Click(object sender, RoutedEventArgs e) {
            if (x > 0) {
                x--;
                deuren.Text = x.ToString();
            }
        }

        private void LoadCBData() {
            brandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
            typewagen.ItemsSource = Enum.GetValues(typeof(WagenTypes));
        }

        private void bestuurder_Click(object sender, RoutedEventArgs e) {
            new ZoekWindow(this).ShowDialog();
        }
    }
}
