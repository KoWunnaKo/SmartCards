using System.Data;
using System.Data.SqlClient;


namespace SmartCardDesc.Db
{
    public class DbHelper
    {
        public readonly SqlConnection connection;
        public readonly SqlCommand command;

        public DbHelper()
        {
            connection = new SqlConnection(ConnectionProperty.Default.GetConnectionString());
            command = new SqlCommand
                {
                    Connection = connection
                    , CommandType = CommandType.StoredProcedure
                };
        }
    }
}
