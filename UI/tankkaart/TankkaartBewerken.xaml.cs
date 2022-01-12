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
    /// Logique d'interaction pour TankkaartBewerken.xaml
    /// </summary>
    public partial class TankkaartBewerken : Window
    {
        public TankkaartBewerken(Tankkaart t)
        {
            InitializeComponent();
            FillWindow(t);
            tankkaart = t;
        }
        Tankkaart tankkaart;
        HashSet<string> brandstoffen = new HashSet<string>();

        private void FillWindow(Tankkaart t)
        {
            dpk_Geldigheid.SelectedDate = t.GeldigheidsDatum;
            cmb_Brandstof.ItemsSource = Enum.GetValues(typeof(Brandstoffen));
            if (t.Brandstoffen != null)
            {
                brandstoffen = t.Brandstoffen.Split("; ").ToHashSet();
                tbl_Brandstoffen.Text = brandstoffen.ToString();
            }
            else
            {
                btn_Brandstoffen_Wissen.IsEnabled = false;
            }
            tbk_Pincode.Text = t.Pincode.ToString();
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

        private void btn_Aanpassen_Click(object sender, RoutedEventArgs e)
        {
            tankkaart.Brandstoffen = tbl_Brandstoffen.Text;
            tankkaart.UpdateGeldigheidsdatum(dpk_Geldigheid.SelectedDate);
            tankkaart.UpdatePincode(tbk_Pincode.Text);

            Connection.Tankkaart().UpdateTankkaart(tankkaart);
        }

        private void btn_Decision_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
