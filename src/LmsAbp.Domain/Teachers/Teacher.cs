using LmsAbp.Sections;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace LmsAbp.Teachers
{
    public class Teacher : FullAuditedAggregateRoot<Guid>
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();

    }
}
