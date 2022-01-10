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

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigBewerken.xaml
    /// </summary>
    public partial class VoertuigBewerken : Window {
        public VoertuigBewerken() {
            InitializeComponent();
        }

        private void update_Click(object sender, RoutedEventArgs e) {
            result.Text = $"Nummerplaat: {nummerplaat.Text}\nKleur: {kleur.Text}\nBestuurder: ";
            bevestig.Visibility = Visibility.Visible;
        }

        private void bevestig_Click(object sender, RoutedEventArgs e) {
            var vs = Connection.Voertuig();
            //vs.UpdateVoertuig();
        }

        private void back_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
