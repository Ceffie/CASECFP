using InfoSupportCase.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InfoSupportCase.Services
{
    public class GetCourses : IGetCourses
    {
        public IEnumerable<CourseToViewModel> GetListOfCourses(InfoSupportCaseContext _context)
        {
            return from course in _context.CourseModel
                   join courseInstance in _context.CourseInstanceModel on course.Id equals courseInstance.CourseId
                   select new CourseToViewModel { Code = course.Code, Days = course.Days, Name = course.Name, Date = courseInstance.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) };

        }
    }
}
