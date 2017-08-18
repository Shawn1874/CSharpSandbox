using NUnit.Framework;
using System;
using System.Timers;
using System.Threading;
namespace Timers
{
    [TestFixture]
    public class TimerTests
    {
        private static bool elapsed = false;
        [Test]
        public void DefaultInitTest()
        {
            var timer = new System.Timers.Timer();
            Assert.AreEqual(true, timer.AutoReset);
            Assert.AreEqual(false, timer.Enabled);
            Assert.AreEqual(null, timer.SynchronizingObject);
            Assert.AreEqual(100.0D, timer.Interval);
        }

        [Test]
        public void AutoResetTest()
        {
            var timer = new System.Timers.Timer(100); // 100 millisecond
            timer.Elapsed += OnElapsed;
            Assert.AreEqual(100.0D, timer.Interval);
            Assert.IsTrue(timer.AutoReset);
            Assert.IsFalse(timer.Enabled);
            Thread.Sleep(110);
            Assert.IsFalse(elapsed);
            timer.Enabled = true;
            Thread.Sleep(110);
            Assert.IsTrue(elapsed);
        }

        [Test]
        public void ExceptionsTest()
        {
            Assert.That(() => { var temp = new System.Timers.Timer(-5); }, Throws.ArgumentException);

            var timer = new System.Timers.Timer();
            timer.Enabled = false;
            Double value = 2147483648;// Int32.MaxValue + 1;
            timer.Interval = value;
            Assert.That(() => timer.Start(), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        private static void OnElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            elapsed = true;
        }
    }
}
