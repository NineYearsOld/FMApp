using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITankkaartRepository
    {
        void CreateTankkaart(Tankkaart tankkaart);

        void DeleteTankkaart(int kaartnummer);

        void UpdateTankkaart(int kaartnummer);

        Tankkaart ToonDetails(int kaartnummer);
    }
}
