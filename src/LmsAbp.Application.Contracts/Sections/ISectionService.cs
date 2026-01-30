using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LmsAbp.Sections
{
    public interface ISectionService :
        ICrudAppService<
            SectionDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSectionDto>
    {
    }
}
