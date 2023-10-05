using System.Diagnostics;

namespace sqlserver_sp_extractor.Commands
{
    internal class OpenFileCommand : Command
    {
        private readonly string path;
        public OpenFileCommand(string name, string path) : base(name) {
            this.path = path;
        }
        public override void Execute()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
