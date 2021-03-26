using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InfoSupportCase.Data;
using InfoSupportCase.Models;
using InfoSupportCase.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoSupportCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly InfoSupportCaseContext _context;
        private IGetCourses getCourses;
        public CourseController(InfoSupportCaseContext context, IGetCourses getCourses)
        {
            _context = context;
            this.getCourses = getCourses;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public IEnumerable<CourseToViewModel> Get()
        {   
            return getCourses.GetListOfCourses(_context);
        }

        public string SayHiToTheTests()
        {
            return "Hi";
        }

        // POST api/<CourseController>
        [HttpPost]
        public void Post(List<CourseToViewModel> courseToViewModels)
        {
            //recreate the gotten list to first one or more CourseModels, and then send those to the db, then create the CourseInstanceModels
            foreach (CourseToViewModel ctvm in courseToViewModels)
            {
                //check for any duplicate CourseModels
                IEnumerable<CourseModel> queryCourse = checkCurrentCourses(_context, ctvm);

                //only add the course if it isn't duplicate
                if (!queryCourse.Any())
                {
                    //upload to db
                    AddCourse(_context, ctvm);

                    //check for the corresponding course (again) for later use of the id for the courseinstace
                    queryCourse = checkCurrentCourses(_context, ctvm);
                }

                //check if the instance already exists
                IEnumerable<CourseInstanceModel> queryCourseInstance = checkCurrentCourseInstances(_context, ctvm, queryCourse);
                
                //only add courseinstance if not a dupe
                if (!queryCourseInstance.Any())
                {
                    //upload to db
                    AddCourseInstance(_context, queryCourse, ctvm);
                }
            }
            //send information here to other page about how much was added if I had time
        }

        private void AddCourse(InfoSupportCaseContext context, CourseToViewModel ctvm)
        {
            CourseModel cm = new CourseModel();
            cm.Name = ctvm.Name;
            cm.Code = ctvm.Code;
            cm.Days = ctvm.Days;
            cm.Id = 0;
            _context.CourseModel.Add(cm);
            _context.SaveChanges();
        }

        private void AddCourseInstance(InfoSupportCaseContext context, IEnumerable<CourseModel> queryCourse, CourseToViewModel ctvm)
        {
            CourseInstanceModel cim = new CourseInstanceModel();
            cim.CourseId = queryCourse.First().Id;
            cim.Date = Convert.ToDateTime(ctvm.Date);
            cim.Id = 0;
            _context.CourseInstanceModel.Add(cim);
            _context.SaveChanges();
        }

        private IEnumerable<CourseInstanceModel> checkCurrentCourseInstances(InfoSupportCaseContext context, CourseToViewModel ctvm, IEnumerable<CourseModel> queryCourse)
        {
            return from courseInstance in _context.CourseInstanceModel
                   where courseInstance.CourseId == queryCourse.First().Id && courseInstance.Date == Convert.ToDateTime(ctvm.Date)
                   select new CourseInstanceModel { Id = courseInstance.Id, Date = courseInstance.Date, CourseId = courseInstance.CourseId };
        }

        private IEnumerable<CourseModel> checkCurrentCourses(InfoSupportCaseContext context, CourseToViewModel ctvm)
        {
            return from course in _context.CourseModel
                   where course.Code == ctvm.Code && course.Days == ctvm.Days && course.Name == ctvm.Name
                   select new CourseModel { Id = course.Id, Code = course.Code, Days = course.Days, Name = course.Name };
        }
    }
}
