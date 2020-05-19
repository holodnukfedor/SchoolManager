using SchoolManagerDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagerBLL
{
    public class ClassService : IClassService
    {
        private ISchoolManagerDb _schoolManagerDb;

        public ClassService(ISchoolManagerDb schoolManagerDb)
        {
            _schoolManagerDb = schoolManagerDb;
        }

        public IdNameTuple[] GetSchoolClassesByName(int schoolId, string name, int count)
        {
            using (var connection = _schoolManagerDb.OpenConnection())
            {
                return _schoolManagerDb.ClassRepository.GetSchoolClassesByName(schoolId, name, count);
            }
        }
    }
}
