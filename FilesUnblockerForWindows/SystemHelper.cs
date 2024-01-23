using System.Configuration;

namespace FilesUnblockerForWindows
{
    public static class SystemHelper
    {
        public static void LoadSettings()
        {
            Options.CreateLogFile = ReadSetting("CreateLogFile", Options.CreateLogFile);
            Options.WriteLogToConsole = ReadSetting("WriteLogToConsole", Options.WriteLogToConsole);
            Options.LogAlsoAlreadyUnblockedFiles = ReadSetting("LogAlsoAlreadyUnblockedFiles", Options.LogAlsoAlreadyUnblockedFiles);
            Options.FastMode = ReadSetting("FastMode", Options.FastMode);
            Options.UseRecursiveScan = ReadSetting("UseRecursiveScan", Options.UseRecursiveScan);
            Options.AskIfUnlockDirFromClipboard = ReadSetting("AskIfUnlockDirFromClipboard", Options.AskIfUnlockDirFromClipboard);
        }

        public static void SaveSettings()
        {
            SaveSetting("CreateLogFile", Options.CreateLogFile);
            SaveSetting("WriteLogToConsole", Options.WriteLogToConsole);
            SaveSetting("LogAlsoAlreadyUnblockedFiles", Options.LogAlsoAlreadyUnblockedFiles);
            SaveSetting("FastMode", Options.FastMode);
            SaveSetting("UseRecursiveScan", Options.UseRecursiveScan);
            SaveSetting("AskIfUnlockDirFromClipboard", Options.AskIfUnlockDirFromClipboard);
        }

        private static bool ReadSetting(string key, bool defaultValue)
        {
            string setting = ConfigurationManager.AppSettings[key];
            if (setting != null)
            {
                return bool.Parse(setting);
            }
            return defaultValue;
        }

        private static void SaveSetting(string key, bool value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = config.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value.ToString());
            }
            else
            {
                settings[key].Value = value.ToString();
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
    }
}
