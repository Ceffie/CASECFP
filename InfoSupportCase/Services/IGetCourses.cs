using InfoSupportCase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoSupportCase.Services
{
    public interface IGetCourses
    {
        IEnumerable<CourseToViewModel> GetListOfCourses(InfoSupportCaseContext _context);
    }
}
