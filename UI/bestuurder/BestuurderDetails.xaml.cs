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
using System.Windows.Shapes;
using BusinessLayer.Entities;

namespace UI.bestuurder
{
    /// <summary>
    /// Logique d'interaction pour BestuurderDetails.xaml
    /// </summary>
    public partial class BestuurderDetails : Window
    {
        public BestuurderDetails(Bestuurder b)
        {
            InitializeComponent();
            bestuurder = b;
            ToonDetails(b);
        }
        public Bestuurder bestuurder;
        private bool hasEdited = false;

        private void ToonDetails(Bestuurder b)
        {
            string postcode = null;
            string aantalDeuren = null;
            if (b.Postcode != null)
            {
                postcode = b.Postcode.ToString();
            }

            string result =
                $"{b.Naam} {b.Voornaam}\ngeboortedatum: {b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\nadres: "
                + (string.IsNullOrWhiteSpace(b.Huisnummer) ? "n/a huisnr, " : b.Huisnummer + ", ")
                + (string.IsNullOrWhiteSpace(b.Straat) ? "n/a straat, " : b.Straat + ", ")
                + (string.IsNullOrWhiteSpace(b.Gemeente) ? "n/a gemeente, " : b.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + b.Postcode + ")");
            if (b.Voertuig != null && b.Voertuig.ChassisNummer != null)
            {
                    if (b.Voertuig.AantalDeuren != null)
                    {
                        aantalDeuren = b.Voertuig.AantalDeuren.ToString();
                    }
                result +=
                    $"\n\nVoertuig:\nMerk: {b.Voertuig.Merk},\nModel: {b.Voertuig.Model},\nCarrosserie: {b.Voertuig.TypeWagen},"
                    + (string.IsNullOrWhiteSpace(aantalDeuren) ? "\nn/a aantal deuren" : "\nAantal deuren" + b.Voertuig.AantalDeuren + ", ")
                    + $"\nBrandstof: {b.Voertuig.Brandstoffen},\nChassisnummer: {b.Voertuig.ChassisNummer},"
                    + (string.IsNullOrWhiteSpace(b.Voertuig.Kleur) ? "\nn/a kleur" : "\nKleur " + b.Voertuig.Kleur + ",")
                    + $"\nNummerplaat: {b.Voertuig.Nummerplaat}";
            }
            else
            {
                result += "\n\nGeen geassocieerde voertuig.";
            }
            if (b.Tankkaart != null && b.Tankkaart.KaartNummer != null)
            {
                result +=
                    "\n\nTankkaart: " + "\nkaartnummer: " + b.Tankkaart.KaartNummer
                    + (string.IsNullOrWhiteSpace(b.Tankkaart.Brandstoffen) ? "\nn/a brandstoffen" : "\nbrandstoffen: " + b.Tankkaart.Brandstoffen)
                    + "\nkaartnummer: " + b.Tankkaart.KaartNummer
                    + (string.IsNullOrWhiteSpace(b.Tankkaart.Pincode) ? "\nn/a pincode" : "\npincode: " + b.Tankkaart.Pincode);

            }
            else
            {
                result += "\n\nGeen geassocieerde tankkaart gevonden.";

            }

            lbl_Details.Content = result;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (hasEdited)
            {
                DialogResult = true;
            }
            this.Close();
        }

        private void btn_Bewerk_Click(object sender, RoutedEventArgs e)
        {
            BestuurderBewerken bw = new BestuurderBewerken(bestuurder);
            bw.Owner = this;
            if (bw.ShowDialog() == true)
            {
                hasEdited = true;
                bestuurder = bw.bestuurder;
                ToonDetails(bw.bestuurder);
            }
        }
    }
}
