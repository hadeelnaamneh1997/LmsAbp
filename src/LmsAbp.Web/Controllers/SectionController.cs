using LmsAbp.Courses;
using LmsAbp.Sections;
using LmsAbp.Students;
using LmsAbp.Teachers;
using LmsAbp.Teachers.LmsAbp.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Web.Controllers
{
    [Route("Section")]
    public class SectionController : AbpController
    {
        private readonly ISectionService _sectionService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public SectionController(
            ISectionService sectionService,
            ICourseService courseService,
            ITeacherService teacherService,
            IStudentService studentService)
        {
            _sectionService = sectionService;
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        private async Task FillLookupsAsync()
        {
            var courses = await _courseService.GetListAsync(new PagedAndSortedResultRequestDto
            {
                MaxResultCount = 1000,
                Sorting = "CourseName"
            });

            var teachers = await _teacherService.GetListAsync(new PagedAndSortedResultRequestDto
            {
                MaxResultCount = 1000,
                Sorting = "FirstName"
            });

            var students = await _studentService.GetListAsync(new PagedAndSortedResultRequestDto
            {
                MaxResultCount = 1000,
                Sorting = "FirstName"
            });

            ViewBag.Courses = courses.Items
                .OrderBy(c => c.CourseName)
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CourseName
                })
                .ToList();

            ViewBag.Teachers = teachers.Items
                .OrderBy(t => t.FirstName).ThenBy(t => t.LastName)
                .Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}".Trim()
                })
                .ToList();

            ViewBag.Students = students.Items
                .OrderBy(s => s.FirstName).ThenBy(s => s.LastName)
                .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.LastName}".Trim()
                })
                .ToList();
        }


        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(
     string? search = null,
     string? semester = null,
     Guid? courseId = null,
     Guid? teacherId = null,
     bool? isFull = null,
     string? sortBy = null
 )
        {
            var sections = await _sectionService.GetListAsync(new PagedAndSortedResultRequestDto
            {
                MaxResultCount = 1000
            });

            var query = sections.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x => x.SectionName != null && x.SectionName.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(semester))
            {
                semester = semester.Trim();
                query = query.Where(x => x.Semester != null && x.Semester.Contains(semester));
            }

            if (courseId.HasValue)
                query = query.Where(x => x.CourseId == courseId.Value);

            if (teacherId.HasValue)
                query = query.Where(x => x.TeacherId == teacherId.Value);

            if (isFull.HasValue)
            {
                if (isFull.Value)
                {
                    query = query.Where(x =>
                        x.StudentIds != null && x.StudentIds.Count >= x.Capacity
                    );
                }
                else
                {
                    query = query.Where(x =>
                        x.StudentIds == null || x.StudentIds.Count < x.Capacity
                    );
                }
            }

            query = (sortBy ?? "SectionName") switch
            {
                "StartDate" => query.OrderByDescending(x => x.StartDate).ThenBy(x => x.SectionName),
                "EndDate" => query.OrderByDescending(x => x.EndDate).ThenBy(x => x.SectionName),
                "Capacity" => query.OrderByDescending(x => x.Capacity).ThenBy(x => x.SectionName),
                _ => query.OrderBy(x => x.SectionName)
            };

            await FillLookupsAsync();
            return View(query.ToList());
        }

        [HttpGet("CreateModal")]
        public async Task<IActionResult> CreateModal()
        {
            await FillLookupsAsync();

            return PartialView("_CreateSection", new CreateUpdateSectionDto
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                Capacity = 30
            });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateSectionDto input)
        {
            if (!ModelState.IsValid)
            {
                await FillLookupsAsync();
                return PartialView("_CreateSection", input);
            }

            await _sectionService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("EditModal/{id:guid}")]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var dto = await _sectionService.GetAsync(id);

            await FillLookupsAsync();
            ViewBag.SectionId = id;

            var model = new CreateUpdateSectionDto
            {
                SectionName = dto.SectionName,
                Semester = dto.Semester,
                Capacity = dto.Capacity,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CourseId = dto.CourseId,
                TeacherId = dto.TeacherId,
                StudentIds = dto.StudentIds ?? new List<Guid>()
            };

            return PartialView("_UpdateSection", model);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateUpdateSectionDto input)
        {
            if (!ModelState.IsValid)
            {
                await FillLookupsAsync();
                ViewBag.SectionId = id;
                return PartialView("_UpdateSection", input);
            }

            await _sectionService.UpdateAsync(id, input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("DeleteModal/{id:guid}")]
        public async Task<IActionResult> DeleteModal(Guid id)
        {
            var dto = await _sectionService.GetAsync(id);
            return PartialView("_DeleteSection", dto);
        }

        [HttpPost("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sectionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("StudentsModal/{id:guid}")]
        public async Task<IActionResult> StudentsModal(Guid id)
        {
            var dto = await _sectionService.GetAsync(id);
            await FillLookupsAsync();

            ViewBag.SectionId = id;
            ViewBag.SelectedStudentIds = dto.StudentIds ?? new List<Guid>();

            return PartialView("_AddStudentsSection", dto);
        }

        [HttpPost("AddStudents/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudents(Guid id, [FromForm] List<Guid> selectedStudentIds)
        {
            var dto = await _sectionService.GetAsync(id);

            var input = new CreateUpdateSectionDto
            {
                SectionName = dto.SectionName,
                Semester = dto.Semester,
                Capacity = dto.Capacity,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CourseId = dto.CourseId,
                TeacherId = dto.TeacherId,
                StudentIds = selectedStudentIds ?? new List<Guid>()
            };

            await _sectionService.UpdateAsync(id, input);
            return RedirectToAction(nameof(Index));
        }
    }
}
