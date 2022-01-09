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

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigMaken.xaml
    /// </summary>
    public partial class VoertuigMaken : Window {
        public VoertuigMaken() {
            InitializeComponent();
        }

        private void Maak_Click(object sender, RoutedEventArgs e) {
            result.Text = $"Merk: {merk.Text}\nModel: {model.Text}\nChassisnummer: {chassisnr.Text}\nNummerplaat: {nummerplaat.Text}\nBrandstof: \nType wagen: \nKleur: {kleur.Text}\nAantal deuren: \nBestuurder: ";
            bevestig.Visibility = Visibility.Visible;
        }

        private void bevestig_Click(object sender, RoutedEventArgs e) {
            var vs = Connection.Voertuig();
            vs.CreateVoertuig(merk.Text,model.Text,chassisnr.Text,nummerplaat.Text,brandstof.Text,typewagen.Text,kleur.Text,0,0);
        }

        private void annuleer_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
