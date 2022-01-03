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
        public BestuurderDetails(Details d)
        {
            InitializeComponent();
            ToonDetails(d);
        }
        private void ToonDetails(Details d)
        {
            string postcode = null;
            string aantalDeuren = null;
            if (d.Bestuurder.Postcode != null)
            {
                postcode = d.Bestuurder.Postcode.ToString();
            }
            if (d.Voertuig.AantalDeuren != null)
            {
                aantalDeuren = d.Voertuig.AantalDeuren.ToString();
            }
            string result =
                $"Bestuurder:\nnaam: {d.Bestuurder.Naam} voornaam: {d.Bestuurder.Voornaam}\ngeboortedatum: {d.Bestuurder.GeboorteDatum.ToShortDateString()}\nrijksregisternummer: {d.Bestuurder.RijksregisterNummer}\nrijbewijs: {d.Bestuurder.Rijbewijs}\nadres: "
                + (string.IsNullOrWhiteSpace(d.Bestuurder.Huisnummer) ? "n/a huisnr, " : d.Bestuurder.Huisnummer + ", ")
                + (string.IsNullOrWhiteSpace(d.Bestuurder.Straat) ? "n/a straat, " : d.Bestuurder.Straat + ", ")
                + (string.IsNullOrWhiteSpace(d.Bestuurder.Gemeente) ? "n/a gemeente" : d.Bestuurder.Gemeente + ", ")
                + (string.IsNullOrWhiteSpace(postcode) ? "n/a postcode" : "(" + d.Bestuurder.Postcode + ")");
            string resultV =
                "Voertuig: " + (string.IsNullOrWhiteSpace(d.Voertuig.Merk) ? "\nn/a merk" : "\nMerk: " + d.Voertuig.Merk + ",")
                + (string.IsNullOrWhiteSpace(d.Voertuig.Model) ? "\nn/a model" : "\nModel: " + d.Voertuig.Model + ",")
                + (string.IsNullOrWhiteSpace(d.Voertuig.TypeWagen) ? "\nn/a carrosserie" : "\nCarrosserie: " + d.Voertuig.TypeWagen + ",")
                + (string.IsNullOrWhiteSpace(aantalDeuren) ? "\nn/a aantal deuren" : "\nAantal deuren" + d.Voertuig.AantalDeuren + ", ")
                + (string.IsNullOrWhiteSpace(d.Voertuig.Brandstoffen) ? "\nn/a brandstof" : "\nBrandstof" + d.Voertuig.Brandstoffen + ",")
                + (string.IsNullOrWhiteSpace(d.Voertuig.ChassisNummer) ? "\nn/a chassisnummer" : "\nChassisnummer" + d.Voertuig.ChassisNummer + ",")
                + (string.IsNullOrWhiteSpace(d.Voertuig.Kleur) ? "\nn/a kleur" : "\nKleur " + d.Voertuig.Kleur + ",")
                + (string.IsNullOrWhiteSpace(d.Voertuig.Nummerplaat) ? "\nn/a nummerplaat" : "\nNummerplaat: " + d.Voertuig.Nummerplaat + ",");
            string resultT;
            if (d.Voertuig != null)
            {
                result += resultV;
            }

            lbl_Details.Content = result;
        }
    }
}
