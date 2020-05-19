using SchoolManagerDAL;
using System;

namespace SchoolManagerBLL.Services
{
    public class SchoolService : ISchoolService
    {
        private ISchoolManagerDb _schoolManagerDb;

        public SchoolService(ISchoolManagerDb schoolManagerDb)
        {
            _schoolManagerDb = schoolManagerDb;
        }

        public IdNameTuple[] GetSchoolsByNumber(string number, int count)
        {
            using (IDisposable connection = _schoolManagerDb.OpenConnection())
            {
                return _schoolManagerDb.SchoolRepository.GetSchoolsByNumber(number, count);
            }
        }
    }
}
