using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FluentAssertions;
using NUnit.Framework;
using Perks.Tests;

namespace Perks.Web.Tests
{
    public class HttpContextExtensionsTests : FixtureWithKernel
    {
        [Test]
        public void GetHostUrl_Should_return_scheme_and_host_of_request_url()
        {
            // setups
            var request = new SimpleHttpRequest("http://mysite.com/products/5");

            // asserts
            request.GetHostUrl().Should().Be("http://mysite.com");
        }

        [Test]
        public void GetHostUrl_For_null_request_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((HttpRequestBase) null).GetHostUrl();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "request");
        }

        [Test]
        public void GetHostDomain_Should_return_first_level_domain_name_of_request_url()
        {
            // setups
            var request = new SimpleHttpRequest("http://mysite.com/products/5");

            // asserts
            request.GetHostDomain().Should().Be("com");
        }

        [Test]
        public void GetHostDomain_For_hosts_only_of_first_level_domain_Should_return_host_name()
        {
            // setups
            var request = new SimpleHttpRequest("http://localhost/products/5");

            // asserts
            request.GetHostDomain().Should().Be("localhost");
        }

        [Test]
        public void GetHostDomain_For_null_request_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((HttpRequestBase) null).GetHostDomain();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "request");
        }
    }
}
