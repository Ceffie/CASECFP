using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoSupportCase.Data;
using InfoSupportCase.Models;

namespace InfoSupportCase.Pages.CourseInstances
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstanceModelsController : ControllerBase
    {
        private readonly InfoSupportCaseContext _context;

        public CourseInstanceModelsController(InfoSupportCaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseInstanceModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseInstanceModel>>> GetCourseInstanceModel()
        {
            return await _context.CourseInstanceModel.ToListAsync();
        }

        // GET: api/CourseInstanceModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseInstanceModel>> GetCourseInstanceModel(int id)
        {
            var courseInstanceModel = await _context.CourseInstanceModel.FindAsync(id);

            if (courseInstanceModel == null)
            {
                return NotFound();
            }

            return courseInstanceModel;
        }

        // PUT: api/CourseInstanceModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseInstanceModel(int id, CourseInstanceModel courseInstanceModel)
        {
            if (id != courseInstanceModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(courseInstanceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseInstanceModelExists(id))
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

        // POST: api/CourseInstanceModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourseInstanceModel>> PostCourseInstanceModel(CourseInstanceModel courseInstanceModel)
        {
            _context.CourseInstanceModel.Add(courseInstanceModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseInstanceModel", new { id = courseInstanceModel.Id }, courseInstanceModel);
        }

        // DELETE: api/CourseInstanceModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseInstanceModel>> DeleteCourseInstanceModel(int id)
        {
            var courseInstanceModel = await _context.CourseInstanceModel.FindAsync(id);
            if (courseInstanceModel == null)
            {
                return NotFound();
            }

            _context.CourseInstanceModel.Remove(courseInstanceModel);
            await _context.SaveChangesAsync();

            return courseInstanceModel;
        }

        private bool CourseInstanceModelExists(int id)
        {
            return _context.CourseInstanceModel.Any(e => e.Id == id);
        }
    }
}
