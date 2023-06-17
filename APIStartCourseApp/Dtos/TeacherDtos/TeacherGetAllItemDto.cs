namespace APIStartCourseApp.Dtos.TeacherDtos
{
    public class TeacherGetAllItemDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public GroupInTeachersDto Group { get; set; }
    }

    public class GroupInTeachersDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public List<StudentsInGroup> GroupStudents { get; set; }
    }

    public class StudentsInGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int AveragePoint { get; set; }
        public string Email { get; set; }
    }
}
