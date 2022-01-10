using BusinessLayer.Entities;
using BusinessLayer.StaticData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IVoertuigRepository
    {
        bool ExistsVoertuig(string id);
        void CreateVoertuig(Voertuig voertuig);
        void DeleteVoertuig(string chassisnummer);
        void UpdateVoertuig(Voertuig voertuig, string chassisnummer);
        Voertuig ToonDetails(string chassisnummer);
        ObservableCollection<Voertuig> GetVoertuigen(string merk, string model, string nummerplaat);
    }
}
