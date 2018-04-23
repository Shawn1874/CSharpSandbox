using NUnit.Framework;
using System;

namespace Types
{
    [TestFixture]
    class Enumerations
    {
        public enum PayrollType
        {
            Contractor = 1,
            Salaried,
            Executive,
            Hourly
        };

        enum Range : long { Max = 2147483648L, Min = 255L };

        [Flags]
        public enum CarOptions
        {
            // The flag for SunRoof is 0001.
            SunRoof = 0x01,
            // The flag for Spoiler is 0010.
            Spoiler = 0x02,
            // The flag for FogLights is 0100.
            FogLights = 0x04,
            // The flag for TintedWindows is 1000.
            TintedWindows = 0x08,
        }

        [Test]
        public void EnumToStringTest()
        {
            var prType = PayrollType.Contractor;
            Assert.That(prType.ToString().Equals("Contractor"));
        }

        /// <summary>
        /// Using an enum like a list of flags allows convenient logical operations to set multiple bits.
        /// It also allows generating a string with a list of the enum values.  To do this the [Flags]
        /// attribute must be specified above the definiton of the enumeration.
        /// </summary>
        [Test]
        public void FlagsTest()
        {
            CarOptions options = CarOptions.SunRoof | CarOptions.FogLights;
            Assert.That(options.ToString().Equals("SunRoof, FogLights"));
            Assert.That((int)options, Is.EqualTo(5));
        }
    }
}
