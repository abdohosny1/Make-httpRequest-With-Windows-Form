using DayOne.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApiDayOne.service.student
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Student> Add(Student student)
        {
            throw new NotImplementedException();
        }

        public Student delete(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var res = await _context.Students.OrderBy(e=>e.Name).ToListAsync();
            return res;
        }

      

        public async Task<Student> GetById(int id)
        {
            var res =await _context.Students.FindAsync(id);
            return res;
        }

        public Student Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
