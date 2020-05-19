using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagerDAL
{
    public class IdNameTuple
    {
        public int Id { get; }
        public String Name { get; }

        public IdNameTuple(int id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}
