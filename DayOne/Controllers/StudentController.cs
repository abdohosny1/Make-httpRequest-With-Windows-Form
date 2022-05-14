using DayOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DayOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static List<Student> Students = new List<Student>()
        {
            new Student(){Id=1,Name="ali",Age=20,City="cairo"},
            new Student(){Id=2,Name="mohamed",Age=22,City="alex"},
            new Student(){Id=3,Name="mona",Age=20,City="mansoura"},
            new Student(){Id=4,Name="ola",Age=21,City="cairo"}
        };


        public IActionResult getAll()
        {
            var res=Students.ToList();
            return Ok(res);

        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (id == null) return NotFound($"Not Found   Id {id}");

            var sts =Students.Find(e=>e.Id==id);
            if (sts == null) return NotFound($"Not Found Sudent with Id {id}");
            return Ok(sts);
        }

        [Route("api/sts/{name}")]
        [HttpGet]
        public IActionResult studentbyname(string name)
        {
            Student s = Students.Find(n => n.Name == name);
            if (s == null)
                return NotFound();
            else
                return Ok(s);
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student == null) return NotFound($"Not Found ");
            Students.Add(student);
            return Ok(student);

        }
        [HttpPut]
        public IActionResult Update(int id ,Student student)
        {
            if(student.Id!=id) return NotFound($"Not Found ");
            var res = Students.Find(n => n.Id == id);
            if (res == null) return NotFound("Not Found");
            res.Name = student.Name;
            res.Age=student.Age;
            res.City=student.City;

            return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound($"Not Found   Id {id}");

            var sts = Students.Find(e => e.Id == id);
            if (sts == null) return NotFound($"Not Found Sudent with Id {id}");

            Students.Remove(sts);
            return Ok(sts);

        }





    }
}
