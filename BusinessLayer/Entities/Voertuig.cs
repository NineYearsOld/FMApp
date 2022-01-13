using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.StaticData;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Voertuig {
        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, string brandstoffen, string typeWagen, string kleur, int? aantalDeuren = null, int? bestuurdeId = null) {
            Merk = merk;
            Model = model;
            ChassisNummer = chassisNummer;
            Nummerplaat = nummerplaat;
            Brandstoffen = brandstoffen;
            TypeWagen = typeWagen;
            Kleur = kleur;
            AantalDeuren = aantalDeuren;
            BestuurderId = bestuurdeId;
        }

        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; set; }
        public string Nummerplaat { get; private set; }
        public string Brandstoffen { get; private set; }
        public string TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int? AantalDeuren { get; private set; }
        public int? BestuurderId { get; set; }
        public Bestuurder Bestuurder { get; set; }

        public void UpdateMerk(string merk) {
            if (!string.IsNullOrWhiteSpace(merk)) {
                Merk = merk;
            } else {
                throw new BestuurderException("Merk is verplicht.");
            }
        }

        public void UpdateModel(string model) {
            if (!string.IsNullOrWhiteSpace(model)) {
                Model = model;
            } else {
                throw new BestuurderException("Model is verplicht.");
            }
        }

        public void UpdateChassisnummer(string chassisnr) {
            if (!string.IsNullOrWhiteSpace(chassisnr)) {
                ChassisNummer = chassisnr;
            } else {
                throw new BestuurderException("Chassisnummer is verplicht.");
            }
        }

        public void UpdateNummerplaat(string nummerplaat) {
            if (!string.IsNullOrWhiteSpace(nummerplaat)) {
                Nummerplaat = nummerplaat;
            } else {
                throw new BestuurderException("Nummerplaat is verplicht.");
            }
        }

        public void UpdateBrandstoffen(string brandstoffen) {
            if (!string.IsNullOrWhiteSpace(brandstoffen)) {
                Brandstoffen = brandstoffen;
            } else {
                throw new BestuurderException("Brandstof is verplicht.");
            }
        }

        public void UpdateTypeWagen(string typewagen) {
            if (!string.IsNullOrWhiteSpace(typewagen)) {
                TypeWagen = typewagen;
            } else {
                throw new BestuurderException("Type wagen is verplicht.");
            }
        }

        public void UpdateKleur(string kleur) {
            Kleur = kleur;
        }

        public void UpdateAantalDeuren(int? deuren) {
            AantalDeuren = deuren;
        }

        public void UpdateBestuurderId(int? bid) {
            BestuurderId = bid;
        }

        public void UpdateBestuurder(Bestuurder bestuurder) {
            Bestuurder = bestuurder;
        }

        public override string ToString() {
            return $"{Merk} {Model} {Nummerplaat}";
        }
    }
}
