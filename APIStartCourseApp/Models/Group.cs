using System.ComponentModel.DataAnnotations;

namespace APIStartCourseApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string No { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }

    }
}
