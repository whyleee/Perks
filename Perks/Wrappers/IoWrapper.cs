using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Perks.Wrappers
{
    /// <summary>
    /// The wrapper for .NET IO API.
    /// </summary>
    public class IoWrapper
    {
        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="path">The path where to create it.</param>
        /// <returns><see cref="Stream"/> to write data to the new file.</returns>
        public virtual Stream CreateFile(string path)
        {
            return File.Create(path);
        }

        /// <summary>
        /// Opens the file to read.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextReader"/> that can read a file.</returns>
        public virtual TextReader OpenRead(string path)
        {
            return new StreamReader(path);
        }

        /// <summary>
        /// Opens the file to write.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextWriter"/> that can write to a file.</returns>
        public virtual TextWriter OpenWrite(string path)
        {
            return new StreamWriter(path);
        }

        /// <summary>
        /// Reads entire file contents as array of bytes.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>Byte array with file contents.</returns>
        public virtual byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="bytes">New file contents.</param>
        public virtual void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        public virtual void DeleteFile(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// Gets the path to the default temporary folder configured for operating system.
        /// </summary>
        /// <returns>Path to the default temporary folder.</returns>
        public virtual string GetTempPath()
        {
            return Path.GetTempPath();
        }
    }
}
