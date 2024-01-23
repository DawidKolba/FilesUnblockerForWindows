using FilesUnblockerForWindows;
using System.Windows.Forms;

SystemHelper.LoadSettings();


string txt = Clipboard.GetText(TextDataFormat.UnicodeText).ToString();

bool extraUnblock = false;
if (Directory.Exists(txt))
{
    Console.WriteLine("Detected following path to directory in clipboard: \n" + txt);
    string odp;
    if (askIfUnlockDirFromClipboard)
    {
        Console.WriteLine("Would you like to unblock files in this directory with recursive scan?(y/n)");
        odp = Console.ReadLine().ToString();
    }
    else
    {
        odp = "y";
    }
    if (odp.ToLower() == "y")
    {
        Console.WriteLine("Extra unblock is active");
        extraUnblock = true;
    }
    else
    {

    }
    //Console.WriteLine("wprowadzona odp: " + odp);

}


loguj("Application started");
if (UsingRecursiveScan)
{
    loguj("Using recursive scan");
    searchDirectory();
    searchDirectory(Directory.GetCurrentDirectory());

    if (extraUnblock)
    {

        try
        {
            Console.WriteLine("extra unblock started");
            searchDirectory(txt, true);
        }
        catch { }
    }
    if (argScan)
    {

        try
        {
            Console.WriteLine("unblock files from paths given by cmd parameters started");
            searchDirectory(args[0], true);
        }
        catch { }
    }
}
else
{
    loguj("Without recursive scan");
    searchDirectory();
    if (extraUnblock)
    {

        try
        {
            Console.WriteLine("extra unblock started2");
            searchDirectory(txt, true);
        }
        catch { }
        if (argScan)
        {

            try
            {
                Console.WriteLine("unblock files from paths given by cmd parameters started");
                searchDirectory(args[0], true);
            }
            catch { }
        }
    }
}
loguj("Changed " + counterUnblockFiles + " files. Checked " + counterCheckedFiles + " files. Have a nice day ;)");
if (!FastMode)
{
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
}