using System;
using System.Data.SqlClient;


namespace SchoolManagerDAL
{
    public class SchoolManagerDb : ISchoolManagerDb
    {
        private SqlConnection _dbConnection;
        public IStudentRepository StudentRepository { get; }
        public ISchoolRepository SchoolRepository { get; }
        public IClassRepository ClassRepository { get; }

        public SchoolManagerDb(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.ConnectionString = connectionString;
            StudentRepository = new StudentRepository(_dbConnection);
            SchoolRepository = new SchoolRepository(_dbConnection);
            ClassRepository = new ClassRepository(_dbConnection);
        }

        public IDisposable OpenConnection()
        {
            _dbConnection.Open();
            return this;
        }

        public void CloseConnection()
        {
            _dbConnection.Close();
        }

        public void Dispose()
        {
            _dbConnection.Close();
        }
    }
}
