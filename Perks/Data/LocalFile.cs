using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perks.Data
{
    public class LocalFile : IFile
    {
        public LocalFile(string path)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");

            Path = path;
        }

        public string Path { get; private set; }

        public string Name
        {
            get { return System.IO.Path.GetFileName(Path); }
            set { Path = Path.Replace(Name, value); }
        }

        public IDirectory Directory { get; set; }
        public bool IsReadOnly { get; set; }
        public long Length { get { return 0; } }
        public IFileIo Io { get; set; }
    }
}
