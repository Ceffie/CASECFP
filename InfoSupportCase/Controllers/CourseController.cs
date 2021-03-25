﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InfoSupportCase.Data;
using InfoSupportCase.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoSupportCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly InfoSupportCaseContext _context;

        public CourseController(InfoSupportCaseContext context)
        {
            _context = context;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public IEnumerable<CourseToViewModel> Get()
        {
            var query = from course in _context.CourseModel
                        join courseInstance in _context.CourseInstanceModel on course.Id equals courseInstance.CourseId
                        select new CourseToViewModel { Code = course.Code, Days = course.Days, Name = course.Name, Date = courseInstance.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) };

            return query;
        }

        public string SayHiToTheTests()
        {
            return "Hi";
        }

        // POST api/<CourseController>
        [HttpPost]
        public void Post(List<CourseToViewModel> courseToViewModels)
        {
            CourseModel cm = new CourseModel();
            CourseInstanceModel cim = new CourseInstanceModel();
            //recreate the gotten list to first one or more CourseModels, and then send those to the db, then create the CourseInstanceModels
            foreach (CourseToViewModel ctvm in courseToViewModels)
            {
                //check for any duplicate CourseModels
                var query = from course in _context.CourseModel
                            where course.Code == ctvm.Code && course.Days == ctvm.Days && course.Name == ctvm.Name
                            select new CourseModel { Id = course.Id, Code = course.Code, Days = course.Days, Name = course.Name};

                //this doesn't work yet
                cm.Name = ctvm.Name;
                cm.Code = ctvm.Code;
                cm.Days = ctvm.Days;

                _context.CourseModel.Add(cm);
                _context.SaveChanges();

                
            }
            if (courseToViewModels.Count > 0)
            {
                //Console.WriteLine(cim.CourseId);
            }
        }
    }
}
