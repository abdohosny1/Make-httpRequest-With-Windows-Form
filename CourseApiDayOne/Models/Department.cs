using DayOne.Models;

namespace CourseApiDayOne.Models
{
    public class Department
    {

        public Department()
        {
            Students = new List<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Loaction { get; set; }

        public List<Student> Students { get; set; }

    }
}
