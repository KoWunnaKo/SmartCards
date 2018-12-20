using CardAPILib.InterfaceCL;
using GID_Client.ServerApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_Reset_Tool
{
    class Program
    {
        public static bool isOldCard = false;

        static void Main(string[] args)
        {
            try
            {
                var loginAndPasw = ConfigurationManager.AppSettings["LoginPswd"];
                //changed 22.11.2018
                if (isOldCard)
                {
                    loginAndPasw = string.Empty;
                }
                //end changed

                if (!string.IsNullOrEmpty(loginAndPasw))
                {
                    string[] log_pas = loginAndPasw.Split(':');

                    if (log_pas.Any())
                    {
                        if (log_pas.Length == 2)
                        {
                            var reqRes = ServerApiController.LoginReqRes(log_pas[0], log_pas[1]);

                            if (reqRes != null)
                                ServerApiController.token = reqRes.data.Token;
                        }
                    }
                }

                SecureMessaging sc = new SecureMessaging();

                var CardNUmber = sc.ReadCardNumber();
                //changed 22.11.2018
                if (isOldCard)
                {
                    sc.InstallAppletV3();

                    Console.ReadKey();

                    return;
                }
                //end changed
                    var KeyValue = ServerApiController.GetKey(CardNUmber);

                if (!string.IsNullOrEmpty(KeyValue._data._message))
                {
                    sc.InstallAppletV3(KeyValue._data._message);
                }
                else
                {
                    sc.InstallAppletV3();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
