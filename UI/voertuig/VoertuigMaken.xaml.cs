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
using UI.bestuurder;

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
            if (!string.IsNullOrWhiteSpace(merk.Text)&& !string.IsNullOrWhiteSpace(model.Text)&& !string.IsNullOrWhiteSpace(chassisnr.Text)&& !string.IsNullOrWhiteSpace(nummerplaat.Text)&& !string.IsNullOrWhiteSpace(brandstof.Text)&& !string.IsNullOrWhiteSpace(typewagen.Text)) {
                result.Text = $"Merk: {merk.Text}\nModel: {model.Text}\n\nChassisnummer: {chassisnr.Text}\nNummerplaat: {nummerplaat.Text}\n\nBrandstof: {brandstof.Text}\nType wagen: {typewagen.Text}\n\nKleur: {kleur.Text}\nAantal deuren: {x}\n\nBestuurder: ";
                bevestig.Visibility = Visibility.Visible;
                error.Text = "";
            } else {
                bevestig.Visibility = Visibility.Hidden;
                error.Text = "*Vul de verplichte velden in.";
                result.Text = "";
            }
        }

        private void bevestig_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(merk.Text) && !string.IsNullOrWhiteSpace(model.Text) && !string.IsNullOrWhiteSpace(chassisnr.Text) && !string.IsNullOrWhiteSpace(nummerplaat.Text) && !string.IsNullOrWhiteSpace(brandstof.Text) && !string.IsNullOrWhiteSpace(typewagen.Text)) {
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
            } else {
                bevestig.Visibility = Visibility.Hidden;
                error.Text = "*Vul de verplichte velden in.";
                result.Text = "";
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
        public BestuurderPage bp;
        private void bestuurder_Click(object sender, RoutedEventArgs e) {
            bp = new BestuurderPage();
            bp.btnOpties.Visibility = Visibility.Hidden;
            var bz = new BestuurderZoeken();
            bz.Title = "BestuurderZoeken";
            bz.Owner = this;
            if (bz.ShowDialog() == true) {
                bestuurderresult.Text = $"{bz.bestuurder.Naam} {bz.bestuurder.Voornaam}";
            } else {
                bestuurderresult.Text = "Zoekopdracht geannulleerd";
            }

        }
    }
}
