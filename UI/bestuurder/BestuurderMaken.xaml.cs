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
using UI.tankkaart;
using UI.utils;
using UI.voertuig;

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
        public Bestuurder bestuurder;


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
        private void FillTankkaartField(Tankkaart t)
        {
            tbl_Tankkaart.Text = $"Kaartnummer: {t.KaartNummer.ToString()}\nBrandstof: "
            + (string.IsNullOrWhiteSpace(t.Brandstoffen) ? "Onbekend." : t.Brandstoffen);
        }
        private void FillVoertuigField(Voertuig v)
        {
            if (v.ChassisNummer != null)
            {
                tbl_Voertuig.Text = $"Nummerplaat: {v.Nummerplaat}\nMerk: {v.Merk} {v.Model}";
            }
        }
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
        }
        private void tbk_Postcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Tools.InputValidation(sender, e);
        }
        private void tbk_Rijksregnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Tools.InputValidation(sender, e);
        }

        private void tbk_Id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Tools.InputValidation(sender, e);
        }

        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFieldState())
            {
                if (!Connection.Bestuurder().ExistsBestuurder(0, tbk_Rijksregnr.Text))
                {
                    bestuurder = Connection.Bestuurder().CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbl_Rijbewijzen.Text.ToString(), tbk_Rijksregnr.Text, tbk_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
                    tbl_BestuurderDetails.Text = FillDetails(bestuurder);
                    btn_BestuurderToevoegen.Visibility = Visibility.Hidden;
                    btn_Decision.Content = "Ok";
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

        private void btn_Decision_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Decision.Content == "Ok")
            {
                DialogResult = true;
            } else DialogResult = false;
            this.Close();
        }

        private void dpk_gebDatum_CalendarOpened(object sender, RoutedEventArgs e)
        {
            Tools.DatePickerOptions(sender, e);
        }
        private void btn_Voertuig_Click(object sender, RoutedEventArgs e)
        {
            VoertuigMaken vm = new VoertuigMaken();
            if (vm.ShowDialog() == true)
            {
                DialogResult = true;
            }
        }

        private void btn_Tankkaart_Click(object sender, RoutedEventArgs e)
        {
            TankkaartMaken tm = new TankkaartMaken(bestuurder.Id);
            if (tm.ShowDialog() == true)
            {
                DialogResult = true;
            }
        }
    }
}
