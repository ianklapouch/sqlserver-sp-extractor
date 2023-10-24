using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    internal class DeleteConnectionCommand : Command
    {
        public DeleteConnectionCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            List<Connection> connections = ConnectionsService.GetConnections();

            if (connections.Any())
            {

                Command[] commands = connections
                    .Select(connection => new DeleteItemCommand(connection.Name))
                    .ToArray();

                commands = commands.Concat(new Command[] { new ManageConnectionsCommand("GoBack") }).ToArray();

                SelectConnectionMenu.Show(commands);
            }
            else
            {
                ErrorMenu.Show($"No connections found! \nCreate a connection in 'Manage connections' -> 'Create new connection'");
            }
        }
    }
}
