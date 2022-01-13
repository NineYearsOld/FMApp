using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.utils
{
    public class Connection
    {
        private static string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True; TrustServerCertificate=True;";

        public static BestuurderService Bestuurder()
        {
            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);
            return bs;
        }
        public static TankkaartService Tankkaart()
        {
            TankkaartRepository br = new TankkaartRepository(connectionString);
            TankkaartService bs = new TankkaartService(br);
            return bs;
        }
        public static VoertuigService Voertuig()
        {
            VoertuigRepository br = new VoertuigRepository(connectionString);
            VoertuigService bs = new VoertuigService(br);
            return bs;
        }
    }
}
