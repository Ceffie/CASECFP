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
        private IHandleCourses handleCourses;
        public CourseController(InfoSupportCaseContext context, IHandleCourses handleCourses)
        {
            _context = context;
            this.handleCourses = handleCourses;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public IEnumerable<CourseToViewModel> Get()
        {   
            return handleCourses.GetListOfCourses(_context);
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
                IEnumerable<CourseModel> queryCourse = handleCourses.checkCurrentCourses(_context, ctvm);

                //only add the course if it isn't duplicate
                if (!queryCourse.Any())
                {
                    //upload to db
                    handleCourses.AddCourse(_context, ctvm);

                    //check for the corresponding course (again) for later use of the id for the courseinstace
                    queryCourse = handleCourses.checkCurrentCourses(_context, ctvm);
                }

                //check if the instance already exists
                IEnumerable<CourseInstanceModel> queryCourseInstance = handleCourses.checkCurrentCourseInstances(_context, ctvm, queryCourse);
                
                //only add courseinstance if not a dupe
                if (!queryCourseInstance.Any())
                {
                    //upload to db
                    handleCourses.AddCourseInstance(_context, queryCourse, ctvm);
                }
            }
            //send information here to other page about how much was added if I had time
            //currently there is no feedback about it succeeding
        }
    }
}
