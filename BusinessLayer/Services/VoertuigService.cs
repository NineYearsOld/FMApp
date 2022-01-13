using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.StaticData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services {
    public class VoertuigService {
        private IVoertuigRepository repo;
        public VoertuigService(IVoertuigRepository repo) {
            this.repo = repo;
        }

        public bool ExistsVoertuig(string id) {
            if (repo.ExistsVoertuig(id)) {
                return true;
            } else return false;
        }

        public Voertuig CreateVoertuig(string merk, string model, string chassisNummer, string nummerplaat, string brandstof, string typeWagen, string kleur, int? aantalDeuren, int? bestuurderId) {
            try {
                if (!string.IsNullOrWhiteSpace(merk) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(chassisNummer) && !string.IsNullOrWhiteSpace(nummerplaat) && !string.IsNullOrWhiteSpace(brandstof) && !string.IsNullOrWhiteSpace(typeWagen)) {
                    Voertuig v = new Voertuig(merk, model, chassisNummer, nummerplaat, brandstof, typeWagen, (kleur != null ? kleur : ""), (aantalDeuren!=null? aantalDeuren:0), (bestuurderId != null ? bestuurderId : null));
                    repo.CreateVoertuig(v);
                    return v;
                } else {
                    throw new Exception();
                }
            } catch (Exception) {
                throw;
            }
        }

        public void DeleteVoertuig(string chassisnummer) {
            try {
                repo.DeleteVoertuig(chassisnummer);
            } catch (Exception) {
                throw;
            }
        }

        public void UpdateVoertuig(Voertuig voertuig, string chassisnummer) {
            try {
                repo.UpdateVoertuig(voertuig, chassisnummer);
            } catch (Exception) {
                throw;
            }
        }

        public ObservableCollection<Voertuig> GetVoertuigen(string merk, string model, string nummerplaat) {
            try {
                return repo.GetVoertuigen(merk, model, nummerplaat);
            } catch (Exception) {
                throw;
            }
        }
    }
}
