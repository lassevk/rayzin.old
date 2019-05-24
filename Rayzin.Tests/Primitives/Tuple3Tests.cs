using System;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable AssignmentIsFullyDiscarded
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class Tuple3Tests
    {
        [Test]
        public void Constructor_PutsAllValuesInTheRightMembers()
        {
            Tuple3 t = new Tuple3(1, 2, 4);

            Assert.That(t.T0, Is.EqualTo(1).Within(Epsilon.Value));
            Assert.That(t.T1, Is.EqualTo(2).Within(Epsilon.Value));
            Assert.That(t.T2, Is.EqualTo(4).Within(Epsilon.Value));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        public void Indexer_ForAllIndices_ReturnsTheCorrectValue(int index, double expected)
        {
            Tuple3 t = new Tuple3(1, 2, 4);

            double actual = t[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Indexer_InvalidIndices_ThrowsArgumentOutOfRangeException(int index)
        {
            Tuple3 t = new Tuple3(1, 2, 4);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = t[index]);
        }

        [Test]
        public void Dot_Examples_ProducesExpectedResults()
        {
            Tuple3 t1 = new Tuple3(1, 2, 3);
            Tuple3 t2 = new Tuple3(2, 3, 4);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(20));
        }

        [Test]
        public void Add_TwoTuples_ProducesCorrectResult()
        {
            Tuple3 t1 = new Tuple3(1, 2, 3);
            Tuple3 t2 = new Tuple3(10, 11, 12);

            Tuple3 actual = t1 + t2;

            Assert.That(actual, Is.EqualTo(new Tuple3(11, 13, 15)));
        }

        [Test]
        public void Subtract_TwoTuples_ProducesCorrectResult()
        {
            Tuple3 t1 = new Tuple3(10, 11, 12);
            Tuple3 t2 = new Tuple3(1, 3, 5);

            Tuple3 actual = t1 - t2;

            Assert.That(actual, Is.EqualTo(new Tuple3(9, 8, 7)));
        }

        [Test]
        public void Negate_Tuple_ProducesCorrectResult()
        {
            Tuple3 t = new Tuple3(1, 2, 3);

            Tuple3 actual = -t;

            Assert.That(actual, Is.EqualTo(new Tuple3(-1, -2, -3)));
        }

        [Test]
        public void DotProduct_TwoTuples_ProducesCorrectResult()
        {
            Tuple3 t1 = new Tuple3(1, 2, 3);
            Tuple3 t2 = new Tuple3(2, 3, 4);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(20));
        }

        [Test]
        public void Multiply_TupleByScalar_ProducesCorrectResult()
        {
            Tuple3 t = new Tuple3(1, 2, 3);

            Tuple3 actual = t * 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple3(3.5, 7, 10.5)));
        }

        [Test]
        public void Divide_TupleByScalar_ProducesCorrectResult()
        {
            Tuple3 t = new Tuple3(3.5, 7, 10.5);

            Tuple3 actual = t / 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple3(1, 2, 3)));
        }

        [Test]
        [TestCase(1, 2, 3, 1, 2, 3, true)]
        [TestCase(1, 2, 3, 0, 2, 3, false)]
        [TestCase(1, 2, 3, 1, 0, 3, false)]
        [TestCase(1, 2, 3, 1, 2, 0, false)]
        [TestCase(1, 2, 3, 1 + Epsilon.Value * 2, 2, 3, false)]
        [TestCase(1, 2, 3, 1, 2 + Epsilon.Value * 2, 3, false)]
        [TestCase(1, 2, 3, 1, 2, 3 + Epsilon.Value * 2, false)]
        [TestCase(1, 2, 3, 1 - Epsilon.Value * 2, 2, 3, false)]
        [TestCase(1, 2, 3, 1, 2 - Epsilon.Value * 2, 3, false)]
        [TestCase(1, 2, 3, 1, 2, 3 - Epsilon.Value * 2, false)]
        [TestCase(1, 2, 3, 1 + Epsilon.Value / 2, 2, 3, true)]
        [TestCase(1, 2, 3, 1, 2 + Epsilon.Value / 2, 3, true)]
        [TestCase(1, 2, 3, 1, 2, 3 + Epsilon.Value / 2, true)]
        [TestCase(1, 2, 3, 1 - Epsilon.Value / 2, 2, 3, true)]
        [TestCase(1, 2, 3, 1, 2 - Epsilon.Value / 2, 3, true)]
        [TestCase(1, 2, 3, 1, 2, 3 - Epsilon.Value / 2, true)]
        public void EqualsTuple_WithTestCases_ProducesExpectedResults(
            double a0, double a1, double a2, double b0, double b1, double b2, bool expected)
        {
            Tuple3 a = new Tuple3(a0, a1, a2);
            Tuple3 b = new Tuple3(b0, b1, b2);

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
            Tuple3 t = new Tuple3(0, 0, 0);

            Assert.Throws<NotSupportedException>(() => t.GetHashCode());
        }
    }
}