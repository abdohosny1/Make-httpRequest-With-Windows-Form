using CourseApiDayOne;
using CourseApiDayOne.service.student;
using DayOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;

namespace DayOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllStudent()
        {

            var res= await _context.Students.ToListAsync();
           // if (res.Count > 0) return BadRequest("List Is Empty");
            return Ok(res);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == null) return NotFound($"Not Found   Id {id}");

            var sts = await _context.Students.FindAsync(id);
            if (sts == null) return NotFound($"Not Found Sudent with Id {id}");
            return Ok(sts);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(String name)
        {
            if (name == null) return NotFound($"Not Found  Name {name}");

            var sts = await _context.Students.FirstOrDefaultAsync(e => e.Name == name);
            if (sts == null) return NotFound($"Not Found Sudent with Name");
            return Ok(sts);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return BadRequest("Student Requset is not valid");

            await _context.Students.AddAsync(student);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (StudentExist(student.Id))
                {
                    return Conflict(); //409 object is already find

                }
                else
                {
                    //  return  InternalServerError();
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

            }
            return Ok(student); //200
                                //     return Created("",student); //201

            //  return CreatedAtRoute("DefaultApi", new { Id = student.Id }, student);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != student.Id) return BadRequest("Pk Is wrong ");

            _context.Students.Update(student);

            try
            {
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                if (!StudentExist(student.Id))
                {
                    return NotFound("Not Found");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError); //500
                }

            }

            return NoContent(); //201
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound($"Not Found  Id {id}");

            var sts = await _context.Students.FindAsync(id);
            if (sts == null) return NotFound($"Not Found Sudent with Id {id}");

            _context.Students.Remove(sts);
            await _context.SaveChangesAsync();
            return Ok(sts);

        }


        private bool StudentExist(int id)
        {
            return _context.Students.Count(e => e.Id == id) > 0;
        }


    }
}
