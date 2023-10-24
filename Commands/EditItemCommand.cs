using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;

namespace sqlserver_sp_extractor.Commands
{
    public class EditItemCommand : Command
    {
        private readonly Connection connection;
        private readonly int index;
        
        public EditItemCommand(Connection connection, int index) : base(connection.Name)
        {
            this.connection = connection;
            this.index = index;
        }

        public override void Execute()
        {
            new EditConnectionMenu().Show(connection, index);
        }
    }
}
