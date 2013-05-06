using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class ObjectExtensionsTests
    {
        [Test]
        public void IfNotNull_For_not_null_object_with_action_Should_perform_action_with_object()
        {
            // setups
            var called = false;
            var obj = new StringBuilder();

            // act
            obj.IfNotNull(x => called = true);

            // asserts
            called.Should().BeTrue();
        }

        [Test]
        public void IfNotNull_For_null_object_with_action_Should_not_do_anything()
        {
            // setups
            var called = false;
            var obj = (StringBuilder)null;

            // act
            obj.IfNotNull(x => called = true);

            // asserts
            called.Should().BeFalse();
        }

        [Test]
        public void IfNotNull_For_not_null_object_with_func_Should_return_result_from_func()
        {
            // setups
            var obj = new StringBuilder("hello");

            // act
            var result = obj.IfNotNull(x => x.ToString());

            // asserts
            result.Should().Be("hello");
        }

        [Test]
        public void IfNotNull_For_null_object_with_func_Should_return_null()
        {
            // setups
            var obj = (StringBuilder)null;

            // act
            var result = obj.IfNotNull(x => x.ToString());

            // asserts
            result.Should().BeNull();
        }

        [Test]
        public void If_When_assertion_is_passed_Should_return_object()
        {
            // setups
            var obj = new StringBuilder();

            // act
            var result = obj.If(x => x.Length == 0);

            // asserts
            result.Should().Be(obj);
        }

        [Test]
        public void If_When_assertion_is_failed_Should_return_null()
        {
            // setups
            var obj = new StringBuilder();

            // act
            var result = obj.If(x => x.Length > 0);

            // asserts
            result.Should().BeNull();
        }

        [Test]
        public void If_When_object_is_of_specified_type_Should_return_the_object()
        {
            // setups
            var obj = new EventArgs();

            // act
            var result = obj.If<EventArgs>();

            // asserts
            result.Should().Be(obj);
        }

        [Test]
        public void If_When_object_is_not_of_the_specified_type_Should_return_null()
        {
            // setups
            var obj = new EventArgs();

            // act
            var result = obj.If<StringBuilder>();

            // asserts
            result.Should().BeNull();
        }

        [Test]
        public void If_For_object_with_expression_When_object_is_of_specified_type_Should_return_the_value_of_expression()
        {
            // setups
            var obj = new StringBuilder("hello");

            // act
            var result = obj.If<StringBuilder>(x => x.ToString());

            // asserts
            result.Should().Be("hello");
        }

        [Test]
        public void If_For_object_with_expression_When_object_is_not_of_specified_type_Should_return_null()
        {
            // setups
            var obj = new EventArgs();

            // act
            var result = obj.If<StringBuilder>(x => x.ToString());

            // asserts
            result.Should().BeNull();
        }
    }
}
