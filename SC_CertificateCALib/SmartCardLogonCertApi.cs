using System;
using CERTENROLLLib;
using CERTCLILib;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace SC_CertificateCALib
{
    public class SmartCardLogonCertApi
    {
        private const int CC_DEFAULTCONFIG = 0;

        private const int CC_UIPICKCONFIG = 0x1;

        private const int CR_IN_BASE64 = 0x1;

        private const int CR_IN_FORMATANY = 0;

        private const int CR_IN_PKCS10 = 0x100;

        private const int CR_DISP_ISSUED = 0x3;

        private const int CR_DISP_UNDER_SUBMISSION = 0x5;

        private const int CR_OUT_BASE64 = 0x1;

        private const int CR_OUT_CHAIN = 0x100;

        public static string CreateCertRequestMessage(string psubjectName, string pCaTemplate)
        {
            //  Create all the objects that will be required

            CX509CertificateRequestPkcs10 objPkcs10 = new CX509CertificateRequestPkcs10Class();

            CX509PrivateKey objPrivateKey = new CX509PrivateKeyClass();

            CCspInformations objCSPs = new CCspInformationsClass();

            CX500DistinguishedName objDN = new CX500DistinguishedNameClass();

            CX509Enrollment objEnroll = new CX509EnrollmentClass();

            CObjectIds objObjectIds = new CObjectIdsClass();

            CObjectId objObjectId = new CObjectIdClass();

            CX509ExtensionKeyUsage objExtensionKeyUsage = new CX509ExtensionKeyUsageClass();

            CX509ExtensionEnhancedKeyUsage objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsageClass();

            CX509ExtensionTemplateName objExtensionTemplate = new CX509ExtensionTemplateName();

            string strRequest = string.Empty;

            try
            {
                // Get all available CSPs
                objCSPs.AddAvailableCsps();

                //  Provide key info


                objPrivateKey.Length = 2048;
                objPrivateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE;
                objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;
                objPrivateKey.MachineContext = false;
                objPrivateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_EXPORT_FLAG;
                objPrivateKey.CspInformations = objCSPs;
                objPrivateKey.Create();

                //objPrivateKey.ContainerName = "SmartCardContainer";

                //objPrivateKey.ProviderName = "eToken Base Cryptographic Provider";

                //objPrivateKey.ProviderType = X509ProviderType.XCN_PROV_RSA_FULL;

                //objPrivateKey.Length = 1024;

                //objPrivateKey.KeySpec = X509KeySpec.XCN_AT_KEYEXCHANGE;

                //objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;

                //objPrivateKey.MachineContext = false;

                //objPrivateKey.CspInformations = objCSPs;

                //  Create the actual key pair

                //objPrivateKey.Create();



                //  Initialize the PKCS#10 certificate request object based on the private key.

                //  Using the context, indicate that this is a user certificate request and don't

                //  provide a template name

                objPkcs10.InitializeFromPrivateKey(

                    X509CertificateEnrollmentContext.ContextUser,

                    objPrivateKey,

                    ""

                );



                // Key Usage Extension 

                objExtensionKeyUsage.InitializeEncode(

                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DIGITAL_SIGNATURE_KEY_USAGE |

                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_NON_REPUDIATION_KEY_USAGE |

                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_KEY_ENCIPHERMENT_KEY_USAGE |

                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DATA_ENCIPHERMENT_KEY_USAGE

                );

                objPkcs10.X509Extensions.Add((CX509Extension)objExtensionKeyUsage);



                // Enhanced Key Usage Extension

                objObjectId.InitializeFromValue("1.3.6.1.4.1.311.20.2.2"); // OID for Smartcard logon

                objObjectIds.Add(objObjectId);

                objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);

                objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);



                // Template Extension

                objExtensionTemplate.InitializeEncode(pCaTemplate);

                objPkcs10.X509Extensions.Add((CX509Extension)objExtensionTemplate);



                //  Encode the name in using the Distinguished Name object

                objDN.Encode( psubjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE );



                //  Assing the subject name by using the Distinguished Name object initialized above

                objPkcs10.Subject = objDN;



                // Create enrollment request

                objEnroll.InitializeFromRequest(objPkcs10);

                strRequest = objEnroll.CreateRequest(

                    EncodingType.XCN_CRYPT_STRING_BASE64

                );


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            return strRequest;
        }

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
    }
}
