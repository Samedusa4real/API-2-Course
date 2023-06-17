using APIStartCourseApp.DAL;
using APIStartCourseApp.Dtos.StudentDtos;
using APIStartCourseApp.Dtos.TeacherDtos;
using APIStartCourseApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIStartCourseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public TeachersController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public ActionResult<List<TeacherGetAllItemDto>> GetAll()
        {
            var data = _context.Teachers
                .Include(x => x.Group)
                .Select(x => new TeacherGetAllItemDto { Id = x.Id, FullName = x.FullName, Group = new GroupInTeachersDto { Id = x.GroupId, No = x.Group.No, GroupStudents = x.Group.Students
                .Select(x => new StudentsInGroup { Id = x.Id, AveragePoint = x.AveragePoint, Email = x.Email, Name = x.Name, SurName = x.SurName }).ToList() } }).ToList();


            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<TeacherGetDto> Get(int id)
        {
            var data = _context.Teachers.Include(x=>x.Group).FirstOrDefault(x=>x.Id == id);

            if (data == null)
                return NotFound();

            var teacherDto = new TeacherGetDto
            {
                Id = id,
                FullName = data.FullName,
                Group = new GroupInTeacherDto
                {
                    Id = data.GroupId,
                    No = data.Group.No,
                    GroupStudents =  _context.Students.Where(x=>x.GroupId == data.GroupId).Select(x => new StudentsInGroup { Id = x.Id, AveragePoint = x.AveragePoint, Email = x.Email, Name = x.Name, SurName = x.SurName }).ToList(),
                }
            };

            return Ok(teacherDto);
        }

        [HttpPost("")]
        public ActionResult Create(TeacherPostDto teacherPostDto)
        {
            if (!_context.Groups.Any(x => x.Id == teacherPostDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group is not exist!");
                return BadRequest(ModelState);
            }

            Teacher teacher = new Teacher
            {
                FullName = teacherPostDto.FullName,
                GroupId = teacherPostDto.GroupId,
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return StatusCode(201, new { Id = teacher.Id});
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, TeacherPutDto teacherPutDto)
        {
            var existingTeacher = _context.Teachers.Find(id);

            if (existingTeacher == null)
                return NotFound();

            if (!_context.Groups.Any(x => x.Id == teacherPutDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group is not exist!");
                return BadRequest(ModelState);
            }

            existingTeacher.FullName = teacherPutDto.FullName;
            existingTeacher.GroupId = teacherPutDto.GroupId;

            _context.SaveChanges();

            return Ok(existingTeacher);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var teacher = _context.Teachers.Find(id);

            if (teacher == null)
                return NotFound();

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
