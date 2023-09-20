using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    internal class OpenOutputFilesDirectoryCommand : Command
    {
        public OpenOutputFilesDirectoryCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            OutputFilesService.OpenOutputFilesDirectory();
        }
    }
}
