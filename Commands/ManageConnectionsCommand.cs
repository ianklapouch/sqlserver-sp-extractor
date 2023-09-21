using sqlserver_sp_extractor.Menus;

namespace sqlserver_sp_extractor.Commands
{
    internal class ManageConnectionsCommand : Command
    {
        public ManageConnectionsCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            ManageConnectionsMenu.Show();
        }
    }
}
