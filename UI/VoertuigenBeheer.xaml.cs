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
using BusinessLayer.Services;
using BusinessLayer.Entities;

namespace UI {
    /// <summary>
    /// Logique d'interaction pour VoertuigenBeheer.xaml
    /// </summary>
    public partial class VoertuigenBeheer : Window {
        public VoertuigenBeheer() {
            InitializeComponent();
        }
        private bool btnAanmaken = true;


        private void Clear_Click(object sender, RoutedEventArgs e) {
            foreach (var c in sp2.Children) {
                if (c.GetType() == tb.GetType()) {
                    TextBox t = (TextBox)c;
                    t.Text = "";
                } else {
                    cb.Items.Clear();
                    break;
                }
            }
            foreach (var c in sp4.Children) {
                if (c.GetType() == tb.GetType()) {
                    TextBox t = (TextBox)c;
                    t.Text = "";
                }
            }
            if (!btnAanmaken) {
                Aanmaken.Visibility = Visibility.Visible;
                Updaten.Visibility = Visibility.Hidden;
                Verwijderen.Visibility = Visibility.Hidden;
                btnAanmaken = true;
            }
        }

        private void Aanmaken_Click(object sender, RoutedEventArgs e) {
            List<string> input = new List<string>();
            foreach (var c in sp2.Children) {
                if (c.GetType() == tb.GetType()) {
                    TextBox t = (TextBox)c;
                    input.Add(t.Text);
                }
            }
        }

        private void Zoek_Click(object sender, RoutedEventArgs e) {
            List<string> input = new List<string>();
            VoertuigService vs = new VoertuigService();
            foreach (var c in sp4.Children) {
                if (c.GetType() == tb.GetType()) {
                    TextBox t = (TextBox)c;
                    input.Add(t.Text);
                }
            }
            int x = 0;
            foreach (var i in input) {
                x++;
                if (!string.IsNullOrWhiteSpace(i)) {
                    switch (x) {
                        case 1:
                            Voertuig v;
                            // v = Zoek op nummerplaat
                            // Display v
                            break;
                        case 2:
                            // Combinatie nodig?
                            break;
                        case 3:
                            break;
                    }
                }
            }
            Aanmaken.Visibility = Visibility.Hidden;
            Updaten.Visibility = Visibility.Visible;
            Verwijderen.Visibility = Visibility.Visible;
            btnAanmaken = false;
        }

        private void Updaten_Click(object sender, RoutedEventArgs e) {

        }

        private void ZoekBestuurder_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(Naam.Text)) {
                List<Bestuurder> output = new List<Bestuurder>();
                List<string> result = new List<string>();
                // output = Zoek op naam
                foreach (var b in output) {
                    result.Add(b.GeboorteDatum + b.Naam + b.Voornaam);
                }

                foreach (var s in result) {
                    cb.Items.Add(s);
                }
            }
            if (!string.IsNullOrWhiteSpace(Voornaam.Text)) {
                List<Bestuurder> output = new List<Bestuurder>();
                List<string> result = new List<string>();
                // output = Zoek op voornaam
                foreach (var b in output) {
                    result.Add(b.GeboorteDatum + b.Naam + b.Voornaam);
                }

                foreach (var s in result) {
                    cb.Items.Add(s);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void Verwijderen_Click(object sender, RoutedEventArgs e) {
            var r = MessageBox.Show(this, "Bent u zeker dat u deze wilt verwijderen?", "Verwijder Confirmation", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.No);
            if (r == MessageBoxResult.Yes) {
                // Delete
                MessageBox.Show("test");
            } 
        }
    }
}
