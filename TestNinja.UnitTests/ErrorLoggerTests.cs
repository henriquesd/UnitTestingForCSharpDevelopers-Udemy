using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

            // this line is gonna throw an exception \/; The way to write assertions for
            // methods that throw and exception is by using a delegate (see line 37);
            //logger.Log(error);

            // using lambda expression \/;
            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
            
            // example of using a type of exception \/;
            // Assert.That(() => logger.Log(error), Throws.Exception.TypeOf<DivideByZeroException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();

            var id = Guid.Empty;
            logger.ErrorLogged += (sender, args) => { id = args; };

            logger.Log("a");

            // to test a method that raises an event;
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

     
    }
}
