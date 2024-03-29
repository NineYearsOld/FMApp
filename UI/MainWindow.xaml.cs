﻿using BusinessLayer.Services;
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
                bp.bestuurders.Add(bm.b);
                bp.lsv_BestuurdersLijst.ItemsSource = bp.bestuurders;
                bp.lsv_BestuurdersLijst.SelectedItem = bm.b;
            }
        }

        private void btn_VoertuigenBeheer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = vp;
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
