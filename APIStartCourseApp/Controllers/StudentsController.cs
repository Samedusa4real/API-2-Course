using APIStartCourseApp.DAL;
using APIStartCourseApp.Dtos.StudentDtos;
using APIStartCourseApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIStartCourseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public StudentsController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public ActionResult<List<StudentGetAllItemDto>> GetAll()
        {
            var data = _context.Students.Include(x => x.Group).Select(x => new StudentGetAllItemDto { Id = x.Id, AveragePoint = x.AveragePoint, Email = x.Email, Name = x.Name, SurName = x.SurName, Group = new GroupInStudentsDto { Id = x.GroupId, No = x.Group.No } }).ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            var data = _context.Students.Include(x=>x.Group).FirstOrDefault(x=>x.Id == id);

            if (data == null)
                return NotFound();

            var studentDto = new StudentGetDto
            {
                Id = id,
                Name = data.Name,
                SurName = data.SurName,
                Email = data.Email,
                AveragePoint = data.AveragePoint,
                Group = new GroupInStudentGetDto
                {
                    Id = data.GroupId,
                    No = data.Group.No
                }
            };

            return Ok(studentDto);
        }

        [HttpPost("")]
        public ActionResult Create(StudentPostDto studentPostDto)
        {
            if (!_context.Groups.Any(x=>x.Id == studentPostDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group is not exist!");
                return BadRequest(ModelState);
            }

            if (_context.Students.Any(x => x.Email == studentPostDto.Email))
            {
                ModelState.AddModelError("Email", "This Email is taken!");
                return BadRequest(ModelState);
            }

            Student student = new Student
            {
                Name = studentPostDto.Name,
                SurName = studentPostDto.SurName,
                Email = studentPostDto.Email,
                AveragePoint = studentPostDto.AveragePoint,
                GroupId = studentPostDto.GroupId,
            };
            
            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(201, new { Id = student.Id});
        
        }


        [HttpPut("{id}")]
        public ActionResult Edit(int id, StudentPutDto studentPutDto)
        {
            var existingStudent = _context.Students.Find(id);

            if (existingStudent == null)
                return NotFound();

            if (!_context.Groups.Any(x=>x.Id == studentPutDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group is not exist!");
                return BadRequest(ModelState);
            }

            if (_context.Students.Any(x => x.Email == studentPutDto.Email))
            {
                ModelState.AddModelError("Email", "This Email is taken!");
                return BadRequest(ModelState);
            }

            existingStudent.Name = studentPutDto.Name;
            existingStudent.SurName = studentPutDto.SurName;
            existingStudent.GroupId = studentPutDto.GroupId;

            _context.SaveChanges();

            return Ok(existingStudent);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
