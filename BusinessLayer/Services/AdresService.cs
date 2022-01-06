using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdresService
    {
        private IAdresRepository repo;
        public AdresService(IAdresRepository repo)
        {
            this.repo = repo;
        }
    }
}
