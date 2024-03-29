﻿using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITankkaartRepository
    {
        bool ExistsTankkaart(int id);
        void CreateTankkaart(Tankkaart tankkaart);

        void DeleteTankkaart(int kaartnummer);

        void UpdateTankkaart(Tankkaart tankkaart, int kaartnummer);

        Tankkaart ToonDetails(int kaartnummer);
    }
}
