using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Enums;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Tankkaart {
        public Tankkaart(int kaartNummer, DateTime geldigheidsDatum) {
            KaartNummer = kaartNummer;
            GeldigheidsDatum = geldigheidsDatum;
        }

        public int KaartNummer { get; private set; }
        public DateTime GeldigheidsDatum { get; private set; }
        public string Pincode { get; private set; }
        public List<Brandstoffen> Brandstoffen { get; private set; }
        public Bestuurder Bestuurder { get; private set; }

        public void UpdateGeldigheidsdatum(DateTime geldigheid) {
            if (DateTime.Now < geldigheid && GeldigheidsDatum < geldigheid) {
                GeldigheidsDatum = geldigheid;
            } else {
                throw new TankkaartException(""); // Message
            }
        }

        public void UpdatePincode(string pin) {
            if (int.TryParse(pin, out int p) && pin.Length == 4) {
                Pincode = pin;
            } else {
                throw new TankkaartException(""); // Message
            }
        }

        public void UpdateBrandstoffen(List<Brandstoffen> brandstoffen) {
            if (brandstoffen.Count != 0) {

            } else {
                throw new TankkaartException(""); // Message
            }
        }

        public void UpdateBestuurder(Bestuurder bestuurder) {
            if (Bestuurder != bestuurder) {
                Bestuurder = bestuurder;
                try {
                    if (Bestuurder != null) {
                        Bestuurder.UpdateTankkaart(this);
                    } else {
                        Bestuurder.UpdateTankkaart(null);
                    }
                } catch {

                }
            } else {
                throw new TankkaartException(""); // Message
            }
        }
    }
}
