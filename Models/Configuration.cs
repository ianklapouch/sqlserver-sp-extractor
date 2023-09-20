using Newtonsoft.Json;

namespace sqlserver_sp_extractor.Models
{
    public class Configuration
    {
        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; } = new List<Connection>();
    }
}
