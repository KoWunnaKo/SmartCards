using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsexClient.Model
{
    public enum CardState
    {
        Normal,
        Brak,
        Process
    }

    public enum CardOperationState
    {
        TsexCardOp,
        PKI_Get,
        CertificateGet
    }

    public class CardModel
    {
        public string cardUuId { get; set; }

        public CardState cState { get; set; }

        public CardOperationState cOpState { get; set; }


    }
}
