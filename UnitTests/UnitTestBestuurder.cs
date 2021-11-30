using BusinessLayer.Exceptions;
using BusinessLayer.Entities;
using BusinessLayer.Services;
using BusinessLayer.Utilities;
using System;
using System.Collections.Generic;
using Xunit;
using BusinessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace UnitTests {
    public class UnitTestBestuurder {/*
        [Fact]
        public void TestValidatieRijksregister() {
            string rijksregister = "12345678912";
            string rijksregister2 = "12345678958";

            Assert.False(Controls.ValidatieRijkregisternummer(rijksregister));
            Assert.True(Controls.ValidatieRijkregisternummer(rijksregister2));

        }

        [Fact]
        public void TestCreateBestuurder() {
            string naam = "Bezos";
            string naam2 = "";
            string voornaam = "Jef";
            string voornaam2 = null;
            DateTime geboortedatum = new DateTime(1990,8,24);
            string rijksregister = "12345678958";
            List<string> rijbewijs = new List<string>();
            // string rijbewijs = "";
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True";

            BestuurderRepository br = new BestuurderRepository(connectionString);
            BestuurderService bs = new BestuurderService(br);

            // lege list rijbewijs
            bs.CreateBestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs);
            Assert.Empty(bc.BestuurdersLijst);

            // lege naam
            rijbewijs.Add("B");
            bc.CreateBestuurder(naam2, voornaam, geboortedatum, rijksregister, rijbewijs);
            Assert.Empty(bc.BestuurdersLijst);

            // voornaam = null
            bc.CreateBestuurder(naam, voornaam2, geboortedatum, rijksregister, rijbewijs);
            Assert.Empty(bc.BestuurdersLijst);

            // correcte invoer
            bc.CreateBestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs);
            Assert.Single(bc.BestuurdersLijst);
        }

        [Fact]
        public void TestUpdateTankkaart() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            List<string> rijbewijs = new List<string>();
            rijbewijs.Add("B");

        }
        */

        [Fact]
        public void TestUpdateNaam() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);

            Assert.Throws<BestuurderException>(() => b.UpdateNaam(naam));
            naam = "";
            Assert.Throws<BestuurderException>(() => b.UpdateNaam(naam));
            Assert.Throws<BestuurderException>(() => b.UpdateNaam(null));
            naam = "Boll";
            b.UpdateNaam(naam);
            Assert.Equal("Boll", b.Naam);
        }

        [Fact]
        public void TestUpdateVoornaam() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);

            Assert.Throws<BestuurderException>(() => b.UpdateVoornaam(voornaam));
            voornaam = "";
            Assert.Throws<BestuurderException>(() => b.UpdateVoornaam(voornaam));
            Assert.Throws<BestuurderException>(() => b.UpdateVoornaam(null));
            voornaam = "Jos";
            b.UpdateVoornaam(voornaam);
            Assert.Equal("Jos", b.Voornaam);
        }

        [Fact]
        public void TestUpdatePostcode() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);
            int postcode = 9000;

            b.UpdatePostcode(postcode);
            Assert.Equal(9000, b.Postcode);
            Assert.Throws<BestuurderException>(() => b.UpdatePostcode(postcode));
            b.UpdatePostcode(null);
            Assert.Null(b.Postcode);
        }

        [Fact]
        public void TestUpdateGemeente() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);
            string gemeente = "Gent";

            b.UpdateGemeente(gemeente);
            Assert.Equal("Gent", b.Gemeente);
            Assert.Throws<BestuurderException>(() => b.UpdateGemeente(gemeente));
            b.UpdateGemeente(null);
            Assert.Null(b.Gemeente);
        }

        [Fact]
        public void TestUpdateStraat() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);
            string straat = "Kerkstraat";

            b.UpdateStraat(straat);
            Assert.Equal("Kerkstraat", b.Straat);
            Assert.Throws<BestuurderException>(() => b.UpdateStraat(straat));
            b.UpdateStraat(null);
            Assert.Null(b.Straat);
        }

        [Fact]
        public void TestUpdateHuisnr() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);
            string huisnr = "14a";

            b.UpdateHuisnummer(huisnr);
            Assert.Equal("14a", b.Huisnummer);
            Assert.Throws<BestuurderException>(() => b.UpdateHuisnummer(huisnr));
            b.UpdateHuisnummer(null);
            Assert.Null(b.Huisnummer);
        }

        [Fact]
        public void TestUpdateRijksregisternummer() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);

            Assert.Throws<BestuurderException>(() => b.UpdateRijksregisternummer(rijksregister));
            Assert.Throws<BestuurderException>(() => b.UpdateRijksregisternummer(null));
        }

        [Fact]
        public void TestUpdateRijbewijs() {
            string naam = "Bezos";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);

            Assert.Throws<BestuurderException>(() => b.UpdateRijbewijs(null));
            rijbewijs = "C";
            b.UpdateRijbewijs(rijbewijs);
            Assert.Equal("C", b.Rijbewijs);
            rijbewijs = "B,C";
            b.UpdateRijbewijs(rijbewijs);
            Assert.Equal("B,C", b.Rijbewijs);
        }

        [Fact]
        public void TestConstructor() {
            string naam = "Bezos";
            string naam2 = "";
            string voornaam = "Jef";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            string rijbewijs = "B";
            string gemeente = "Gent";
            string straat = "Kerkstraat";
            string huisnr = "14a";
            int postcode = 9000;

            Bestuurder b = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, gemeente, straat, huisnr, postcode);
            Assert.Equal("Bezos", b.Naam);
            Assert.Equal("Jef", b.Voornaam);
            Assert.Equal(new DateTime(1990, 8, 24), b.GeboorteDatum);
            Assert.Equal("12345678958", b.RijksregisterNummer);
            Assert.Equal("B", b.Rijbewijs);
            Assert.Equal("Gent", b.Gemeente);
            Assert.Equal("Kerkstraat", b.Straat);
            Assert.Equal("14a", b.Huisnummer);
            Assert.Equal(9000, b.Postcode);

            Assert.Throws<BestuurderException>(() => new Bestuurder(naam2, voornaam, geboortedatum, rijksregister, rijbewijs, gemeente, straat, huisnr, postcode)); // naam = ""
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, null, geboortedatum, rijksregister, rijbewijs, gemeente, straat, huisnr, postcode)); // voornaam = null
            //Assert.Throws<BestuurderException>(() => new Bestuurder(naam, voornaam, geboortedatum, rijksregister, "", gemeente, straat, huisnr, postcode)); // rijbewijs leeg
            //Assert.Throws<BestuurderException>(() => new Bestuurder(naam2, voornaam, geboortedatum, "12345678912", rijbewijs, gemeente, straat, huisnr, postcode)); // Ongeldig rijksregisternummer
            //Conclusie, rijbewijs mag een lege string zijn en validatie rijksregisternummer is weg

            Bestuurder b2 = new Bestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs, null, null, null, null);
            Assert.Null(b2.Gemeente);
            Assert.Null(b2.Straat);
            Assert.Null(b2.Huisnummer);
            Assert.Null(b2.Postcode);
        }
    }
}
