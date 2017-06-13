using System;
using CERTENROLLLib;
using CERTCLILib;
using System.Security.Cryptography.X509Certificates;

namespace SC_CertificateCALib
{
    /// <summary>
    /// 
    /// </summary>
    public class CertInterOpApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string CreateCertRequestMessage()
        {
            var objCSPs = new CCspInformations();
            objCSPs.AddAvailableCsps();

            var objPrivateKey = new CX509PrivateKey();
            objPrivateKey.Length = 2048;
            objPrivateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE;
            objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;
            objPrivateKey.MachineContext = false;
            objPrivateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_EXPORT_FLAG;
            objPrivateKey.CspInformations = objCSPs;
            objPrivateKey.Create();

            //objPrivateKey.Export()

            var objPkcs10 = new CX509CertificateRequestPkcs10();
            objPkcs10.InitializeFromPrivateKey(
                X509CertificateEnrollmentContext.ContextUser,
                objPrivateKey,
                "TestTemplate");

            //var objExtensionKeyUsage = new CX509ExtensionKeyUsage();
            //objExtensionKeyUsage.InitializeEncode(
            //    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DIGITAL_SIGNATURE_KEY_USAGE |
            //    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_NON_REPUDIATION_KEY_USAGE |
            //    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_KEY_ENCIPHERMENT_KEY_USAGE |
            //    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DATA_ENCIPHERMENT_KEY_USAGE);
            //objPkcs10.X509Extensions.Add((CX509Extension)objExtensionKeyUsage);

            //var objObjectId = new CObjectId();
            //var objObjectIds = new CObjectIds();
            //var objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsage();
            //objObjectId.InitializeFromValue("1.3.6.1.5.5.7.3.2");
            //objObjectIds.Add(objObjectId);
            //objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);
            //objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);

            var objDN = new CX500DistinguishedName();
            var subjectName = "CN = test, OU = ADCS, O = Blog, L = Beijng, S = Beijing, C = CN";
            objDN.Encode(subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);
            objPkcs10.Subject = objDN;

            var objEnroll = new CX509Enrollment();
            objEnroll.InitializeFromRequest(objPkcs10);
            var strRequest = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);
            return strRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="psubjectName"></param>
        /// <param name="pCaTemplate"></param>
        /// <returns></returns>
        public static string CreateCertRequestMessage(string psubjectName, string pCaTemplate)
        {
            var objCSPs = new CCspInformations();
            objCSPs.AddAvailableCsps();

            var objPrivateKey = new CX509PrivateKey();
            objPrivateKey.Length = 2048;
            objPrivateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE;
            objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;
            objPrivateKey.MachineContext = false;
            objPrivateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_EXPORT_FLAG;
            objPrivateKey.CspInformations = objCSPs;
            objPrivateKey.Create();

            var objPkcs10 = new CX509CertificateRequestPkcs10();
            objPkcs10.InitializeFromPrivateKey(
                X509CertificateEnrollmentContext.ContextUser,
                objPrivateKey,
                pCaTemplate);

            var objDN = new CX500DistinguishedName();
            var subjectName = psubjectName; //
            objDN.Encode(subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);
            objPkcs10.Subject = objDN;

            var objEnroll = new CX509Enrollment();
            objEnroll.InitializeFromRequest(objPkcs10);
            var strRequest = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);
            return strRequest;
        }

        private const int CC_DEFAULTCONFIG = 0;
        private const int CC_UIPICKCONFIG = 0x1;
        private const int CR_IN_BASE64 = 0x1;
        private const int CR_IN_FORMATANY = 0;
        private const int CR_IN_PKCS10 = 0x100;
        private const int CR_DISP_ISSUED = 0x3;
        private const int CR_DISP_UNDER_SUBMISSION = 0x5;
        private const int CR_OUT_BASE64 = 0x1;
        private const int CR_OUT_CHAIN = 0x100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        public static int SendCertificateRequest(string message, string serverAddress)
        {
            var objCertRequest = new CCertRequest();
            var iDisposition = objCertRequest.Submit(
                    CR_IN_BASE64 | CR_IN_FORMATANY,
                    message,
                    string.Empty,
                    @serverAddress); //"192.168.56.101\pal-CPAL-CA"

            switch (iDisposition)
            {
                case CR_DISP_ISSUED:
                    Console.WriteLine("The certificate had been issued.");
                    break;
                case CR_DISP_UNDER_SUBMISSION:
                    Console.WriteLine("The certificate is still pending.");
                    break;
                default:
                    Console.WriteLine("The submission failed: " + objCertRequest.GetDispositionMessage());
                    Console.WriteLine("Last status: " + objCertRequest.GetLastStatus().ToString());
                    break;
            }
            return objCertRequest.GetRequestId();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        public static string DownloadCert(int requestId, string serverAddress)
        {
            var objCertRequest = new CCertRequest();
            var iDisposition = objCertRequest.RetrievePending(requestId, @serverAddress);//"192.168.56.101\pal-CPAL-CA"

            if (iDisposition == CR_DISP_ISSUED)
            {
                var cert = objCertRequest.GetCertificate(CR_OUT_BASE64 | CR_OUT_CHAIN);

                return cert;
            }

            return string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serverAddress"></param>
        private static void DownloadAndInstallCert(int requestId, string serverAddress)
        {
            var objCertRequest = new CCertRequest();
            var iDisposition = objCertRequest.RetrievePending(requestId, @serverAddress);//"192.168.56.101\pal-CPAL-CA"

            if (iDisposition == CR_DISP_ISSUED)
            {
                var cert = objCertRequest.GetCertificate(CR_OUT_BASE64 | CR_OUT_CHAIN);
                var objEnroll = new CX509Enrollment();
                objEnroll.Initialize(X509CertificateEnrollmentContext.ContextUser);
                objEnroll.InstallResponse(
                    InstallResponseRestrictionFlags.AllowUntrustedRoot,
                    cert,
                    EncodingType.XCN_CRYPT_STRING_BASE64,
                    null);
                Console.WriteLine("The certificate had been installed successfully.");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        private static int Renew(string serverAddress)
        {
            X509Certificate2 certificate = null;
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                certificate = store.Certificates.Find(X509FindType.FindByThumbprint, "c1555218deed2c6dbe5101178617ef7628388a85", false)[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                store.Close();
            }

            var objPkcs7 = new CX509CertificateRequestPkcs7();
            objPkcs7.InitializeFromCertificate(
                X509CertificateEnrollmentContext.ContextUser,
                true,
                Convert.ToBase64String(certificate.RawData),
                EncodingType.XCN_CRYPT_STRING_BASE64,
                X509RequestInheritOptions.InheritPrivateKey & X509RequestInheritOptions.InheritValidityPeriodFlag);

            var objEnroll = new CX509Enrollment();
            objEnroll.InitializeFromRequest(objPkcs7);
            var message = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);

            var objCertRequest = new CCertRequest();
            var iDisposition = objCertRequest.Submit(
                    CR_IN_BASE64 | CR_IN_FORMATANY,
                    message,
                    string.Empty,
                    @serverAddress);//"192.168.56.101\pal-CPAL-CA"

            switch (iDisposition)
            {
                case CR_DISP_ISSUED:
                    Console.WriteLine("The certificate had been issued.");
                    break;
                case CR_DISP_UNDER_SUBMISSION:
                    Console.WriteLine("The certificate is still pending.");
                    break;
                default:
                    Console.WriteLine("The submission failed: " + objCertRequest.GetDispositionMessage());
                    Console.WriteLine("Last status: " + objCertRequest.GetLastStatus().ToString());
                    break;
            }
            return objCertRequest.GetRequestId();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void MainLogic(string[] args)
        {
            try
            {
                Console.WriteLine("Enter server like [CA_SERVER_IP]\\[CA_NAME]");
                var serverAddress = Console.ReadLine();

                Console.WriteLine("Request a new certificate? (y|n)");
                if (Console.ReadLine() == "y")
                {
                    var request = CreateCertRequestMessage();
                    Console.WriteLine("Create Message Finished");
                    Console.WriteLine("Message = {0}", request);
                    var id = SendCertificateRequest(request, serverAddress);
                    Console.WriteLine("Request ID: " + id.ToString());
                }

                Console.WriteLine("Download & install certificate? (y|n)");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Request ID?");
                    var id = int.Parse(Console.ReadLine());
                    DownloadAndInstallCert(id, serverAddress);
                }

                Console.WriteLine("Renew an existing certificate? (y|n)");
                if (Console.ReadLine() == "y")
                {
                    var id = Renew(serverAddress);
                    Console.WriteLine("Request ID: " + id.ToString());
                }

                Console.WriteLine("Download & install renewed certificate? (y|n)");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Request ID?");
                    var id = int.Parse(Console.ReadLine());
                    DownloadAndInstallCert(id, serverAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }

        }
    }
}
