namespace Perks.Configuration
{
    /// <summary>
    /// Represents any kind of configuration provider.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the setting from the configuration provider.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mandatory">if set to <c>true</c> setting should exist.</param>
        /// <returns>Setting value.</returns>
        string GetSetting(string key, bool mandatory = false);

        /// <summary>
        /// Gets the setting from the configuration provider.
        /// </summary>
        /// <typeparam name="T">The type of setting value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="mandatory">if set to <c>true</c> setting should exist.</param>
        /// <returns>Typed value for setting.</returns>
        T GetSetting<T>(string key, bool mandatory = false);
    }
}