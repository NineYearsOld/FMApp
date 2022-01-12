using BusinessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Repositories {
    public class UnitTestTankkaartRepository {
        public string conn = "Data Source=LAPTOP-TFOBODKP\\SQLEXPRESS;Initial Catalog=FleetManagementApp;Integrated Security=True";

        [Fact]
        public void TestGetConnection() {
            var tr = new TankkaartRepository(conn);
            var c = tr.getConnection();

            Assert.IsType<SqlConnection>(c);
            Assert.Equal(conn, c.ConnectionString);
        }
        /*
        [Fact]
        public void TestBestaatTankkaart() {
            var tr = new TankkaartRepository(conn);
            var t1 = new Tankkaart(new DateTime(2022,1,1),"1234","Diesel",0, 1);
            tr.CreateTankkaart(t1);

            Assert.True(tr.BestaatTankkaart(1));
            Assert.False(tr.BestaatTankkaart(0));
        }

        [Fact]
        public void TestCreateTankkaart() {
            var tr = new TankkaartRepository(conn);
            var t1 = new Tankkaart(1, new DateTime(2022, 1, 1), "1234", "Diesel", 0);
            tr.CreateTankkaart(t1);
        }

        [Fact]
        public void TestDeleteTankkaart() {
            var tr = new TankkaartRepository(conn);
            var t1 = new Tankkaart(new DateTime(2022, 1, 1), "1234", "Diesel", 0, 1);
            tr.CreateTankkaart(t1);
            tr.DeleteTankkaart(1);
            Assert.False(tr.BestaatTankkaart(1));
        }
        
        [Fact]
        public void TestUpdateTankkaart() {
            var tr = new TankkaartRepository(conn);
            var t1 = new Tankkaart(new DateTime(2022, 1, 1), "1234", "Diesel", 0, 1);
            tr.CreateTankkaart(t1);

        }

        [Fact]
        public void TestToonDetails() {
            var tr = new TankkaartRepository(conn);
            var t1 = new Tankkaart(new DateTime(2022, 1, 1), "1234", "Diesel", 0, 1);
            tr.CreateTankkaart(t1);
            var result1 = tr.ToonDetails(1);

            Assert.Equal(1, result1.KaartNummer);
            Assert.Equal(new DateTime(2022, 1, 1), result1.GeldigheidsDatum);
            Assert.Equal("1234", result1.Pincode);
            Assert.Equal("Diesel", result1.Brandstoffen);
            Assert.Equal(0, result1.BestuurderId);
        }
        */
    }
}
