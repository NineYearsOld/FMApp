using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.StaticData;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
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

namespace UI.bestuurder
{
    /// <summary>
    /// Logique d'interaction pour BestuurderMaken.xaml
    /// </summary>
    public partial class BestuurderMaken : Window
    {
        public BestuurderMaken()
        {
            InitializeComponent();
            FillCmbBoxes();
        }
        HashSet<string> rijbewijzen = new HashSet<string>();
        public Bestuurder b;
        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
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

        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFieldState())
            {
                if (!Connection.Bestuurder().ExistsBestuurder(0, tbk_Rijksregnr.Text))
                {
                    b = Connection.Bestuurder().CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbl_Rijbewijzen.Text.ToString(), tbk_Rijksregnr.Text, tbk_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
                    tbl_BestuurderDetails.Text = FillDetails(b);
                    btn_BestuurderToevoegen.Visibility = Visibility.Hidden;
                    btn_Annuleren.Content = "Ok";
                }
                else tbl_BestuurderDetails.Text = "Rijksregisternummer bestaat al in de databank";

            }
            else tbl_BestuurderDetails.Text = "Gelieve alle verreiste velden in te vullen.";
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
        private void ClearFields()
        {
            tbl_BestuurderDetails.Text = null;
            tbk_Gemeente.Text = null;
            cmb_Rijbewijs.SelectedItem = null;
            tbk_Huisnummer.Text = null;
            tbk_Naam.Text = null;
            tbk_Postcode.Text = null;
            tbk_Rijksregnr.Text = null;
            tbk_Straat.Text = null;
            tbk_Voornaam.Text = null;
            tbl_Rijbewijzen.Text = null;
            dpk_gebDatum.SelectedDate = null;
            rijbewijzen.Clear();
        }
        private bool CheckFieldState()
        {
            bool filledFields = false;
            string naam = null;
            string voornaam = tbk_Voornaam.Text;
            string rijksreg = tbk_Rijksregnr.Text;
            if (tbl_Rijbewijzen.Text != null)
            {
                naam = tbl_Rijbewijzen.Text.ToString();
            }

            if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam) && !string.IsNullOrWhiteSpace(rijksreg) && dpk_gebDatum.SelectedDate != null)
            {
                filledFields = true;
            }
            return filledFields;
        }
        public void FillCmbBoxes()
        {
            cmb_Rijbewijs.ItemsSource = Enum.GetValues(typeof(Rijbewijzen));
        }

        private void btn_Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
