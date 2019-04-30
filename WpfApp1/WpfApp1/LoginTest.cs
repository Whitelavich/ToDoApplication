using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1 {
    [TestFixture]
    class LoginTest {
        [Test]
        public void ReturnFalseGivenValueOf1() {
            var result = LoginWindow.login("", "");
            Assert.IsFalse(result, "1 should not be prime");
            result = LoginWindow.login("admin", " OR 1=1");
            Assert.IsFalse(result, "Can't log into a account not existent");
            result = LoginWindow.register("esda98", "password1");
            Assert.IsFalse(result, "Can't register for an account that already exists");
        }
    }
}
