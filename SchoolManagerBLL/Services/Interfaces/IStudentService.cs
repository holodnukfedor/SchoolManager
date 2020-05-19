using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagerDAL;

namespace SchoolManagerBLL
{
    public interface IStudentService
    {
        StudentWithSchoolList GetStudents(PaginationParameters paginationParameters);
        void Edit(StudentDTO student);
        StudentWithSchool GetStudent(int id);
        StudentDTO Create(StudentDTO student);
        void Delete(int id);
    }
}
