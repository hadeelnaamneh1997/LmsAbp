using LmsAbp.Teachers.LmsAbp.Teachers;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LmsAbp.Teachers
{
    public class TeacherService :
        CrudAppService<Teacher, TeacherDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTeacherDto>,ITeacherService
    {
        public TeacherService(IRepository<Teacher, Guid> repository)
            : base(repository)
        {
        }

        protected override TeacherDto MapToGetOutputDto(Teacher entity)
        {
            return new TeacherDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                NationalId = entity.NationalId,
                DateOfBirth = entity.DateOfBirth,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Specialization = entity.Specialization,
                YearsOfExperience = entity.YearsOfExperience,
                HireDate = entity.HireDate,
                IsActive = entity.IsActive
            };
        }

        protected override Teacher MapToEntity(CreateUpdateTeacherDto createInput)
        {
            return new Teacher
            {
                FirstName = createInput.FirstName,
                LastName = createInput.LastName,
                NationalId = createInput.NationalId,
                DateOfBirth = createInput.DateOfBirth,
                Email = createInput.Email,
                PhoneNumber = createInput.PhoneNumber,
                Specialization = createInput.Specialization,
                YearsOfExperience = createInput.YearsOfExperience,
                HireDate = createInput.HireDate,
                IsActive = createInput.IsActive
            };
        }

        protected override void MapToEntity(CreateUpdateTeacherDto updateInput, Teacher entity)
        {
            entity.FirstName = updateInput.FirstName;
            entity.LastName = updateInput.LastName;
            entity.NationalId = updateInput.NationalId;
            entity.DateOfBirth = updateInput.DateOfBirth;
            entity.Email = updateInput.Email;
            entity.PhoneNumber = updateInput.PhoneNumber;
            entity.Specialization = updateInput.Specialization;
            entity.YearsOfExperience = updateInput.YearsOfExperience;
            entity.HireDate = updateInput.HireDate;
            entity.IsActive = updateInput.IsActive;
        }
    }
}
