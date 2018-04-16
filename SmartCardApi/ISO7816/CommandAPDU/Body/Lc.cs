﻿using System.Linq;
using SmartCardApi.Infrastructure;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.ISO7816.CommandAPDU.Body
{
    public class Lc : IBinary
    {
        private readonly IBinary _commandApduBody;

        public Lc(IBinary commandApduBody)
        {
            _commandApduBody = commandApduBody;
        }
        public byte[] Bytes()
        {
            var commandApduBodyBytes = _commandApduBody
                .Bytes();

            // Command Structure: [CLA][INS][P1][P2][Lc][Data][Le]
            // Command Header:    [CLA][INS][P1][P2]
            // Command Body:      [Lc][Data][Le]
            // Case2: [No command Data][Excepted Data] 
            // [Le] - Expected Data Length
            // [Lc] - Command Data Length
            // ReadBinaryFormat: [CLA][INS][P1][P2][Le] - 00 84 00 00 08
            // SelectEFCom:      [CLA][INS][P1][P2][Lc][Command Data] - 00 A4 02 0C 02 011E 

            // if CommandBody length grather then 1, we have Lc with Data, and maybe Le
            // if CommandBody length equal 1, then we don't Lc with Data, only Le
            if (commandApduBodyBytes.Count() > 1)
            {
                return commandApduBodyBytes
                    .Take(1)
                    .ToArray();  
            }
            else
            {
                return new byte[0];
            }
        }
    }
}
