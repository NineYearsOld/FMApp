using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Utilities;

namespace BusinessLayer.Services {
    public class BestuurderService {
        private IBestuurderRepository repo;

        public BestuurderService(IBestuurderRepository repo) {
            this.repo = repo;
        }

        public Bestuurder CreateBestuurder(string naam, string voornaam, DateTime geboorteDatum, string rijbewijs, string rijksregisternr, int? postcode = null, string gemeente = null, string straat = null, string huisNummer = null) {
            try {
                if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam) && !string.IsNullOrWhiteSpace(rijbewijs) && Controls.ValidatieRijkregisternummer(rijksregisternr)) {

                    Bestuurder b = new Bestuurder(naam, voornaam, geboorteDatum, rijksregisternr, rijbewijs, postcode, gemeente, straat, huisNummer);
                    // DB Create
                    // Id uit db invullen
                    repo.CreateBestuurder(b);
                    return b;

                } else {
                    throw new BestuurderException("blabla.");
                }
                return null;
            } catch (Exception) {

                throw;
            }
        }

        public void DeleteBestuurder(int id) {
            try {
                repo.DeleteBestuurder(id);
            } catch (Exception) {

                throw;
            }
        }

        public Bestuurder UpdateBestuurder(Bestuurder bestuurder) {
            try {
                repo.UpdateBestuurder(bestuurder);
                return bestuurder;
            } catch (Exception) {

                throw;
            }
        }

        public Bestuurder ToonDetails(int id) {
            try {
                return repo.ToonDetails(id);
            } catch (Exception) {

                throw;
            }
        }
    }
}
