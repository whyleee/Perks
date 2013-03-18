using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Ninject;
using Perks.Data;

namespace Perks.Tests.Data
{
    public class InMemoryFileStorageTests : FixtureWithKernel
    {
        private InMemoryFileStorage storage;

        [SetUp]
        public void SetUp()
        {
            storage = kernel.Get<InMemoryFileStorage>();
        }

        [Test]
        public void CreateFile_Should_create_new_file_with_empty_contents()
        {
            // setups
            var path = @"D:\work\file.txt";

            // act
            storage.CreateFile(path);

            // asserts
            storage.FileExists(path).Should().BeTrue();
            storage.ReadFileText(path).Should().BeEmpty();
        }

        [Test]
        public void CreateFile_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.CreateFile(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void CreateFile_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.CreateFile(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void CreateTempFile_For_empty_contents_Should_create_empty_temp_file_and_return_path_to_it()
        {
            // act
            var path = storage.CreateTempFile(null, "txt");

            // asserts
            path.Should().EndWith(".txt");
            storage.FileExists(path).Should().BeTrue();
            storage.ReadFileText(path).Should().BeEmpty();
        }

        [Test]
        public void CreateTempFile_For_specified_contents_Should_create_temp_file_with_contents_and_return_path_to_it()
        {
            // setups
            var contents = new byte[] {1, 2, 3};

            // act
            var path = storage.CreateTempFile(contents, "txt");

            // asserts
            path.Should().EndWith(".txt");
            storage.FileExists(path).Should().BeTrue();
            storage.ReadFile(path).Should().BeEquivalentTo(contents);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateTempFile_For_empty_extension_Should_create_temp_file_with_default_extension(string extension)
        {
            // setups
            var contents = new byte[] {1, 2, 3};

            // act
            var path = storage.CreateTempFile(contents, extension);

            // asserts
            path.Should().EndWith(".tmp");
        }

        [Test]
        public void OpenRead_Should_return_text_reader_for_file_contents()
        {
            // setups
            var path = @"D:\work\file.txt";

            storage.WriteFile(path, "Hello world");

            // act
            var reader = storage.OpenRead(path);
            var contents = reader.ReadToEnd();

            // asserts
            contents.Should().Be("Hello world");
        }

        [Test]
        public void OpenRead_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.OpenRead(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void OpenRead_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.OpenRead(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void OpenRead_When_file_is_not_found_Should_throw_file_not_found_exception()
        {
            // act
            Action call = () => storage.OpenRead(@"D:\wrong\path.txt");

            // asserts
            call.ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void OpenWrite_Should_return_text_writer_for_the_file()
        {
            // setups
            var path = @"D:\work\file.txt";

            storage.CreateFile(path);

            // act
            using (var writer = storage.OpenWrite(path))
            {
                writer.Write("Hello world");
            }
            
            // asserts
            storage.ReadFileText(path).Should().Be("Hello world");
        }

        [Test]
        public void OpenWrite_When_file_not_exists_Should_create_new_file_and_return_text_writer_for_it()
        {
            // setups
            var path = @"D:\work\file.txt";

            // act
            using (var writer = storage.OpenWrite(path))
            {
                writer.Write("Hello world");
            }

            // asserts
            storage.ReadFileText(path).Should().Be("Hello world");
        }

        [Test]
        public void OpenWrite_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.OpenWrite(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void OpenWrite_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.OpenWrite(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void ReadFile_Should_return_file_contents()
        {
            // setups
            var path = @"D:\work\file.txt";
            var fileContents = new byte[] {1, 2, 3};
            
            storage.WriteFile(path, fileContents);

            // act
            var result = storage.ReadFile(path);

            // asserts
            result.Should().BeEquivalentTo(fileContents);
        }

        [Test]
        public void ReadFile_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.ReadFile(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void ReadFile_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.ReadFile(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void ReadFile_When_file_is_not_found_Should_throw_file_not_found_exception()
        {
            // act
            Action call = () => storage.ReadFile(@"D:\wrong\path.txt");

            // asserts
            call.ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void ReadFileText_Should_return_file_content_as_string()
        {
            // setups
            var path = @"D:\work\file.txt";
            var fileContents = "Hello world";

            storage.WriteFile(path, fileContents);

            // act
            var result = storage.ReadFileText(path);

            // asserts
            result.Should().BeEquivalentTo(fileContents);
        }

        [Test]
        public void ReadFileText_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.ReadFileText(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void ReadFileText_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.ReadFileText(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void ReadFileText_When_file_is_not_found_Should_throw_file_not_found_exception()
        {
            // act
            Action call = () => storage.ReadFileText(@"D:\wrong\path.txt");

            // asserts
            call.ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void WriteFile_Should_write_new_contents_to_the_file()
        {
            var path = @"D:\work\file.txt";
            var fileContents = new byte[] {1, 2, 3};

            storage.CreateFile(path);

            // act
            storage.WriteFile(path, fileContents);

            // asserts
            storage.ReadFile(path).Should().BeEquivalentTo(fileContents);
        }

        [Test]
        public void WriteFile_When_file_not_exists_Should_create_new_file_and_write_contents_to_it()
        {
            var path = @"D:\work\file.txt";
            var fileContents = new byte[] {1, 2, 3};

            // act
            storage.WriteFile(path, fileContents);

            // asserts
            storage.ReadFile(path).Should().BeEquivalentTo(fileContents);
        }

        [Test]
        public void WriteFile_For_null_path_Should_throw_argument_null_exception()
        {
            // setups
            var fileContents = new byte[] {1, 2, 3};

            // act
            Action call = () => storage.WriteFile(null, fileContents);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void WriteFile_For_empty_path_Should_throw_argument_exception()
        {
            // setups
            var fileContents = new byte[] {1, 2, 3};

            // act
            Action call = () => storage.WriteFile(string.Empty, fileContents);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void WriteFile_For_string_contents_Should_write_new_contents_to_the_file()
        {
            var path = @"D:\work\file.txt";
            var fileContents = "Hello world";

            storage.CreateFile(path);

            // act
            storage.WriteFile(path, fileContents);

            // asserts
            storage.ReadFileText(path).Should().Be(fileContents);
        }

        [Test]
        public void WriteFile_For_string_contents_When_file_not_exists_Should_create_new_file_and_write_contents_to_it()
        {
            var path = @"D:\work\file.txt";
            var fileContents = "Hello world";

            // act
            storage.WriteFile(path, fileContents);

            // asserts
            storage.ReadFileText(path).Should().Be(fileContents);
        }

        [Test]
        public void WriteFile_For_null_path_and_string_contents_Should_throw_argument_null_exception()
        {
            // setups
            var fileContents = "Hello world";

            // act
            Action call = () => storage.WriteFile(null, fileContents);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void WriteFile_For_empty_path_and_string_contents_Should_throw_argument_exception()
        {
            // setups
            var fileContents = "Hello world";

            // act
            Action call = () => storage.WriteFile(string.Empty, fileContents);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void DeleteFile_Should_delete_file_from_the_storage()
        {
            // setups
            var path = @"D:\work\file.txt";

            storage.CreateFile(path);

            // act
            storage.DeleteFile(path);

            // asserts
            storage.FileExists(path).Should().BeFalse();
        }

        [Test]
        public void DeleteFile_When_file_not_exists_Should_not_do_anything()
        {
            // setups
            var path = @"D:\work\file.txt";

            // act
            storage.DeleteFile(path);

            // asserts
            storage.FileExists(path).Should().BeFalse();
        }

        [Test]
        public void DeleteFile_For_null_path_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => storage.DeleteFile(null);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void DeleteFile_For_empty_path_Should_throw_argument_exception()
        {
            // act
            Action call = () => storage.DeleteFile(string.Empty);

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "path");
        }

        [Test]
        public void FileExists_When_file_exists_Should_return_true()
        {
            // setups
            var path = @"D:\work\file.txt";

            storage.CreateFile(path);

            // act
            var exists = storage.FileExists(path);

            // asserts
            exists.Should().BeTrue();
        }

        [Test]
        public void FileExists_When_file_not_exists_Should_return_false()
        {
            // setups
            var path = @"D:\work\file.txt";

            // act
            var exists = storage.FileExists(path);

            // asserts
            exists.Should().BeFalse();
        }

        [Test]
        public void CreateDirectory_Should_not_do_anything()
        {
            // setups
            var path = @"D:\work";

            // act
            Action call = () => storage.CreateDirectory(path);

            // asserts
            call.ShouldNotThrow();
        }

        [Test]
        public void GetTempFolderPath_Should_return_path_to_the_C_temp_folder()
        {
            // act
            var tempPath = storage.GetTempFolderPath();

            // asserts
            tempPath.Should().Be(@"C:\temp");
        }
    }
}
