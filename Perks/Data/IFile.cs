using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Perks.Data
{
    public interface IFile : IFsItem
    {
        IDirectory Directory { get; set; }

        bool IsReadOnly { get; set; }

        long Length { get; }

        IFileIo Io { get; set; }
    }
}