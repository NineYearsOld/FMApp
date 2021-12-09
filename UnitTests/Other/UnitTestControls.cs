using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BusinessLayer.Utilities;

namespace UnitTests.Other {
    public class UnitTestControls {
        [Fact]
        public void TestValidatieRijksregisternummer() {
            string rr1 = "12345678958";
            string rr2 = "12345678912";
            string rr3 = "98765432167";
            string rr4 = "98765432100";
            string rr5 = "00000000000";
            string rr6 = "99999999999";
            string rr7 = "123456789588";
            string rr8 = "1234567895";
            string rr9 = "15796315785";

            Assert.True(Controls.ValidatieRijkregisternummer(rr1));
            Assert.True(Controls.ValidatieRijkregisternummer(rr3));

            Assert.False(Controls.ValidatieRijkregisternummer(rr2));
            Assert.False(Controls.ValidatieRijkregisternummer(rr4));
            Assert.False(Controls.ValidatieRijkregisternummer(rr5));
            Assert.False(Controls.ValidatieRijkregisternummer(rr6));
            Assert.False(Controls.ValidatieRijkregisternummer(rr7));
            Assert.False(Controls.ValidatieRijkregisternummer(rr8));
            Assert.False(Controls.ValidatieRijkregisternummer(rr9));
        }
    }
}
