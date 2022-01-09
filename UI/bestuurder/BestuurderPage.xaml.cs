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
using UI.voertuig;

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
                + (string.IsNullOrWhiteSpace(b.Gemeente) ? "n/a gemeente, " : b.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + b.Postcode + ")");
            return result;
        }


        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            BestuurderAanpassen();
        }

        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bent u zeker ?", "Bestuurder Verwijderen", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;

                Connection.Bestuurder().DeleteBestuurder(bestuurder.Id);
                bestuurders.RemoveAt(lsb_BestuurdersLijst.SelectedIndex);
                lsb_BestuurdersLijst.SelectedItem = null;
                tbl_BestuurderDetails.Text = "Bestuurder succesvol verwijderd!";
            }

        }

        private void BestuurderAanpassen()
        {
            Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
            BestuurderBewerken bw = new BestuurderBewerken(bestuurder);
            if (bw.ShowDialog() == true)
            {
                bestuurders[lsb_BestuurdersLijst.SelectedIndex] = bw.bestuurder;
                lsb_BestuurdersLijst.SelectedItem = bw.bestuurder;
                tbl_BestuurderDetails.Text = FillDetails(bw.bestuurder);
            }
        }

        private void ToonDetails()
        {
            int id = ((Bestuurder)lsb_BestuurdersLijst.SelectedItem).Id;
            Bestuurder b = Connection.Bestuurder().ToonDetails(id);
            BestuurderDetails bestuurderDetails = new BestuurderDetails(b);

            if (bestuurderDetails.ShowDialog() == true)
            {
                BestuurderAanpassen();
            }
        }
        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {
            ToonDetails();
        }

        private void btn_ToonOvereenkomende_Click(object sender, RoutedEventArgs e)
        {
            btnOpties.Visibility = Visibility.Hidden;
            tbl_BestuurderDetails.Text = null;
            DateTime date;
            string textDate = string.Empty;
            string naam = (tbk_ZoekenOpNaam.Text).Trim();
            string voornaam = tbk_ZoekenOpVoornaam.Text;
            if (dpk_ZoekenOpGeboortedatum.SelectedDate != null)
            {
                date = (DateTime)dpk_ZoekenOpGeboortedatum.SelectedDate;
                textDate = date.ToString("yyyy-MM-dd");
            }
            if ((bool)!ckb_ExacteNaam.IsChecked && !string.IsNullOrWhiteSpace(naam))
            {
                naam = '%' + naam + '%';
            }
            if ((bool)!cbk_ExacteVoornaam.IsChecked && !string.IsNullOrWhiteSpace(voornaam))
            {
                voornaam = '%' + voornaam + '%';
            }
            bestuurders = Connection.Bestuurder().FetchBestuurders(naam, voornaam, textDate);
            if (bestuurders.Count == 0)
            {
                tbl_BestuurderDetails.Text = "Geen overeenkomende resultaten gevonden"; 
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
                tbl_BestuurderDetails.Text = FillDetails(b);
                btnOpties.Visibility = Visibility.Visible;
            }
        }

        private void btnOpties_Click(object sender, RoutedEventArgs e)
        {
            ToonDetails();
        }
    }
}
