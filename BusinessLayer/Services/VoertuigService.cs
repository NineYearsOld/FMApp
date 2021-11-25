using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
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
