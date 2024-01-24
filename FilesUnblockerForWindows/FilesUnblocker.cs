using System.Runtime.InteropServices;
using FilesUnblockerForWindows.Helpers;

namespace FilesUnblockerForWindows
{
    internal class FilesUnblocker
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string name);
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();


        private static async Task<bool> UnblockAsync(string fileName)
        {
            string zoneIdentifierPath = fileName + ":Zone.Identifier";

            if (File.Exists(zoneIdentifierPath))
            {
                try
                {
                    File.Delete(zoneIdentifierPath);
                    return true;
                }
                catch (Exception ex)
                {
                    if (Options.CreateLogFile)
                        _logger.Info(ex, "Exception");
                    return false;
                }
            }

            return false; // Zone.Identifier not found or already removed
        }

        public static async Task SearchDirectory(string directoryPath)
        {
            try
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    await ProcessFile(file);
                }

                if (!Options.UseRecursiveScan)
                    return;

                string[] directories = Directory.GetDirectories(directoryPath);

                foreach (string dir in directories)
                {
                    await SearchDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                if (Options.CreateLogFile)
                    _logger.Info(ex, "Exception");
            }
        }

        private static async Task ProcessFile(string filePath)
        {
            try
            {
                Counters.CheckedFilesCounter++;
                if (Options.CreateLogFile)
                    _logger.Info($"Current processing file: {filePath}");

                string zoneIdentifierPath = filePath + ":Zone.Identifier";

                if (File.Exists(zoneIdentifierPath))
                {
                    var status = await UnblockAsync(filePath);
                    Task.Run(async () =>
                    {
                        if (Options.WriteLogToConsole)
                            await Console.Out.WriteLineAsync($"File: {filePath} is blocked!");

                        if (Options.CreateLogFile)
                        {
                            _logger.Info($"File: {filePath} is blocked!");
                            _logger.Info($"Unblocking status: {status}");
                        }
                    }).GetAwaiter().GetResult();

                    Counters.UnblockFilesCounter++;
                }
                else
                {
                    if (Options.LogAlsoAlreadyUnblockedFiles)
                    {
                        if (Options.CreateLogFile)
                            _logger.Info($"This file isn't blocked: {filePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Options.CreateLogFile)
                    _logger.Error(ex, "Exception on unblocking file");
            }
        }
    }
}
