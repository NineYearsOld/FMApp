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
    /// Interaction logic for VoertuigDetails.xaml
    /// </summary>
    public partial class VoertuigDetails : Window {
        public VoertuigDetails() {
            InitializeComponent();
        }

        private void update_Click(object sender, RoutedEventArgs e) {
            VoertuigBewerken vb = new VoertuigBewerken();
            vb.Show();
        }

        private void verwijder_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("Bent u zeker dat u dit voertuig wilt verwijderen?", "Voertuig verwijderen  ", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                var vs = Connection.Voertuig();
                vs.DeleteVoertuig("");
            }
        }

        private void back_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
