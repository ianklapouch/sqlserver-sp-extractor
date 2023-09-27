using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    public class EditItemCommand : Command
    {
        private readonly string name;

        public EditItemCommand(string name) : base(name)
        {
            this.name = name;
        }

        public override void Execute()
        {
            Connection connection = ConnectionsService.GetConnectionByName(name);
            EditConnectionMenu.Show(connection);
        }
    }
}
