
namespace SchoolManagerDAL
{
    public interface IStudentRepository : IRepository<Student>
    {
        StudentWithSchoolList GetStudents(PaginationParameters paginationParameters);
        StudentWithSchool GetStudent(int id);
    }
}
