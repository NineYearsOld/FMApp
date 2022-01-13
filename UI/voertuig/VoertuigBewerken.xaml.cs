using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.StaticData;
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
        public VoertuigBewerken(Voertuig v) {
            InitializeComponent();
            this.v = v;
            LoadCBData();
            FillData();
        }
        public Voertuig v;
        int? x;

        private void LoadCBData() {
            brandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
            typewagen.ItemsSource = Enum.GetValues(typeof(WagenTypes));
        }

        private void FillData() {
            chassisnr.Text = v.ChassisNummer;
            nummerplaat.Text = v.Nummerplaat;
            merk.Text = v.Merk;
            model.Text = v.Model;
            brandstof.SelectedItem = v.Brandstoffen;
            typewagen.SelectedItem = v.TypeWagen;
            kleur.Text = v.Kleur;
            x = v.AantalDeuren;
            deuren.Text = x.ToString();
            //BESTUURDER
        }

        private void update_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(merk.Text) && !string.IsNullOrWhiteSpace(model.Text) && !string.IsNullOrWhiteSpace(chassisnr.Text) && !string.IsNullOrWhiteSpace(nummerplaat.Text) && !string.IsNullOrWhiteSpace(brandstof.Text) && !string.IsNullOrWhiteSpace(typewagen.Text)) {
                result.Text = $"Chassissnummer: {chassisnr.Text}\nNummerplaat: {nummerplaat.Text}\nMerk: {merk.Text}\nModel: {model.Text}\nBrandstof: {brandstof.Text}\nType wagen: {typewagen.Text}\nKleur: {kleur.Text}\nAantaldeuren: {x}\nBestuurder: ";
                bevestig.Visibility = Visibility.Visible;
                error.Text = "";
            } else {
                result.Text = "";
                bevestig.Visibility = Visibility.Hidden;
                error.Text = "*Vul de verplichte velden in";
            }
        }

        private void bevestig_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(merk.Text) && !string.IsNullOrWhiteSpace(model.Text) && !string.IsNullOrWhiteSpace(chassisnr.Text) && !string.IsNullOrWhiteSpace(nummerplaat.Text) && !string.IsNullOrWhiteSpace(brandstof.Text) && !string.IsNullOrWhiteSpace(typewagen.Text)) {
                var vs = Connection.Voertuig();
                string cnr = v.ChassisNummer;
                v = new Voertuig(merk.Text, model.Text, chassisnr.Text, nummerplaat.Text, brandstof.Text, typewagen.Text, kleur.Text, x, null);
                vs.UpdateVoertuig(v, cnr);
            } else {
                result.Text = "";
                bevestig.Visibility = Visibility.Hidden;
                error.Text = "*Vul de verplichte velden in";
            }
        }

        private void back_Click(object sender, RoutedEventArgs e) {
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
    }
}