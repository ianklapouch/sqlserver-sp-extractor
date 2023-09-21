using Newtonsoft.Json;
using sqlserver_sp_extractor.Models;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace sqlserver_sp_extractor.Services
{
    public class ConfigurationService
    {
        private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string configurationFolderPath = Path.Combine(appDataPath, "SQLServerSpExtractor");
        private static readonly string configurationFilePath = Path.Combine(appDataPath, "SQLServerSpExtractor\\configuration.json");
        public static void CheckConfigurations()
        {
            CheckDirectory(configurationFolderPath);
            CheckFile(configurationFilePath);
        }

        static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                using FileStream fs = File.Create(path);

                Configuration configuration = new();
                string configurationJson = JsonConvert.SerializeObject(configuration, Formatting.Indented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(configurationJson);
                fs.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static void OpenConfigurationFile()
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(configurationFilePath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        public static void AddConnection(Connection connection)
        {
            string configurationJson = File.ReadAllText(configurationFilePath);
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(configurationJson);

            if (configuration is not null)
            {
                using FileStream fs = File.Create(configurationFilePath);
                configuration.Connections.Add(connection);
                configurationJson = JsonConvert.SerializeObject(configuration, Formatting.Indented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(configurationJson);
                fs.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static Configuration GetConfiguration()
        {
            string configurationJson = File.ReadAllText(configurationFilePath);
            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
        }

        public static Connection? GetConnectionByName(string name)
        {
            string configurationJson = File.ReadAllText(configurationFilePath);
            if (string.IsNullOrEmpty(configurationJson))
            {
                return null;
            }

            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(configurationJson);
            Connection connection = configuration.Connections.FirstOrDefault(connection => connection.Name == name);
            if (connection is not null)
            {
                return connection;
            }

            return null;
        }
    }
}
