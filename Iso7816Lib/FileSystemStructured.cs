using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso7816Lib
{
    public interface FileSystemStructured
    {
        /**
        * Selects a file.
        * 
        * @param fid indicates which file to select
        * @throws CardServiceException in case of error
        */
        void selectFile(short fid);

        /**
         * Reads a fragment of the currently selected file.
         * 
         * @param offset offset
         * @param length the number of bytes to read (the result may be shorter, though)
         * @return contents of currently selected file, contains at least 1 byte, at most length.
         * @throws CardServiceException on error (for instance: end of file)
         */
        byte[] readBinary(int offset, int length);

        /**
         * Identifies the currently selected file.
         * 
         * @return a path of file identifiers or <code>null</code>.
         */
        FileInfox[] getSelectedPath();
    }
}
