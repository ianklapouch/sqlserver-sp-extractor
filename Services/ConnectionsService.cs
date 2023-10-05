using Newtonsoft.Json;
using sqlserver_sp_extractor.Models;
using System.Diagnostics;

namespace sqlserver_sp_extractor.Services
{
    public class ConnectionsService
    {
        private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string connectionsDirectoryPath = Path.Combine(appDataPath, "SQLServerSpExtractor");
        private static readonly string connectionsFilePath = Path.Combine(connectionsDirectoryPath, "connections.json");

        public static void EnsureConnectionsFileExists()
        {
            if (!Directory.Exists(connectionsDirectoryPath))
            {
                Directory.CreateDirectory(connectionsDirectoryPath);
                List<Connection> connections = new();
                string connectionsJson = JsonConvert.SerializeObject(connections, Formatting.Indented);
                File.WriteAllText(connectionsFilePath, connectionsJson);
            }
        }

        private static void SaveConnections(List<Connection> connections)
        {
            string connectionsJson = JsonConvert.SerializeObject(connections, Formatting.Indented);
            File.WriteAllText(connectionsFilePath, connectionsJson);
        }

        public static void OpenConfigurationFile()
        {
            ProcessStartInfo startInfo = new(connectionsFilePath)
            {
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }

        public static List<Connection> GetConnections()
        {
            string connectionsJson = File.ReadAllText(connectionsFilePath);
            return JsonConvert.DeserializeObject<List<Connection>>(connectionsJson) ?? new List<Connection>();
        }

        public static void CreateConnection(Connection connection)
        {
            List<Connection> connections = GetConnections();
            connections.Add(connection);
            SaveConnections(connections);
        }
        public static Connection? GetConnectionByName(string name)
        {
            List<Connection> connections = GetConnections();
            return connections.FirstOrDefault(connection => connection.Name == name);
        }

        public static void UpdateConnection(Connection connection, int index)
        {
            List<Connection> connections = GetConnections();
            connections[index] = connection;
            SaveConnections(connections);
        }

        public static void DeleteConnection(string name)
        {
            List<Connection> connections = GetConnections();
            connections.RemoveAll(connection => connection.Name == name);
            SaveConnections(connections);
        }

        public static string GetConnectionsFilePath()
        {
            return connectionsFilePath;
        }
    }
}
