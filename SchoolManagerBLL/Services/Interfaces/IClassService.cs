using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagerDAL;

namespace SchoolManagerBLL
{
    public interface IClassService
    {
        IdNameTuple[] GetSchoolClassesByName(int schoolId, string name, int count);
    }
}
