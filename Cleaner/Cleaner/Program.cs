using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Cleaner
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var filepath = args[0];
      if (string.IsNullOrEmpty(filepath))
      {
        var executingPath = GetExecutingDirectory();
        filepath = executingPath.FullName;
      }
      
      CleanMetadata(filepath);
      DeleteFiles(filepath);

      Console.ReadLine();

    }
    public static DirectoryInfo GetExecutingDirectory()
    {
      var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
      return new FileInfo(location.AbsolutePath).Directory;
    }

    private static void CleanMetadata(string path)
    {
      var startInfo = new ProcessStartInfo
      {
        CreateNoWindow = false,
        UseShellExecute = false,
        FileName = @"exiftool.exe",
        WindowStyle = ProcessWindowStyle.Hidden
      };
      var dir = new DirectoryInfo(path);
      var allFolders = dir.GetDirectories("*", SearchOption.AllDirectories);
      foreach (var folder in allFolders)
      {
        startInfo.Arguments = "exiftool -all= " + folder.FullName;
        var process = Process.Start(startInfo);
        process?.WaitForExit();
      }
      startInfo.Arguments = "exiftool -all= " + path;
      var process2 = Process.Start(startInfo);
      process2?.WaitForExit();

      Console.WriteLine("Finished cleaning metadata from files");
    }


    private static void DeleteFiles(string path)
    {
      
      Console.WriteLine("");
      Console.WriteLine("Started removing originals with metadata");

      var dir = new DirectoryInfo(path);
      var allFiles = dir.GetFiles("*", SearchOption.AllDirectories);
      foreach (var file in allFiles)
      {
        if (file.Exists && file.Name.Contains("_original"))
        {
          try
          {
            file.Delete();
            Console.WriteLine("File: {0} deleted successfully", file.Name);
          }
          catch
          {
            Console.WriteLine("File: {0} could not be deleted", file.Name);
          }
        }

      }

      Console.WriteLine("Finished removing duplicates.");
      Console.WriteLine($"Successfully removed metadata from {allFiles.Length} files.");
      Console.WriteLine("Press any key to close...");
    }
  }
}
