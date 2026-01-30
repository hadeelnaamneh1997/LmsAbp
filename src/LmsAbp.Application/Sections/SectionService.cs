using LmsAbp.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LmsAbp.Sections
{
    public class SectionService :
        CrudAppService<
            Section,
            SectionDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSectionDto>,
        ISectionService
    {
        private readonly IRepository<Section, Guid> _sectionRepository;
        private readonly IRepository<Student, Guid> _studentRepository;

        public SectionService(
            IRepository<Section, Guid> sectionRepository,
            IRepository<Student, Guid> studentRepository)
            : base(sectionRepository)
        {
            _sectionRepository = sectionRepository;
            _studentRepository = studentRepository;
        }
        protected override SectionDto MapToGetOutputDto(Section entity)
        {
            return new SectionDto
            {
                Id = entity.Id,
                SectionName = entity.SectionName,
                Semester = entity.Semester,
                Capacity = entity.Capacity,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CourseId = entity.CourseId,
                TeacherId = entity.TeacherId,
                StudentIds = entity.Students != null
                    ? entity.Students.Select(s => s.Id).ToList()
                    : new List<Guid>()
            };
        }

        protected override Section MapToEntity(CreateUpdateSectionDto input)
        {
            return new Section
            {
                SectionName = input.SectionName,
                Semester = input.Semester,
                Capacity = input.Capacity,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CourseId = input.CourseId,
                TeacherId = input.TeacherId
            };
        }

        protected override void MapToEntity(CreateUpdateSectionDto input, Section entity)
        {
            entity.SectionName = input.SectionName;
            entity.Semester = input.Semester;
            entity.Capacity = input.Capacity;
            entity.StartDate = input.StartDate;
            entity.EndDate = input.EndDate;
            entity.CourseId = input.CourseId;
            entity.TeacherId = input.TeacherId;
        }

        public override async Task<SectionDto> CreateAsync(CreateUpdateSectionDto input)
        {
            var entity = MapToEntity(input);

            await SetStudentsAsync(entity, input.StudentIds);

            await _sectionRepository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task<SectionDto> UpdateAsync(Guid id, CreateUpdateSectionDto input)
        {
            var queryable = await _sectionRepository.WithDetailsAsync(x => x.Students);
            var entity = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(Section), id);

            MapToEntity(input, entity);

            await SetStudentsAsync(entity, input.StudentIds);

            await _sectionRepository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task<SectionDto> GetAsync(Guid id)
        {
            var queryable = await _sectionRepository.WithDetailsAsync(x => x.Students);
            var entity = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(Section), id);

            return MapToGetOutputDto(entity);
        }

        public override async Task<PagedResultDto<SectionDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _sectionRepository.WithDetailsAsync(x => x.Students);

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            queryable = ApplySorting(queryable, input);
            queryable = ApplyPaging(queryable, input);

            var entities = await AsyncExecuter.ToListAsync(queryable);
            var items = entities.Select(MapToGetOutputDto).ToList();

            return new PagedResultDto<SectionDto>(totalCount, items);
        }
        private async Task SetStudentsAsync(Section entity, List<Guid>? studentIds)
        {
            entity.Students ??= new List<Student>();
            entity.Students.Clear();

            if (studentIds == null || studentIds.Count == 0)
                return;

            studentIds = studentIds.Distinct().ToList();

            var students = await _studentRepository.GetListAsync(
                s => studentIds.Contains(s.Id)
            );

            if (students.Count != studentIds.Count)
                throw new BusinessException("Some students were not found.");

            foreach (var student in students)
            {
                entity.Students.Add(student);
            }
        }
    }
}
