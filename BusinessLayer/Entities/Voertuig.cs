using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.StaticData;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Voertuig {
        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, Brandstoffen brandstoffen, WagenTypes typeWagen)
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
        public string ChassisNummer { get; private set; }
        public string Nummerplaat { get; private set; }
        public Brandstoffen Brandstoffen { get; private set; }
        public WagenTypes TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }

        public void UpdateBestuurder(Bestuurder bestuurder) {
            if (Bestuurder != bestuurder) {
                Bestuurder = bestuurder;
                try {
                    if (Bestuurder != null) {
                        Bestuurder.UpdateVoertuig(this);
                    } else {
                        Bestuurder.UpdateVoertuig(null);
                    }
                } catch {

                }
            } else {
                throw new VoertuigException(""); // Message
            }
        }
    }
}
