using System;

namespace SchoolManagerDAL
{
    public class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }
        public string Phone { get; }
        public int ClassId { get; }

        public Student(int id, string firstName, string lastName, string patronymic, string phone, int classId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Phone = phone;
            ClassId = classId;
        }
    }
}
