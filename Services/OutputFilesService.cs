using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver_sp_extractor.Services
{
    public class OutputFilesService
    {
        private static readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string outputFilesPath = Path.Combine(documentsPath, "SQLServerSpExtractor");
        public static void CheckOutputFilesDirectory()
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
            new Process
            {
                StartInfo = new ProcessStartInfo(outputFilesPath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
