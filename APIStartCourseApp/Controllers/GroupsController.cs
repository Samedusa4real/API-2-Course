using Microsoft.AspNetCore.Http;
using APIStartCourseApp.DAL;
using APIStartCourseApp.Models;
using Microsoft.AspNetCore.Mvc;
using APIStartCourseApp.Dtos.GroupDtos;
using Microsoft.EntityFrameworkCore;
using APIStartCourseApp.Dtos;

namespace APIStartCourseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public GroupsController(CourseDbContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public ActionResult<List<GetAllItemDto>> GetAll()
        {
            var data = _context.Groups.Include(x => x.Students).Select(x => new GetAllItemDto { Id = x.Id, No = x.No, StudentsCount = x.Students.Count }).ToList();
            return Ok(data);
        }

        [HttpGet("")]
        public ActionResult<List<GetAllItemDto>> GetAll(int page = 1)
        {
            var query = _context.Groups.Include(x => x.Students).AsQueryable();

            var items = query.Skip((page - 1) * 4).Take(4).Select(x => new GetAllItemDto { Id = x.Id, No = x.No, StudentsCount = x.Students.Count }).ToList();
            var totalPages = (int)Math.Ceiling(query.Count() / 4d);

            var data = new PaginatedListDto<GetAllItemDto>(items, totalPages, page);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            var data = _context.Groups.Include(x=>x.Students).FirstOrDefault(x=>x.Id == id);

            if (data == null)
                return NotFound();

            GroupGetDto groupGetDto = new GroupGetDto
            {
                Id = data.Id,
                No = data.No,
                Students = data.Students.Select(x => new StudentItemInGroupGetDto { Id = x.Id, AveragePoint = x.AveragePoint, Name = x.Name, SurName = x.SurName }).ToList(), 
            };

            return Ok(groupGetDto);
        }

        [HttpPost("")]
        public ActionResult Create(GroupPostDto groupPostDto)
        {
            if (_context.Groups.Any(x => x.No == groupPostDto.No))
            {
                ModelState.AddModelError("No", "No is already exist");
                return BadRequest(ModelState);
            }

            Group group = new Group
            {
                No = groupPostDto.No,
            };
            
            _context.Groups.Add(group);
            _context.SaveChanges();

            return StatusCode(201, new { Id = group.Id });
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, GroupPutDto groupPutDto)
        {
            var existingGroup = _context.Groups.Find(id);

            if (existingGroup == null)
                return NotFound();

            if (existingGroup.No != groupPutDto.No && _context.Groups.Any(x=>x.No == groupPutDto.No))
            {
                ModelState.AddModelError("No", "No is already exist");
                return BadRequest(ModelState);
            }

            existingGroup.No = groupPutDto.No;

            _context.SaveChanges();

            return Ok(existingGroup);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = _context.Groups.Find(id);

            if (group == null)
                return NotFound();

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
