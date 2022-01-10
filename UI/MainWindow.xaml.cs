using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UI.bestuurder;
using UI.voertuig;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }
        BestuurderPage bp = new BestuurderPage();
        VoertuigPage vp = new VoertuigPage();
        TankkaartPage tp = new TankkaartPage();
        private void LoadPages()
        {
            Main.Content = bp;
        }

        private void btn_BestuurderMaken_Click(object sender, RoutedEventArgs e)
        {
            BestuurderMaken bm = new BestuurderMaken();
            bm.Owner = this;
            bm.ShowDialog();
            bp.bestuurders.Add(bm.b);
            bp.lsb_BestuurdersLijst.ItemsSource = bp.bestuurders;
            bp.lsb_BestuurdersLijst.SelectedItem = bm.b;
        }

        private void btn_VoertuigMaken_Click(object sender, RoutedEventArgs e)
        {
            VoertuigMaken vm = new VoertuigMaken();
            vm.Owner = this;
            vm.ShowDialog();
        }

        private void btn_TankkaartenBeheer_Click(object sender, RoutedEventArgs e)
        {
            tp.FillCmbBox();
            Main.Content = tp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPages();
        }
    }
}
