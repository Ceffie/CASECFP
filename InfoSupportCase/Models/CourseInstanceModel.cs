using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InfoSupportCase.Models
{
    public class CourseInstanceModel
    {
        public int Id {get; set;}
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
    }
}
