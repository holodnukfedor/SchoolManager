using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagerDAL
{
    public class StudentWithSchool : Student
    {
        public string Class { get; }
        public string School { get; }
        public int SchoolId { get; }

        public StudentWithSchool(int id, string firstName, string lastName, string patronymic, string phone, int classId, string @class, string school, int schoolId)
            : base(id, firstName, lastName, patronymic, phone, classId)
        {
            Class = @class;
            School = school;
            SchoolId = schoolId;
        }
    }
}
