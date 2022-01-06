using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.StaticData;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace UI.bestuurder
{
    /// <summary>
    /// Logique d'interaction pour BestuurderPage.xaml
    /// </summary>
    public partial class BestuurderPage : Page
    {
        public BestuurderPage()
        {
            InitializeComponent();
        }

        private VoertuigPage VoertuigPage;
        public ObservableCollection<Bestuurder> bestuurders = new ObservableCollection<Bestuurder>();

        private string FillDetails(Bestuurder b)
        {
            string postcode = null;
            if (b.Postcode != null)
            {
                postcode = b.Postcode.ToString();
            }
            string result = 
                $"{b.Naam} {b.Voornaam}\ngeboortedatum: {b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\nadres: " 
                + (string.IsNullOrWhiteSpace(b.Huisnummer) ? "n/a huisnr, " : b.Huisnummer + ", ") 
                + (string.IsNullOrWhiteSpace(b.Straat) ? "n/a straat, " : b.Straat + ", ") 
                + (string.IsNullOrWhiteSpace(b.Gemeente) ? "n/a gemeente" : b.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + b.Postcode + ")");
            return result;
        }


        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
            BestuurderBewerken bw = new BestuurderBewerken(bestuurder);
            bw.ShowDialog();

        }

        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Bent u zeker ?", "Bestuurder Verwijderen", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;

                Connection.Bestuurder().DeleteBestuurder(bestuurder.Id);
                bestuurders.RemoveAt(lsb_BestuurdersLijst.SelectedIndex);
                lsb_BestuurdersLijst.SelectedItem = null;
                lbl_BestuurderDetails.Content = "Bestuurder succesvol verwijderd!";
            }

        }

        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {
            Bestuurder b = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
            int id = b.Id;
            Details details = Connection.Bestuurder().ToonDetails(id);

            BestuurderDetails bestuurderDetails = new BestuurderDetails(details);
            bestuurderDetails.ShowDialog();
        }

        private void btn_ToonOvereenkomende_Click(object sender, RoutedEventArgs e)
        {
            lbl_BestuurderDetails.Content = null;
            DateTime date;
            string textDate = string.Empty;
            if (dpk_ZoekenOpGeboortedatum.SelectedDate != null)
            {
                date = (DateTime)dpk_ZoekenOpGeboortedatum.SelectedDate;
                textDate = date.ToString("yyyy-MM-dd");
            }
            bestuurders = Connection.Bestuurder().FetchBestuurders(tbk_ZoekenOpNaam.Text, tbk_ZoekenOpVoornaam.Text, textDate);
            if (bestuurders.Count == 0)
            {
                lbl_BestuurderDetails.Content = "Geen overeenkomende resultaten gevonden"; 
            }
            lsb_BestuurdersLijst.ItemsSource = bestuurders;
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            if (VoertuigPage == null)
            {
                VoertuigPage = new VoertuigPage(this);
            }
            NavigationService.Navigate(VoertuigPage);
        }

        private void lsb_BestuurdersLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsb_BestuurdersLijst.SelectedIndex>-1)
            {
                Bestuurder b = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
                lbl_BestuurderDetails.Content = FillDetails(b);
            }
        }
    }
}
