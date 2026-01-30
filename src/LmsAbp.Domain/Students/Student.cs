using LmsAbp.Sections;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace LmsAbp.Students
{
    public class Student : FullAuditedAggregateRoot<Guid>
    {
        public string? NationalNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        public double GPA { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();

    }
}
