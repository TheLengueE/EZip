namespace EZip.Controller
{
    using System.Globalization;
    using System.Text.Json;
    using Microsoft.Maui.Storage;

    /// <summary>
    /// JSON-based language service for dynamic localization.
    /// </summary>
    public class LocalLanguageService
    {
        private Dictionary<string, string> _translations = new();
        private string _currentLanguage = "zh-CN";  // en
        public event Action? OnLanguageChanged;

        public LocalLanguageService()
        {
            LoadLanguageFile(_currentLanguage);
        }

        /// <summary>
        /// Gets the localized string for the given key.
        /// </summary>
        /// <param name="key">The key to look up in the language dictionary.</param>
        /// <returns>The localized string, or the key itself if not found.</returns>
        public string GetString(string key)
        {
            if (_translations.TryGetValue(key, out var value))
                return value;

            return key; // fallback if key not found
        }

        public void SetLanguage(string languageCode)
        {
            if (_currentLanguage != languageCode)
            {
                LoadLanguageFile(languageCode);
                _currentLanguage = languageCode;
                OnLanguageChanged?.Invoke(); // 🔔 通知组件更新
            }
        }


        /// <summary>
        /// Loads the JSON translation file based on the culture code.
        /// </summary>
        /// <param name="cultureCode">Language code</param>
        private void LoadLanguageFile(string cultureCode)
        {
            try
            {
                _currentLanguage = cultureCode;
                CultureInfo.CurrentUICulture = new CultureInfo(cultureCode);
                CultureInfo.CurrentCulture = new CultureInfo(cultureCode);

                var fileName = $"Resources/Languages/lang.{cultureCode}.json";
                using var stream = FileSystem.OpenAppPackageFileAsync(fileName).Result;
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                _translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            }
            catch
            {
                _translations = new(); // fallback to empty if not found
            }
        }
    }
}

