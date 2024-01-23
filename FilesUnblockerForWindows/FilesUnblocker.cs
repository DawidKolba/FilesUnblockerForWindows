using System.Runtime.InteropServices;


namespace FilesUnblockerForWindows
{
    internal class FilesUnblocker
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string name);
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static async Task<bool> Unblock(string fileName)
        {
            return DeleteFile(fileName + ":Zone.Identifier");
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
            if (Options.LogAlsoAlreadyUnblockedFiles)
            {
                if (Options.CreateLogFile)
                    _logger.Info($"Current processing file: {filePath}");
            }
            await Unblock(filePath);
        }
    }
}
