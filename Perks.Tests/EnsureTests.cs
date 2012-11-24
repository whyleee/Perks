using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class EnsureTests
    {
        [Test]
        public void ArgumentNotNull_For_null_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => Ensure.ArgumentNotNull(null, "foo");

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "foo");
        }

        [Test]
        public void ArgumentNotNull_For_not_null_Should_be_ok()
        {
            // act
            Ensure.ArgumentNotNull(3, "foo");
        }

        [Test]
        public void ArgumentNotNullOrEmpty_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => Ensure.ArgumentNotNullOrEmpty(null, "str");

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "str");
        }

        [Test]
        public void ArgumentNotNullOrEmpty_For_empty_string_Should_throw_argument_exception()
        {
            // act
            Action call = () => Ensure.ArgumentNotNullOrEmpty(string.Empty, "str");

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "str");
        }

        [Test]
        public void ArgumentNotNullOrEmpty_For_not_empty_string_Should_be_ok()
        {
            // act
            Ensure.ArgumentNotNullOrEmpty("here we go", "str");
        }

        [Test]
        public void Argument_For_failed_assert_Should_throw_argument_exception()
        {
            // act
            Action call = () => Ensure.Argument(() => -5 > 0, "num", "should be a positive number");

            // asserts
            call.ShouldThrow<ArgumentException>()
                .Where(x => x.ParamName == "num")
                .Where(x => x.Message.StartsWith("should be a positive number"));
        }

        [Test]
        public void Argument_For_succeeded_assert_Should_be_ok()
        {
            // act
            Action call = () => Ensure.Argument(() => 7 > 0, "num", "should be a positive number");
        }
    }
}
