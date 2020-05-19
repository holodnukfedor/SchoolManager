using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolManagerDAL;

namespace SchoolManagerBLL 
{
    public class StudentService : IStudentService
    {
        private ISchoolManagerDb _schoolManagerDb;

        private void ThrowExIfStudentInvalid(StudentDTO student)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(student);
            if (!Validator.TryValidateObject(student, context, results, true))
            {
                string aggreagatedErrorMsg =String.Join(Environment.NewLine, results.Select(x => x.ErrorMessage));
                throw new ArgumentException(aggreagatedErrorMsg);
            }
        }

        public StudentService(ISchoolManagerDb schoolManagerDb)
        {
            _schoolManagerDb = schoolManagerDb;
        }

        public void Edit(StudentDTO student)
        {
            ThrowExIfStudentInvalid(student);
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                _schoolManagerDb.StudentRepository.Edit(AutoMapperConfigurer.Mapper.Map<Student>(student));
            }
        }

        public StudentWithSchoolList GetStudents(PaginationParameters paginationParameters)
        {
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                return _schoolManagerDb.StudentRepository.GetStudents(paginationParameters);
            }
        }

        public StudentWithSchool GetStudent(int id)
        {
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                return _schoolManagerDb.StudentRepository.GetStudent(id);
            }
        }

        public StudentDTO Create(StudentDTO student)
        {
            ThrowExIfStudentInvalid(student);
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                int id =_schoolManagerDb.StudentRepository.Create(AutoMapperConfigurer.Mapper.Map<Student>(student));
                return new StudentDTO(id, student.FirstName, student.LastName, student.Patronymic, student.Phone, student.ClassId);
            }
        }

        public void Delete(int id)
        {
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                _schoolManagerDb.StudentRepository.Delete(id);
            }
        }
    }
}
