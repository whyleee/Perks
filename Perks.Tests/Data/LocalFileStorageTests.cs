using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Configuration;
using Perks.Data;
using Perks.Wrappers;

namespace Perks.Tests.Data
{
    public class LocalFileStorageTests : FixtureWithKernel
    {
        private OldLocalFileStorage storage;

        public LocalFileStorageTests()
        {
            kernel.Bind<IoWrapper>().ToMock().InSingletonScope();
        }

        [SetUp]
        public void SetUp()
        {
            storage = kernel.Get<OldLocalFileStorage>();

            MockTempFileCreation();
        }

        private void MockTempFileCreation()
        {
            var tempFile = kernel.Get<Stream>();
            kernel.Get<IoWrapper>().CreateFile(Arg.Any<string>()).Returns(tempFile);
        }

        [Test]
        public void CreateTempFile_For_empty_contents_Should_create_empty_temp_file_and_return_path_to_it()
        {
            // setups
            kernel.Get<IoWrapper>().GetTempPath().Returns(@"C:\temp");

            kernel.Get<IConfigProvider>().GetSetting("Storage.TempDirectory").Returns((string) null);

            // act
            var path = storage.CreateTempFile(null, "txt");

            // asserts
            path.Should().Match(@"C:\temp\*.txt");

            kernel.Get<IoWrapper>().Received().CreateFile(Arg.Is<string>(x => x.EndsWith(".txt")));
        }

        [Test]
        public void CreateTempFile_For_specified_contents_Should_create_temp_file_with_contents_and_return_path_to_it()
        {
            // setups
            var contents = new byte[] {1, 2, 3};

            kernel.Get<IoWrapper>().GetTempPath().Returns(@"C:\temp");

            kernel.Get<IConfigProvider>().GetSetting("Storage.TempDirectory").Returns((string) null);

            // act
            var path = storage.CreateTempFile(contents, "txt");

            // asserts
            path.Should().Match(@"C:\temp\*.txt");

            kernel.Get<IoWrapper>().Received().WriteAllBytes(Arg.Is<string>(x => x.EndsWith(".txt")), contents);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateTempFile_For_empty_extension_Should_create_temp_file_with_default_extension(string extension)
        {
            // setups
            var contents = new byte[] {1, 2, 3};

            kernel.Get<IoWrapper>().GetTempPath().Returns(@"C:\temp");

            kernel.Get<IConfigProvider>().GetSetting("Storage.TempDirectory").Returns((string) null);

            // act
            var path = storage.CreateTempFile(contents, extension);

            // asserts
            path.Should().EndWith(".tmp");
        }

        [Test]
        public void CreateTempFile_When_custom_temp_directory_is_configured_Should_create_temp_file_there()
        {
            // setups
            kernel.Get<IoWrapper>().GetTempPath().Returns(@"C:\temp");

            kernel.Get<IConfigProvider>().GetSetting("Storage.TempDirectory")
                .Returns(@"D:\work\temp");

            // act
            var path = storage.CreateTempFile(null, "txt");

            // asserts
            path.Should().Match(@"D:\work\temp\*.txt");
        }
    }
}
