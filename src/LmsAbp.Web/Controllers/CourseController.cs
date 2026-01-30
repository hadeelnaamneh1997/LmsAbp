using System;
using System.Linq;
using System.Threading.Tasks;
using LmsAbp.Courses;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Web.Controllers
{
    [Route("Course")]
    public class CourseController : AbpController
    {
        private readonly ICourseService _courseAppService;

        public CourseController(ICourseService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(
            string? search = null,
            bool? isActive = null,
            int? minCreditHours = null,
            int? maxCreditHours = null,
            string? sortBy = null 
        )
        {
            var result = await _courseAppService.GetListAsync(new PagedAndSortedResultRequestDto
            {
                MaxResultCount = 1000
            });

            var query = result.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(c =>
                    (c.CourseName != null && c.CourseName.Contains(search)) ||
                    (c.CourseCode != null && c.CourseCode.Contains(search)) ||
                    (c.Description != null && c.Description.Contains(search))
                );
            }

            if (isActive.HasValue)
                query = query.Where(c => c.IsActive == isActive.Value);

            if (minCreditHours.HasValue)
                query = query.Where(c => c.CreditHours >= minCreditHours.Value);

            if (maxCreditHours.HasValue)
                query = query.Where(c => c.CreditHours <= maxCreditHours.Value);

            query = (sortBy ?? nameof(CourseDTO.CourseName)) switch
            {
                nameof(CourseDTO.CourseCode) => query.OrderBy(c => c.CourseCode).ThenBy(c => c.CourseName),
                nameof(CourseDTO.CreditHours) => query.OrderByDescending(c => c.CreditHours).ThenBy(c => c.CourseName),
                _ => query.OrderBy(c => c.CourseName)
            };

            var courses = query.ToList();
            return View(courses);
        }

        [HttpGet("CreateModal")]
        public IActionResult CreateModal()
        {
            return PartialView("_CreateCourse", new CreateUpdateCourseDto
            {
                IsActive = true
            });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCourseDto input)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateCourse", input);

            await _courseAppService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("EditModal/{id:guid}")]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var dto = await _courseAppService.GetAsync(id);

            ViewBag.Id = id;

            var model = new CreateUpdateCourseDto
            {
                CourseCode = dto.CourseCode,
                CourseName = dto.CourseName,
                Description = dto.Description,
                CreditHours = dto.CreditHours,
                IsActive = dto.IsActive
            };

            return PartialView("_UpdateCourse", model);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateUpdateCourseDto input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return PartialView("_UpdateCourse", input);
            }

            await _courseAppService.UpdateAsync(id, input);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseAppService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
