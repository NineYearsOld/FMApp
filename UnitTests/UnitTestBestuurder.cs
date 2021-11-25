using BusinessLayer.Exceptions;
using BusinessLayer.Entities;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests {
    public class UnitTestBestuurder {
        [Fact]
        public void TestValidatieRijksregister() {
            string rijksregister = "12345678912";
            string rijksregister2 = "12345678958";

            Assert.False(BestuurderController.ValidatieRijkregisternummer(rijksregister));
            Assert.True(BestuurderController.ValidatieRijkregisternummer(rijksregister2));

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

            BestuurderController bc = new BestuurderController();


            // lege list rijbewijs
            bc.CreateBestuurder(naam, voornaam, geboortedatum, rijksregister, rijbewijs);
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
            string naam = "Jef";
            string voornaam = "Bezos";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            List<string> rijbewijs = new List<string>();
            rijbewijs.Add("B");

        }
    }
}
