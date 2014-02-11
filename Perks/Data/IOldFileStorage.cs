using System.IO;

namespace Perks.Data
{
    /// <summary>
    /// Represents any kind of file storage.
    /// </summary>
    public interface IOldFileStorage
    {
        /// <summary>
        /// Creates a temp file in the storage.
        /// </summary>
        /// <param name="contents">Temp file contents.</param>
        /// <param name="extension">Temp file extension.</param>
        /// <returns>Path to created temp file.</returns>
        string CreateTempFile(byte[] contents, string extension);

        /// <summary>
        /// Opens the file to read.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextReader"/> that can read a file.</returns>
        TextReader OpenRead(string path);

        /// <summary>
        /// Opens the file to write or creates a new file if not exist.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><see cref="TextWriter"/> that can write to a file.</returns>
        TextWriter OpenWrite(string path);

        /// <summary>
        /// Reads entire file contents as array of bytes.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>Byte array with file contents.</returns>
        byte[] ReadFile(string path);

        /// <summary>
        /// Reads entire file contents as string.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns>String with file contents.</returns>
        string ReadFileText(string path);

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="contents">New file contents.</param>
        void WriteFile(string path, byte[] contents);

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <param name="contents">New file contents.</param>
        void WriteFile(string path, string contents);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path to file.</param>
        void DeleteFile(string path);

        /// <summary>
        /// Checks whether a file exists for provided path.
        /// </summary>
        /// <param name="path">The path to file.</param>
        /// <returns><c>true</c> if file exists for provided path; otherwise <c>false</c>.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Creates all directories and subdirectores in the specified path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Gets the path to the folder provided by the storage for temporary files.
        /// </summary>
        /// <returns>The path to the temp folder.</returns>
        string GetTempFolderPath();
    }
}