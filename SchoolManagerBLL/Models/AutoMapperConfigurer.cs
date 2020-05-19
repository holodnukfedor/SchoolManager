using AutoMapper;
using SchoolManagerDAL;

namespace SchoolManagerBLL
{
    public class AutoMapperConfigurer
    {
        public static IMapper Mapper { get; }

        static AutoMapperConfigurer()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<StudentDTO, Student>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
