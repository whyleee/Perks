using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Perks.Configuration;
using Perks.Wrappers;

namespace Perks.Data
{
    /// <summary>
    /// Local file system storage.
    /// </summary>
    public class LocalFileStorage : IStorage
    {
        protected readonly IConfigurationProvider _config;
        protected readonly IoWrapper _io;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalFileStorage" /> class.
        /// </summary>
        public LocalFileStorage(IConfigurationProvider config, IoWrapper io)
        {
            Ensure.ArgumentNotNull(config, "config");
            Ensure.ArgumentNotNull(io, "io");

            _config = config;
            _io = io;
        }

        /// <summary>
        /// Creates new file in the storage.
        /// </summary>
        /// <param name="path">The path where to create a file.</param>
        public virtual void CreateFile(string path)
        {
            _io.CreateFile(path).Close();
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

            var tempDirectory = _config.GetSetting("Storage.TempDirectory") ?? _io.GetTempPath();
            var path = Path.Combine(tempDirectory, string.Format("{0}.{1}", Guid.NewGuid().ToString("N"), extension));

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
            return _io.OpenRead(path);
        }

        /// <summary>
        /// Opens the file to write.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextWriter" /> that can write to a file.</returns>
        public virtual TextWriter OpenWrite(string path)
        {
            return _io.OpenWrite(path);
        }

        /// <summary>
        /// Reads entire file contents as array of bytes.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>Byte array with file contents.</returns>
        public virtual byte[] ReadFile(string path)
        {
            return _io.ReadAllBytes(path);
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="contents">New file contents.</param>
        public virtual void WriteFile(string path, byte[] contents)
        {
            _io.WriteAllBytes(path, contents);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        public virtual void DeleteFile(string path)
        {
            _io.DeleteFile(path);
        }
    }
}
