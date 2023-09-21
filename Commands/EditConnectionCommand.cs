using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    internal class EditConnectionCommand : Command
    {
        public EditConnectionCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            Configuration configuration = ConfigurationService.GetConfiguration();

            if (configuration is not null && configuration.Connections.Count > 0)
            {

                Command[] commands = configuration.Connections
                    .Select(connection => new DeleteItemCommand(connection.Name))
                    .ToArray();

                commands = commands.Concat(new Command[] { new ManageConnectionsCommand("GoBack") }).ToArray();

                SelectConnectionMenu.Show(commands);
            }
        }
    }
}
