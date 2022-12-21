# MetadataCleaner
Small executable that cleans metadata from all files in a specified directory

To build you only require .NET Framework 4.8:
Just open the solution and build with Release configuration.

2 ways to run:

1. Run using command promt with a specified path of a folder you want to clean
Example:
Cleaner.exe C:\dev
This will remove metadatada from all the files within "dev" folder and its subfolders.

2. Copy and place Cleaner.exe and exiftool.exe in a designated folder that contains files you want to clean and run Cleaner.exe
