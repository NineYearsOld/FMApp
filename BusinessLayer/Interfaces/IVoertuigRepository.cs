using BusinessLayer.Entities;
using BusinessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IVoertuigRepository
    {
        void CreateVoertuig(string merk, string model, string chassisNummer, string nummerplaat, Brandstoffen brandstof, WagenTypes typeWagen);
        void DeleteVoertuig(string chassisnummer);
        void UpdateVoertuig(string chassisnummer);
        Voertuig ToonDetails(string chassisnummer);
    }
}
