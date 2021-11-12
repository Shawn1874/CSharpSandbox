using System;
using System.Linq;
using NUnit.Framework;

namespace Enumerations
{

    public enum ShippingMethod
    {
       Ground,   // defaults to 0, and subsequent elements are the next in the series
       TwoDayExpress, // 1
       OvernightExpress // 2
    };

    [TestFixture]
    class EnumerationTests
    {
        /// <summary>
        /// Assert that the Any extension method also works for strings.
        /// </summary>
        [Test]
        public void EnumToIntTest()
        {
            var method = ShippingMethod.OvernightExpress;
            Assert.That((int) method, Is.EqualTo(2));
            method = ShippingMethod.Ground;
            Assert.That((int)method, Is.EqualTo(0));
        }

        [Test]
        public void EnumToStringTest()
        {
            var method = ShippingMethod.Ground;
            Assert.That(method.ToString(), Is.EqualTo("Ground"));
        }

        [Test]
        public void ParseStringTest()
        {
            ShippingMethod method;
           
            // Oops!  There is a typo.
            bool result = Enum.TryParse<ShippingMethod>("TowDayExpress", out method);
            Assert.That(result, Is.False);

            // Typo fixed!
            var methodStr = "TwoDayExpress";
            result = Enum.TryParse<ShippingMethod>(methodStr, out method);
            Assert.That(result, Is.True);

            var parsed = (ShippingMethod)Enum.Parse(typeof(ShippingMethod), methodStr);
            Assert.That(parsed, Is.EqualTo(ShippingMethod.TwoDayExpress));
        }
    }
}
