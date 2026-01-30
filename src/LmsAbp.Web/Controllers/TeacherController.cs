using LmsAbp.Teachers;
using LmsAbp.Teachers.LmsAbp.Teachers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Web.Controllers
{
    [Route("Teacher")]
    public class TeacherController : AbpController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(
            string? search = null,
            bool? isActive = null,
            string? specialization = null,
            int? minYears = null,
            string? sortBy = null 
        )
        {
            var result = await _teacherService.GetListAsync(
                new PagedAndSortedResultRequestDto
                {
                    MaxResultCount = 1000
                });

            var query = result.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(t =>
                    (t.FirstName != null && t.FirstName.Contains(search)) ||
                    (t.LastName != null && t.LastName.Contains(search)) ||
                    (t.Email != null && t.Email.Contains(search)) ||
                    (t.NationalId != null && t.NationalId.Contains(search))
                );
            }

            if (isActive.HasValue)
                query = query.Where(t => t.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(specialization))
            {
                specialization = specialization.Trim();
                query = query.Where(t => t.Specialization != null && t.Specialization.Contains(specialization));
            }

            if (minYears.HasValue)
                query = query.Where(t => t.YearsOfExperience >= minYears.Value);

            query = (sortBy ?? nameof(TeacherDto.FirstName)) switch
            {
                nameof(TeacherDto.LastName) => query.OrderBy(t => t.LastName).ThenBy(t => t.FirstName),
                nameof(TeacherDto.HireDate) => query.OrderByDescending(t => t.HireDate),
                nameof(TeacherDto.YearsOfExperience) => query.OrderByDescending(t => t.YearsOfExperience),
                _ => query.OrderBy(t => t.FirstName).ThenBy(t => t.LastName)
            };

            var teachers = query.ToList();
            return View(teachers);
        }

        [HttpGet("CreateModal")]
        public IActionResult CreateModal()
        {
            return PartialView("_CreateTeacher", new CreateUpdateTeacherDto
            {
                IsActive = true,
                HireDate = DateTime.Today
            });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateTeacherDto input)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateTeacher", input);

            await _teacherService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("EditModal/{id:guid}")]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var dto = await _teacherService.GetAsync(id);

            ViewBag.Id = id;

            var model = new CreateUpdateTeacherDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalId = dto.NationalId,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Specialization = dto.Specialization,
                YearsOfExperience = dto.YearsOfExperience,
                HireDate = dto.HireDate,
                IsActive = dto.IsActive
            };

            return PartialView("_UpdateTeacher", model);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateUpdateTeacherDto input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return PartialView("_UpdateTeacher", input);
            }

            await _teacherService.UpdateAsync(id, input);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _teacherService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
