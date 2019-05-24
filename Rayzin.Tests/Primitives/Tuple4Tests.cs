using System;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable AssignmentIsFullyDiscarded
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class Tuple4Tests
    {
        [Test]
        public void Constructor_PutsAllValuesInTheRightMembers()
        {
            Tuple4 t = new Tuple4(1, 2, 4, 8);

            Assert.That(t.T0, Is.EqualTo(1).Within(Epsilon.Value));
            Assert.That(t.T1, Is.EqualTo(2).Within(Epsilon.Value));
            Assert.That(t.T2, Is.EqualTo(4).Within(Epsilon.Value));
            Assert.That(t.T3, Is.EqualTo(8).Within(Epsilon.Value));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 8)]
        public void Indexer_ForAllIndices_ReturnsTheCorrectValue(int index, double expected)
        {
            Tuple4 t = new Tuple4(1, 2, 4, 8);

            double actual = t[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(4)]
        [TestCase(5)]
        public void Indexer_InvalidIndices_ThrowsArgumentOutOfRangeException(int index)
        {
            Tuple4 t = new Tuple4(1, 2, 4, 8);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = t[index]);
        }

        [Test]
        public void Dot_Examples_ProducesExpectedResults()
        {
            Tuple4 t1 = new Tuple4(1, 2, 3, 0);
            Tuple4 t2 = new Tuple4(2, 3, 4, 0);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(20));
        }

        [Test]
        public void Add_TwoTuples_ProducesCorrectResult()
        {
            Tuple4 t1 = new Tuple4(1, 2, 3, 4);
            Tuple4 t2 = new Tuple4(10, 11, 12, 13);

            Tuple4 actual = t1 + t2;

            Assert.That(actual, Is.EqualTo(new Tuple4(11, 13, 15, 17)));
        }

        [Test]
        public void Subtract_TwoTuples_ProducesCorrectResult()
        {
            Tuple4 t1 = new Tuple4(10, 11, 12, 13);
            Tuple4 t2 = new Tuple4(1, 3, 5, 7);

            Tuple4 actual = t1 - t2;

            Assert.That(actual, Is.EqualTo(new Tuple4(9, 8, 7, 6)));
        }

        [Test]
        public void Negate_Tuple_ProducesCorrectResult()
        {
            Tuple4 t = new Tuple4(1, 2, 3, 4);

            Tuple4 actual = -t;

            Assert.That(actual, Is.EqualTo(new Tuple4(-1, -2, -3, -4)));
        }

        [Test]
        public void DotProduct_TwoVectors_ProducesCorrectResult()
        {
            Tuple4 t1 = new Tuple4(1, 2, 3, 0);
            Tuple4 t2 = new Tuple4(2, 3, 4, 0);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(20));
        }

        [Test]
        public void DotProduct_TwoTuples_ProducesCorrectResult()
        {
            Tuple4 t1 = new Tuple4(1, 2, 3, 4);
            Tuple4 t2 = new Tuple4(2, 3, 4, 5);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(40));
        }

        [Test]
        public void Multiply_TupleByScalar_ProducesCorrectResult()
        {
            Tuple4 t = new Tuple4(1, 2, 3, 4);

            Tuple4 actual = t * 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple4(3.5, 7, 10.5, 14)));
        }

        [Test]
        public void Divide_TupleByScalar_ProducesCorrectResult()
        {
            Tuple4 t = new Tuple4(3.5, 7, 10.5, 14);

            Tuple4 actual = t / 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple4(1, 2, 3, 4)));
        }

        [Test]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 4, true)]
        [TestCase(1, 2, 3, 4, 0, 2, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 0, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 0, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 0, false)]
        [TestCase(1, 2, 3, 4, 1 + Epsilon.Value * 2, 2, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2 + Epsilon.Value * 2, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 3 + Epsilon.Value * 2, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 4 + Epsilon.Value * 2, false)]
        [TestCase(1, 2, 3, 4, 1 - Epsilon.Value * 2, 2, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2 - Epsilon.Value * 2, 3, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 3 - Epsilon.Value * 2, 4, false)]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 4 - Epsilon.Value * 2, false)]
        [TestCase(1, 2, 3, 4, 1 + Epsilon.Value / 2, 2, 3, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2 + Epsilon.Value / 2, 3, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2, 3 + Epsilon.Value / 2, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 4 + Epsilon.Value / 2, true)]
        [TestCase(1, 2, 3, 4, 1 - Epsilon.Value / 2, 2, 3, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2 - Epsilon.Value / 2, 3, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2, 3 - Epsilon.Value / 2, 4, true)]
        [TestCase(1, 2, 3, 4, 1, 2, 3, 4 - Epsilon.Value / 2, true)]
        public void EqualsTuple_WithTestCases_ProducesExpectedResults(
            double a0, double a1, double a2, double a3, double b0, double b1, double b2, double b3, bool expected)
        {
            Tuple4 a = new Tuple4(a0, a1, a2, a3);
            Tuple4 b = new Tuple4(b0, b1, b2, b3);

            bool actual = a.Equals(b);
            Assert.That(actual, Is.EqualTo(expected));

            actual = a.Equals((object)b);
            Assert.That(actual, Is.EqualTo(expected));

            if (expected)
                Assert.That(a == b, Is.True);
            else
                Assert.That(a != b, Is.True);
        }

        [Test]
        public void GetHashCode_ThrowsNotSupportedException()
        {
            Tuple4 t = new Tuple4(0, 0, 0, 0);

            Assert.Throws<NotSupportedException>(() => t.GetHashCode());
        }
    }
}