﻿using BusinessLayer.Entities;
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

        public Tankkaart CreateTankkaart(DateTime geldigheidsdatum, string pincode, string brandstof, int bestuurderId, int? kaartnummer = null)
        {
            try
            {
                Tankkaart k = new Tankkaart(geldigheidsdatum, pincode, brandstof, bestuurderId, kaartnummer);
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

        public void UpdateTankkaart(Tankkaart tankkaart, int kaartnummer)
        { 
            try
            {
                repo.UpdateTankkaart(tankkaart, kaartnummer);
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
