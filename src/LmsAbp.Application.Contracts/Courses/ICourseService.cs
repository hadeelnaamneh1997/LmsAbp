using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LmsAbp.Courses
{
    public interface ICourseService :
        ICrudAppService<CourseDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCourseDto>
    {
    }
}
