using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.StaticData;
using System;
using Xunit;

namespace UnitTests.Entities {
    public class UnitTestVoertuig {
        [Fact]
        public void TestConstructor() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            Brandstoffen brandstoffen = Brandstoffen.Diesel;
            WagenTypes typewagen = WagenTypes.Berline;
            string kleur = "Zwart";
            int aantaldeuren = 5;
            int bestuurderid = 1;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid);
            Voertuig v2 = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);

            Assert.Equal("bmw", v.Merk);
            Assert.Equal("360tdi", v2.Model);
            Assert.Equal("ABCDE12345", v.ChassisNummer);
            Assert.Equal("1ABC234", v2.Nummerplaat);
            Assert.Equal(Brandstoffen.Diesel, v.Brandstoffen);
            Assert.Equal(WagenTypes.Berline, v2.TypeWagen);

            Assert.Equal("Zwart", v.Kleur);
            Assert.Equal(5, v.AantalDeuren);
            Assert.Equal(1, v.BestuurderId);

            Assert.Null(v2.Kleur);
            Assert.Equal(0, v2.AantalDeuren);
            Assert.Equal(0, v2.BestuurderId);

            Assert.Throws<VoertuigException>(() => new Voertuig("", model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, null, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, "1", nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, "", brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, -5, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, -7));

        }

        [Fact]
        public void TestUpdateNummerplaat() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            Brandstoffen brandstoffen = Brandstoffen.Diesel;
            WagenTypes typewagen = WagenTypes.Berline;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);
            /*
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(nummerplaat));
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(""));
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(null));

            v.UpdateNummerplaat("1AAA111");
            Assert.Equal("1AAA111", v.Nummerplaat);
            */
            
        }

        [Fact]
        public void TestUpdateKleur() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            Brandstoffen brandstoffen = Brandstoffen.Diesel;
            WagenTypes typewagen = WagenTypes.Berline;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);
            /*
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(kleur));

            v.UpdateKleur("Groen");
            Assert.Equal("Groen", v.Kleur);
            */
        }

        [Fact]
        public void TestUpdateBestuurderid() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            Brandstoffen brandstoffen = Brandstoffen.Diesel;
            WagenTypes typewagen = WagenTypes.Berline;
            int bestuurderid = 1;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, bestuurderid);
            /*
            Assert.Throws<VoertuigException>(() => v.UpdateBestuurderid(bestuurderid));

            v.UpdateBestuurderid(0);
            Assert.Equal(0, v.Bestuurderid);
            v.UpdateBestuurderid(5);
            Assert.Equal(5, v.Bestuurderid);
            */

        }
    }
}
