using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LmsAbp.Students
{
    public class StudentService :
        CrudAppService<Student, StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto>,
        IStudentService
    {
        public StudentService(IRepository<Student, Guid> repository)
            : base(repository)
        {
        }

        protected override StudentDto MapToGetOutputDto(Student entity)
        {
            return new StudentDto
            {
                Id = entity.Id,
                NationalNumber = entity.NationalNumber,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                EnrollmentDate = entity.EnrollmentDate,
                IsActive = entity.IsActive,
                GPA = entity.GPA
            };
        }

        protected override Student MapToEntity(CreateUpdateStudentDto createInput)
        {
            return new Student
            {
                NationalNumber = createInput.NationalNumber,
                FirstName = createInput.FirstName,
                LastName = createInput.LastName,
                DateOfBirth = createInput.DateOfBirth,
                Gender = createInput.Gender,
                Email = createInput.Email,
                PhoneNumber = createInput.PhoneNumber,
                Address = createInput.Address,
                EnrollmentDate = createInput.EnrollmentDate,
                IsActive = createInput.IsActive,
                GPA = createInput.GPA
            };
        }

        protected override void MapToEntity(CreateUpdateStudentDto updateInput, Student entity)
        {
            entity.NationalNumber = updateInput.NationalNumber;
            entity.FirstName = updateInput.FirstName;
            entity.LastName = updateInput.LastName;
            entity.DateOfBirth = updateInput.DateOfBirth;
            entity.Gender = updateInput.Gender;
            entity.Email = updateInput.Email;
            entity.PhoneNumber = updateInput.PhoneNumber;
            entity.Address = updateInput.Address;
            entity.EnrollmentDate = updateInput.EnrollmentDate;
            entity.IsActive = updateInput.IsActive;
            entity.GPA = updateInput.GPA;
        }
    }
}
