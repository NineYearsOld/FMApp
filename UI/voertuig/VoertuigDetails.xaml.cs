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
using UI.bestuurder;
using UI.utils;

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigDetails.xaml
    /// </summary>
    public partial class VoertuigDetails : Window {
        public VoertuigDetails(Voertuig v) {
            InitializeComponent();
            this.voertuig = v;
            FillText();
        }
        Voertuig voertuig;

        private void FillText() {
            if (voertuig.BestuurderId != null) {
                result.Text = $"Chassisnummer: {voertuig.ChassisNummer}\nNummerplaat: {voertuig.Nummerplaat}\nMerk: {voertuig.Merk}\nModel: {voertuig.Model}\nBrandstoffen: {voertuig.Brandstoffen}\nType wagen: {voertuig.TypeWagen}\nKleur: " + (voertuig.Kleur != null ? voertuig.Kleur : "Niet ingevuld") + "\nAantal deuren: " + (voertuig.AantalDeuren != null ? voertuig.AantalDeuren:"Niet ingevuld")+ $"\nBestuurder: {voertuig.Bestuurder.Naam} {voertuig.Bestuurder.Voornaam}";
            } else {
                result.Text = $"Chassisnummer: {voertuig.ChassisNummer}\nNummerplaat: {voertuig.Nummerplaat}\nMerk: {voertuig.Merk}\nModel: {voertuig.Model}\nBrandstoffen: {voertuig.Brandstoffen}\nType wagen: {voertuig.TypeWagen}\nKleur: " + (voertuig.Kleur != null ? voertuig.Kleur : "Niet ingevuld") + "\nAantal deuren: " + (voertuig.AantalDeuren != null ? voertuig.AantalDeuren : "Niet ingevuld") + $"\nBestuurder: Niet ingevuld";
            }
        }

        private void update_Click(object sender, RoutedEventArgs e) {
            VoertuigBewerken vb = new VoertuigBewerken(voertuig);
            vb.Show();
        }

        private void verwijder_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("Bent u zeker dat u dit voertuig wilt verwijderen?", "Voertuig verwijderen  ", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                if (voertuig.BestuurderId == null) {
                    var vs = Connection.Voertuig();
                    vs.DeleteVoertuig(voertuig.ChassisNummer);
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void bestuurder_Click(object sender, RoutedEventArgs e) {
            var bz = new BestuurderZoeken();
            bz.Owner = this;
            if (bz.ShowDialog() == true) {
                voertuig.Bestuurder = bz.bestuurder;
                if (voertuig.Bestuurder != null) {
                    voertuig.BestuurderId = voertuig.Bestuurder.Id;
                    result.Text = $"Chassisnummer: {voertuig.ChassisNummer}\nNummerplaat: {voertuig.Nummerplaat}\nMerk: {voertuig.Merk}\nModel: {voertuig.Model}\nBrandstoffen: {voertuig.Brandstoffen}\nType wagen: {voertuig.TypeWagen}\nKleur: {voertuig.Kleur}\nAantal deuren: " + (voertuig.AantalDeuren != null ? voertuig.AantalDeuren : "Niet ingevuld") + $"\nBestuurder: {voertuig.Bestuurder.Naam} {voertuig.Bestuurder.Voornaam}";
                } else {
                    voertuig.BestuurderId = null;
                    result.Text = $"Chassisnummer: {voertuig.ChassisNummer}\nNummerplaat: {voertuig.Nummerplaat}\nMerk: {voertuig.Merk}\nModel: {voertuig.Model}\nBrandstoffen: {voertuig.Brandstoffen}\nType wagen: {voertuig.TypeWagen}\nKleur: {voertuig.Kleur}\nAantal deuren: " + (voertuig.AantalDeuren != null ? voertuig.AantalDeuren : "Niet ingevuld") + $"\nBestuurder: Niet ingevuld";
                }
            } else {

            }
        }

        private void details_Click(object sender, RoutedEventArgs e) {
            if (voertuig.BestuurderId != null) {
                BestuurderDetails bd = new BestuurderDetails(voertuig.Bestuurder);
                bd.Owner = this;
                bd.ShowDialog();
            }
        }
    }
}
