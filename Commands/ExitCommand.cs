namespace sqlserver_sp_extractor.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(string name) : base(name)
        {
        }
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
