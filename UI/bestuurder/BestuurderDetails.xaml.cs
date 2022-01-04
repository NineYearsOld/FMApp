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
            ToonDetails(b);
        }
        private void ToonDetails(Bestuurder b)
        {
            string postcode = null;
            string aantalDeuren = null;
            if (b.Postcode != null)
            {
                postcode = b.Postcode.ToString();
            }
            if (b.Voertuig.AantalDeuren != null)
            {
                aantalDeuren = b.Voertuig.AantalDeuren.ToString();
            }
            string result =
                $"Bestuurder:\nnaam: {b.Naam} voornaam: {b.Voornaam}\ngeboortedatum: {b.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {b.RijksregisterNummer}\nrijbewijs: {b.Rijbewijs}\nadres: "
                + (string.IsNullOrWhiteSpace(b.Huisnummer) ? "n/a huisnr, " : b.Huisnummer + ", ")
                + (string.IsNullOrWhiteSpace(b.Straat) ? "n/a straat, " : b.Straat + ", ")
                + (string.IsNullOrWhiteSpace(b.Gemeente) ? "n/a gemeente" : b.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + b.Postcode + ")");
            if (b.Voertuig.ChassisNummer != null)
            {
                result +=
                    "\n\nVoertuig: " + (string.IsNullOrWhiteSpace(b.Voertuig.Merk) ? "\nn/a merk" : "\nMerk: " + b.Voertuig.Merk + ",")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.Model) ? "\nn/a model" : "\nModel: " + b.Voertuig.Model + ",")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.TypeWagen) ? "\nn/a carrosserie" : "\nCarrosserie: " + b.Voertuig.TypeWagen + ",")
                    + (string.IsNullOrWhiteSpace(aantalDeuren) ? "\nn/a aantal deuren" : "\nAantal deuren" + b.Voertuig.AantalDeuren + ", ")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.Brandstoffen) ? "\nn/a brandstof" : "\nBrandstof" + b.Voertuig.Brandstoffen + ",")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.ChassisNummer) ? "\nn/a chassisnummer" : "\nChassisnummer" + b.Voertuig.ChassisNummer + ",")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.Kleur) ? "\nn/a kleur" : "\nKleur " + b.Voertuig.Kleur + ",")
                    + (string.IsNullOrWhiteSpace(b.Voertuig.Nummerplaat) ? "\nn/a nummerplaat" : "\nNummerplaat: " + b.Voertuig.Nummerplaat + ",");
            }
            else
            {
                result += "\n\nGeen geassocieerde voertuig.";
            }
            if (b.Tankkaart.KaartNummer != null)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
