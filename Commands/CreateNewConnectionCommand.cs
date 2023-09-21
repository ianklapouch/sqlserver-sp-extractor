using sqlserver_sp_extractor.Menus;

namespace sqlserver_sp_extractor.Commands
{
    internal class CreateNewConnectionCommand : Command
    {
        public CreateNewConnectionCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            CreateNewConnectionMenu.Show();
        }
    }
}
