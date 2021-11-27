using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Utilities;

namespace BusinessLayer.Entities {
    public class Bestuurder {
        public Bestuurder(string naam, string voornaam, DateTime geboorteDatum, string rijksregisternummer, string rijbewijs, string gemeente, string straat, string huisnummer, int? postcode = null)
        {
            UpdateNaam(naam);
            UpdateVoornaam(voornaam);
            UpdatePostcode(postcode);
            UpdateGemeente(gemeente);
            UpdateStraat(straat);
            UpdateHuisnummer(huisnummer);
            GeboorteDatum = geboorteDatum;
            UpdateRijksregisternummer(rijksregisternummer);
            UpdateRijbewijs(rijbewijs);
        }

        public int Id { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        public int? Postcode { get; private set; }
        public string Gemeente { get; private set; }
        public string Straat { get; private set; }
        public string Huisnummer { get; private set; }
        public DateTime GeboorteDatum { get; private set; }
        public string RijksregisterNummer { get; private set; }
        public string Rijbewijs { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public Tankkaart Tankkaart { get; private set; }


        // Update functies hier om private setters te houden
        public void UpdateNaam(string naam) {
            if (!string.IsNullOrWhiteSpace(naam) && Naam != naam) {
                Naam = naam;
            } else {
                throw new BestuurderException("Naam is verplicht en moet verschillen met de huidige.");
            }
        }

        public void UpdateVoornaam(string voornaam) {
            if (!string.IsNullOrWhiteSpace(voornaam) && Voornaam != voornaam) {
                Voornaam = voornaam;
            } else {
                throw new BestuurderException("Voornaam is verplicht en moet verschillen met de huidige.");
            }
        }

        public void UpdatePostcode(int? postcode) {
            if (Postcode != postcode) {
                Postcode = postcode;
            }
            else if (postcode == null)
            {
                Postcode = null;
            }
            else {
                throw new BestuurderException("blablablabkakak"); // Message nog in te vullen
            }
        }

        public void UpdateGemeente(string gemeente) {
            if (Gemeente != gemeente) {
                Gemeente = gemeente;
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }

        public void UpdateStraat(string straat) {
            if (Straat != straat) {
                Straat = straat;
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }

        public void UpdateHuisnummer(string huisnummer) {
            if (Huisnummer != huisnummer) {
                Huisnummer = huisnummer;
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }

        public void UpdateRijksregisternummer(string rijksregisternummer) {
            if (!string.IsNullOrWhiteSpace(rijksregisternummer) && RijksregisterNummer != rijksregisternummer /*&& Controls.ValidatieRijkregisternummer(rijksregisternummer)*/) {
                RijksregisterNummer = rijksregisternummer;
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }

        public void UpdateRijbewijs(string rijbewijs) {
            if (rijbewijs != null) {
                Rijbewijs = rijbewijs;
            } else {
                throw new BestuurderException("Een rijbewijs is verplicht.");
            }
        }

        public void UpdateVoertuig(Voertuig voertuig) {
            if (Voertuig != voertuig) {
                Voertuig = voertuig;
                try {
                    if (Voertuig != null) {
                        Voertuig.UpdateBestuurder(this);
                    } else {
                        Voertuig.UpdateBestuurder(null);
                    }
                } catch {

                }
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }

        public void UpdateTankkaart(Tankkaart tankkaart) {
            if (Tankkaart != tankkaart) {
                Tankkaart = tankkaart;
                try {
                    if (Tankkaart != null) {
                        Tankkaart.UpdateBestuurder(this);
                    } else {
                        Tankkaart.UpdateBestuurder(null);
                    }
                } catch {
                    
                }
            } else {
                throw new BestuurderException(""); // Message nog in te vullen
            }
        }
    }
}
