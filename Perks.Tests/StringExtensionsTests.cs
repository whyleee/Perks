﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Perks.Tests
{
    public class StringExtensionsTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsNullOrEmpty_For_null_or_empty_string_Should_return_true(string nullOrEmpty)
        {
            nullOrEmpty.IsNullOrEmpty().Should().BeTrue();
        }

        [Test]
        public void IsNullOrEmpty_For_not_empty_string_Should_return_false()
        {
            "hello".IsNullOrEmpty().Should().BeFalse();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsNotNullOrEmpty_For_null_or_empty_string_Should_return_false(string nullOrEmpty)
        {
            nullOrEmpty.IsNotNullOrEmpty().Should().BeFalse();
        }

        [Test]
        public void IsNotNullOrEmpty_For_not_empty_string_Should_return_true()
        {
            "hello".IsNotNullOrEmpty().Should().BeTrue();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IfNotNullOrEmpty_For_null_or_empty_string_Should_return_null(string str)
        {
            // act
            var result = str.IfNotNullOrEmpty();

            // asserts
            result.Should().BeNull();
        }

        [Test]
        public void IfNotNullOrEmpty_For_not_empty_string_Should_return_that_string()
        {
            // act
            var result = "hello".IfNotNullOrEmpty();

            // asserts
            result.Should().Be("hello");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IfNotNullOrEmpty_For_null_or_empty_string_with_func_Should_return_null(string str)
        {
            // act
            var result = str.IfNotNullOrEmpty(x => x.ToUpper());

            // asserts
            result.Should().BeNull();
        }

        [Test]
        public void IfNotNullOrEmpty_For_not_empty_string_with_func_Should_return_result_from_func()
        {
            // act
            var result = "hello".IfNotNullOrEmpty(x => x.ToUpper());

            // asserts
            result.Should().Be("HELLO");
        }

        [Test]
        public void Contains_For_string_not_containing_specified_part_Should_return_false()
        {
            // act
            var result = "hello".Contains("holla", StringComparison.Ordinal);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void Contains_For_string_containing_specified_part_Should_return_true()
        {
            // act
            var result = "hello".Contains("hell", StringComparison.Ordinal);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void Contains_For_string_containing_specified_part_but_in_diff_case_Should_return_false()
        {
            // act
            var result = "hello".Contains("HELL", StringComparison.Ordinal);

            // asserts
            result.Should().BeFalse();
        }

        [Test]
        public void Contains_For_string_containing_specified_part_but_in_diff_case_and_ignore_case_comparison_is_used_Should_return_true()
        {
            // act
            var result = "hello".Contains("HELL", StringComparison.OrdinalIgnoreCase);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void Contains_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((string) null).Contains("other", StringComparison.Ordinal);

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "source");
        }

        [Test]
        public void Contains_For_empty_string_Should_not_throw_exception()
        {
            // act
            var result = string.Empty.Contains(string.Empty, StringComparison.Ordinal);

            // asserts
            result.Should().BeTrue();
        }

        [Test]
        public void ToTitleCase_Should_return_string_with_each_word_starting_from_a_char_in_upper_case()
        {
            // act
            var result = "hello world".ToTitleCase();

            // asserts
            result.Should().Be("Hello World");
        }

        [Test]
        public void ToTitleCase_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((string) null).ToTitleCase();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "str");
        }

        [Test]
        public void ToTitleCase_For_empty_string_Should_return_empty_string()
        {
            // act
            var result = string.Empty.ToTitleCase();

            // asserts
            result.Should().Be(string.Empty);
        }

        [Test]
        public void ToCamelCase_Should_lower_first_char_in_string()
        {
            // act
            var result = "HelloWorld".ToCamelCase();

            // asserts
            result.Should().Be("helloWorld");
        }

        [Test]
        public void ToCamelCase_For_string_with_one_char_Should_return_that_char_lowered()
        {
            // act
            var result = "D".ToCamelCase();

            // asserts
            result.Should().Be("d");
        }

        [Test]
        public void ToCamelCase_For_string_with_first_lowered_char_Should_return_not_changed_string()
        {
            // act
            var result = "helloWorld".ToCamelCase();

            // asserts
            result.Should().Be("helloWorld");
        }

        [Test]
        public void ToCamelCase_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((string) null).ToCamelCase();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "source");
        }

        [Test]
        public void ToCamelCase_For_empty_string_Should_return_empty_string()
        {
            // act
            var result = string.Empty.ToCamelCase();

            // asserts
            result.Should().Be(string.Empty);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CutTo_For_null_or_empty_string_Should_return_that_string(string nullOrEmpty)
        {
            nullOrEmpty.CutTo(10).Should().Be(nullOrEmpty);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void CutTo_For_small_strings_Should_return_that_string(int maxChars)
        {
            "hello".CutTo(maxChars).Should().Be("hello");
        }

        [Test]
        public void CutTo_For_too_big_string_Should_return_cut_string()
        {
            "Hello world!".CutTo(5).Should().Be("Hello");
        }

        [Test]
        public void CutTo_For_too_big_string_When_last_word_become_splitted_Should_cut_up_to_the_last_word()
        {
            "Hello world!".CutTo(10).Should().Be("Hello");
        }

        [Test]
        public void CutTo_For_too_big_string_without_spaces_Should_just_cut_the_string()
        {
            "[{\"some\":\"json\"}]".CutTo(10).Should().Be("[{\"some\":\"");
        }

        [Test]
        public void CutTo_If_insertion_string_is_specified_Should_return_cut_string_ending_with_insertion()
        {
            "Hello world!".CutTo(10, "...").Should().Be("Hello...");
        }

        [Test]
        public void ToSecureString_Should_return_secure_equivalent_of_source_string()
        {
            // act
            var result = "hello".ToSecureString();

            // asserts
            result.ToInsecureString().Should().Be("hello");
        }

        [Test]
        public void ToSecureString_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((string) null).ToSecureString();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "source");
        }

        [Test]
        public void ToInsecureString_Should_return_ordinary_string_equivalent()
        {
            // setups
            var secureString = "hello".ToSecureString();

            // act
            var result = secureString.ToInsecureString();

            // asserts
            result.Should().Be("hello");
        }

        [Test]
        public void ToInsecureString_For_null_string_Should_throw_argument_null_exception()
        {
            // act
            Action call = () => ((SecureString) null).ToInsecureString();

            // asserts
            call.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "s");
        }

        [Test]
        public void ToFriendlyString_Should_insert_spaces_between_words()
        {
            "HelloToTheWorld!".ToFriendlyString().Should().Be("Hello To The World!");
        }

        [Test]
        public void ToFriendlyString_Should_not_insert_spaces_in_acronyms()
        {
            "HandsUpNYPD!".ToFriendlyString().Should().Be("Hands Up NYPD!");
        }

        [Test]
        public void ToFriendlyString_Should_insert_spaces_between_numbers_and_words()
        {
            "8Plus8Equals16Right?".ToFriendlyString().Should().Be("8 Plus 8 Equals 16 Right?");
        }

        [Test]
        public void ToFriendlyString_Should_not_do_anything_with_friendly_string()
        {
            "I'm already OK!".ToFriendlyString().Should().Be("I'm already OK!");
        }
    }
}
