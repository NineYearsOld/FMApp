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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour BestuurdersBeheer.xaml
    /// </summary>
    public partial class BestuurdersBeheer : Window
    {
        public BestuurdersBeheer()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void btn_BestuurderToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True";
            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);

        }
}
}
