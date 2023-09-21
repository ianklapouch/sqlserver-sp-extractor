using Newtonsoft.Json;

namespace sqlserver_sp_extractor.Models
{
    public class Connection
    {
        public Connection() { }
        public Connection(string name, string serverName, string dataBase, string login, string password)
        {
            Name = name;
            ServerName = serverName;
            DataBase = dataBase;
            Login = login;
            Password = password;
        }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("serverName")]
        public required string ServerName { get; set; }
        [JsonProperty("dataBase")]
        public required string DataBase { get; set; }
        [JsonProperty("login")]
        public required string Login { get; set; }
        [JsonProperty("password")]
        public required string Password { get; set; }
    }
}
