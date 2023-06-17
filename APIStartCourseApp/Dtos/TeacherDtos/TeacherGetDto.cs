namespace APIStartCourseApp.Dtos.TeacherDtos
{
    public class TeacherGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public GroupInTeacherDto Group { get; set; }
    }

    public class GroupInTeacherDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public List<StudentsInGroup> GroupStudents { get; set; }
    }
}
