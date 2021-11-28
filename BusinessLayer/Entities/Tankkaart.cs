using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.StaticData;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Tankkaart {
        public Tankkaart(int kaartNummer, DateTime geldigheidsDatum, string pincode, string brandstof, int bestuurderId) {
            KaartNummer = kaartNummer;
            GeldigheidsDatum = UpdateGeldigheidsdatum(geldigheidsDatum);

        }

        public int KaartNummer { get; private set; }
        public DateTime GeldigheidsDatum { get; private set; }
        public string Pincode { get; private set; }
        public List<Brandstoffen> Brandstoffen { get; private set; }
        public int bestuurderId { get; set; }
        public DateTime UpdateGeldigheidsdatum(DateTime geldigheid) {
            if (DateTime.Now < geldigheid && GeldigheidsDatum < geldigheid) {
                GeldigheidsDatum = geldigheid;
                return geldigheid;
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
    }
}
