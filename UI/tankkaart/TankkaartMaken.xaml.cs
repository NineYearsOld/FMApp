using BusinessLayer.Entities;
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
using System.Windows.Shapes;
using UI.utils;

namespace UI.tankkaart
{
    /// <summary>
    /// Logique d'interaction pour TankkaartMaken.xaml
    /// </summary>
    public partial class TankkaartMaken : Window
    {
        public TankkaartMaken(int? id = null)
        {
            InitializeComponent();
            bestuurderId = id;
            FillWindow();
        }
        public Tankkaart tankkaart;
        private int? bestuurderId;
        HashSet<string> brandstoffen = new HashSet<string>();

        private void FillWindow()
        {
            cmb_Brandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
        }
        private void cmb_Brandstof_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Brandstof.SelectedItem == null) { }
            else if (brandstoffen.Contains(cmb_Brandstof.SelectedValue.ToString()))
            {
                btn_Brandstof_Toevoegen.IsEnabled = false;
            }
            else btn_Brandstof_Toevoegen.IsEnabled = true;
        }

        private void btn_Brandstoffen_Wissen_Click(object sender, RoutedEventArgs e)
        {
            brandstoffen.Clear();
            tbl_Brandstoffen.Text = null;
            btn_Brandstof_Toevoegen.IsEnabled = true;
            btn_Brandstoffen_Wissen.IsEnabled = false;
        }

        private void btn_Brandstof_Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            brandstoffen.Add(cmb_Brandstof.SelectedValue.ToString());
            tbl_Brandstoffen.Text = string.Join("; ", brandstoffen);
            btn_Brandstof_Toevoegen.IsEnabled = false;
            btn_Brandstoffen_Wissen.IsEnabled = true;
        }
        private void btn_TankkaartMaken_Click(object sender, RoutedEventArgs e)
        {
            if (dpk_Geldigheid.SelectedDate != null)
            {
                tankkaart = Connection.Tankkaart().CreateTankkaart((DateTime)dpk_Geldigheid.SelectedDate, tbk_Pincode.Text, tbl_Brandstoffen.Text, bestuurderId);
                btn_TankkaartMaken.Visibility = Visibility.Hidden;
                btn_Decision.Content = "Ok";
            }
            else { tbl_TankkaartDetails.Text = "Gelieve de vervaldatum in te geven."; }

        }

        private void btn_Decision_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Decision.Content == "Ok")
            {
                DialogResult = true;
            }
            else { DialogResult = false; }
            Close();
        }
    }
}
