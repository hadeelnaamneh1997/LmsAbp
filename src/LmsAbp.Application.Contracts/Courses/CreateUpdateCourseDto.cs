using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LmsAbp.Courses
{
    public class CreateUpdateCourseDto: AuditedEntityDto<Guid>
    {
        public string? CourseCode { get; set; }

        [Required]
        [StringLength(128)]
        public string? CourseName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 10)]
        public int CreditHours { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
