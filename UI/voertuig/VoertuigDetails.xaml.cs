using BusinessLayer.Entities;
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
        public VoertuigDetails(Voertuig v) {
            InitializeComponent();
            this.v = v;
            FillText();
        }
        Voertuig v;

        private void FillText() {
            if (v.BestuurderId != null) {
                result.Text = $"Chassisnummer: {v.ChassisNummer}\nNummerplaat: {v.Nummerplaat}\nMerk: {v.Merk}\nModel: {v.Model}\nBrandstoffen: {v.Brandstoffen}\nType wagen: {v.TypeWagen}\nKleur: {v.Kleur}\nAantal deuren: "+ (v.AantalDeuren != null ? v.AantalDeuren:"Niet ingevuld")+ $"\nBestuurder: {v.Bestuurder.Naam} {v.Bestuurder.Voornaam}";
            } else {
                result.Text = $"Chassisnummer: {v.ChassisNummer}\nNummerplaat: {v.Nummerplaat}\nMerk: {v.Merk}\nModel: {v.Model}\nBrandstoffen: {v.Brandstoffen}\nType wagen: {v.TypeWagen}\nKleur: {v.Kleur}\nAantal deuren: {v.AantalDeuren}\nBestuurder:";
            }
        }

        private void update_Click(object sender, RoutedEventArgs e) {
            VoertuigBewerken vb = new VoertuigBewerken(v);
            vb.Show();
        }

        private void verwijder_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("Bent u zeker dat u dit voertuig wilt verwijderen?", "Voertuig verwijderen  ", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                var vs = Connection.Voertuig();
                vs.DeleteVoertuig(v.ChassisNummer);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
