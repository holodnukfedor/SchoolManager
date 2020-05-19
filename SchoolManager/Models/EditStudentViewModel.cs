using SchoolManagerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class EditStudentViewModel
    {
        public StudentViewModel StudentViewModel { get; set; }
        public PaginationParameters PaginationParameters { get; set; }

        public EditStudentViewModel(StudentViewModel student, PaginationParameters paginationParams)
        {
            StudentViewModel = student;
            PaginationParameters = paginationParams;
        }

        public EditStudentViewModel()
        {

        }
    }
}
