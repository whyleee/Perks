using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class ReflectionExtensionsTests
    {
        private class AncestorType
        {
            protected double distance = 84.89;
            protected internal int length = 20;
        }

        private class BaseType : AncestorType
        {
            private int _count = 5;
            internal static long RES_ID = 0x12345678;
            private new int length = 55;
        }

        private class TestType : BaseType
        {
            private decimal _price = 55.49M;
            private static readonly string DEFAULT_MESSAGE = "Error";
            public const int MAX_COUNT = 10;
            public bool ForTests = true;
            protected new int length = 1000;
            private Exception error = null;
        }

        [Test]
        public void Field_Should_return_value_of_a_non_public_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<decimal>("_price");

            // asserts
            value.Should().Be(55.49M);
        }

        [Test]
        public void Field_Should_return_value_of_public_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<string>("DEFAULT_MESSAGE");

            // asserts
            value.Should().Be("Error");
        }

        [Test]
        public void Field_Should_return_value_of_static_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<int>("MAX_COUNT");

            // asserts
            value.Should().Be(10);
        }

        [Test]
        public void Field_Should_return_value_of_instance_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<bool>("ForTests");

            // asserts
            value.Should().Be(true);
        }

        [Test]
        public void Field_Should_return_value_of_instance_base_type_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<int>("_count");

            // asserts
            value.Should().Be(5);
        }

        [Test]
        public void Field_Should_return_value_of_static_base_type_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<long>("RES_ID");

            // asserts
            value.Should().Be(0x12345678);
        }

        [Test]
        public void Field_Should_return_value_of_protected_ancestor_type_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<double>("distance");

            // asserts
            value.Should().Be(84.89);
        }

        [Test]
        public void Field_Should_return_new_value_of_most_derived_type_field_by_name()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<int>("length");

            // asserts
            value.Should().Be(1000);
        }

        [Test]
        public void Field_Should_return_null_for_null_field_value()
        {
            // setups
            var obj = new TestType();

            // act
            var value = obj.Field<Exception>("error");

            // asserts
            value.Should().BeNull();
        }

        [Test]
        public void Field_When_not_found_by_name_Should_throw_argument_exception()
        {
            // setups
            var obj = new TestType();

            // act
            Action call = () => obj.Field<string>("NotExists");

            // asserts
            call.ShouldThrow<ArgumentException>().Where(x => x.ParamName == "name");
        }

        [Test]
        public void Field_When_value_type_and_requested_type_dont_match_Should_throw_invalid_cast_exception()
        {
            // setups
            var obj = new TestType();

            // act
            Action call = () => obj.Field<decimal>("MAX_COUNT");

            // asserts
            call.ShouldThrow<InvalidCastException>()
                .Where(x => x.Message.Contains("System.Int32") && x.Message.Contains("System.Decimal"));
        }
    }
}
