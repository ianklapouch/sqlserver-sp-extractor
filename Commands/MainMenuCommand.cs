using sqlserver_sp_extractor.Menus;

namespace sqlserver_sp_extractor.Commands
{
    internal class MainMenuCommand : Command
    {
        public MainMenuCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            MainMenu.Show();
        }
    }
}
