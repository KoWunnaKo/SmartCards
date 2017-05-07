using System;
using SC_CertificateCALib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_CertificateCALibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter server like [CA_SERVER_IP]\\[CA_NAME]");
                var serverAddress = Console.ReadLine();

                Console.WriteLine("Enter domain user like [domainName]\\[user]");
                var userName = Console.ReadLine();

                //Console.WriteLine("Enter template Name:  ");
                var templateName = "SmartcardUser";

                Console.WriteLine("Request a new certificate? (y|n)");
                if (Console.ReadLine() == "y")
                {
                    //Generate Subject
                    string fio = string.Format("{0} {1} {2}", "RADJABOV", "BAKHODIR", "BOBOKULOVICH");
                    string subjectTxt = string.Format(@"CN = helpdesk, OU = Users, OU = Akho, DC = akho, DC = local");

                    Console.WriteLine("Subject ");
                    Console.WriteLine(subjectTxt);

                    //Create Request
                    var request = CertApiOnBehalfOfOther.CreateCertRequestMessage(subjectTxt, templateName, userName);

                    Console.WriteLine("request ");
                    Console.WriteLine(request);

                    //Send Request
                    var id = CertApiOnBehalfOfOther.SendCertificateRequest(request, serverAddress);

                    //Download Certificate
                    var cert = CertApiOnBehalfOfOther.DownloadCert(id, serverAddress);

                    Console.WriteLine("The certificate had been installed successfully.");

                    Console.WriteLine(string.Format("Certificate in Base64 : {0}", cert));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
