﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GID_Client.TsexLogic
{
    interface ICertificateProcess
    {
        bool mondatoryFlag { get; set; }

        bool Process();
    }
}