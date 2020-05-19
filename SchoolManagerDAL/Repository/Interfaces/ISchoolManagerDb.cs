using System;

namespace SchoolManagerDAL
{
    public interface ISchoolManagerDb : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        IClassRepository ClassRepository { get; }
        IDisposable OpenConnection();
        void CloseConnection();
    }
}
