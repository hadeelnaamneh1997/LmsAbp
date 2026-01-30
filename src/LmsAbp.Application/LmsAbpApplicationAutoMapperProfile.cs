using AutoMapper;
using LmsAbp.Courses;
using LmsAbp.Sections;
using LmsAbp.Students;
using LmsAbp.Teachers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LmsAbp
{
    public class LmsAbpApplicationAutoMapperProfile : Profile
    {
        public LmsAbpApplicationAutoMapperProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CreateUpdateCourseDto, Course>();
            CreateMap<Student, StudentDto>();
            CreateMap<CreateUpdateStudentDto, Student>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<CreateUpdateTeacherDto, Teacher>();
            CreateMap<Section, SectionDto>();
            CreateMap<CreateUpdateSectionDto, Section>();
        }
    }
}
