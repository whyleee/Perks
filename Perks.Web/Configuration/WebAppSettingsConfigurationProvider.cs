using System;
using Perks.Configuration;
using Perks.Web.Wrappers;
using Perks.Wrappers;

namespace Perks.Web.Configuration
{
    /// <summary>
    /// Provider for standard .NET web application configuration file.
    /// </summary>
    public class WebAppSettingsConfigurationProvider : AppSettingsConfigurationProvider
    {
        protected readonly HostWrapper _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAppSettingsConfigurationProvider" /> class.
        /// </summary>
        public WebAppSettingsConfigurationProvider(ConfigWrapper config, HostWrapper host) : base(config)
        {
            Ensure.ArgumentNotNull(host, "host");

            _host = host;
        }

        /// <summary>
        /// Gets the setting from the web application configuration file.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mandatory">if set to <c>true</c> setting should exist.</param>
        /// <returns>Setting value.</returns>
        /// <remarks>
        /// <para>
        /// For mandatory setting without value or with value starting from "[ENTER" string, <see cref="ConfigurationErrorsException"/> will be thrown.
        /// </para>
        /// <para>
        /// If setting value starts with "~/" string, it will be treated as a virtual path and mapped to a physical path automatically.
        /// </para>
        /// </remarks>
        public override string GetSetting(string key, bool mandatory = false)
        {
            var value = base.GetSetting(key, mandatory);

            if (value != null && value.StartsWith("~/"))
            {
                value = _host.MapPath(value);
            }

            return value;
        }
    }
}
