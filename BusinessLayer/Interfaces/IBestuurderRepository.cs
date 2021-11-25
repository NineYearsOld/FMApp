using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBestuurderRepository
    {
        void CreateBestuurder(Bestuurder bestuurder);
        void DeleteBestuurder(int id);
        void UpdateBestuurder(Bestuurder bestuurder);
        Bestuurder ToonDetails(int id);
    }
}
