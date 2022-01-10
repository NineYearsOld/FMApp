using BusinessLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBestuurderRepository
    {
        bool ExistsBestuurder(int id, string rijksreg);
        void CreateBestuurder(Bestuurder bestuurder);
        void DeleteBestuurder(int id);
        void UpdateBestuurder(Bestuurder bestuurder, int id);
        ObservableCollection<Bestuurder> FetchBestuurders(string naam, string voornaam, string geboortedatum);
    }
}
