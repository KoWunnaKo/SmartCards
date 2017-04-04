using SmartCardDesc.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Db
{
    public class ConnectionProperty
    {
        public static ConnectionProperty Default = new ConnectionProperty();
        
        private const string DataSourceKey = "Data Source";
        private const string DatabaseKey = "Initial Catalog";
        private const string IntegratedSecurityKey = "Integrated Security";
        private const string UserIdKey = "  User Id";
        private const string PasswordKey = "    Password";

        private string DataSource { get; set; }
        private string Database { get; set; }
        private bool IntegratedSecurity { get; set; }
        private string UserId { get; set; }
        private string Password { get; set; }

        private ConnectionProperty()
        {
            DataSource = Settings.Default.DB_DataSource;
            Database = Settings.Default.DB_Database;
            IntegratedSecurity = Boolean.Parse(Settings.Default.DB_IntegratedSecurity);
            UserId = Settings.Default.DB_UserId;
            Password = Settings.Default.DB_Password;
        }

        public string GetConnectionString()
        {
            var result = new StringBuilder();
            result.Append(DataSourceKey);
            result.Append('=');
            result.Append(DataSource);
            result.Append(';');
            result.Append(DatabaseKey);
            result.Append('=');
            result.Append(Database);
            result.Append(';');
            if (IntegratedSecurity)
            {
                result.Append(IntegratedSecurityKey);
                result.Append('=');
                result.Append(bool.TrueString);
            }
            else
            {
                result.Append(IntegratedSecurityKey);
                result.Append('=');
                result.Append(bool.FalseString);
                result.Append(';');
                result.Append(UserIdKey);
                result.Append('=');
                result.Append(UserId);
                result.Append(';');
                result.Append(PasswordKey);
                result.Append('=');
                result.Append(Password);
            }

            result.Append(';');
            result.Append("Connect Timeout");
            result.Append('=');
            result.Append(15);
            result.Append(';');
            result.Append("Encrypt");
            result.Append('=');
            result.Append(bool.FalseString);
            result.Append(';');
            result.Append("TrustServerCertificate");
            result.Append('=');
            result.Append(bool.FalseString);

            return result.ToString();
        }
    }
}
