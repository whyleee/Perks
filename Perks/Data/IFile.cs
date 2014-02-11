using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Perks.Data
{
    public interface IFile : IFsItem
    {
        IDirectory Directory { get; }

        bool IsReadOnly { get; set; }

        long Length { get; }

        // streamed IO

        Stream Open();

        Stream Open(FileMode mode);

        Stream Open(FileMode mode, FileAccess access);

        Stream Open(FileMode mode, FileAccess access, FileShare share);

        Stream OpenRead();

        Stream OpenWrite();

        StreamReader OpenText();

        StreamWriter CreateText();

        StreamWriter AppendText();

        // reads

        byte[] ReadAllBytes();

        IReadOnlyList<string> ReadAllLines();

        IReadOnlyList<string> ReadAllLines(Encoding encoding);

        string ReadAllText();

        string ReadAllText(Encoding encoding);

        IEnumerable<string> ReadLines();

        IEnumerable<string> ReadLines(Encoding encoding);

        // writes

        void WriteAllBytes(byte[] bytes);

        void WriteAllLines(IEnumerable<string> lines);

        void WriteAllLines(IEnumerable<string> lines, Encoding encoding);

        void WriteAllText(string text);

        void WriteAllText(string text, Encoding encoding);

        // appends

        void AppendAllLines(IEnumerable<string> lines);

        void AppendAllLines(IEnumerable<string> lines, Encoding encoding);

        void AppendAllText(string text);

        void AppendAllText(string text, Encoding encoding);
    }
}