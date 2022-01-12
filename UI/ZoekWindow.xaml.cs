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
using UI.bestuurder;

namespace UI {
    /// <summary>
    /// Interaction logic for ZoekWindow.xaml
    /// </summary>
    public partial class ZoekWindow : Window {
        public ZoekWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            zoek.Content = new BestuurderPage();
        }
    }
}
