using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LmsAbp.Students
{
    public interface IStudentService :
        ICrudAppService<StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto>
    {
    }
}
