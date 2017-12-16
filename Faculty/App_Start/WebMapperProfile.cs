using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.Models;

namespace Faculty
{
    public class WebMapperProfile: Profile
    {
        public WebMapperProfile()
        {
            CreateMap<UserDTO, UserViewModel>().MaxDepth(4).ReverseMap();
            CreateMap<SubjectDTO, SubjectViewModel>().ReverseMap();
            CreateMap<CourseDTO, CourseViewModel>()
                .ForMember(dest => dest.Subject, opt => opt.Ignore())
                .ForMember(dest => dest.Teachers, opt => opt.Ignore())
                .ForMember(dest => dest.CourseStatus, opt => opt.Ignore());
            CreateMap<CourseViewModel, CourseDTO>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => new SubjectDTO() {Id = src.SubjectId}))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => new TeacherDTO() {Id = src.TeacherId}))
                .ForMember(dest => dest.CourseStatus, opt => opt.MapFrom(src => src.CourseStatusId));
            CreateMap<CourseDTO, CourseDetailsViewModel>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Title))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher.ApplicationUser.SecondName + " "
                                                                           + src.Teacher.ApplicationUser.FirstName +
                                                                           " " + src.Teacher.ApplicationUser
                                                                               .Patronymic));
            CreateMap<StudentDTO, StudentViewModel>().MaxDepth(4).ReverseMap();
            CreateMap<TeacherDTO, TeacherViewModel>().MaxDepth(4).ReverseMap();
            CreateMap<CourseStudentDTO, CourseStudentViewModel>().MaxDepth(4).ReverseMap();
            CreateMap<ExceptionDetailDTO, ExceptionDetailViewModel>().ReverseMap();
            CreateMap<ActionDetailDTO, ActionDetailViewModel>().ReverseMap();

        }
    }
}