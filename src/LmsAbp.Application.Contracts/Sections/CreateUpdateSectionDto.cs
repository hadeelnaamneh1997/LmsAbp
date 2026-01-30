using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace LmsAbp.Sections
{
    public class CreateUpdateSectionDto: AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(64)] 
        public string? SectionName { get; set; }

        [Required]
        [StringLength(32)]
        public string? Semester { get; set; }

        [Range(1, 500)]
        public int Capacity { get; set; } = 30;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(3);

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public Guid TeacherId { get; set; }

        public List<Guid> StudentIds { get; set; } = new();
    }
}
