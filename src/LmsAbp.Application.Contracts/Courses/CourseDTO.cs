using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LmsAbp.Courses
{
    public class CourseDTO : AuditedEntityDto<Guid>
    {
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int CreditHours { get; set; }
        public bool IsActive { get; set; }
    }
}
