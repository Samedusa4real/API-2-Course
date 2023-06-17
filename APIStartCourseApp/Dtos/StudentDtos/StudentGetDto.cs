namespace APIStartCourseApp.Dtos.StudentDtos
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int AveragePoint { get; set; }
        public string Email { get; set; }
        public GroupInStudentGetDto Group { get; set; }
    }

    public class GroupInStudentGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
    }
}
