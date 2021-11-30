using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.StaticData;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Voertuig {
        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, string brandstoffen, string typeWagen, string kleur, int aantalDeuren, int bestuurdeId)
        {
            Merk = merk;
            Model = model;
            ChassisNummer = chassisNummer;
            Nummerplaat = nummerplaat;
            Brandstoffen = brandstoffen;
            TypeWagen = typeWagen;           
        }

        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; set; }
        public string Nummerplaat { get; private set; }
        public string Brandstoffen { get; private set; }
        public string TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public int BestuurderId { get; set; }

        public void UpdateMerk(string merk) {
            if (!string.IsNullOrWhiteSpace(merk) && Merk != merk)
            {
                Merk = merk;
            }
            else
            {
                throw new BestuurderException("Merk is verplicht en moet verschillen met de huidige.");
            }
        }
    }
}
