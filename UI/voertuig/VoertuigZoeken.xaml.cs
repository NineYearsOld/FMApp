using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
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

namespace UI.voertuig {
    /// <summary>
    /// Interaction logic for VoertuigZoeken.xaml
    /// </summary>
    public partial class VoertuigZoeken : Window {
        public VoertuigZoeken() {
            InitializeComponent();
        }
        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;
        public Voertuig voertuig;

        private void Zoek_Click(object sender, RoutedEventArgs e) {
            select.IsEnabled = false;
            var vs = Connection.Voertuig();
            var voertuigen = vs.GetVoertuigen(merk.Text, model.Text, nummerplaat.Text);
            lv.ItemsSource = voertuigen;
        }

        private void lv_Click(object sender, RoutedEventArgs e) {
            if (!(e.OriginalSource is GridViewColumnHeader headerClicked)) {
                return;
            }
            ListSortDirection direction = ListSortDirection.Ascending;

            if (headerClicked == lastHeaderClicked && lastDirection == ListSortDirection.Ascending) {
                direction = ListSortDirection.Descending;
            }
            Tools.Sort(headerClicked, direction, lv);

            if (direction == ListSortDirection.Ascending) {
                headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowUp"] as DataTemplate;
            } else {
                headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowDown"] as DataTemplate;
            }

            if (lastHeaderClicked != null && lastHeaderClicked != headerClicked) {
                lastHeaderClicked.Column.HeaderTemplate = null;
            }

            lastHeaderClicked = headerClicked;
            lastDirection = direction;
        }

        private void select_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            select.IsEnabled = true;
            voertuig = (Voertuig)lv.SelectedItem;
        }
    }
}
