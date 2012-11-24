using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Configuration;
using Perks.Wrappers;

namespace Perks.Tests.Configuration
{
    public class AppSettingsConfigurationProviderTests : FixtureWithKernel
    {
        private AppSettingsConfigurationProvider config;

        public AppSettingsConfigurationProviderTests()
        {
            kernel.Bind<ConfigWrapper>().ToMock().InSingletonScope();
        }

        [SetUp]
        public void SetUp()
        {
            config = kernel.Get<AppSettingsConfigurationProvider>();
        }

        [Test]
        public void GetSetting_Should_return_setting_from_config_app_settings()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("username").Returns("john");

            // act
            var value = config.GetSetting("username");

            // asserts
            value.Should().Be("john");
        }

        [Test]
        public void GetSetting_When_value_is_empty_Should_return_null()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("username").Returns("");

            // act
            var value = config.GetSetting("username");

            // asserts
            value.Should().BeNull();
        }

        [Test]
        public void GetSetting_For_mandatory_When_value_is_null_Should_throw_configuration_errors_exception()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("username").Returns((string) null);

            // act
            Action call = () => config.GetSetting("username", mandatory: true);

            // asserts
            call.ShouldThrow<ConfigurationErrorsException>()
                .WithMessage("No configuration found for setting 'username'. It is mandatory");
        }

        [Test]
        public void GetSetting_For_mandatory_When_value_starts_from_enter_word_upper_cased_Should_throw_configuration_errors_exception()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("username").Returns("[ENTER USERNAME]");

            // act
            Action call = () => config.GetSetting("username", mandatory: true);

            // asserts
            call.ShouldThrow<ConfigurationErrorsException>()
                .WithMessage("No configuration found for setting 'username'. It is mandatory");
        }

        [Test]
        public void GetSetting_When_key_starts_with_connection_strings_token_Should_return_value_of_connection_string()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetConnectionString("AppDb").Returns("Server=.;Database=MyAppDb");

            // act
            var connectionString = config.GetSetting("ConnectionStrings.AppDb");

            // asserts
            connectionString.Should().Be("Server=.;Database=MyAppDb");
        }

        [Test]
        public void GetSettingTyped_Should_return_value_of_requested_type()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("def_color").Returns("Black");

            // act
            var statusCode = config.GetSetting<ConsoleColor>("def_color");

            // asserts
            statusCode.Should().Be(ConsoleColor.Black);
        }

        [Test]
        public void GetSettingTyped_When_value_is_null_Should_return_default_value_for_requested_type()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("def_width").Returns((string) null);

            // act
            var width = config.GetSetting<int>("def_width");

            // asserts
            width.Should().Be(0);
        }

        [Test]
        public void GetSettingTyped_Should_handle_any_exceptions_during_value_conversion_and_rethrow_configuration_errors_exception()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("min_date").Returns("wrong_val");

            // act
            Action call = () => config.GetSetting<DateTime>("min_date");

            // asserts
            call.ShouldThrow<ConfigurationErrorsException>()
                .WithMessage("Setting 'min_date' should be convertible to 'System.DateTime' type. Current value: 'wrong_val'")
                .WithInnerException<FormatException>();
        }
    }
}
