using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SchoolManager.Models;
using SchoolManagerBLL;
using SchoolManagerDAL;

namespace SchoolManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentWithSchool, StudentViewModel>();
            CreateMap<StudentWithSchoolList, StudentViewModelList>();
            CreateMap<IdNameTuple, OptionViewModel>()
                .ForMember(x => x.value, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.text, opt => opt.MapFrom(x => x.Name));
            CreateMap<StudentViewModel, StudentDTO>();
        }
    }
}
