using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perks.Data
{
    public class InMemoryFile : IFile
    {
        public string Path { get; private set; }
        public string Name { get; set; }
        public IDirectory Directory { get; set; }
        public bool IsReadOnly { get; set; }
        public long Length { get { return 0; } }
        public IFileIo Io { get; set; }
    }
}
