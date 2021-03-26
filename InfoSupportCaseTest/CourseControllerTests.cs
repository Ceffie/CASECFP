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
        Mock<IHandleCourses> handleCoursesMock;
        CourseToViewModel ctvm = new CourseToViewModel { Days = 2, Name = "Testing in C Sharp", Code = "TESTCSHARP", Date = "10/10/1010" };
        List<CourseToViewModel> ienctvm = new List<CourseToViewModel>();

        [TestInitialize]
        public void Init()
        {
            handleCoursesMock = new Mock<IHandleCourses>();
            ienctvm.Add(ctvm);
            handleCoursesMock.Setup(x => x.GetListOfCourses(null)).Returns(ienctvm);
            sut = new CourseController(null, handleCoursesMock.Object);
        }
        [TestMethod]
        public void ShouldReturnHi()
        {
            Assert.AreEqual("Hi", sut.SayHiToTheTests());
        }

        //there aren't a whole lot of tests to write, as all current functions talk with the db
    }
}
