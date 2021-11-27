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

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);

            bs.CreateBestuurder(tbk_Naam.Text, tbk_Voornaam.Text, (DateTime)dpk_gebDatum.SelectedDate, cmb_Rijbewijs.Text, tbk_Rijksregnr.Text, cmb_Gemeente.Text, tbk_Straat.Text, tbk_Huisnummer.Text, TryParseNullable(tbk_Postcode.Text));

        }

        public int? TryParseNullable(string val)
        {
            int outValue;
            return int.TryParse(val, out outValue) ? (int?)outValue : null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbBoxes();
        }
        private void FillCmbBoxes()
        {
            cmb_Rijbewijs.Items.Add("B");
            cmb_Rijbewijs.Items.Add("C");
            cmb_Gemeente.Items.Add("West-Vlaanderen");
            cmb_Gemeente.Items.Add("Oost-Vlaanderen");
        }

    }
}
