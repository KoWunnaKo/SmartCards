using System.Data;
using System.Data.SqlClient;

namespace SmartCardDesc.Db
{
    public class DbConnection
    {
        private DbConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            Command = _connection.CreateCommand();
            Command.CommandTimeout = 180000;
            Command.CommandType = CommandType.StoredProcedure;
            _adapter = new SqlDataAdapter();
        }

        public static DbConnection GetInstance(string connectionString)
        {
            return new DbConnection(connectionString);
        }

        private readonly SqlConnection _connection;
        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        private readonly SqlDataAdapter _adapter;

        public void FillThroughAdapter(DataTable dataTable)
        {
            _adapter.SelectCommand = Command;
            _adapter.Fill(dataTable);
        }

        public SqlCommand Command { get; set; }

        public void OpenConnection()
        {
            switch (_connection.State)
            {
                case ConnectionState.Closed: _connection.Open();
                    break;
                case ConnectionState.Broken: _connection.Close(); _connection.Open();
                    break;
            }
        }

        public void CloseConnection()
        {
            switch (_connection.State)
            {
                case ConnectionState.Open: _connection.Close();
                    break;
                case ConnectionState.Broken: _connection.Close();
                    break;
            }
        }
    }
}
