using System;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace SchoolManagerDAL
{
    public class DatabaseDeployer : IDisposable
    {
        private const string _batchSeparator = "GO";
        private string _pathToSchema = "..\\SchoolManagerDAL\\Sql\\Schema.sql";
        private string _pathToProcedures = "..\\SchoolManagerDAL\\Sql\\Procedures.sql";
        private string _pathToInitialization = "..\\SchoolManagerDAL\\Sql\\Initialization.sql";
        private SqlConnection _connection;
        private bool _requireInitialization;

        private static async Task ExecuteNonQueryAsync(SqlConnection connection, string sql)
        {
            SqlCommand command = connection.CreateTextCommand(sql);
            await command.ExecuteNonQueryAsync();
        }

        private void GetConnectionWithoutDb(out string dbName, out SqlConnection connWithoutDb)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(_connection.ConnectionString);
            dbName = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.InitialCatalog = String.Empty;
            string connectionStrWithoutDb = connectionStringBuilder.ConnectionString;
            connWithoutDb = new SqlConnection(connectionStrWithoutDb);
        }

        private static SqlCommand GetCreateDbCommand(string dbName, SqlConnection connWithoutDb)
        {
            StringBuilder createDbSqlTextBuilder = new StringBuilder();
            createDbSqlTextBuilder.Append($"IF (DB_ID('{dbName}') IS NULL) BEGIN CREATE DATABASE {dbName}; SELECT 1; END ");
            createDbSqlTextBuilder.Append("ELSE SELECT 0");
            string createDbSqlText = createDbSqlTextBuilder.ToString();
            SqlCommand createDbCommand = connWithoutDb.CreateTextCommand(createDbSqlText);
            return createDbCommand;
        }

        public DatabaseDeployer(string connectionString, string pathToSchema, string pathToProcedures, string pathToInitialization)
        {
            if (String.IsNullOrEmpty(pathToSchema))
                throw new Exception("Path to schema can't be empty string");

            if (String.IsNullOrEmpty(pathToProcedures))
                throw new Exception("Path to procedures can't be empty string");

            _pathToSchema = pathToSchema;
            _pathToProcedures = pathToProcedures;
            _pathToInitialization = pathToInitialization;
            _connection = new SqlConnection(connectionString);
        }

        public DatabaseDeployer(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public static async Task ExecuteSqlFileAsync(SqlConnection connection, string path)
        {
            bool batchesFound = false;

            using (StreamReader reader = new StreamReader(path))
            {
                StringBuilder batchBuilder = new StringBuilder();
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    if (String.Equals(line.Trim(), _batchSeparator, StringComparison.OrdinalIgnoreCase))
                    {
                        batchesFound = true;
                        string batch = batchBuilder.ToString();
                        await ExecuteNonQueryAsync(connection, batch);
                        batchBuilder.Clear();
                    }
                    else
                    {
                        batchBuilder.Append(line).Append(Environment.NewLine);
                    }
                }
            }

            if (!batchesFound)
                throw new Exception($"No batches was found. Sql file has to have sql batches divided by '{_batchSeparator}'");
        }

        public async Task ExecuteNonQueryAsync(string sql)
        {
            try
            {
                _connection.Open();
                await ExecuteNonQueryAsync(_connection, sql);
            }
            finally
            {
                _connection.Close();
            }
        }

        public T ExecuteReader<T>(string sql, Func<SqlDataReader, T> read)
        {
            try
            {
                _connection.Open();
                SqlCommand command = _connection.CreateTextCommand(sql);
                SqlDataReader reader = command.ExecuteReader();
                return read(reader);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task CreateDbIfNotExistsAsync()
        {
            string dbName;
            SqlConnection connWithoutDb;
            GetConnectionWithoutDb(out dbName, out connWithoutDb);

            try
            {
                connWithoutDb.Open();
                SqlCommand createDbCommand = GetCreateDbCommand(dbName, connWithoutDb);

                using (SqlDataReader reader = await createDbCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        throw new Exception("Unknown error. Database was not created properly");

                    int result = reader.GetInt32(0);
                    if (result == 1)
                        _requireInitialization = true;
                }
            }
            finally
            {
                connWithoutDb.Close();
            }
        }

        async public Task DeployAsync()
        {
            try
            {
                await CreateDbIfNotExistsAsync();
                _connection.Open();
                await ExecuteSqlFileAsync(_connection, _pathToSchema);
                await ExecuteSqlFileAsync(_connection, _pathToProcedures);
                if (_requireInitialization)
                    await ExecuteSqlFileAsync(_connection, _pathToInitialization);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
