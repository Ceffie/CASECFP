using InfoSupportCase;
using InfoSupportCase.Controllers;
using InfoSupportCase.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace InfoSupportCaseTest
{
    [TestClass]
    public class CourseControllerTests
    {
        CourseController sut;
        Mock<IGetCourses> getCoursesMock;

        [TestInitialize]
        public void Init()
        {
            getCoursesMock = new Mock<IGetCourses>();
            CourseToViewModel ctvm = new CourseToViewModel { Days = 2, Name = "Testing in C Sharp", Code = "TESTCSHARP", Date = "10/10/1010" };
            List<CourseToViewModel> ienctvm = new List<CourseToViewModel>();
            ienctvm.Add(ctvm);
            getCoursesMock.Setup(x => x.GetListOfCourses(null)).Returns(ienctvm);
            sut = new CourseController(null, getCoursesMock.Object);
        }
        [TestMethod]
        public void ShouldReturnHi()
        {
            Assert.AreEqual("Hi", sut.SayHiToTheTests());
        }

        [TestMethod]
        public void ShouldReturnctvm()
        {
            CourseToViewModel ctvm = new CourseToViewModel { Days = 2, Name = "Testing in C Sharp", Code = "TESTCSHARP", Date = "10/10/1010" };
            List<CourseToViewModel> ienctvm = new List<CourseToViewModel>();
            ienctvm.Add(ctvm);
            Assert.AreEqual(ienctvm, sut.Get().ToString());
        }
    }
}
