using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InfoSupportCase.Models;

namespace InfoSupportCase.Data
{
    public class InfoSupportCaseContext : DbContext
    {
        public InfoSupportCaseContext (DbContextOptions<InfoSupportCaseContext> options)
            : base(options)
        {
        }

        public DbSet<InfoSupportCase.Models.CourseModel> CourseModel { get; set; }

        public DbSet<InfoSupportCase.Models.CourseInstanceModel> CourseInstanceModel { get; set; }
    }
}
