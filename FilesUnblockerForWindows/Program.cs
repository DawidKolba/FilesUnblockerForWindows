using FilesUnblockerForWindows;
using FilesUnblockerForWindows.Helpers;

class Program
{
    static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    [STAThread]
    static void Main(string[] args)
    {
        SystemHelper.LoadSettings();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        static void log(string message)
        {
            Console.WriteLine(message);

            if (Options.CreateLogFile)
                _logger.Info(message);
        }

        bool extraUnblock = false;
        string getDirectoryFromClipboard = Clipboard.GetText(TextDataFormat.UnicodeText);

        if (Directory.Exists(getDirectoryFromClipboard))
        {
            Console.WriteLine("Detected following path to directory in clipboard: \n" + getDirectoryFromClipboard);
            string answer = "y";

            if (Options.AskIfUnlockDirFromClipboard)
            {
                Console.WriteLine("Would you like to unblock files in this directory with recursive scan?(y/n)");
                answer = Console.ReadLine();
            }

            if (answer?.ToLower() == "y")
            {
                Console.WriteLine("Extra unblock is active");
                extraUnblock = true;
            }
        }

        log("Application started");
        FilesUnblocker.SearchDirectory(Directory.GetCurrentDirectory());

        if (extraUnblock)
        {
            FilesUnblocker.SearchDirectory(getDirectoryFromClipboard);
        }

        if (args.Length > 0)
        {
            Console.WriteLine("Unblocking files from paths given by cmd parameters started");
            FilesUnblocker.SearchDirectory(args[0]);
        }

        log($"Changed {Counters.UnblockFilesCounter} files. Checked {Counters.CheckedFilesCounter} files. Have a nice day ;)");

        if (!Options.FastMode)
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
