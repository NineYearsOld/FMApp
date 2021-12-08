using BusinessLayer.Entities;
using BusinessLayer.StaticData;
using DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Repositories {
    public class UnitTestVoertuigRepository {
        public string conn = "Data Source=LAPTOP-TFOBODKP\\SQLEXPRESS;Initial Catalog=FleetManagementApp;Integrated Security=True";

        [Fact]
        public void TestGetConnection() {
            var vr = new VoertuigRepository(conn);
            var c = vr.getConnection();

            Assert.IsType<SqlConnection>(c);
            Assert.Equal(conn, c.ConnectionString);
        }

        [Fact]
        public void TestBestaatVoertuig() {
            var vr = new VoertuigRepository(conn);
            var v1 = new Voertuig("bmw","360tdi","ABCDE12345","1ABC234",Brandstoffen.Diesel,WagenTypes.Berline,"Zwart",5,0);

            Assert.True(vr.BestaatVoertuig("ABCDE12345"));
            Assert.False(vr.BestaatVoertuig(""));
            Assert.False(vr.BestaatVoertuig("AAAAAAAAAA"));
        }

        [Fact]
        public void TestCreateVoertuig() {
            var vr = new VoertuigRepository(conn);
            var v1 = new Voertuig("bmw", "360tdi", "ABCDE12345", "1ABC234", Brandstoffen.Diesel, WagenTypes.Berline, "Zwart", 5, 0);
            vr.CreateVoertuig(v1);

            Assert.True(vr.BestaatVoertuig("ABCDE12345"));
        }

        [Fact]
        public void TestDeleteVoertuig() {
            var vr = new VoertuigRepository(conn);
            var v1 = new Voertuig("bmw", "360tdi", "ABCDE12345", "1ABC234", Brandstoffen.Diesel, WagenTypes.Berline, "Zwart", 5, 0);
            vr.CreateVoertuig(v1);
            vr.DeleteVoertuig("ABCDE12345");

            Assert.False(vr.BestaatVoertuig("ABCDE12345"));
        }

        [Fact]
        public void TestUpdateVoertuig() {
            var vr = new VoertuigRepository(conn);
            var v1 = new Voertuig("bmw", "360tdi", "ABCDE12345", "1ABC234", Brandstoffen.Diesel, WagenTypes.Berline, "Zwart", 5, 0);
            vr.CreateVoertuig(v1);
            vr.UpdateVoertuig("ABCDE12345");
        }

        [Fact]
        public void TestToonDetails() {
            var vr = new VoertuigRepository(conn);
            var v1 = new Voertuig("bmw", "360tdi", "ABCDE12345", "1ABC234", Brandstoffen.Diesel, WagenTypes.Berline, "Zwart", 5, 0);
            vr.CreateVoertuig(v1);
            var result1 = vr.ToonDetails("ABCDE12345");

            Assert.Equal("bwm", result1.Merk);
            Assert.Equal("360tdi", result1.Model);
            Assert.Equal("ABCDE12345", result1.ChassisNummer);
            Assert.Equal("1ABC234", result1.Nummerplaat);
            Assert.Equal(Brandstoffen.Diesel, result1.Brandstoffen);
            Assert.Equal(WagenTypes.Berline, result1.TypeWagen);
            Assert.Equal("Zwart", result1.Kleur);
            Assert.Equal(5, result1.AantalDeuren);
            Assert.Equal(0, result1.BestuurderId);
        }
    }
}
