using InfoSupportCase.Data;
using InfoSupportCase.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InfoSupportCase.Services
{
    public class HandleCourses : IHandleCourses
    {
        public void AddCourse(InfoSupportCaseContext _context, CourseToViewModel ctvm)
        {
            CourseModel cm = new CourseModel();
            cm.Name = ctvm.Name;
            cm.Code = ctvm.Code;
            cm.Days = ctvm.Days;
            cm.Id = 0;
            _context.CourseModel.Add(cm);
            _context.SaveChanges();
        }

        public void AddCourseInstance(InfoSupportCaseContext _context, IEnumerable<CourseModel> queryCourse, CourseToViewModel ctvm)
        {
            CourseInstanceModel cim = new CourseInstanceModel();
            cim.CourseId = queryCourse.First().Id;
            cim.Date = Convert.ToDateTime(ctvm.Date);
            cim.Id = 0;
            _context.CourseInstanceModel.Add(cim);
            _context.SaveChanges();
        }

        public IEnumerable<CourseInstanceModel> checkCurrentCourseInstances(InfoSupportCaseContext _context, CourseToViewModel ctvm, IEnumerable<CourseModel> queryCourse)
        {
            return from courseInstance in _context.CourseInstanceModel
                   where courseInstance.CourseId == queryCourse.First().Id && courseInstance.Date == Convert.ToDateTime(ctvm.Date)
                   select new CourseInstanceModel { Id = courseInstance.Id, Date = courseInstance.Date, CourseId = courseInstance.CourseId };
        }

        public IEnumerable<CourseModel> checkCurrentCourses(InfoSupportCaseContext _context, CourseToViewModel ctvm)
        {
            return from course in _context.CourseModel
                   where course.Code == ctvm.Code && course.Days == ctvm.Days && course.Name == ctvm.Name
                   select new CourseModel { Id = course.Id, Code = course.Code, Days = course.Days, Name = course.Name };
        }

        public IEnumerable<CourseToViewModel> GetListOfCourses(InfoSupportCaseContext _context)
        {
            return from course in _context.CourseModel
                   join courseInstance in _context.CourseInstanceModel on course.Id equals courseInstance.CourseId
                   orderby courseInstance.Date ascending
                   select new CourseToViewModel { Code = course.Code, Days = course.Days, Name = course.Name, Date = courseInstance.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) };

        }
    }
}
