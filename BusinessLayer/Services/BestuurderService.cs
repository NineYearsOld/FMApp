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

        public Bestuurder CreateBestuurder(string naam, string voornaam, DateTime geboorteDatum, string rijbewijs, string rijksregisternr, string gemeente, string straat, string huisNummer, int? postcode = null) {
            try {
                if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam) && !string.IsNullOrWhiteSpace(rijbewijs)) {

                    Bestuurder b = new Bestuurder(naam, voornaam, geboorteDatum, rijksregisternr, rijbewijs, gemeente, straat, huisNummer, postcode);
                    // DB Create
                    // Id uit db invullen
                    repo.CreateBestuurder(b);
                    return b;

                } else {
                    throw new BestuurderException("blabla.");
                }
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

        public Bestuurder UpdateBestuurder(Bestuurder bestuurder, int id) {
            try {
                repo.UpdateBestuurder(bestuurder, id);
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
