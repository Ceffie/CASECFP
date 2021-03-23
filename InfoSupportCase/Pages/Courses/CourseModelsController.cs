using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoSupportCase.Data;
using InfoSupportCase.Models;

namespace InfoSupportCase.Pages.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseModelsController : ControllerBase
    {
        private readonly InfoSupportCaseContext _context;

        public CourseModelsController(InfoSupportCaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseModel>>> GetCourseModel()
        {
            return await _context.CourseModel.ToListAsync();
        }

        // GET: api/CourseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseModel>> GetCourseModel(int id)
        {
            var courseModel = await _context.CourseModel.FindAsync(id);

            if (courseModel == null)
            {
                return NotFound();
            }

            return courseModel;
        }

        // PUT: api/CourseModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseModel(int id, CourseModel courseModel)
        {
            if (id != courseModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(courseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CourseModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourseModel>> PostCourseModel(CourseModel courseModel)
        {
            _context.CourseModel.Add(courseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseModel", new { id = courseModel.Id }, courseModel);
        }

        // DELETE: api/CourseModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseModel>> DeleteCourseModel(int id)
        {
            var courseModel = await _context.CourseModel.FindAsync(id);
            if (courseModel == null)
            {
                return NotFound();
            }

            _context.CourseModel.Remove(courseModel);
            await _context.SaveChangesAsync();

            return courseModel;
        }

        private bool CourseModelExists(int id)
        {
            return _context.CourseModel.Any(e => e.Id == id);
        }
    }
}
