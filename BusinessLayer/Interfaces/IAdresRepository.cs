using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAdresRepository
    {
        Adres GetSuggestedAdresWithPostCode(int postcode);
        List<Adres> GetSuggestedAdresWithGemeente(string gemeente);
        Adres GetSuggestedAdresWithDeelGemeente(string deelgemeente);
        string GetSuggestedProvince(string province);

    }
}
