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
using System.ComponentModel;
using UI.utils;

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
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
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
            Bestuurder bestuurder = (Bestuurder)lsv_BestuurdersLijst.SelectedItem;
            BestuurderBewerken bw = new BestuurderBewerken(bestuurder);
            if (bw.ShowDialog() == true)
            {
                bestuurders[lsv_BestuurdersLijst.SelectedIndex] = bw.bestuurder;
                lsv_BestuurdersLijst.SelectedItem = bw.bestuurder;
                tbl_BestuurderDetails.Text = FillDetails(bw.bestuurder);
            }
        }

        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bent u zeker ?", "Bestuurder Verwijderen", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Bestuurder bestuurder = (Bestuurder)lsv_BestuurdersLijst.SelectedItem;

                Connection.Bestuurder().DeleteBestuurder(bestuurder.Id);
                btnOpties.Visibility = Visibility.Hidden;
                bestuurders.RemoveAt(lsv_BestuurdersLijst.SelectedIndex);
                lsv_BestuurdersLijst.SelectedItem = null;
                tbl_BestuurderDetails.Text = "Bestuurder succesvol verwijderd!";
            }

        }

        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {
            int index = lsv_BestuurdersLijst.SelectedIndex;
            BestuurderDetails bd = new BestuurderDetails(bestuurders[index]);
            if (bd.ShowDialog() == true)
            {
                bestuurders[index] = bd.bestuurder;
                lsv_BestuurdersLijst.SelectedItem = bd.bestuurder;
                tbl_BestuurderDetails.Text = FillDetails(bd.bestuurder);
            }
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
             lsv_BestuurdersLijst.ItemsSource = bestuurders;
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            if (VoertuigPage == null)
            {
                VoertuigPage = new VoertuigPage(this);
            }
            NavigationService.Navigate(VoertuigPage);
        }

        private void lsv_BestuurdersLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsv_BestuurdersLijst.SelectedIndex>-1)
            {
                Bestuurder b = (Bestuurder)lsv_BestuurdersLijst.SelectedItem;
                tbl_BestuurderDetails.Text = FillDetails(b);
                btnOpties.Visibility = Visibility.Visible;
            }
        }

        private void lsv_BestuurdersLijst_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }

            }
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
            CollectionViewSource.GetDefaultView(bestuurders);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void dpk_ZoekenOpGeboortedatum_CalendarOpened(object sender, RoutedEventArgs e)
        {
            Tools.DatePickerOptions(sender, e);
        }
    }
}
