using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2
{
    public class DrivingLicenseService
    {
        /** Data group 1 contains the mandatory driver data. */
        public  const  short EF_DG1 = 0x0001;

        public  const  byte SF_DG1 = 0x01;

        /** Data group 2 contains optional license holder data. */
        public  const  short EF_DG2 = 0x0002;

        public  const  byte SF_DG2 = 0x02;

        /** Data group 3 contains issuing authority details. */
        public  const  short EF_DG3 = 0x0003;

        public  const  byte SF_DG3 = 0x03;

        /** Data group 4 contains optional portrait image. */
        public  const  short EF_DG4 = 0x0004;

        public  const  byte SF_DG4 = 0x0004;

        /** Data group 5 contains optional signature image. */
        public  const  short EF_DG5 = 0x0005;

        public  const  byte SF_DG5 = 0x05;

        /** Data group 6 contains optional facial biometric template. */
        public  const  short EF_DG6 = 0x0006;

        public  const  byte SF_DG6 = 0x06;

        /** Data group 7 contains optional finger biometric template. */
        public  const  short EF_DG7 = 0x0007;

        public  const  byte SF_DG7 = 0x07;

        /** Data group 8 contains optional iris biometric template. */
        public  const  short EF_DG8 = 0x0008;

        public  const  byte SF_DG8 = 0x08;

        /** Data group 9 contains optional other biometric templates. */
        public  const  short EF_DG9 = 0x0009;

        public  const  byte SF_DG9 = 0x09;

        /** Data group 10 is RFU. */
        public  const  short EF_DG10 = 0x000A;

        public  const  byte SF_DG10 = 0x0A;

        /** Data group 11 contains optional domestic application data. */
        public  const  short EF_DG11 = 0x000B;

        public  const  byte SF_DG11 = 0x0B;

        /** Data group 12 contains non-match alert. */
        public  const  short EF_DG12 = 0x000C;

        public  const  byte SF_DG12 = 0x0C;

        /** Data group 13 contains active authentication (AA) certificate. */
        public  const  short EF_DG13 = 0x000D;

        public  const  byte SF_DG13 = 0x0D;

        /** Data group 14 contains extended access protection (EAP) public key. */
        public  const  short EF_DG14 = 0x000E;

        public  const  byte SF_DG14 = 0x0E;

        /** The security document. */
        public  const  short EF_SOD = 0x001D;

        public  const  byte SF_SOD = 0x1D;

        /**
         * File indicating which data groups are present and security mechanism
         * indicators.
         */
        public const short EF_COM = 0x001E;

        public const byte SF_COM = 0x1E;

        /**
        * The file read block size, some cards cannot handle large values.
        * 
        * TODO: get the read block size from the card FCI data or similar.
        * 
        * @deprecated hack
        */
        public  int maxBlockSize = 255;

        private  int SESSION_STOPPED_STATE = 0;

        private  int SESSION_STARTED_STATE = 1;

        private  int BAP_AUTHENTICATED_STATE = 2;

        private  int AA_AUTHENTICATED_STATE = 3;

        private  int CA_AUTHENTICATED_STATE = 5;

        private  int TA_AUTHENTICATED_STATE = 6;

        private  int EAP_AUTHENTICATED_STATE = 7;
    }
}
