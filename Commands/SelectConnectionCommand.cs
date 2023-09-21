using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    internal class SelectConnectionCommand : Command
    {
        public SelectConnectionCommand(string name) : base(name) { }
   
        public override void Execute()
        {
            Configuration configuration = ConfigurationService.GetConfiguration();

            if (configuration is not null && configuration.Connections.Count > 0)
            {
                Command[] commands = configuration.Connections
                  .Select(connection => new SelectItemCommand(connection.Name))
                  .ToArray();

                commands = commands.Concat(new Command[] { new MainMenuCommand("GoBack") }).ToArray();

                SelectConnectionMenu.Show(commands);
            }
        }
    }
}
