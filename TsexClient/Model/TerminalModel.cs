using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsexClient.Model
{
    public enum TerminalState
    {
        Empty,
        CardInserted,
        CardProsess,
        CardRemoved,
        Disconnected
    }

    public class TerminalModel
    {
        public string terminalName { get; set; }

        public TerminalState state { get; set; }

        public CardModel card { get; set; }
    }
}
