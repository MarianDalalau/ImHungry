using ImHungry.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImHungry.Tests.Helpers
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void UtilsNullTest()
        {
            var actual = Utils.FormatSearchString(null);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UtilsEmptyTest()
        {
            var actual = Utils.FormatSearchString(new string[0]);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void UtilsTest()
        {
            var actual = Utils.FormatSearchString(new string[] { "abc", "drf" });
            var expected = "abc,drf";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UtilsTestNotEqual()
        {
            var actual = Utils.FormatSearchString(new string[] { "abc", "drf" });
            var expected = "abcdrf";
            Assert.AreNotEqual(expected, actual);
        }
    }
}
