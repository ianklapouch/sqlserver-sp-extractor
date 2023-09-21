using Newtonsoft.Json;
using sqlserver_sp_extractor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace sqlserver_sp_extractor.Services
{
    public class ConfigurationService
    {
        private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string configurationFolderPath = Path.Combine(appDataPath, "SQLServerSpExtractor");
        private static readonly string configurationFilePath = Path.Combine(configurationFolderPath, "configuration.json");

        public static void CheckConfigurations()
        {
            EnsureConfigFileExists();
        }

        private static void EnsureConfigFileExists()
        {
            if (!File.Exists(configurationFilePath))
            {
                Configuration configuration = new Configuration();
                SaveConfiguration(configuration);
            }
        }

        private static void SaveConfiguration(Configuration configuration)
        {
            string configurationJson = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(configurationFilePath, configurationJson);
        }

        public static void OpenConfigurationFile()
        {
            EnsureConfigFileExists();
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
            EnsureConfigFileExists();
            Configuration configuration = GetConfiguration();
            configuration.Connections.Add(connection);
            SaveConfiguration(configuration);
        }

        public static Configuration GetConfiguration()
        {
            EnsureConfigFileExists();
            string configurationJson = File.ReadAllText(configurationFilePath);
            return JsonConvert.DeserializeObject<Configuration>(configurationJson) ?? new Configuration();
        }

        public static Connection? GetConnectionByName(string name)
        {
            EnsureConfigFileExists();
            Configuration configuration = GetConfiguration();
            return configuration.Connections.FirstOrDefault(connection => connection.Name == name);
        }

        public static void DeleteConnection(string name)
        {
            EnsureConfigFileExists();
            Configuration configuration = GetConfiguration();
            configuration.Connections.RemoveAll(connection => connection.Name == name);
            SaveConfiguration(configuration);
        }
    }
}
