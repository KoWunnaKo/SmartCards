using SmartCardDesc.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCardDesc.EntityModel.SmartCardDsTableAdapters;

namespace SmartCardDesc.Model
{
    public class DataActionController
    {
        public static void LoadDbObjects()
        {
            var conn = DbModel.db;

            try
            {
                if (DbModel.dataSetSc == null) return;

                DbModel.dataSetSc.AUDIT.Clear();
                DbModel.dataSetSc.CARD_INFO.Clear();
                DbModel.dataSetSc.DEPARTMENT.Clear();
                DbModel.dataSetSc.DICTIONARY.Clear();
                DbModel.dataSetSc.DICTIONARY_TYPE.Clear();
                DbModel.dataSetSc.EXTERNAL_REQUESTS.Clear();
                DbModel.dataSetSc.FIXED_INTERNAL_VALUES.Clear();
                DbModel.dataSetSc.READERS_INFO.Clear();
                DbModel.dataSetSc.USERS.Clear();
                DbModel.dataSetSc.WAREHOUSE.Clear();
                DbModel.dataSetSc.WAREHOUSE_DTL.Clear();

                var daStAudit = new AUDITTableAdapter { Connection = conn.Connection };
                var daStCardInfo = new CARD_INFOTableAdapter { Connection = conn.Connection };
                var daStDepartment = new DEPARTMENTTableAdapter { Connection = conn.Connection };
                var daStDictionary = new DICTIONARYTableAdapter { Connection = conn.Connection };
                var daStDictionaryType = new DICTIONARY_TYPETableAdapter { Connection = conn.Connection };
                var daStExternalRequests = new EXTERNAL_REQUESTSTableAdapter { Connection = conn.Connection };
                var daStFixedInternalValues = new FIXED_INTERNAL_VALUESTableAdapter { Connection = conn.Connection };
                var daStRedersInfo = new READERS_INFOTableAdapter { Connection = conn.Connection };
                var daStUsers = new USERSTableAdapter { Connection = conn.Connection };
                var daStWarehouse = new WAREHOUSETableAdapter { Connection = conn.Connection };
                var daStWarehouseDtl = new WAREHOUSE_DTLTableAdapter { Connection = conn.Connection };

                conn.OpenConnection();

                daStAudit.Fill(DbModel.dataSetSc.AUDIT);
                daStCardInfo.Fill(DbModel.dataSetSc.CARD_INFO);
                daStDepartment.Fill(DbModel.dataSetSc.DEPARTMENT);
                daStDictionary.Fill(DbModel.dataSetSc.DICTIONARY);
                daStDictionaryType.Fill(DbModel.dataSetSc.DICTIONARY_TYPE);
                daStExternalRequests.Fill(DbModel.dataSetSc.EXTERNAL_REQUESTS);
                daStFixedInternalValues.Fill(DbModel.dataSetSc.FIXED_INTERNAL_VALUES);
                daStRedersInfo.Fill(DbModel.dataSetSc.READERS_INFO);
                daStUsers.Fill(DbModel.dataSetSc.USERS);
                daStWarehouse.Fill(DbModel.dataSetSc.WAREHOUSE);
                daStWarehouseDtl.Fill(DbModel.dataSetSc.WAREHOUSE_DTL);

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
