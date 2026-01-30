using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LmsAbp.Sections
{
    public class SectionDto : AuditedEntityDto<Guid>
    {
        public string? SectionName { get; set; }
        public string? Semester { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }

        public List<Guid> StudentIds { get; set; } = new();
    }
}
