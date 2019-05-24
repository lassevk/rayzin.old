using System;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable AssignmentIsFullyDiscarded
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class Tuple2Tests
    {
        [Test]
        public void Constructor_PutsAllValuesInTheRightMembers()
        {
            Tuple2 t = new Tuple2(1, 2);

            Assert.That(t.T0, Is.EqualTo(1).Within(Epsilon.Value));
            Assert.That(t.T1, Is.EqualTo(2).Within(Epsilon.Value));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void Indexer_ForAllIndices_ReturnsTheCorrectValue(int index, double expected)
        {
            Tuple2 t = new Tuple2(1, 2);

            double actual = t[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(2)]
        [TestCase(3)]
        public void Indexer_InvalidIndices_ThrowsArgumentOutOfRangeException(int index)
        {
            Tuple2 t = new Tuple2(1, 2);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = t[index]);
        }

        [Test]
        public void Dot_Examples_ProducesExpectedResults()
        {
            Tuple2 t1 = new Tuple2(1, 2);
            Tuple2 t2 = new Tuple2(2, 3);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(8));
        }

        [Test]
        public void Add_TwoTuples_ProducesCorrectResult()
        {
            Tuple2 t1 = new Tuple2(1, 2);
            Tuple2 t2 = new Tuple2(10, 11);

            Tuple2 actual = t1 + t2;

            Assert.That(actual, Is.EqualTo(new Tuple2(11, 13)));
        }

        [Test]
        public void Subtract_TwoTuples_ProducesCorrectResult()
        {
            Tuple2 t1 = new Tuple2(10, 11);
            Tuple2 t2 = new Tuple2(1, 3);

            Tuple2 actual = t1 - t2;

            Assert.That(actual, Is.EqualTo(new Tuple2(9, 8)));
        }

        [Test]
        public void Negate_Tuple_ProducesCorrectResult()
        {
            Tuple2 t = new Tuple2(1, 2);

            Tuple2 actual = -t;

            Assert.That(actual, Is.EqualTo(new Tuple2(-1, -2)));
        }

        [Test]
        public void DotProduct_TwoTuples_ProducesCorrectResult()
        {
            Tuple2 t1 = new Tuple2(1, 2);
            Tuple2 t2 = new Tuple2(2, 3);

            double actual = t1 * t2;

            Assert.That(actual, Is.EqualTo(20));
        }

        [Test]
        public void Multiply_TupleByScalar_ProducesCorrectResult()
        {
            Tuple2 t = new Tuple2(1, 2);

            Tuple2 actual = t * 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple2(3.5, 7)));
        }

        [Test]
        public void Divide_TupleByScalar_ProducesCorrectResult()
        {
            Tuple2 t = new Tuple2(3.5, 7);

            Tuple2 actual = t / 3.5;

            Assert.That(actual, Is.EqualTo(new Tuple2(1, 2)));
        }

        [Test]
        [TestCase(1, 2, 1, 2, true)]
        [TestCase(1, 2, 0, 2, false)]
        [TestCase(1, 2, 1, 0, false)]
        [TestCase(1, 2, 1 + Epsilon.Value * 2, 2, false)]
        [TestCase(1, 2, 1, 2 + Epsilon.Value * 2, false)]
        [TestCase(1, 2, 1 - Epsilon.Value * 2, 2, false)]
        [TestCase(1, 2, 1, 2 - Epsilon.Value * 2, false)]
        [TestCase(1, 2, 1 + Epsilon.Value / 2, 2, true)]
        [TestCase(1, 2, 1, 2 + Epsilon.Value / 2, true)]
        [TestCase(1, 2, 1 - Epsilon.Value / 2, 2, true)]
        [TestCase(1, 2, 1, 2 - Epsilon.Value / 2, true)]
        public void EqualsTuple_WithTestCases_ProducesExpectedResults(
            double a0, double a1, double b0, double b1, bool expected)
        {
            Tuple2 a = new Tuple2(a0, a1);
            Tuple2 b = new Tuple2(b0, b1);

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
            Tuple2 t = new Tuple2(0, 0);

            Assert.Throws<NotSupportedException>(() => t.GetHashCode());
        }
    }
}