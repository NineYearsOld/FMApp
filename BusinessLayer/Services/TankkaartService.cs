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
        public TankkaartService(ITankkaartRepository repo)
        {
            this.repo = repo;
        }
        public bool ExistsTankkaart(int id)
        {
            if (repo.ExistsTankkaart(id))
            {
                return true;
            }
            else return false;
        }

        public Tankkaart CreateTankkaart(DateTime geldigheidsdatum, string pincode, string brandstof, int? bestuurderId)
        {
            try
            {
                Tankkaart k = new Tankkaart(geldigheidsdatum, pincode, brandstof, bestuurderId);
                repo.CreateTankkaart(k);
                return k;
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

        public void UpdateTankkaart(Tankkaart tankkaart)
        { 
            try
            {
                repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
