
namespace SchoolManagerDAL
{
    public class StudentWithSchoolList
    {
        public StudentWithSchool[] Students { get; }
        public PaginationInfo PaginationInfo { get; }

        public StudentWithSchoolList(StudentWithSchool[] students, PaginationInfo paginationInfo)
        {
            Students = students;
            PaginationInfo = paginationInfo;
        }
    }
}
