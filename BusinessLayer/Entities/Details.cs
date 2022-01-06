using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class Details
    {
        public Details(Bestuurder bestuurder, Voertuig voertuig, Tankkaart tankkaart)
        {
            Bestuurder = bestuurder;
            Voertuig = voertuig;
            Tankkaart = tankkaart;
        }

        public Bestuurder Bestuurder { get; set; }
        public Voertuig Voertuig { get; set; }
        public Tankkaart Tankkaart { get; set; }
    }
}
