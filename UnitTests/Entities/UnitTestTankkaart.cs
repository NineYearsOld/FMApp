using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using System;
using Xunit;

namespace UnitTests.Entities {
    public class UnitTestTankkaart {
        [Fact]
        public void TestConstructor() {
            int kaartnummer = 123456;
            DateTime geldigheidsdatum = new DateTime(2022, 1, 1);
            string pin = "1234";
            string brandstof = "diesel";
            int bestuurderid = 1;

            Tankkaart t = new Tankkaart(geldigheidsdatum, pin, brandstof, bestuurderid, kaartnummer);

            Assert.Equal(123456, t.KaartNummer);
            Assert.Equal(new DateTime(2022, 1, 1), t.GeldigheidsDatum);
            Assert.Equal("1234", t.Pincode);
            Assert.Equal("diesel", t.Brandstoffen);
            Assert.Equal(1, t.BestuurderId);

            Assert.Throws<TankkaartException>(() => new Tankkaart(new DateTime(2021, 1, 1), pin, brandstof, bestuurderid, kaartnummer)); // geldigheidsdatum is al verlopen
            Assert.Throws<TankkaartException>(() => new Tankkaart(geldigheidsdatum, pin, brandstof, -3, kaartnummer)); // bestuurderid is negatief
            Assert.Throws<TankkaartException>(() => new Tankkaart(geldigheidsdatum, "", brandstof, bestuurderid, kaartnummer)); // Lege pin
            Assert.Throws<TankkaartException>(() => new Tankkaart(geldigheidsdatum, "123", brandstof, bestuurderid, kaartnummer)); // pin te kort
            Assert.Throws<TankkaartException>(() => new Tankkaart(geldigheidsdatum, "12345", brandstof, bestuurderid, kaartnummer)); // pin te lang
            Assert.Throws<TankkaartException>(() => new Tankkaart(geldigheidsdatum, null, brandstof, bestuurderid, kaartnummer)); // pin = null
        }

        [Fact]
        public void TestUpdateGeldigheidsdatum() {
            int kaartnummer = 123456;
            DateTime geldigheidsdatum = new DateTime(2022, 1, 1);
            string pin = "1234";
            string brandstof = "diesel";
            int bestuurderid = 1;

            Tankkaart t = new Tankkaart(geldigheidsdatum,pin,brandstof,bestuurderid, kaartnummer);

            Assert.Throws<TankkaartException>(() => t.UpdateGeldigheidsdatum(geldigheidsdatum));
            Assert.Throws<TankkaartException>(() => t.UpdateGeldigheidsdatum(new DateTime(2021,12,30))) ;
            Assert.Throws<TankkaartException>(() => t.UpdateGeldigheidsdatum(new DateTime()));

            t.UpdateGeldigheidsdatum(new DateTime(2022, 5, 5));
            Assert.Equal(new DateTime(2022, 5, 5),t.GeldigheidsDatum);
        }

        [Fact]
        public void TestUpdatePincode() {
            int kaartnummer = 123456;
            DateTime geldigheidsdatum = new DateTime(2022, 1, 1);
            string pin = "1234";
            string brandstof = "diesel";
            int bestuurderid = 1;

            Tankkaart t = new Tankkaart(geldigheidsdatum, pin, brandstof, bestuurderid, kaartnummer);

            Assert.Throws<TankkaartException>(() => t.UpdatePincode(pin));
            Assert.Throws<TankkaartException>(() => t.UpdatePincode("12"));
            Assert.Throws<TankkaartException>(() => t.UpdatePincode("123456"));
            Assert.Throws<TankkaartException>(() => t.UpdatePincode(null));
            Assert.Throws<TankkaartException>(() => t.UpdatePincode(""));

            t.UpdatePincode("5678");
            Assert.Equal("5678", t.Pincode);
        }

        [Fact]
        public void TestUpdateBrandstoffen() {
            int kaartnummer = 123456;
            DateTime geldigheidsdatum = new DateTime(2022, 1, 1);
            string pin = "1234";
            string brandstof = "diesel";
            int bestuurderid = 1;

            Tankkaart t = new Tankkaart(geldigheidsdatum, pin, brandstof, bestuurderid, kaartnummer);
        }

        [Fact]
        public void TestUpdateBestuurderid() {
            int kaartnummer = 123456;
            DateTime geldigheidsdatum = new DateTime(2022, 1, 1);
            string pin = "1234";
            string brandstof = "diesel";
            int bestuurderid = 1;

            Tankkaart t = new Tankkaart(geldigheidsdatum, pin, brandstof, bestuurderid, kaartnummer);
            /*
            Assert.Throws<TankkaartException>(() => t.UpdateBestuurderid(-3));

            t.UpdateBestuurderid(5);
            Assert.Equal(5, t.BestuurderId);
            */
        }
    }
}
