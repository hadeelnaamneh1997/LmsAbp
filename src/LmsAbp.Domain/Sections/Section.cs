using LmsAbp.Courses;
using LmsAbp.Students;
using LmsAbp.Teachers;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace LmsAbp.Sections
{
    public class Section : FullAuditedAggregateRoot<Guid>
    {
        public string? SectionName { get; set; }
        public string? Semester { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
