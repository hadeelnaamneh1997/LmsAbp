using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LmsAbp.Courses
{
    public class CourseService :
        CrudAppService<Course, CourseDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCourseDto>,
        ICourseService
    {
        public CourseService(IRepository<Course, Guid> repository)
            : base(repository)
        {
        }

        protected override CourseDTO MapToGetOutputDto(Course entity)
        {
            return new CourseDTO
            {
                Id = entity.Id,
                CourseCode = entity.CourseCode,
                CourseName = entity.CourseName,
                Description = entity.Description,
                CreditHours = entity.CreditHours,
                IsActive = entity.IsActive
            };
        }

        protected override Course MapToEntity(CreateUpdateCourseDto createInput)
        {
            return new Course
            {
                CourseCode = createInput.CourseCode,
                CourseName = createInput.CourseName,
                Description = createInput.Description,
                CreditHours = createInput.CreditHours,
                IsActive = createInput.IsActive
            };
        }

        protected override void MapToEntity(CreateUpdateCourseDto updateInput, Course entity)
        {
            entity.CourseCode = updateInput.CourseCode;
            entity.CourseName = updateInput.CourseName;
            entity.Description = updateInput.Description;
            entity.CreditHours = updateInput.CreditHours;
            entity.IsActive = updateInput.IsActive;
        }
    }
}
