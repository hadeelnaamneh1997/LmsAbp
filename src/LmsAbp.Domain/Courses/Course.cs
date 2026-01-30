using LmsAbp.Sections;
using LmsAbp.Students;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace LmsAbp.Courses
{
    public class Course : FullAuditedAggregateRoot<Guid>
    {
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int CreditHours { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Section> Sections { get; set; } = new List<Section>();

    }
}
