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

namespace UI
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
            FillCmbBox();
        }
        private TankkaartService TankkaartService()
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
            TankkaartRepository tr = new TankkaartRepository(connectionString);
            TankkaartService ts = new TankkaartService(tr);
            return ts;
        }
        private static int? TryParseNullable(string val)
        {
            int nInt;
            return int.TryParse(val, out nInt) ? nInt : null;
        }
        public void FillCmbBox()
        {
            cmb_Brandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (VoertuigPage == null)
            {
                VoertuigPage = new VoertuigPage();
            }
            NavigationService.Navigate(VoertuigPage);
        }

        private void btn_TankkaartToevoegen_Click(object sender, RoutedEventArgs e)
        {
            int? id = TankkaartService().CreateTankkaart((DateTime)dpk_Geldigheid.SelectedDate, tbk_Pincode.Text, cmb_Brandstof.SelectedItem.ToString(), Convert.ToInt32(tbk_BestuurderId.Text)).KaartNummer;
            // ClearFields();
            Tankkaart t = TankkaartService().ToonDetails((int)id);
            tbk_BestuurderId.Text = id.ToString();
            //tbl_BestuurderDetails.Text = $"{b.Naam} {b.Voornaam}\n{b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\ngemeente: {b.Gemeente}\nstraat: {b.Straat}\nhuisnummer: {b.Huisnummer}\npostcode: {b.Postcode}";
        }
    }
}
