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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void btn_BestuurdersBeheer_Click(object sender, RoutedEventArgs e)
        {
            BestuurderPage bp = new BestuurderPage();
            bp.FillCmbBoxes();
            Main.Content = bp;
            /*
            BestuurdersBeheer bb = new BestuurdersBeheer();
            this.Close();
            bb.Show();
            */
        }

        private void btn_VoertuigenBeheer_Click(object sender, RoutedEventArgs e)
        {
            VoertuigPage vp = new VoertuigPage();
            Main.Content = vp;
        }

        private void btn_TankkaartenBeheer_Click(object sender, RoutedEventArgs e)
        {
            TankkaartPage tp = new TankkaartPage();
            Main.Content = tp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BestuurderPage bp = new BestuurderPage();
            bp.FillCmbBoxes();
            Main.Content = bp;
        }
    }
}
