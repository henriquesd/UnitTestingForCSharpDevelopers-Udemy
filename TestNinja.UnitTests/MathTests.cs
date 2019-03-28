﻿using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        // SetUp - The NUnit test runner will call that method before running each test;
        // TearDown - The NUnit test runner will call that method before after each test;
            // TearDown This is often used with integration tests, because on integration tests,
            // you may create some data in your database and then you wanna do clean up after each test,
            // thats where you use the TearDown attribute;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        //[Ignore("Write here the reason why you disabled this test")] 
            // Use the 'Ignore("")' to temporarily disable an test;
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expetedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expetedResult));
        }

        // Code before be refactored \/;
        //[Test]
        //public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        //{
        //    var result = _math.Max(2, 1);

        //    Assert.That(result, Is.EqualTo(2));
        //}

        //[Test]
        //public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        //{
        //    var result = _math.Max(1, 2);

        //    Assert.That(result, Is.EqualTo(2));
        //}

        //[Test]
        //public void Max_ArgumentAreEqual_ReturnTheSameArgument()
        //{
        //    var result = _math.Max(1, 1);

        //    Assert.That(result, Is.EqualTo(1));
        //}

    }
}