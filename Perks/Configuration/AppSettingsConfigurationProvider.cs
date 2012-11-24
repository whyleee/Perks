using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using Perks.Wrappers;

namespace Perks.Configuration
{
    /// <summary>
    /// Provider for standard .NET application configuration file.
    /// </summary>
    public class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        protected readonly ConfigWrapper _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsConfigurationProvider" /> class.
        /// </summary>
        public AppSettingsConfigurationProvider(ConfigWrapper config)
        {
            Ensure.ArgumentNotNull(config, "config");

            _config = config;
        }

        /// <summary>
        /// Gets the setting from the application configuration file.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mandatory">if set to <c>true</c> setting should exist.</param>
        /// <returns>Setting value.</returns>
        /// <remarks>
        /// For mandatory setting without value or with value starting from "[ENTER" string, <see cref="ConfigurationErrorsException"/> will be thrown.
        /// </remarks>
        public virtual string GetSetting(string key, bool mandatory = false)
        {
            if (key != null && key.StartsWith("ConnectionStrings."))
            {
                return _config.GetConnectionString(key.Substring("ConnectionStrings.".Length));
            }

            var value = _config.GetSetting(key).IfNotNullOrEmpty();

            if (mandatory && (value == null || value.StartsWith("[ENTER")))
            {
                throw new ConfigurationErrorsException(string.Format("No configuration found for setting '{0}'. It is mandatory", key));
            }

            return value;
        }

        /// <summary>
        /// Gets the setting from the application configuration file.
        /// </summary>
        /// <typeparam name="T">The type of setting value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="mandatory">if set to <c>true</c> setting should exist.</param>
        /// <returns>Typed value for setting.</returns>
        /// <remarks>
        /// For mandatory setting without value or with value starting from "[ENTER" string, <see cref="ConfigurationErrorsException"/> will be thrown.
        /// If setting value can't be converted to the specified type, <see cref="ConfigurationErrorsException"/> also will be thrown.
        /// </remarks>
        public virtual T GetSetting<T>(string key, bool mandatory = false)
        {
            var value = GetSetting(key, mandatory);

            if (value == null)
            {
                return default(T);
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                return (T) converter.ConvertFromString(value);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(string.Format("Setting '{0}' should be convertible to '{1}' type. Current value: '{2}'", key, typeof(T).FullName, value), ex);
            }
        }
    }
}
