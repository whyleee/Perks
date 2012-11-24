using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Perks.Wrappers
{
    /// <summary>
    /// The wrapper for .NET configuration API.
    /// </summary>
    public class ConfigWrapper
    {
        /// <summary>
        /// Gets the setting by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Setting value from appliction configuration file by its key.</returns>
        public virtual string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets connection string by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Connection string value from application configuration file by its name.</returns>
        public virtual string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].IfNotNull(x => x.ConnectionString);
        }
    }
}
