using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManagerDAL
{
    public class StudentRepository : IStudentRepository
    {
        private SqlConnection _dbConnection;

        private void FillCreateParameters(SqlCommand command, Student student)
        {
            command.Parameters.Add(command.CreateNCharLength("@lastName", student.LastName, 50));
            command.Parameters.Add(command.CreateNCharLength("@firstName", student.FirstName, 50));
            command.Parameters.Add(command.CreateNCharLength("@patronymic", student.Patronymic, 50));
            command.Parameters.Add(command.CreateCharLength("@phone", student.Phone, 10));
            command.Parameters.Add(command.CreateInt("@classId", student.ClassId));
        }

        private void FillEditParameters(SqlCommand command, Student student)
        {
            command.Parameters.Add(command.CreateInt("@id", student.Id));
            FillCreateParameters(command, student);
        }

        private void FillGetStudentsParameters(SqlCommand command, PaginationParameters paginationParameters)
        {
            command.Parameters.Add(command.CreateInt("@pageNumber", paginationParameters.PageNumber));
            command.Parameters.Add(command.CreateInt("@pageSize", paginationParameters.PageSize));
        }

        private void FillStudentIdParameters(SqlCommand command, int id)
        {
            command.Parameters.Add(command.CreateInt("@id", id));
        }

        private StudentWithSchool ReadStudent(IDataReader reader)
        {
            int id = reader.GetInt32(0);
            string lastName = reader.GetString(1).Trim();
            string firstName = reader.GetString(2).Trim();
            string patronymic = reader.GetString(3).Trim();
            string phone = reader.GetString(4).Trim();
            string @class = reader.GetString(5).Trim();
            string school = reader.GetString(6).Trim();
            int classId = reader.GetInt32(7);
            int schoolId = reader.GetInt32(8);
            return new StudentWithSchool(id, firstName, lastName, patronymic, phone, classId, @class, school, schoolId);
        }

        public StudentRepository(SqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Edit(Student model)
        {
            SqlCommand editCommand = _dbConnection.CreateProcedureCommand("EditStudent");
            FillEditParameters(editCommand, model);
            editCommand.ExecuteNonQuery();
        }

        public int Create(Student model)
        {
            SqlCommand createCommand = _dbConnection.CreateProcedureCommand("CreateStudent");
            FillCreateParameters(createCommand, model);
            using (IDataReader reader = createCommand.ExecuteReader())
            {
                if (!reader.Read())
                    throw new Exception("Can't read last row id");

                return (int) reader.GetDecimal(0);
            }
        }

        public StudentWithSchoolList GetStudents(PaginationParameters paginationParameters)
        {
            SqlCommand command = _dbConnection.CreateProcedureCommand("GetStudents");
            FillGetStudentsParameters(command, paginationParameters);

            using (IDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                    throw new Exception("Can't read page count");

                int pageCount = reader.GetInt32(0);
                int pageSize = reader.GetInt32(1);
                int pageNumber = reader.GetInt32(2);
                PaginationInfo paginationInfo = new PaginationInfo(pageCount, pageSize, pageNumber);

                reader.NextResult();

                List<StudentWithSchool> students = new List<StudentWithSchool>();
              
                while (reader.Read())
                {
                    StudentWithSchool student = ReadStudent(reader);
                    students.Add(student);
                }
                return new StudentWithSchoolList(students.ToArray(), paginationInfo);
            }
        }

        public StudentWithSchool GetStudent(int id)
        {
            SqlCommand command = _dbConnection.CreateProcedureCommand("GetStudent");
            FillStudentIdParameters(command, id);

            using (IDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                    throw new Exception($"Student not found. Student id: [{id}]");

                StudentWithSchool student = ReadStudent(reader);
                return student;
            }
        }

        public void Delete(int id)
        {
            SqlCommand deleteCommand = _dbConnection.CreateProcedureCommand("DeleteStudent");
            FillStudentIdParameters(deleteCommand, id);
            deleteCommand.ExecuteNonQuery();
        }
    }
}
