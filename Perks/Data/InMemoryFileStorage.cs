using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Perks.Data
{
    /// <summary>
    /// Simple in-memory file manager, doesn't permanent, but can be useful for unit testing or quick prototyping.
    /// </summary>
    public class InMemoryFileStorage : IFileStorage
    {
        protected readonly string TempPath = @"C:\temp";

        protected readonly Dictionary<string, string> _files = new Dictionary<string, string>();

        /// <summary>
        /// Creates a new file in the storage or overwrites existing.
        /// </summary>
        /// <param name="path">The path where to create a file.</param>
        public virtual void CreateFile(string path)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");

            _files[path] = string.Empty;
        }

        /// <summary>
        /// Creates a temp file in the storage.
        /// </summary>
        /// <param name="contents">Temp file contents.</param>
        /// <param name="extension">Temp file extension.</param>
        /// <returns>Path to created temp file.</returns>
        public virtual string CreateTempFile(byte[] contents, string extension)
        {
            if (string.IsNullOrEmpty(extension))
            {
                extension = "tmp";
            }

            var path = Path.Combine(TempPath, string.Format("{0}.{1}", Guid.NewGuid().ToString("N"), extension));

            if (contents != null && contents.Length > 0)
            {
                WriteFile(path, contents);
            }
            else
            {
                CreateFile(path);
            }

            return path;
        }

        /// <summary>
        /// Opens the file to read.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextReader" /> that can read a file.</returns>
        public virtual TextReader OpenRead(string path)
        {
            EnsureFileExists(path);

            return new StringReader(_files[path]);
        }

        /// <summary>
        /// Opens the file to write.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextWriter" /> that can write to a file.</returns>
        public virtual TextWriter OpenWrite(string path)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");

            return new FileStringWriter(_files, path);
        }

        /// <summary>
        /// Reads entire file contents as array of bytes.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>Byte array with file contents.</returns>
        public virtual byte[] ReadFile(string path)
        {
            EnsureFileExists(path);

            return Encoding.UTF8.GetBytes(_files[path]);
        }

        /// <summary>
        /// Reads entire file contents as string.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>String with file contents.</returns>
        public virtual string ReadFileText(string path)
        {
            EnsureFileExists(path);

            return _files[path];
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="contents">New file contents.</param>
        public virtual void WriteFile(string path, byte[] contents)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");
            Ensure.ArgumentNotNull(contents, "contents");

            _files[path] = Encoding.UTF8.GetString(contents);
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="contents">New file contents.</param>
        public virtual void WriteFile(string path, string contents)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");
            Ensure.ArgumentNotNull(contents, "contents");

            _files[path] = contents;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        public virtual void DeleteFile(string path)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");

            _files.Remove(path);
        }

        /// <summary>
        /// Checks whether a file exists for provided path.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><c>true</c> if file exists for provided path; otherwise <c>false</c>.</returns>
        public virtual bool FileExists(string path)
        {
            return _files.ContainsKey(path);
        }

        /// <summary>
        /// Creates all directories and subdirectores in the specified path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public void CreateDirectory(string path)
        {
            // TODO: does nothing now
        }

        /// <summary>
        /// Gets the path to the folder provided by the storage for temporary files.
        /// </summary>
        /// <returns>The path to the temp folder.</returns>
        public string GetTempFolderPath()
        {
            return TempPath;
        }

        /// <summary>
        /// Ensures the file exists.
        /// </summary>
        /// <param name="path">The path to file.</param>
        private void EnsureFileExists(string path)
        {
            Ensure.ArgumentNotNullOrEmpty(path, "path");

            if (!_files.ContainsKey(path))
            {
                throw new FileNotFoundException(string.Format("Could not find file '{0}'", path));
            }
        }

        /// <summary>
        /// Special string writer, that updates file contents on disposing.
        /// </summary>
        private class FileStringWriter : StringWriter
        {
            private readonly string _filePath;
            private readonly IDictionary<string, string> _files;

            /// <summary>
            /// Initializes a new instance of the <see cref="FileStringWriter" /> class.
            /// </summary>
            /// <param name="files">Collection of all files.</param>
            /// <param name="filePath">The path to file.</param>
            public FileStringWriter(IDictionary<string, string> files, string filePath)
            {
                Ensure.ArgumentNotNull(files, "files");
                Ensure.ArgumentNotNullOrEmpty(filePath, "filePath");

                _files = files;
                _filePath = filePath;
            }

            /// <summary>
            /// Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.
            /// </summary>
            /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
            protected override void Dispose(bool disposing)
            {
                _files[_filePath] = ToString();

                base.Dispose(disposing);
            }
        }
    }
}
