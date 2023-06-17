using System.ComponentModel.DataAnnotations;

namespace APIStartCourseApp.Dtos.TeacherDtos
{
    public class TeacherPostDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        public int GroupId { get; set; }
    }
}
