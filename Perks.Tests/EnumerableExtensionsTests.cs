using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class EnumerableExtensionsTests
    {
        [Test]
        public void IsEmpty_For_empty_collection_Should_return_true()
        {
            new int[] { }.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void IsEmpty_For_not_empty_collection_Should_return_false()
        {
            new[] { 1, 2, 3 }.IsEmpty().Should().BeFalse();
        }
    }
}
