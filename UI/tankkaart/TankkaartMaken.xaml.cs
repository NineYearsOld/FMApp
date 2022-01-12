﻿using BusinessLayer.Entities;
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

namespace UI.tankkaart
{
    /// <summary>
    /// Logique d'interaction pour TankkaartMaken.xaml
    /// </summary>
    public partial class TankkaartMaken : Window
    {
        public TankkaartMaken()
        {
            InitializeComponent();
        }
        public Tankkaart tankkaart;
    }
}
