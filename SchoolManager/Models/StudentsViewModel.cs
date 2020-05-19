using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagerDAL;

namespace SchoolManager
{
    public class StudentViewModelList
    {
        public StudentViewModel[] Students { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
