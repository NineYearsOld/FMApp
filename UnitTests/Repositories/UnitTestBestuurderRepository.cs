using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using BusinessLayer.Entities;

namespace UnitTests.Repositories {
    public class UnitTestBestuurderRepository {
        public string conn = "Data Source=LAPTOP-TFOBODKP\\SQLEXPRESS;Initial Catalog=FleetManagementApp;Integrated Security=True";

        [Fact]
        public void TestGetConnection() {
            var br = new BestuurderRepository(conn);
            var c = br.getConnection();

            Assert.IsType<SqlConnection>(c);
            Assert.Equal(conn, c.ConnectionString);
        }

        [Fact]
        public void TestExistsBestuurder() {
            var br = new BestuurderRepository(conn);
            Assert.False(br.ExistsBestuurder(0));
        }

        [Fact]
        public void TestCreateBestuurder() {
            var b = new Bestuurder("Bezos", "Jef", new DateTime(1965, 9, 15), "12345678958", "B", null, null, null);
            var br = new BestuurderRepository(conn);
            br.CreateBestuurder(b);

        }

        [Fact]
        public void TestDeleteBestuurder() {
            var b = new Bestuurder("Bezos", "Jef", new DateTime(1965, 9, 15), "12345678958", "B", null, null, null);
            var br = new BestuurderRepository(conn);
            br.CreateBestuurder(b);
            br.DeleteBestuurder(0);

            Assert.False(br.ExistsBestuurder(0));
        }

        [Fact]
        public void TestUpdateBestuurder() {
            var b = new Bestuurder("Bezos", "Jef", new DateTime(1965, 9, 15), "12345678958", "B", null, null, null);
            var br = new BestuurderRepository(conn);
            br.CreateBestuurder(b);
            br.UpdateBestuurder(b, 0);
        }

        [Fact]
        public void TestToonDetails() {
            var b1 = new Bestuurder("Bezos", "Jef", new DateTime(1965, 9, 15), "12345678958", "B", null, null, null);
            var b2 = new Bestuurder("Baazos", "Jos", new DateTime(2000, 3, 1), "98765432167", "A", "Gent", "Kerkstraat", "14a", 9000);
            var br = new BestuurderRepository(conn);
            br.CreateBestuurder(b1);
            br.CreateBestuurder(b2);
            var result1 = br.ToonDetails(0);
            var result2 = br.ToonDetails(0);
            /*
            Assert.Equal("Bezos", result1.Naam);
            Assert.Equal("Jef", result1.Voornaam);
            Assert.Equal(new DateTime(1965, 9, 15), result1.GeboorteDatum);
            Assert.Equal("12345678958", result1.RijksregisterNummer);
            Assert.Equal("B", result1.Rijbewijs);

            Assert.Null(result1.Gemeente);
            Assert.Null(result1.Straat);
            Assert.Null(result1.Huisnummer);
            Assert.Null(result1.Postcode);

            Assert.Equal("Baazos", result2.Naam);
            Assert.Equal("Jos", result2.Voornaam);
            Assert.Equal(new DateTime(2000, 3, 1), result2.GeboorteDatum);
            Assert.Equal("98765432167", result2.RijksregisterNummer);
            Assert.Equal("A", result2.Rijbewijs);

            Assert.Equal("Gent", result2.Gemeente);
            Assert.Equal("Kerkstraat", result2.Straat);
            Assert.Equal("14a", result2.Huisnummer);
            Assert.Equal(9000, result2.Postcode);
            */
        }
    }
}
