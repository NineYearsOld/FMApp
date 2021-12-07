using BusinessLayer.Entities;
using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            static BestuurderService BestuurderService()
            {
                string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True;TrustServerCertificate=True";
                BestuurderRepository br = new BestuurderRepository(connectionString);
                BestuurderService bs = new BestuurderService(br);
                return bs;
            }
            Console.WriteLine("Input naam");
            string naam = Console.ReadLine();
            Console.WriteLine("Input voornaam");
            string voornaam = Console.ReadLine();

            List<Bestuurder> bss = BestuurderService().FetchBestuurders(naam, voornaam);
            foreach (Bestuurder bs in bss)
            {
                Console.WriteLine(bs.Id);
            }
        }
    }
}
