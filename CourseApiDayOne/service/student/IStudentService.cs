using DayOne.Models;

namespace CourseApiDayOne.service.student
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();


        Task<Student> Add(Student student);
        Task<Student> GetById(int id);
        Student Update(Student student);

        Student delete(Student student);
    }
}
