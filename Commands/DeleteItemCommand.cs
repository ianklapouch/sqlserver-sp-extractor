using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    public class DeleteItemCommand : Command
    {
        private readonly string name;

        public DeleteItemCommand(string name) : base(name)
        {
            this.name = name;
        }

        public override void Execute()
        {
            ConfigurationService.DeleteConnection(name);
            MainMenu.Show();
        }
    }
}
