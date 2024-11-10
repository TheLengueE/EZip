namespace EZip.Controller
{
    using System.Globalization;
    using System.Resources;

    /// <summary>
    /// This class is used to get localized strings from the resource files.
    /// </summary>
    public class LocalLanguageService
    {
        private readonly ResourceManager _resourceManager;
        private const string DefaultLanguage = "zh";

        /// <summary>
        /// Initializes the LocalLanguageService with the user's preferred language or the default language.
        /// </summary>
        public LocalLanguageService()
        {
            _resourceManager = new ResourceManager("EZip.Resources.Languages.Strings", typeof(LocalLanguageService).Assembly);

            var savedLanguage = Preferences.Get("AppLanguage", DefaultLanguage);
            SetLanguage(savedLanguage);
        }

        /// <summary>
        /// Gets the localized string for the given key.
        /// </summary>
        /// <param name="key">The key to look up in the resource file.</param>
        /// <returns>The localized string, or the key itself if not found.</returns>
        public string GetString(string key)
        {
            return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
        }

        /// <summary>
        /// Sets the application language and saves it as the user's preference.
        /// </summary>
        /// <param name="cultureCode">The culture code (e.g., "en" for English, "zh" for Chinese).</param>
        public void SetLanguage(string cultureCode)
        {
            try
            {
                var cultureInfo = new CultureInfo(cultureCode);
                CultureInfo.CurrentUICulture = cultureInfo;
                CultureInfo.CurrentCulture = cultureInfo;

                Preferences.Set("AppLanguage", cultureCode);
            }
            catch (CultureNotFoundException)
            {
                SetLanguage(DefaultLanguage);
            }
        }
    }
}
