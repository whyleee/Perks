using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Perks.Data;
using Xunit;

namespace Perks.Tests.Data
{
    public class LocalFileTests : IocFixture
    {
        [Fact]
        public void NewFile_With_null_path_Throws_argument_null_exception()
        {
            Action call = () => new LocalFile(null);
            call.ShouldThrow<ArgumentNullException>().Where(ex => ex.ParamName == "path");
        }

        [Fact]
        public void NewFile_With_empty_path_Throws_argument_exception()
        {
            Action call = () => new LocalFile("");
            call.ShouldThrow<ArgumentException>().Where(ex => ex.ParamName == "path");
        }

        [Fact]
        public void NewFile_Sets_path()
        {
            var file = new LocalFile(@"C:\new.txt");
            file.Path.Should().Be(@"C:\new.txt");
        }

        [Fact]
        public void NewFile_Returns_zero_length()
        {
            var file = new LocalFile(@"D:\work\start");
            file.Length.Should().Be(0);
        }

        [Fact]
        public void GetName_Returns_file_name_from_path()
        {
            var file = new LocalFile(@"C:\temp\file.txt");
            file.Name.Should().Be("file.txt");
        }

        [Fact]
        public void SetName_Updates_file_path()
        {
            var file = new LocalFile(@"C:\temp\file.txt");
            file.Name = "renamed.txt";
            file.Path.Should().Be(@"C:\temp\renamed.txt");
        }
    }
}
