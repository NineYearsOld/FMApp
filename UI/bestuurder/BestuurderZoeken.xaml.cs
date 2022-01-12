using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace UI.bestuurder
{
    /// <summary>
    /// Logique d'interaction pour BestuurderZoeken.xaml
    /// </summary>
    public partial class BestuurderZoeken : Window
    {
        public BestuurderZoeken()
        {
            InitializeComponent();
        }
        public ObservableCollection<Bestuurder> bestuurders;
        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;

        private void btn_ToonOvereenkomende_Click(object sender, RoutedEventArgs e)
        {
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
                //tbl_BestuurderDetails.Text = "Geen overeenkomende resultaten gevonden";
            }
            lsv_BestuurdersLijst.ItemsSource = bestuurders;
        }
        private void lsv_BestuurdersLijst_Click(object sender, RoutedEventArgs e)
        {

            if (!(e.OriginalSource is GridViewColumnHeader headerClicked))
            {
                return;
            }
            ListSortDirection direction = ListSortDirection.Ascending;

            if (headerClicked == lastHeaderClicked && lastDirection == ListSortDirection.Ascending)
            {
                direction = ListSortDirection.Descending;
            }
            Tools.Sort(headerClicked, direction, lsv_BestuurdersLijst);

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

            if (lastHeaderClicked != null && lastHeaderClicked != headerClicked)
            {
                lastHeaderClicked.Column.HeaderTemplate = null;
            }

            lastHeaderClicked = headerClicked;
            lastDirection = direction;
        }

        private void dpk_ZoekenOpGeboortedatum_CalendarOpened(object sender, RoutedEventArgs e)
        {
            Tools.DatePickerOptions(sender, e);
        }

        private void lsv_BestuurdersLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
