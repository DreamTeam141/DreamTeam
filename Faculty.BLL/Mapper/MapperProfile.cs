using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.DAL.Entities;
using Faculty.Logger.Entities;

namespace Faculty.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().MaxDepth(3).ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(x => x.Id, v => v.Ignore());
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<Course, CourseDTO>().MaxDepth(3);
            CreateMap<CourseDTO, Course>().MaxDepth(3)
                .ForMember(dest => dest.Teacher, opt => opt.Ignore());
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<CourseStudent, CourseStudentDTO>().MaxDepth(4).ReverseMap();
            CreateMap<CourseStudent, CourseStudentDTO>().MaxDepth(4).ReverseMap();
            CreateMap<ExceptionDetail, ExceptionDetailDTO>().ReverseMap();
            CreateMap<ActionDetail, ActionDetailDTO>().ReverseMap();
        }
    }
}