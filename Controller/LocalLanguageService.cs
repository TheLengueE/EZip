namespace EZip.Controller
{
    using System.Globalization;
    using System.Resources;
    using Microsoft.Maui.Storage;

    /// <summary>
    /// This class is used to set the local language
    /// and get localized strings from the resource file.
    /// </summary>
    public class LocalLanguageService
    {
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCulture;
        private string _defaultLanguage = "zh-CN";

        /// <summary>
        /// Initializes the LocalLanguageService with the user's preferred language.
        /// </summary>
        public LocalLanguageService()
        {
            _resourceManager = new ResourceManager("EZip.Resources.Languages.Strings", typeof(LocalLanguageService).Assembly);
            var savedLanguage = Preferences.Get("AppLanguage", _defaultLanguage);

            // if SetLanguage fails, default to English
            _currentCulture = new CultureInfo("en-US");

            SetLanguage(savedLanguage); 
        }

        /// <summary>
        /// Gets the localized string for the given key.
        /// </summary>
        /// <param name="key">The key to look up in the resource file.</param>
        /// <returns>The localized string, or the key itself if not found.</returns>
        public string GetString(string key)
        {
            string? value = _resourceManager.GetString(key, _currentCulture);

            return value ?? key; 
        }

        /// <summary>
        /// Sets the application language and saves it as the user's preference.
        /// </summary>
        /// <param name="cultureCode">The culture code (e.g., "en" for English, "zh-CN" for Chinese).</param>
        public void SetLanguage(string cultureCode)
        {
            try
            {
                _currentCulture = new CultureInfo(cultureCode);
                CultureInfo.CurrentUICulture = _currentCulture;
                CultureInfo.CurrentCulture = _currentCulture;
                Preferences.Set("AppLanguage", cultureCode);
            }
            catch (CultureNotFoundException)
            {
                // write to log
            }
        }
    }
}
