namespace APIStartCourseApp.Dtos.StudentDtos
{
    public class StudentGetAllItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int AveragePoint { get; set; }
        public string Email { get; set; }
        public GroupInStudentsDto Group { get; set; }
    }

    public class GroupInStudentsDto
    {
        public int Id { get; set; }
        public string No { get; set; }
    }
}
