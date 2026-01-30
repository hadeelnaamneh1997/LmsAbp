using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LmsAbp.Teachers
{
    public class CreateUpdateTeacherDto: AuditedEntityDto<Guid>
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
