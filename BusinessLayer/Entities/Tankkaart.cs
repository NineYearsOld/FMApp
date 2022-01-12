using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.StaticData;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities {
    public class Tankkaart {
        public Tankkaart(DateTime? geldigheidsDatum, string pincode, string brandstoffen, int? bestuurderId = null) {
            GeldigheidsDatum = UpdateGeldigheidsdatum(geldigheidsDatum);
            Pincode = pincode;
            Brandstoffen = brandstoffen;
            BestuurderId = bestuurderId;
        }

        public int? KaartNummer { get; set; }
        public DateTime? GeldigheidsDatum { get; private set; }
        public string Pincode { get; private set; }
        public string Brandstoffen { get; set; }
        public int? BestuurderId { get; set; }
        public DateTime? UpdateGeldigheidsdatum(DateTime? geldigheid) {
            if (DateTime.Now < geldigheid || geldigheid == null) {
                GeldigheidsDatum = geldigheid;
                return geldigheid;
            }
            else {
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
