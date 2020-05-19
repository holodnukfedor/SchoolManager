using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagerDAL;

namespace SchoolManagerBLL
{
    public interface ISchoolService
    {
        IdNameTuple[] GetSchoolsByNumber(string number, int count);
    }
}
