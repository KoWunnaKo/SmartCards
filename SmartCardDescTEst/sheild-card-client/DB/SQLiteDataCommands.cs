using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GID_Client.DB
{
    public class SQLiteDataCommands
    {
        private readonly DataSet _dataSet = new DataSet();

        public DataSet DataSet
        {
            get { return _dataSet; }
        }

        private SQLiteDataAdapter _daActivator;
        
        private const string ActivatorTableName = "ActivationData";

        public async Task<bool> LoadDataAsync()
        {
            return await LoadData();
        }

        private Task<bool> LoadData()
        {

            var result = Task.Factory.StartNew(() =>
            {
                var conn = new SQLiteConnection("Data Source=Activator.db; Version=3;");

                // services
                string query = @"SELECT ID
                                          ,JSON
                                          ,Status
                                      FROM ActivationData";

                _daActivator = new SQLiteDataAdapter(query, conn);
                _daActivator.TableMappings.Add("Table", ActivatorTableName);
                _daActivator.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                var sqlCmd = new SQLiteCommand(@"INSERT INTO ActivationData (
                               ID,
                               JSON,
                               Status
                           )
                           VALUES (
                               @ID,
                               @JSON,
                               @Status
                           );
                ", conn);


                sqlCmd.Parameters.Add("@ID", DbType.Int32, 0, "ID").IsNullable = false;
                sqlCmd.Parameters.Add("@JSON", DbType.String, 0, "JSON").IsNullable = false;
                sqlCmd.Parameters.Add("@Status", DbType.Boolean, 0, "Status").IsNullable = false;

                _daActivator.InsertCommand = sqlCmd;
                _daActivator.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

                sqlCmd =
                    new SQLiteCommand(
                        @"UPDATE ActivationData
                               SET Status = @Status
                             WHERE ID = @ID ;",
                        conn);


                sqlCmd.Parameters.Add("@ID", DbType.Int32, 0, "ID").SourceVersion = DataRowVersion.Original; 
                sqlCmd.Parameters.Add("@Status", DbType.Boolean, 0, "Status").IsNullable = false;

                _daActivator.UpdateCommand = sqlCmd;

                sqlCmd = new SQLiteCommand("DELETE FROM ActivationData" + "  WHERE ID = @ID", conn);
                sqlCmd.Parameters.Add("@ID", DbType.Int32, 0, "ID").SourceVersion = DataRowVersion.Original;
                _daActivator.DeleteCommand = sqlCmd;

                try
                {
                    conn.Open();
                    _daActivator.Fill(_dataSet, ActivatorTableName);
                }
                catch (Exception ex)
                {
                    _daActivator.Dispose();
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            });

            return result;
        }

        public async Task<bool> UpdateDataAsync()
        {
            return await UpdateData();
        }

        private Task<bool> UpdateData()
        {
            var result = Task.Factory.StartNew(() =>
            {
                var conn = new SQLiteConnection("Data Source=Activator.db; Version=3;");

                SQLiteTransaction sqlTrans = null;

                try
                {
                    conn.Open();

                    sqlTrans = conn.BeginTransaction();

                    _daActivator.UpdateCommand.Connection = conn;
                    _daActivator.UpdateCommand.Transaction = sqlTrans;
                    _daActivator.SelectCommand.Connection = conn;
                    _daActivator.SelectCommand.Transaction = sqlTrans;
                    _daActivator.DeleteCommand.Connection = conn;
                    _daActivator.DeleteCommand.Transaction = sqlTrans;
                    _daActivator.InsertCommand.Connection = conn;
                    _daActivator.InsertCommand.Transaction = sqlTrans;

                    _daActivator.Update(_dataSet.Tables[ActivatorTableName]);

                    // Attempt to commit the transaction.
                    sqlTrans.Commit();
                }
                catch (Exception ex)
                {

                    // Attempt to roll back the transaction.
                    try
                    {
                        if (sqlTrans != null)
                            sqlTrans.Rollback();
                    }
                    catch (Exception ex2)
                    {

                    }

                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            });

            return result;
        }

        public static void Proc1()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Activator.db; Version=3;"))
            {
                // ******
            }
        }
    }
}
