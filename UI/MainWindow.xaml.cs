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
using UI.tankkaart;
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
            if (bm.ShowDialog() == true)
            {
                bp.bestuurders.Add(bm.bestuurder);
                bp.lsv_BestuurdersLijst.ItemsSource = bp.bestuurders;
                bp.lsv_BestuurdersLijst.SelectedItem = bm.bestuurder;
            }
        }
        private void btn_TankkaartMaken_Click(object sender, RoutedEventArgs e)
        {
            TankkaartMaken tm = new TankkaartMaken();
            tm.Owner = this;
            if (tm.ShowDialog() == true)
            {
                tp.tankkaarten.Add(tm.tankkaart);
                tp.lsv_TankkaartLijst.ItemsSource = bp.bestuurders;
                tp.lsv_TankkaartLijst.SelectedItem = tm.tankkaart;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPages();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = ((TabItem)tab_Selector.SelectedItem).Name.ToString();
            switch (name)
            {
                case "b": Main.Content = bp;
                    break;
                case "v": Main.Content = vp;
                    break;
                case "t": Main.Content = tp;
                    break;
            }
        }
    }
}
