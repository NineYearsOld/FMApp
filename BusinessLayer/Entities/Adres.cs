using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class Adres
    {
        public int PostCode { get; set; }
        public string Gemeente { get; set; }
        public string Deelgemeente { get; set; }
        public string Provincie { get; set; }

        public Adres(int postCode, string gemeente, string deelgemeente, string provincie)
        {
            PostCode = postCode;
            Gemeente = gemeente;
            Deelgemeente = deelgemeente;
            Provincie = provincie;
        }
    }
}
