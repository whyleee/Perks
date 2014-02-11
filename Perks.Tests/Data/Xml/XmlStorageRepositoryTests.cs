using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.Parameters;
using Perks.Data;
using Perks.Data.Xml;

namespace Perks.Tests.Data.Xml
{
    public class XmlStorageRepositoryTests : FixtureWithKernel
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        public class PersonWithGuidId
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        private XmlStorageRepository<T> CreateRepo<T>() where T : class
        {
            return kernel.Get<XmlStorageRepository<T>>(
                new ConstructorArgument("xmlPath", @"D:\some\path.xml")
            );
        }

        [Test]
        public void On_construction_When_xml_storage_not_exists_Should_create_empty_storage()
        {
            // setups
            kernel.Get<IOldFileStorage>().FileExists(@"D:\some\path.xml").Returns(false);

            // act
            var repo = CreateRepo<Person>();

            // asserts
            kernel.Get<IOldFileStorage>().Received().WriteFile(@"D:\some\path.xml", Arg.Is<string>(x =>
                x.StartsWith("<?xml version=\"1.0\"?>") && x.Contains("<root>") && x.Contains("</root>")));
        }

        [Test]
        public void GetAll_Should_return_collection_of_objects_from_xml()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <root>
                          <Person>
                            <Id>11</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </Person>
                          <Person>
                            <Id>12</Id>
                            <Name>James</Name>
                            <Surname>Bond</Surname>
                          </Person>
                        </root>";
            var doc = new StringReader(xml);

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);

            // act
            var all = CreateRepo<Person>().GetAll();

            // asserts
            all.Should().HaveCount(2);
            all.Should().Contain(x => x.Id == 11 && x.Name == "Bob" && x.Surname == "Marley");
            all.Should().Contain(x => x.Id == 12 && x.Name == "James" && x.Surname == "Bond");
        }

        [Test]
        public void GetAll_For_empty_xml_root_element_Should_return_empty_collection()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <root>
                        </root>";
            var doc = new StringReader(xml);

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);

            // act
            var all = CreateRepo<Person>().GetAll();

            // asserts
            all.Should().BeEmpty();
        }

        [Test]
        public void Get_For_type_with_int_id_Should_return_element_by_id_from_xml()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <Users>
                          <Person>
                            <Id>11</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </Person>
                          <Person>
                            <Id>12</Id>
                            <Name>James</Name>
                            <Surname>Bond</Surname>
                          </Person>
                        </Users>";
            var doc = new StringReader(xml);

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);

            // act
            var user = CreateRepo<Person>().Get(12);

            // asserts
            user.Id.Should().Be(12);
            user.Name.Should().Be("James");
            user.Surname.Should().Be("Bond");
        }

        [Test]
        public void Get_For_type_with_guid_id_Should_return_element_by_id_from_xml()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <Users>
                          <PersonWithGuidId>
                            <Id>a15832d0-6796-4146-8597-f4ffcd321184</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </PersonWithGuidId>
                          <PersonWithGuidId>
                            <Id>cc0e1d01-6400-42e3-847e-c0c9e237fa3f</Id>
                            <Name>James</Name>
                            <Surname>Bond</Surname>
                          </PersonWithGuidId>
                        </Users>";
            var doc = new StringReader(xml);

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);

            var bobId = new Guid("cc0e1d01-6400-42e3-847e-c0c9e237fa3f");

            // act
            var user = CreateRepo<PersonWithGuidId>().Get(bobId);

            // asserts
            user.Id.Should().Be(bobId);
            user.Name.Should().Be("James");
            user.Surname.Should().Be("Bond");
        }

        [Test]
        public void Get_When_element_is_not_found_in_xml_Should_return_null()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <Users>
                          <Person>
                            <Id>11</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </Person>
                          <Person>
                            <Id>12</Id>
                            <Name>James</Name>
                            <Surname>Bond</Surname>
                          </Person>
                        </Users>";
            var doc = new StringReader(xml);

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);

            // act
            var user = CreateRepo<Person>().Get(20);

            // asserts
            user.Should().BeNull();
        }

        [Test]
        public void Save_For_new_item_Should_add_the_item_to_xml_and_save()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <Users>
                          <Person>
                            <Id>11</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </Person>
                        </Users>";
            var doc = new StringReader(xml);
            var resultXmlBuilder = new StringBuilder();

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);
            kernel.Get<IOldFileStorage>().OpenWrite(Arg.Any<string>())
                .Returns(new StringWriter(resultXmlBuilder));

            var james = new Person {Id = 12, Name = "James", Surname = "Bond"};

            // act
            CreateRepo<Person>().Save(james);
            var resultXml = resultXmlBuilder.ToString();

            // asserts
            resultXml.Replace(" ", "").Replace("\r\n", "")
                .Should().Contain("<Person><Id>12</Id><Name>James</Name><Surname>Bond</Surname></Person>");
        }

        [Test]
        public void Delete_Should_delete_the_item_from_xml_and_save()
        {
            // setups
            var xml = @"<?xml version=""1.0""?>
                        <Users>
                          <Person>
                            <Id>11</Id>
                            <Name>Bob</Name>
                            <Surname>Marley</Surname>
                          </Person>
                          <Person>
                            <Id>12</Id>
                            <Name>James</Name>
                            <Surname>Bond</Surname>
                          </Person>
                        </Users>";
            var doc = new StringReader(xml);
            var resultXmlBuilder = new StringBuilder();

            kernel.Get<IOldFileStorage>().OpenRead(Arg.Any<string>()).Returns(doc);
            kernel.Get<IOldFileStorage>().OpenWrite(Arg.Any<string>())
                .Returns(new StringWriter(resultXmlBuilder));

            var james = new Person {Id = 12, Name = "James", Surname = "Bond"};

            // act
            CreateRepo<Person>().Delete(james);
            var resultXml = resultXmlBuilder.ToString();

            // asserts
            resultXml.Should().NotContain("12");
            resultXml.Should().NotContain("James");
            resultXml.Should().NotContain("Bond");
        }
    }
}
