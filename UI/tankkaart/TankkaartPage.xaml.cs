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
        }
        public ObservableCollection<Tankkaart> tankkaarten = new ObservableCollection<Tankkaart>();
        private TankkaartService TankkaartService()
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
            TankkaartRepository tr = new TankkaartRepository(connectionString);
            TankkaartService ts = new TankkaartService(tr);
            return ts;
        }

        private void dpk_ZoekenOpGeldigheid_CalendarOpened(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Zoek_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_ToonDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsv_TankkaartLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lsv_TankkaartLijst_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_BestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_BestuurderVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
