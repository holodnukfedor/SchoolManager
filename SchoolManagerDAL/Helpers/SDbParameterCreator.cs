using System.Data;
using System.Data.SqlClient;

namespace SchoolManagerDAL
{
    public static class SDbParameterCreator
    {
        public static SqlParameter CreateInt(this SqlCommand command, string name, int value)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.DbType = DbType.Int32;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter CreateNCharLength(this SqlCommand command, string name, string value, int length)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.DbType = DbType.StringFixedLength;
            parameter.Size = length;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
        
        public static SqlParameter CreateNChar(this SqlCommand command, string name, string value, int length)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.DbType = DbType.String;
            parameter.Size = length;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter CreateCharLength(this SqlCommand command, string name, string value, int length)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.DbType = DbType.AnsiStringFixedLength;
            parameter.Size = length;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}
