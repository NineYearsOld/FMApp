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
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();
            string kleur = "Zwart";
            int aantaldeuren = 5;
            int bestuurderid = 1;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen.ToString(), typewagen.ToString(), kleur, aantaldeuren, bestuurderid);
            Voertuig v2 = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen.ToString(), typewagen.ToString(), null, 0, 0);

            Assert.Equal("bmw", v.Merk);
            Assert.Equal("360tdi", v2.Model);
            Assert.Equal("ABCDE12345", v.ChassisNummer);
            Assert.Equal("1ABC234", v2.Nummerplaat);
            Assert.Equal(Brandstoffen.Diesel.ToString(), v.Brandstoffen);
            Assert.Equal(WagenTypes.Berline.ToString(), v2.TypeWagen);

            Assert.Equal("Zwart", v.Kleur);
            Assert.Equal(5, v.AantalDeuren);
            Assert.Equal(1, v.BestuurderId);

            Assert.Null(v2.Kleur);
            Assert.Equal(0, v2.AantalDeuren);
            Assert.Equal(0, v2.BestuurderId);

            Assert.Throws<VoertuigException>(() => new Voertuig("", model, chassisnummer, nummerplaat, brandstoffen.ToString(), typewagen.ToString(), kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, null, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, null, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, "", brandstoffen, typewagen, kleur, aantaldeuren, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, -5, bestuurderid));
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, kleur, aantaldeuren, -7));

        }

        [Fact]
        public void TestUpdateChassisnummer() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);

            Assert.Throws<VoertuigException>(() => v.UpdateChassisnummer(""));
            Assert.Throws<VoertuigException>(() => v.UpdateChassisnummer(null));

            v.UpdateChassisnummer("AAAAA11111");
            Assert.Equal("AAAAA11111", v.ChassisNummer);
        }

            [Fact]
        public void TestUpdateNummerplaat() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);
            
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(""));
            Assert.Throws<VoertuigException>(() => v.UpdateNummerplaat(null));

            v.UpdateNummerplaat("1AAA111");
            Assert.Equal("1AAA111", v.Nummerplaat);
        }

        [Fact]
        public void TestUpdateMerk() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);

            Assert.Throws<VoertuigException>(() => v.UpdateMerk(""));
            Assert.Throws<VoertuigException>(() => v.UpdateMerk(null));

            v.UpdateMerk("audi");
            Assert.Equal("audi", v.Merk);
        }

        [Fact]
        public void TestUpdateModel() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);

            Assert.Throws<VoertuigException>(() => v.UpdateModel(""));
            Assert.Throws<VoertuigException>(() => v.UpdateModel(null));

            v.UpdateModel("audi");
            Assert.Equal("audi", v.Model);
        }

        [Fact]
        public void TestUpdateKleur() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, 0);

            v.UpdateKleur("Groen");
            Assert.Equal("Groen", v.Kleur);
        }

        [Fact]
        public void TestUpdateAantalDeuren() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();
            int bestuurderid = 1;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, bestuurderid);
            Assert.Throws<VoertuigException>(() => v.UpdateAantalDeuren(-1));

            v.UpdateAantalDeuren(0);
            Assert.Equal(0, v.AantalDeuren);
            v.UpdateAantalDeuren(null);
            Assert.Null(v.AantalDeuren);
        }

        [Fact]
        public void TestUpdateBestuurderid() {
            string merk = "bmw";
            string model = "360tdi";
            string chassisnummer = "ABCDE12345";
            string nummerplaat = "1ABC234";
            string brandstoffen = Brandstoffen.Diesel.ToString();
            string typewagen = WagenTypes.Berline.ToString();
            int bestuurderid = 1;

            Voertuig v = new Voertuig(merk, model, chassisnummer, nummerplaat, brandstoffen, typewagen, null, 0, bestuurderid);
            Assert.Throws<VoertuigException>(() => v.UpdateBestuurderId(-1));

            v.UpdateBestuurderId(0);
            Assert.Equal(0, v.BestuurderId);
            v.UpdateBestuurderId(null);
            Assert.Null(v.BestuurderId);
        }
    }
}
