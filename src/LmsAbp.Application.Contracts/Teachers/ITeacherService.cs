using System;
using System.Collections.Generic;
using System.Text;

namespace LmsAbp.Teachers
{
    using System;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;

    namespace LmsAbp.Teachers
    {
        public interface ITeacherService :
            ICrudAppService<TeacherDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTeacherDto>
        {
        }
    }

}
