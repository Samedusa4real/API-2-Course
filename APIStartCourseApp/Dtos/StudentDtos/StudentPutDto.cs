using System.ComponentModel.DataAnnotations;

namespace APIStartCourseApp.Dtos.StudentDtos
{
    public class StudentPutDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string SurName { get; set; }
        [Required]
        [Range(1, 100)]
        public int AveragePoint { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public int GroupId { get; set; }
    }
}
