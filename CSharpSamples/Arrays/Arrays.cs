using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    [TestFixture]
    public class Arrays
    {
        [Test]
        public void SingleDimensionalArraysTests()
        {
            int[] arrayOfInts = new int[5];
            foreach(var val in arrayOfInts)
            {
                Assert.That(val, Is.EqualTo(0)); // default for int is 0
            }

            var arrayOfStrings = new string[5]; // type inference
            foreach (var val in arrayOfStrings)
            {
                Assert.That(val, Is.Null); // default for all reference types is null
            }
        }

        [Test]
        public void ArrayDeclarationTests()
        {
            var values = new int[] { 7, 6, 8, 1, 2, 3, 4, 5 };
            Array.Sort(values);
            Assert.That(values[0], Is.EqualTo(1));
        }

        [Test]
        public void MultiDimensionalArraysTests()
        {
            int[,,] array3D = new int[2, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } },
                                                 { { 7, 8, 9 }, { 10, 11, 12 } } };
            Assert.That(array3D[0, 0, 1], Is.EqualTo(2));
            Assert.That(array3D[1, 1, 1], Is.EqualTo(11));
            Assert.That(array3D.Rank, Is.EqualTo(3));
           
            string[,] array2D = new string[3, 2] { { "one", "two" }, 
                                                    { "three", "four" },
                                                    { "five", "six" } };
            Assert.That(array2D.Rank, Is.EqualTo(2));
            Assert.That(array2D[1, 1], Is.EqualTo("four"));
        }

        [Test]
        public void JaggedArraysTests()
        {
            // Declare the array of two elements:
            int[][] arr = new int[2][];

            // Initialize the elements:
            arr[0] = new int[5] { 1, 3, 5, 7, 9 };
            arr[1] = new int[4] { 2, 4, 6, 8 };

            // Accessing jagged arrays is different from multi-dimensional arrays
            Assert.That(arr[0][0], Is.EqualTo(1));
            Assert.That(arr[1][3], Is.EqualTo(8));
        }

        [Test]
        public void ParameterPassingArraysTests()
        {

        }

        [Test]
        public void ExceptionsArraysTests()
        {
        }

        [Test]
        public void LinqTests()
        {

        }
    }
}
