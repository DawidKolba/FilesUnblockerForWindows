# FilesUnblockerForWindows

## Overview
FilesUnblockerForWindows is a utility designed to bulk-unblock files on Windows that may have been restricted due to their origin, such as files downloaded from the internet. Windows often blocks these files for safety reasons, and FilesUnblockerForWindows provides a convenient way to remove these restrictions.

## Features

- **Clipboard Path Detection**: Automatically detects a file path copied to the clipboard and attempts to unblock all files found in that location.
- **Recursive Directory Scan**: Offers an option to recursively scan and unblock files within a directory, ensuring that all nested files are also processed.
- **Command Line Parameters**: Supports operation with command-line parameters, allowing users to specify file paths directly through the terminal.
- **Customizable Options**: Includes several options to customize the behavior of the program, such as enabling a log file, opting for a fast mode, and deciding whether to perform a recursive scan.

## Usage

1. **Clipboard Monitoring**: Simply copy a directory path to your clipboard. If the program detects a valid path, it will prompt you (unless configured otherwise) to confirm unblocking files in that directory, including subdirectories.

2. **Command Line**: You can also use the program via command line by providing a path as an argument. This is useful for integrating the utility into scripts or batch operations.

3. **Options Configuration**: Customize the program's behavior through various flags, such as enabling detailed logging or fast mode for quicker execution.

## Configuration Options

- `CreateLogFile`: Enable or disable the creation of a log file to record the program's operations.
- `FastMode`: Speed up the process by minimizing prompts and user interactions.
- `UseRecursiveScan`: Determine whether to recursively scan directories for files to unblock.

## Getting Started

To use FilesUnblockerForWindows, clone the repository and compile the program with your preferred C# development environment. Ensure that .NET Framework is installed on your machine.

After compiling, run the executable directly or via command line with the appropriate options set as per your requirements.

## Note

This project is developed as a part of my professional portfolio. Feel free to explore the code, and suggestions for improvements or collaborations are always welcome.

## Contributing

Contributions are welcome. Please feel free to fork the repository, make changes, and submit pull requests. If you're planning to propose major changes, please open an issue first to discuss what you would like to change.

## Contact

If you have any questions or suggestions, please open an issue in the repository.
