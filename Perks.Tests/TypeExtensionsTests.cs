using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class TypeExtensionsTests
    {
        [Test]
        [TestCase(typeof(bool))]
        [TestCase(typeof(byte))]
        [TestCase(typeof(sbyte))]
        [TestCase(typeof(short))]
        [TestCase(typeof(ushort))]
        [TestCase(typeof(int))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(long))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(IntPtr))]
        [TestCase(typeof(UIntPtr))]
        [TestCase(typeof(char))]
        [TestCase(typeof(double))]
        [TestCase(typeof(Single))]
        public void IsSimpleType_For_primitive_type_Should_return_true(Type primitiveType)
        {
            primitiveType.IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_string_type_Should_return_true()
        {
            typeof(string).IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_datetime_type_Should_return_true()
        {
            typeof(DateTime).IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_decimal_type_Should_return_true()
        {
            typeof(decimal).IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_guid_type_Should_return_true()
        {
            typeof(Guid).IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_datetime_offset_type_Should_return_true()
        {
            typeof(DateTimeOffset).IsSimpleType().Should().BeTrue();
        }

        [Test]
        public void IsSimpleType_For_timespan_type_Should_return_true()
        {
            typeof(TimeSpan).IsSimpleType().Should().BeTrue();
        }

        [Test]
        [TestCase(typeof(Exception))]
        [TestCase(typeof(IEnumerable<>))]
        [TestCase(typeof(Type))]
        [TestCase(typeof(Attribute))]
        [TestCase(typeof(EventArgs))]
        public void IsSimpleType_For_any_type_except_string_Should_return_false(Type complexType)
        {
            complexType.IsSimpleType().Should().BeFalse();
        }

        [Test]
        [TestCase(typeof (int?))]
        [TestCase(typeof (ConsoleColor?))]
        [TestCase(typeof(Nullable<bool>))]
        public void IsNullableValueType_For_value_type_that_allows_nulls_Should_return_true(Type valueTypeAllowingNulls)
        {
            valueTypeAllowingNulls.IsNullableValueType().Should().BeTrue();
        }

        [Test]
        [TestCase(typeof(int))]
        [TestCase(typeof(ConsoleColor))]
        [TestCase(typeof(EventHandler<>))]
        [TestCase(typeof(string))]
        [TestCase(typeof(Exception))]
        public void IsNullableValueType_For_ordinary_value_type_or_any_reference_type_Should_return_false(Type ordinaryValueTypeOrReferenceType)
        {
            ordinaryValueTypeOrReferenceType.IsNullableValueType().Should().BeFalse();
        }
    }
}
