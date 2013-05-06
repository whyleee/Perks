using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Security;
using Perks.Web.Security;
using Perks.Web.Wrappers;

namespace Perks.Tests.Web.Security
{
    public class AspNetMembershipServiceTests : FixtureWithKernel
    {
        private AspNetMembershipService membership;

        public AspNetMembershipServiceTests()
        {
            kernel.Bind<MembershipWrapper>().ToMock().InSingletonScope();
        }

        [SetUp]
        public void SetUp()
        {
            membership = kernel.Get<AspNetMembershipService>();
        }

        [Test]
        public void IsUserExists_Should_return_true_for_existing_user()
        {
            // setups
            var john = new User {Name = "John"};

            kernel.Get<MembershipWrapper>().GetUser("john").Returns(john);

            // act
            var userExists = membership.IsUserExists("john");

            // asserts
            userExists.Should().BeTrue();
        }

        [Test]
        public void IsUserExists_Should_return_false_for_not_existing_user()
        {
            membership.IsUserExists("ghost").Should().BeFalse();
        }
    }
}
