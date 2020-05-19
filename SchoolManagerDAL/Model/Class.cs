using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagerDAL
{
    public class Class
    {
        public int Id { get; }
        public String Name { get; }
        public int SchoolId { get; }

        public Class(int id, string name, int schoolId)
        {
            Id = id;
            Name = name;
            SchoolId = schoolId;
        }
    }
}
