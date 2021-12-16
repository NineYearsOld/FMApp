using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.StaticData;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace UI
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
        private BestuurderService BestuurderService()
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);
            return bs;
        }
        HashSet<string> rijbewijzen = new HashSet<string>();
        ObservableCollection<Bestuurder> bestuurders;
        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void FillCmbBoxes()
        {
            cmb_Rijbewijs.ItemsSource = Enum.GetValues(typeof(Rijbewijzen));            
        }
        private string FillDetails(Bestuurder b)
        {
            string postcode = null;
            if (b.Postcode != null)
            {
                postcode = b.Postcode.ToString();
            }
            string result = 
                $"{b.Naam} {b.Voornaam}\ngeboortedatum: {b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\nadres: " 
                + (string.IsNullOrWhiteSpace(b.Huisnummer) ? "" : b.Huisnummer + ", ") 
                + (string.IsNullOrWhiteSpace(b.Straat) ? "" : b.Straat + ", ") + b.Gemeente 
                + (string.IsNullOrWhiteSpace(postcode) ? "" : "(" + b.Postcode + ")");
            return result;
        }
        private void ClearFields()
        {
            lbl_BestuurderDetails.Content = null;
            tbk_Gemeente.Text = null;
            cmb_Rijbewijs.SelectedItem = null;
            tbk_Huisnummer.Text = null;
            tbk_Naam.Text = null;
            tbk_Postcode.Text = null;
            tbk_Rijksregnr.Text = null;
            tbk_Straat.Text = null;
            tbk_Voornaam.Text = null;
            lbl_Rijbewijzen.Content = null;
            dpk_gebDatum.SelectedDate = null;
            rijbewijzen.Clear();
        }
        private bool CheckFieldState()
        {
            bool filledFields = false;
            if (lbl_Rijbewijzen.Content != null && tbk_Naam.Text != null && tbk_Voornaam.Text != null && tbk_Rijksregnr.Text != null && dpk_gebDatum.SelectedDate != null)
            {
                filledFields = true;
            }
            return filledFields;
        }
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
        }
        private void btn_RijbewijsToevoegen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Add(cmb_Rijbewijs.SelectedValue.ToString());
            lbl_Rijbewijzen.Content = string.Join("; ", rijbewijzen);
            btn_RijbewijsToevoegen.IsEnabled = false;
            btn_RijbewijzenWissen.IsEnabled = true;
        }

        private void cmb_Rijbewijs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Rijbewijs.SelectedItem == null) { }
            else if (rijbewijzen.Contains(cmb_Rijbewijs.SelectedValue.ToString()))
            {
                btn_RijbewijsToevoegen.IsEnabled = false;
            }
            else btn_RijbewijsToevoegen.IsEnabled = true;
        }

        private void btn_RijbewijzenWissen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Clear();
            lbl_Rijbewijzen.Content = null;
            btn_RijbewijsToevoegen.IsEnabled = true;
            btn_RijbewijzenWissen.IsEnabled = false;
        }
        private void tbk_Postcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
        private void tbk_Rijksregnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFieldState())
            {
                if (!BestuurderService().ExistsBestuurder(0, tbk_Rijksregnr.Text))
                {
                    int id = BestuurderService().CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, lbl_Rijbewijzen.Content.ToString(), tbk_Rijksregnr.Text, tbk_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text)).Id;
                    Bestuurder b = BestuurderService().ToonDetails(id);
                    lbl_BestuurderDetails.Content = FillDetails(b);
                    bestuurders.Add(b);
                    lsb_BestuurdersLijst.SelectedItem = null;   
                }
                else lbl_BestuurderDetails.Content = "Rijksregisternummer bestaat al in de databank";

            }
            else lbl_BestuurderDetails.Content = "Gelieve alle verreiste velden in te vullen.";
        }

        private void tbk_Id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
            if (bestuurder == null)
            {
                lbl_BestuurderDetails.Content = "Gelieve een bestuurder te kiezen";
            }
            else
            {
                Bestuurder b = new Bestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbk_Rijksregnr.Text, lbl_Rijbewijzen.Content.ToString(), tbk_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
                BestuurderService().UpdateBestuurder(b, bestuurder.Id);

                lbl_BestuurderDetails.Content = FillDetails(b);
            }
        }

        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            Bestuurder bestuurder = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
            if (bestuurder == null)
            {
                lbl_BestuurderDetails.Content = "Gelieve een bestuurder te kiezen.";
            }
            else
            {
                BestuurderService().DeleteBestuurder(bestuurder.Id);
                ClearFields();
                bestuurders.RemoveAt(bestuurders.Count - 1);
                lsb_BestuurdersLijst.SelectedItem = null;
                lbl_BestuurderDetails.Content = "Bestuurder succesvol verwijderd!";
            }
        }

        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {
            lbl_BestuurderDetails.Content = null;
            DateTime date;
            string textDate = string.Empty;
            if (dpk_ZoekenOpGeboortedatum.SelectedDate != null)
            {
                date = (DateTime)dpk_gebDatum.SelectedDate;
                textDate = date.ToString("yyyy-MM-dd");
            }
            bestuurders = BestuurderService().FetchBestuurders(tbk_ZoekenOpNaam.Text, tbk_ZoekenOpVoornaam.Text, textDate);
            if (bestuurders.Count == 0)
            {
                lbl_BestuurderDetails.Content = "Geen overeenkomende resultaten gevonden"; 
            }
            lsb_BestuurdersLijst.ItemsSource = bestuurders;
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            VoertuigPage vp = new VoertuigPage(this);
            NavigationService.Navigate(vp);
        }

        private void lsb_BestuurdersLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsb_BestuurdersLijst.SelectedIndex>-1)
            {
                Bestuurder b = (Bestuurder)lsb_BestuurdersLijst.SelectedItem;
                lbl_BestuurderDetails.Content = FillDetails(b);
                rijbewijzen = b.Rijbewijs.Split("; ").ToHashSet();
                lbl_Rijbewijzen.Content = b.Rijbewijs;
                tbk_Naam.Text = b.Naam;
                tbk_Voornaam.Text = b.Voornaam;
                tbk_Huisnummer.Text = b.Huisnummer;
                tbk_Straat.Text = b.Straat;
                tbk_Gemeente.Text = b.Gemeente;
                tbk_Postcode.Text = b.Postcode.ToString();
                tbk_Rijksregnr.Text = b.RijksregisterNummer;
                dpk_gebDatum.SelectedDate = b.GeboorteDatum;
            }
        }
    }
}
