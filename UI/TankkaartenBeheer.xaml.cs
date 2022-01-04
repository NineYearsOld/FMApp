﻿using System;
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
    /// Logique d'interaction pour TankkaartenBeheer.xaml
    /// </summary>
    public partial class TankkaartenBeheer : Window
    {
        public TankkaartenBeheer()
        {
            InitializeComponent();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            BestuurdersBeheer bh = new BestuurdersBeheer();
            this.Close();
            bh.Show();
        }
    }
}