using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManagerDAL
{
    public class ClassRepository : IClassRepository
    {
        private SqlConnection _dbConnection;

        private void FillGetSchoolClassesByNameParameters(SqlCommand procedure, int schoolId, string name, int count)
        {
            procedure.Parameters.Add(procedure.CreateInt("@schoolId", schoolId));
            procedure.Parameters.Add(procedure.CreateNChar("@name", name, 5));
            procedure.Parameters.Add(procedure.CreateInt("@count", count));
        }

        public ClassRepository(SqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Edit(Class model)
        {
            throw new NotImplementedException();
        }

        public IdNameTuple[] GetSchoolClassesByName(int schoolId, string searchName, int count)
        {
            SqlCommand procedure = _dbConnection.CreateProcedureCommand("GetClassIdNameByNameSchoolId");
            FillGetSchoolClassesByNameParameters(procedure, schoolId, searchName, count);

            List<IdNameTuple> schools = new List<IdNameTuple>();

            using (IDataReader reader = procedure.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    schools.Add(new IdNameTuple(id, name));
                }
            }

            return schools.ToArray();
        }

        public int Create(Class model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
