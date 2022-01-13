using BusinessLayer.Entities;
using DataAccessLayer.Repositories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.voertuig;
using System.Collections.ObjectModel;
using UI.utils;

namespace UI.tankkaart
{
    /// <summary>
    /// Logique d'interaction pour TankkaartPage.xaml
    /// </summary>
    public partial class TankkaartPage : Page
    {
        private VoertuigPage VoertuigPage;
        public TankkaartPage(VoertuigPage vp = null)
        {
            InitializeComponent();
            VoertuigPage = vp;
            FillWindow();
        }
        public ObservableCollection<Tankkaart> tankkaarten = new ObservableCollection<Tankkaart>();
        private void FillWindow()
        {
            cmb_ZoekenOpBrandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
        }
        private string FillDetails(Tankkaart t)
        {
            string result =
                $"\nkaartnummer: {t.KaartNummer}\ngeldigheid: {t.GeldigheidsDatum}"
                + (string.IsNullOrWhiteSpace(t.Brandstoffen) ? "n/a brandstoffen, " : t.Brandstoffen + ", ")
                + (string.IsNullOrWhiteSpace(t.Pincode) ? "n/a pincode, " : t.Pincode + ", ");
            return result;
        }

        private void dpk_ZoekenOpGeldigheid_CalendarOpened(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Zoek_Click(object sender, RoutedEventArgs e)
        {
            tbl_Details.Text = null;
            DateTime date;
            string kaartnummer = (tbkZoekenOpKaartnummer.Text).Trim();
            string brandstof = string.Empty;
            string textDate = string.Empty;
            bool goForIt = false;
            if (!string.IsNullOrWhiteSpace(kaartnummer))
            {
                goForIt = true;
            }
            if (cmb_ZoekenOpBrandstof.SelectedIndex > -1)
            {
                brandstof = cmb_ZoekenOpBrandstof.SelectedValue.ToString();
                goForIt = true;
            }
            if (dpk_ZoekenOpGeldigheid.SelectedDate != null)
            {
                date = (DateTime)dpk_ZoekenOpGeldigheid.SelectedDate;
                textDate = date.ToString("yyyy-MM-dd");
                goForIt = true;
            }
            if (goForIt==true)
            {
                tankkaarten = Connection.Tankkaart().FetchBestuurders(kaartnummer, brandstof, textDate);
                lsv_TankkaartLijst.ItemsSource = tankkaarten;

                if (tankkaarten.Count == 0)
                {
                    tbl_Details.Text = "Geen overeenkomende resultaten gevonden";
                }
            }
            else
            {
                tbl_Details.Text = "Gelieve een zoekparameter in te geven.";
            }

        }

        private void lsv_TankkaartLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsv_TankkaartLijst.SelectedIndex > -1)
            {
                Tankkaart t = (Tankkaart)lsv_TankkaartLijst.SelectedItem;
                tbl_Details.Text = FillDetails(t);
            }
        }

        private void lsv_TankkaartLijst_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_TankkaartAanpassen_Click(object sender, RoutedEventArgs e)
        {
            Tankkaart bestuurder = (Tankkaart)lsv_TankkaartLijst.SelectedItem;

            TankkaartBewerken tb = new TankkaartBewerken(bestuurder);
            if (tb.ShowDialog() == true)
            {
                tankkaarten[lsv_TankkaartLijst.SelectedIndex] = tb.tankkaart;
                lsv_TankkaartLijst.SelectedItem = tb.tankkaart;
                tbl_Details.Text = FillDetails(tb.tankkaart);
            }
        }
        private void btn_TankkaartVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bent u zeker ?", "Tankkaart Verwijderen", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Tankkaart tankkaart = (Tankkaart)lsv_TankkaartLijst.SelectedItem;

                Connection.Tankkaart().DeleteTankkaart((int)tankkaart.KaartNummer);
                tankkaarten.RemoveAt(lsv_TankkaartLijst.SelectedIndex);
                lsv_TankkaartLijst.SelectedItem = null;
                tbl_Details.Text = "Tankkaart succesvol verwijderd!";
            }
        }

        private void btn_Wis_Click(object sender, RoutedEventArgs e)
        {
            cmb_ZoekenOpBrandstof.SelectedIndex = -1;
            btn_Wis.IsEnabled = false;
        }

        private void cmb_ZoekenOpBrandstof_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_ZoekenOpBrandstof.SelectedIndex != -1)
            {
                btn_Wis.IsEnabled = true;
            }
        }
    }
}
