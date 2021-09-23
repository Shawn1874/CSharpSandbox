using NUnit.Framework;
using System.Linq;
using CustomString;

namespace Types
{
    [TestFixture]
    class StringExtMethodsTests
    {

        [Test]
        public void TitleCaseTest()
        {
            var test1 = "the cow jumped over the moon";
            var test1Transformed = "The Cow Jumped Over The Moon";

            Assert.That(test1Transformed, Is.EqualTo(test1.ConvertToTitleCase()));

            var test2 = "la vaca brincó sobre la Luna";
            var test2Transformed = "La Vaca Brincó Sobre La Luna";

            Assert.That(test2Transformed, Is.EqualTo(test2.ConvertToTitleCase()));
        }

    }
}
