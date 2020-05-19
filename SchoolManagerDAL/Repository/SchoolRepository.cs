using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManagerDAL
{
    public class SchoolRepository : ISchoolRepository
    {
        private SqlConnection _dbConnection;

        private void FillGetSchoolsByNumberParameters(SqlCommand procedure, string number, int count)
        {
            procedure.Parameters.Add(procedure.CreateNChar("@number", number, 5));
            procedure.Parameters.Add(procedure.CreateInt("@count", count));
        }

        public SchoolRepository(SqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Edit(School model)
        {
            throw new NotImplementedException();
        }

        public IdNameTuple[] GetSchoolsByNumber(string number, int count)
        {
            SqlCommand procedure = _dbConnection.CreateProcedureCommand("GetSchoolIdNumberByNumber");
            FillGetSchoolsByNumberParameters(procedure, number, count);

            List<IdNameTuple> schools = new List<IdNameTuple>();

            using (IDataReader reader = procedure.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string schoolNumber = reader.GetString(1);
                    schools.Add(new IdNameTuple(id, schoolNumber));
                }
            }

            return schools.ToArray();
        }

        public int Create(School model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
