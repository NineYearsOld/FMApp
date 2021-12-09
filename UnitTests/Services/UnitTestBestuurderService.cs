using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services {
    public class UnitTestBestuurderService {
        IBestuurderRepository ibr;

        [Fact]
        public void TestExistsBestuurder() {
            var bs = new BestuurderService(ibr);
        }

        [Fact]
        public void TestCreateBestuurder() {
            var bs = new BestuurderService(ibr);
        }

        [Fact]
        public void TestDeleteBestuurder() {
            var bs = new BestuurderService(ibr);
        }

        [Fact]
        public void TestUpdateBestuurder() {
            var bs = new BestuurderService(ibr);
        }

        [Fact]
        public void TestToonDetails() {
            var bs = new BestuurderService(ibr);
        }
    }
}
