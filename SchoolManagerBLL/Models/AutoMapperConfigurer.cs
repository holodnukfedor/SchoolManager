using System;
using AutoMapper;
using SchoolManagerDAL;

namespace SchoolManagerBLL
{
    public class AutoMapperConfigurer
    {
        public static Lazy<IMapper> Mapper = new Lazy<IMapper>(GetAutoMapper);

        private static IMapper GetAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<StudentDTO, Student>();
            });
            return config.CreateMapper();
        }
    }
}
