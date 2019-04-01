using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var storage = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(storage.Object);

            // in this case we don't care about the result, we just wanna test the
            // interaction of this controller with the storage object;
            controller.DeleteEmployee(1);

            storage.Verify(s => s.DeleteEmployy(1));
        }
    }
}
