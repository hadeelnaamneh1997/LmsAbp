using System;
using System.Linq;
using System.Threading.Tasks;
using LmsAbp.Students;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Web.Controllers
{
    [Route("Student")]
    public class StudentController : AbpController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(
            string? search = null,
            bool? isActive = null,
            string? gender = null,
            decimal? minGpa = null,
            string? sortBy = null 
        )
        {
            var result = await _studentService.GetListAsync(
                new PagedAndSortedResultRequestDto
                {
                    MaxResultCount = 1000
                });

            var query = result.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(s =>
                    (s.NationalNumber != null && s.NationalNumber.Contains(search)) ||
                    (s.FirstName != null && s.FirstName.Contains(search)) ||
                    (s.LastName != null && s.LastName.Contains(search)) ||
                    (s.Email != null && s.Email.Contains(search)) ||
                    (s.PhoneNumber != null && s.PhoneNumber.Contains(search))
                );
            }

            if (isActive.HasValue)
                query = query.Where(s => s.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(gender))
            {
                gender = gender.Trim();
                query = query.Where(s => s.Gender != null && s.Gender.ToString().Contains(gender));
            }

            if (minGpa.HasValue)
                query = query.Where(s => s.GPA >= (double)minGpa.Value);

            query = (sortBy ?? nameof(StudentDto.FirstName)) switch
            {
                nameof(StudentDto.LastName) => query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName),
                nameof(StudentDto.EnrollmentDate) => query.OrderByDescending(s => s.EnrollmentDate),
                nameof(StudentDto.GPA) => query.OrderByDescending(s => s.GPA),
                _ => query.OrderBy(s => s.FirstName).ThenBy(s => s.LastName)
            };

            var students = query.ToList();
            return View(students);
        }

        [HttpGet("CreateModal")]
        public IActionResult CreateModal()
        {
            return PartialView("_CreateStudent", new CreateUpdateStudentDto
            {
                IsActive = true,
                EnrollmentDate = DateTime.Today
            });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateStudentDto input)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateStudent", input);

            await _studentService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("EditModal/{id:guid}")]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var dto = await _studentService.GetAsync(id);

            ViewBag.Id = id;

            var model = new CreateUpdateStudentDto
            {
                NationalNumber = dto.NationalNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                EnrollmentDate = dto.EnrollmentDate,
                GPA = dto.GPA,
                IsActive = dto.IsActive
            };

            return PartialView("_UpdateStudent", model);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateUpdateStudentDto input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return PartialView("_UpdateStudent", input);
            }

            await _studentService.UpdateAsync(id, input);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _studentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
