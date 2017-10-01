using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso7816Lib
{
    public abstract class FileInfox
    {
        public abstract short getFID();

        public abstract int getFileLength();
    }
}
