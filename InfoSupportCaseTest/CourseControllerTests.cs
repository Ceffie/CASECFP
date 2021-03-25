using InfoSupportCase.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupportCaseTest
{
    [TestClass]
    public class CourseControllerTests
    {
        CourseController sut;

        [TestInitialize]
        public void Init()
        {
            sut = new CourseController(null);
        }
        [TestMethod]
        public void ShouldReturnHi()
        {
            Assert.AreEqual("Hi", sut.SayHiToTheTests());
        }
    }
}
