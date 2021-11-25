using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TankkaartService
    {
        private ITankkaartRepository repo;

        public void CreateTankkaart(int kaartnummer, DateTime geldigheidsdatum)
        {
            try
            {
                repo.CreateTankkaart(kaartnummer, geldigheidsdatum);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteTankkaart(int kaartnummer)
        {
            try
            {
                repo.DeleteTankkaart(kaartnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateTankkaart(int kaartnummer)
        { 
            try
            {
                repo.UpdateTankkaart(kaartnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Tankkaart ToonDetails(int kaartnummer)
        {
            try
            {
                return repo.ToonDetails(kaartnummer);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
