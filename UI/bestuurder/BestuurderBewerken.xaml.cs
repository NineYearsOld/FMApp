using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.StaticData;
using DataAccessLayer.Repositories;

namespace UI.bestuurder
{
    /// <summary>
    /// Logique d'interaction pour BestuurderBewerken.xaml
    /// </summary>
    public partial class BestuurderBewerken : Window
    {
        public BestuurderBewerken(Bestuurder b = null)
        {
            InitializeComponent();
            bestuurder = b;
            FillWindow(b);
        }
        public Bestuurder bestuurder;
        public string ass = "ass";
        HashSet<string> rijbewijzen = new HashSet<string>();

        private void FillWindow(Bestuurder b)
        {
            tbl_Rijbewijzen.Text = b.Rijbewijs;
            tbk_Naam.Text = b.Naam;
            tbk_Voornaam.Text = b.Voornaam;
            tbk_Huisnummer.Text = b.Huisnummer;
            tbk_Straat.Text = b.Straat;
            tbk_Gemeente.Text = b.Gemeente;
            tbk_Postcode.Text = b.Postcode.ToString();
            tbk_Rijksregnr.Text = b.RijksregisterNummer;
            dpk_gebDatum.SelectedDate = b.GeboorteDatum;
            cmb_Rijbewijs.ItemsSource = Enum.GetValues(typeof(Rijbewijzen));
            rijbewijzen = b.Rijbewijs.Split("; ").ToHashSet();

        }
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;

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
                + (string.IsNullOrWhiteSpace(b.Huisnummer) ? "n/a huisnr, " : b.Huisnummer + ", ")
                + (string.IsNullOrWhiteSpace(b.Straat) ? "n/a straat, " : b.Straat + ", ")
                + (string.IsNullOrWhiteSpace(b.Gemeente) ? "n/a gemeente" : b.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + b.Postcode + ")");
            return result;
        }
        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            int id = bestuurder.Id;
            bestuurder = new Bestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbk_Rijksregnr.Text, tbl_Rijbewijzen.Text.ToString(), tbk_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
            bestuurder.Id = id;
            Connection.Bestuurder().UpdateBestuurder(bestuurder, id);
            tbl_BestuurderDetails.Text = FillDetails(bestuurder);
            btn_Annuleren.Content = "Ok";
            btn_BestuurderAanpassen.Visibility = Visibility.Hidden;
        }

        private void btn_Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
        private void tbk_Postcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
        private void tbk_Rijksregnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }

        private void tbk_Id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
        private void btn_RijbewijsToevoegen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Add(cmb_Rijbewijs.SelectedValue.ToString());
            tbl_Rijbewijzen.Text = string.Join("; ", rijbewijzen);
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
            tbl_Rijbewijzen.Text = null;
            btn_RijbewijsToevoegen.IsEnabled = true;
            btn_RijbewijzenWissen.IsEnabled = false;
        }
    }
}
