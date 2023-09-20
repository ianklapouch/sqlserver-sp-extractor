using sqlserver_sp_extractor.Menus;

namespace sqlserver_sp_extractor.Commands
{
    internal class SelectConnectionCommand : Command
    {
        public SelectConnectionCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            SelectConnectionMenu.Show();
        }
    }
}
