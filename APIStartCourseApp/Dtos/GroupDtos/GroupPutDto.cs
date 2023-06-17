using System.ComponentModel.DataAnnotations;

namespace APIStartCourseApp.Dtos.GroupDtos
{
    public class GroupPutDto
    {
        [Required]
        [MaxLength(50)]
        public string No { get; set; }
    }
}
