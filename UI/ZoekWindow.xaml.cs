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
using UI.tankkaart;
using UI.voertuig;

namespace UI {
    /// <summary>
    /// Interaction logic for ZoekWindow.xaml
    /// </summary>
    public partial class ZoekWindow : Window {
        public ZoekWindow(object sender) {
            this.sender = sender;
            InitializeComponent();
        }
        Object sender;
        string origin;

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DecideOrigin();
            LoadContent();
        }

        private void LoadContent() {
            if (origin=="v") {
                zoek.Content = new BestuurderPage();
            } else if (origin=="b") {
                zoek.Content = new VoertuigPage(); // of TankkaartPage
            } else if (origin=="t") {
                zoek.Content = new BestuurderPage();
            }
        }

        private Object ReturnResult() {
            if (origin=="v") {
                // return bestuurder
            }else if (origin == "b") {
                // return voertuig of tankkaart
            }else if (origin == "t") {
                // return bestuurder
            }
            return null;
        }

        private void DecideOrigin() {
            if (sender.GetType().FullName.StartsWith("UI.voertuig.")) {
                origin = "v";
            } else if (sender.GetType().FullName.StartsWith("UI.bestuurder.")) {
                origin = "b";
            } else if (sender.GetType().FullName.StartsWith("UI.tankkaart.")) {
                origin = "t";
            }
        }

        private void Test() {
            if (sender.GetType().FullName.StartsWith("UI.voertuig.")) {
                
            }
        }
    }
}
