namespace APIStartCourseApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int AveragePoint { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
