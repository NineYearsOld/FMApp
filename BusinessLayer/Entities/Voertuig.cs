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
            UpdateMerk(merk);
            UpdateModel(model);
            UpdateChassisnummer(chassisNummer);
            UpdateNummerplaat(nummerplaat);
            UpdateBrandstoffen(brandstoffen);
            UpdateTypeWagen(typeWagen);
            UpdateKleur(kleur);
            UpdateAantalDeuren(aantalDeuren);
            UpdateBestuurderId(bestuurdeId);
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
            if (!string.IsNullOrWhiteSpace(merk) || merk == null) {
                Merk = merk;
            } else {
                throw new VoertuigException("Merk is verplicht.");
            }
        }

        public void UpdateModel(string model) {
            if (!string.IsNullOrWhiteSpace(model) || model == null) {
                Model = model;
            } else {
                throw new VoertuigException("Model is verplicht.");
            }
        }

        private void UpdateChassisnummer(string chassisnr) {
            if (!string.IsNullOrWhiteSpace(chassisnr) || chassisnr == null) {
                ChassisNummer = chassisnr;
            } else {
                throw new VoertuigException("Chassisnummer is verplicht.");
            }
        }

        public void UpdateNummerplaat(string nummerplaat) {
            if (!string.IsNullOrWhiteSpace(nummerplaat) || nummerplaat == null) {
                Nummerplaat = nummerplaat;
            } else {
                throw new VoertuigException("Nummerplaat is verplicht.");
            }
        }

        public void UpdateBrandstoffen(string brandstoffen) {
            if (!string.IsNullOrWhiteSpace(brandstoffen) || brandstoffen == null) {
                Brandstoffen = brandstoffen;
            } else {
                throw new VoertuigException("Brandstof is verplicht.");
            }
        }

        public void UpdateTypeWagen(string typewagen) {
            if (!string.IsNullOrWhiteSpace(typewagen) || typewagen == null) {
                TypeWagen = typewagen;
            } else {
                throw new VoertuigException("Type wagen is verplicht.");
            }
        }

        public void UpdateKleur(string kleur) {
            Kleur = kleur;
        }

        public void UpdateAantalDeuren(int? deuren) {
            if (deuren > -1 || deuren == null) {
                AantalDeuren = deuren;
            } else {
                throw new VoertuigException("Aantal deuren kan niet negatief zijn.");
            }
        }

        public void UpdateBestuurderId(int? bid) {
            if (bid > -1 || bid == null) {
                BestuurderId = bid;
            } else {
                throw new VoertuigException("Bestuurderid kan niet negatief zijn.");
            }
        }

        public void UpdateBestuurder(Bestuurder bestuurder) {
            Bestuurder = bestuurder;
        }

        public override string ToString() {
            return $"{Merk} {Model} {Nummerplaat}";
        }
    }
}
