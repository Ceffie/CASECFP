using InfoSupportCase.Data;
using InfoSupportCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoSupportCase.Services
{
    public interface IHandleCourses
    {
        IEnumerable<CourseToViewModel> GetListOfCourses(InfoSupportCaseContext _context);
        void AddCourse(InfoSupportCaseContext _context, CourseToViewModel ctvm);
        void AddCourseInstance(InfoSupportCaseContext _context, IEnumerable<CourseModel> queryCourse, CourseToViewModel ctvm);
        IEnumerable<CourseInstanceModel> checkCurrentCourseInstances(InfoSupportCaseContext _context, CourseToViewModel ctvm, IEnumerable<CourseModel> queryCourse);
        IEnumerable<CourseModel> checkCurrentCourses(InfoSupportCaseContext _context, CourseToViewModel ctvm);
    }
}
