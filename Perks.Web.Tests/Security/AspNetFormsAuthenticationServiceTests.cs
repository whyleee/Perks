using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Security;
using Perks.Tests;
using Perks.Web.Security;
using Perks.Web.Wrappers;

namespace Perks.Web.Tests.Security
{
    public class AspNetFormsAuthenticationServiceTests : FixtureWithKernel
    {
        private AspNetFormsAuthenticationService authService;

        public AspNetFormsAuthenticationServiceTests()
        {
            kernel.Bind<FormsAuthWrapper>().ToMock().InSingletonScope();
            kernel.Bind<HttpContextBase>().To<SimpleHttpContext>().InSingletonScope()
                  .WithConstructorArgument("url", "http://mysite.com");
        }

        [SetUp]
        public void SetUp()
        {
            authService = kernel.Get<AspNetFormsAuthenticationService>();
        }

        [Test]
        public void LogIn_For_valid_credentials_Should_set_auth_cookie_and_return_true()
        {
            // setups
            kernel.Get<IMembershipService>().IsValidUser("john", "1234").Returns(true);

            // act
            var loggedIn = authService.LogIn("john", "1234", rememberMe: true);

            // asserts
            loggedIn.Should().BeTrue();

            kernel.Get<FormsAuthWrapper>().Received().SetAuthCookie("john", rememberMe: true);
        }

        [Test]
        public void LogIn_For_not_valid_credentials_Should_return_false()
        {
            // setups
            kernel.Get<IMembershipService>().IsValidUser("thief", "1111").Returns(false);

            // act
            var loggedIn = authService.LogIn("thief", "1111", rememberMe: true);

            // asserts
            loggedIn.Should().BeFalse();
        }

        [Test]
        public void User_Should_return_http_context_user()
        {
            // setups
            var john = new GenericIdentity("john");
            var johnPrincipal = new GenericPrincipal(john, new string[] {});

            kernel.Get<HttpContextBase>().User = johnPrincipal;

            // act
            authService.User.Should().Be(john);
        }
    }
}
