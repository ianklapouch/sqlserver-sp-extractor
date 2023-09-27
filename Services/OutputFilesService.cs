using System.Diagnostics;

namespace sqlserver_sp_extractor.Services
{
    public class OutputFilesService
    {
        private static readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string outputFilesPath = Path.Combine(documentsPath, "SQLServerSpExtractor");
        public static void EnsureOutputFilesDirectoryExists()
        {
            if (!Directory.Exists(outputFilesPath))
            {
                Directory.CreateDirectory(outputFilesPath);
            }
        }
        public static string GetOutputFilesPath()
        {
            return outputFilesPath;
        }
        public static void OpenOutputFilesDirectory()
        {
            ProcessStartInfo startInfo = new(outputFilesPath)
            {
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }
    }
}
