using NUnit.Framework;
using System;

namespace Types
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }

        public Invoice(int id, string descr, Decimal amount)
        {
            this.Id = id;
            this.Amount = amount;
            this.Description = descr;
        }

        public Invoice Clone()
        {
            Invoice clone = (Invoice)this.MemberwiseClone();
            return clone;
        }
    };

    [TestFixture]
    public class ObjectOperations
    {
        [Test]
        public void EqualsTests()
        {
            var fenceRepair = new Invoice(1, "Patio Fence Repair", 200m);
            var plumbingRepair = new Invoice(2, "Kitchen Sink Repair", 350.75m);
            var plumbingRepair2 = plumbingRepair;
            Assert.That(ReferenceEquals(fenceRepair, plumbingRepair) == false);
            Assert.That(ReferenceEquals(plumbingRepair2, plumbingRepair));

            // After cloning the string properties are still the same reference until one of the strings 
            // is reassigned to a new string.
            var fenceRepair2 = fenceRepair.Clone();
            Assert.That(ReferenceEquals(fenceRepair.Description, fenceRepair2.Description));

            fenceRepair2.Description = "Fence Repair";
            Assert.That(!ReferenceEquals(fenceRepair.Description, fenceRepair2.Description));

            // for reference types, Equals and ReferenceEquals tests for the same thing, but the
            // ID property is an integer which is a value type
            Assert.That(ReferenceEquals(fenceRepair, fenceRepair2) == false);
            Assert.That(Equals(fenceRepair2, fenceRepair) == false);
            Assert.That(Equals(fenceRepair2.Id, fenceRepair.Id));
            var frString = fenceRepair.ToString();
            Assert.That(frString == "Types.Invoice");
        }

        [Test]
        public void OperatorsTests()
        {

        }
    }
}
