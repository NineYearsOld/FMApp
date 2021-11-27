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
        public static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
        }
        public void FillCmbBoxes()
        {
            cmb_Rijbewijs.ItemsSource = Enum.GetValues(typeof(Rijbewijzen));
            var gems = typeof(Gemeenten).GetFields()
                .Select(f => f.GetValue(f) as string);
            cmb_Gemeente.ItemsSource = gems;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }


        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            BestuurderService().CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, cmb_Rijbewijs.Text, tbk_Rijksregnr.Text, cmb_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbBoxes();
        }

        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            int? id = TryParseNullable(tbk_Id.Text);
            if (id == null)
            {
                MessageBox.Show("Gelieve een id in te geven.");
            }
            else BestuurderService().DeleteBestuurder((int)id);
        }

        private void btn_RijbewijsToevoegen_Click(object sender, RoutedEventArgs e)
        {
            rijbewijzen.Add(cmb_Rijbewijs.SelectedValue.ToString());
            lbl_Rijbewijzen.Content = string.Join("; ", rijbewijzen);
            btn_RijbewijsToevoegen.IsEnabled = false;
        }

        private void cmb_Rijbewijs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rijbewijzen.Contains(cmb_Rijbewijs.SelectedValue.ToString()))
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
        }

        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            int? id = TryParseNullable(tbk_Id.Text);
            if (id == null)
            {
                MessageBox.Show("Gelieve een id in te geven");
            }
            else { 
            Bestuurder b = new Bestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, tbk_Rijksregnr.Text, lbl_Rijbewijzen.Content.ToString(), cmb_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));
            BestuurderService().UpdateBestuurder(b, (int)id);
            }
        }
    }
}
