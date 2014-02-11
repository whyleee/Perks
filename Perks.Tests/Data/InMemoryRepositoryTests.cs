using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Ninject;
using Perks.Data;

namespace Perks.Tests.Data
{
    public class InMemoryRepositoryTests : FixtureWithKernel
    {
        public class Customer
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
        }

        public class Caller
        {
            public string Phone { get; set; }
        }

        private IOldRepository<T> CreateRepo<T>() where T : class
        {
            return kernel.Get<OldInMemoryRepository<T>>();
        }

        private IOldRepository<Customer> CreateRepoWithBobAndJames()
        {
            var repo = CreateRepo<Customer>();

            var bob = new Customer {Name = "Bob", Surname = "Marley"};
            var james = new Customer {Name = "James", Surname = "Bond"};

            repo.Save(bob);
            repo.Save(james);

            return repo;
        }
        
        [Test]
        public void GetAll_Should_return_all_repo_items()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var all = repo.GetAll();

            // asserts
            all.Should().HaveCount(2);
            all.Should().Contain(x => x.Name == "Bob");
            all.Should().Contain(x => x.Name == "James");
        }

        [Test]
        public void GetAll_When_repo_is_empty_Should_return_empty_collection()
        {
            // setups
            var repo = CreateRepo<Customer>();

            // act
            var all = repo.GetAll();

            // asserts
            all.Should().BeEmpty();
        }

        [Test]
        public void Get_Should_return_repo_item_by_id()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var customer = repo.Get("James");

            // asserts
            customer.Should().NotBeNull();
            customer.Name.Should().Be("James");
        }

        [Test]
        public void Get_When_item_is_not_found_Should_return_null()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var customer = repo.Get("John");

            // asserts
            customer.Should().BeNull();
        }

        [Test]
        public void Get_For_items_with_int_id_Should_work()
        {
            // setups
            var repo = CreateRepo<Product>();

            var table = new Product {Id = 1, Name = "Table", Price = 39.99M};
            var chair = new Product {Id = 2, Name = "Chair", Price = 14.59M};

            repo.Save(table);
            repo.Save(chair);

            // act
            var product = repo.Get(2);

            // asserts
            product.Should().Be(chair);
        }

        [Test]
        public void Get_For_items_with_guid_id_Should_work()
        {
            // setups
            var repo = CreateRepo<Book>();

            var cplusplusBookId = Guid.NewGuid();
            var csharpBookId = Guid.NewGuid();

            var cplusplusBook = new Book {Id = cplusplusBookId, Author = "Deitel", Name = "How to program on C++"};
            var csharpBook = new Book {Id = csharpBookId, Author = "Nagel", Name = "Professional C# and .NET"};

            repo.Save(cplusplusBook);
            repo.Save(csharpBook);

            // act
            var product = repo.Get(csharpBookId);

            // asserts
            product.Should().Be(csharpBook);
        }

        [Test]
        public void Get_For_items_with_phone_Should_work()
        {
            // setups
            var repo = CreateRepo<Caller>();

            var bob = new Caller {Phone = "11223344"};
            var james = new Caller {Phone = "87654321"};

            repo.Save(bob);
            repo.Save(james);

            // act
            var caller = repo.Get("11223344");

            // asserts
            caller.Should().Be(bob);
        }

        [Test]
        public void Save_For_new_item_Should_add_it_to_the_repo()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var john = new Customer {Name = "John", Surname = "Smith"};
            repo.Save(john);

            // asserts
            repo.GetAll().Should().HaveCount(3);
            repo.Get("John").Should().Be(john);
        }

        [Test]
        public void Save_For_existing_item_Should_update_it_in_the_repo()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var existing = repo.Get("James");
            existing.Surname = "Smith";

            repo.Save(existing);

            // asserts
            repo.GetAll().Should().HaveCount(2);
            repo.Get("James").Surname.Should().Be("Smith");
        }

        [Test]
        public void Save_For_existing_item_When_id_is_changed_Should_delete_old_item_and_add_new_item_to_the_repo()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var existing = repo.Get("James");
            existing.Name = "John";

            repo.Save(existing);

            // asserts
            repo.GetAll().Should().HaveCount(2);
            repo.Get("James").Should().BeNull();
            repo.Get("John").Should().Be(existing);
        }

        [Test]
        public void Delete_Should_delete_the_item_from_the_repo()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var bob = repo.Get("Bob");
            repo.Delete(bob);

            // asserts
            repo.GetAll().Should().HaveCount(1);
            repo.Get("Bob").Should().BeNull();
        }

        [Test]
        public void Delete_For_not_exising_item_Should_not_do_anything()
        {
            // setups
            var repo = CreateRepoWithBobAndJames();

            // act
            var john = new Customer { Name = "John", Surname = "Smith" };
            repo.Delete(john);

            // asserts
            repo.GetAll().Should().HaveCount(2);
        }
    }
}
