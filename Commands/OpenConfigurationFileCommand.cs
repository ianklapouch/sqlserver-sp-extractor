using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    internal class OpenConfigurationFileCommand : Command
    {
        public OpenConfigurationFileCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            ConnectionsService.OpenConfigurationFile();
        }
    }
}
