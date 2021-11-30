using BusinessLayer.StaticData;
using BusinessLayer.Services;
using DataAccessLayer.Repositories;
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
using System.Windows.Shapes;
using BusinessLayer.Entities;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour BestuurdersBeheer.xaml
    /// </summary>
    public partial class BestuurdersBeheer : Window
    {
        public BestuurdersBeheer()
        {
            InitializeComponent();
        }
        HashSet<string> rijbewijzen = new HashSet<string>();
        private BestuurderService BestuurderService()
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);
            return bs;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbBoxes();
        }
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
        }
        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void FillCmbBoxes()
        {
            cmb_Rijbewijs.ItemsSource = Enum.GetValues(typeof(Rijbewijzen));
            var gems = typeof(Gemeenten).GetFields()
                .Select(f => f.GetValue(f) as string);
            cmb_Gemeente.ItemsSource = gems;
        }
        private void ClearFields()
        {
            tbl_BestuurderDetails.Text = null;
            cmb_Gemeente.SelectedItem = null;
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
        private void cmb_Rijbewijs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Rijbewijs.SelectedItem == null) { }
            else if (rijbewijzen.Contains(cmb_Rijbewijs.SelectedValue.ToString()))
            {
                btn_RijbewijsToevoegen.IsEnabled = false;
            }
            else btn_RijbewijsToevoegen.IsEnabled = true;
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            TankkaartenBeheer tb = new TankkaartenBeheer();
            tb.Show();
            this.Hide();
        }
        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            int id = BestuurderService().CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, lbl_Rijbewijzen.Content.ToString(), tbk_Rijksregnr.Text, cmb_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text)).Id;
            ClearFields();
            Bestuurder b = BestuurderService().ToonDetails(id);
            tbk_Id.Text = id.ToString();
            tbl_BestuurderDetails.Text = $"{b.Naam} {b.Voornaam}\n{b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\ngemeente: {b.Gemeente}\nstraat: {b.Straat}\nhuisnummer: {b.Huisnummer}\npostcode: {b.Postcode}";
        }



        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            int? id = TryParseNullable(tbk_Id.Text);
            if (id == null)
            {
                MessageBox.Show("Gelieve een id in te geven.");
            }
            else if (!BestuurderService().ExistsBestuurder((int)id))
            {
                tbl_BestuurderDetails.Text = "Bestuurder id werd niet gevonden.";
            }
            else
            {
                BestuurderService().DeleteBestuurder((int)id);
                tbl_BestuurderDetails.Text = "Bestuurder succesvol verwijderd!";
                tbk_Id.Text = null;
            }
        }

        private void btn_RijbewijsToevoegen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Add(cmb_Rijbewijs.SelectedValue.ToString());
            lbl_Rijbewijzen.Content = string.Join("; ", rijbewijzen);
            btn_RijbewijsToevoegen.IsEnabled = false;
            btn_RijbewijzenWissen.IsEnabled = true;
        }

        private void btn_RijbewijzenWissen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Clear();
            lbl_Rijbewijzen.Content = null;
            btn_RijbewijsToevoegen.IsEnabled = true;
            btn_RijbewijzenWissen.IsEnabled = false;
        }

        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            int? id = TryParseNullable(tbk_Id.Text);
            if (id == null)
            {
                MessageBox.Show("Gelieve een id in te geven");
            }
            else if(!BestuurderService().ExistsBestuurder((int)id))
            {
                tbl_BestuurderDetails.Text = "Bestuurder id werd niet gevonden.";
            }
            else { 
                Bestuurder b = new Bestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbk_Rijksregnr.Text, lbl_Rijbewijzen.Content.ToString(), cmb_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
                BestuurderService().UpdateBestuurder(b, (int)id);
                tbl_BestuurderDetails.Text = $"{b.Naam} {b.Voornaam}\n{b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\ngemeente: {b.Gemeente}\nstraat: {b.Straat}\nhuisnummer: {b.Huisnummer}\npostcode: {b.Postcode}";
            }
        }

        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {
            int? id = TryParseNullable(tbk_Id.Text);
            if (id == null)
            {
                MessageBox.Show("Gelieve een id in te geven");
            }
            else if (!BestuurderService().ExistsBestuurder((int)id))
            {
                tbl_BestuurderDetails.Text = "Bestuurder id werd niet gevonden.";
            }
            else {
                Bestuurder b = BestuurderService().ToonDetails((int)id);
                tbl_BestuurderDetails.Text = $"{b.Naam} {b.Voornaam}\n{b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\ngemeente: {b.Gemeente}\nstraat: {b.Straat}\nhuisnummer: {b.Huisnummer}\npostcode: {b.Postcode}";
            }
        }

        private void btnIdUp_Click(object sender, RoutedEventArgs e)
        {
            long.TryParse(tbk_Id.Text, out long count);
            count ++;
            tbk_Id.Text = count.ToString("0.##");
        }

        private void btnIdDown_Click(object sender, RoutedEventArgs e)
        {
            long.TryParse(tbk_Id.Text, out long count);
            if (count<1)
            {
                count = 0;
            }  else count --;
            tbk_Id.Text = count.ToString("0.##");
        }

        private void tbk_Id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }

        private void tbk_Rijksregnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }

        private void tbk_Postcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputValidation(sender, e);
        }
    }
}
