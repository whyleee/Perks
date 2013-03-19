using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ToUnixTime_Should_return_count_of_seconds_passed_for_time_from_1st_of_January_1970()
        {
            // act
            var unixTime = new DateTime(1971, 2, 3).ToUnixTime();

            // asserts
            unixTime.Should().Be(3600 * 24 * 365 /* year */
                               + 3600 * 24 * 31  /* month */
                               + 3600 * 24 * 2L  /* two days */);
        }

        [Test]
        public void ToUnixMilliTime_Should_return_count_of_milliseconds_passed_for_time_from_1st_of_January_1970()
        {
            // act
            var unixMilliTime = new DateTime(1971, 2, 3).ToUnixMilliTime();

            // asserts
            unixMilliTime.Should().Be(1000L * 3600 * 24 * 365 /* year */
                                    + 1000L * 3600 * 24 * 31  /* month */
                                    + 1000L * 3600 * 24 * 2   /* two days */);
        }

        [Test]
        public void FromUnixTime_Should_return_date_for_seconds_added_to_1st_of_January_1970()
        {
            // act
            var time = (3600L * 24 * 100 /* 100 days */).FromUnixTime();

            // asserts
            time.Should().Be(new DateTime(1970, 4, 11));
        }

        [Test]
        public void FromUnixMilliTime_Should_return_date_for_milliseconds_added_to_1st_of_January_1970()
        {
            // act
            var time = (1000L * 3600 * 24 * 200 /* 200 days */).FromUnixMilliTime();

            // asserts
            time.Should().Be(new DateTime(1970, 7, 20));
        }
    }
}
