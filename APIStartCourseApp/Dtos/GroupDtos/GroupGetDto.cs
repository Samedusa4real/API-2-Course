namespace APIStartCourseApp.Dtos.GroupDtos
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public List<StudentItemInGroupGetDto> Students { get; set; }
    }

    public class StudentItemInGroupGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int AveragePoint { get; set; }
    }
}
