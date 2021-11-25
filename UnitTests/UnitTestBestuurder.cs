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
            string connectionString = @"Data Source=LAPTOP-3DP97NFE\SQLEXPRESS;Initial Catalog=FleetManagementDb;Integrated Security=True";

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
            string naam = "Jef";
            string voornaam = "Bezos";
            DateTime geboortedatum = new DateTime(1990, 8, 24);
            string rijksregister = "12345678958";
            List<string> rijbewijs = new List<string>();
            rijbewijs.Add("B");

        }*/
    }
}
