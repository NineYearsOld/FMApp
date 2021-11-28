using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class VoertuigService
    {
        private IVoertuigRepository repo;

        public void CreateVoertuig(string merk, string model, string chassisNummer, string nummerplaat, Brandstoffen brandstof, WagenTypes typeWagen, string kleur, int aantalDeuren, int bestuurderId)
        {
            try
            {
                Voertuig v = new Voertuig(merk, model, chassisNummer, nummerplaat, brandstof, typeWagen, kleur, aantalDeuren, bestuurderId);
                repo.CreateVoertuig(v);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteVoertuig(string chassisnummer)
        {
            try
            {
                repo.DeleteVoertuig(chassisnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateVoertuig(string chassisnummer)
        {
            try
            {
                repo.UpdateVoertuig(chassisnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Voertuig ToonDetails(string chassisnummer)
        {
            try
            {
                return repo.ToonDetails(chassisnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
