using System.Collections.Generic;
using System.IO;

namespace Perks.Data
{
    public interface IDirectory : IFsItem
    {
        IDirectory Parent { get; }

        IDirectory Root { get; }

        IEnumerable<IFsItem> Children { get; }

        IEnumerable<IDirectory> Directories { get; }

        IEnumerable<IFile> Files { get; }

        IReadOnlyList<IFsItem> GetChildren();

        IReadOnlyList<IDirectory> GetDirectories();

        IReadOnlyList<IFile> GetFiles();

        IReadOnlyList<IFsItem> Search(string pattern);

        IReadOnlyList<IFsItem> Search(string pattern, SearchOption opts);

        IReadOnlyList<IDirectory> SearchDirectories(string pattern);

        IReadOnlyList<IDirectory> SearchDirectories(string pattern, SearchOption opts);

        IReadOnlyList<IFile> SearchFiles(string pattern);

        IReadOnlyList<IFile> SearchFiles(string pattern, SearchOption opts);

        IEnumerable<IFsItem> Filter(string pattern);

        IEnumerable<IFsItem> Filter(string pattern, SearchOption opts);

        IEnumerable<IDirectory> FilterDirectories(string pattern);

        IEnumerable<IDirectory> FilterDirectories(string pattern, SearchOption opts);

        IEnumerable<IFile> FilterFiles(string pattern);

        IEnumerable<IFile> FilterFiles(string pattern, SearchOption opts);
    }
}