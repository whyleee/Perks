﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Perks.Tests;
using Perks.Web.Configuration;
using Perks.Web.Wrappers;
using Perks.Wrappers;

namespace Perks.Web.Tests.Configuration
{
    public class WebAppSettingsConfigurationProviderTests : FixtureWithKernel
    {
        private WebAppSettingsConfigurationProvider config;

        public WebAppSettingsConfigurationProviderTests()
        {
            kernel.Bind<ConfigWrapper>().ToMock().InSingletonScope();
            kernel.Bind<HostWrapper>().ToMock().InSingletonScope();
        }

        [SetUp]
        public void SetUp()
        {
            config = kernel.Get<WebAppSettingsConfigurationProvider>();
        }

        [Test]
        public void GetSetting_When_value_is_virtual_path_Should_return_host_mapped_path()
        {
            // setups
            kernel.Get<ConfigWrapper>().GetSetting("imgdir_path").Returns("~/images");

            kernel.Get<HostWrapper>().MapPath("~/images").Returns(@"D:\web\site\images");

            // act
            var value = config.GetSetting("imgdir_path");

            // asserts
            value.Should().Be(@"D:\web\site\images");
        }
    }
}