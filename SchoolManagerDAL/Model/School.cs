using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagerDAL
{ 
    public class School
    {
        public int Id { get; }
        public String Name { get; }
        public String Number { get; }
        public String Phone { get; }

        public School(int id, string name, string number, string phone)
        {
            Id = id;
            Name = name;
            Number = number;
            Phone = phone;
        }
    }
}
