using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
            BestuurdersBeheer bb = new BestuurdersBeheer();
            this.Close();
            bb.Show();
        }

        private void btn_VoertuigenBeheer_Click(object sender, RoutedEventArgs e)
        {
            VoertuigenBeheer vb = new VoertuigenBeheer();
            this.Close();
            vb.Show();
        }

        private void btn_TankkaartenBeheer_Click(object sender, RoutedEventArgs e)
        {
            TankkaartenBeheer tb = new TankkaartenBeheer();
            this.Close();
            tb.Show();
        }
    }
}
