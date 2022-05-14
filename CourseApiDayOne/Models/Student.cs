using CourseApiDayOne.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayOne.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }

        public Department Department { get; set; }
    }
}
