namespace sqlserver_sp_extractor.Commands
{
    public abstract class Command
    {
        public string Name { get; }

        public Command(string name)
        {
            Name = name;
        }
        public abstract void Execute();
    }
}
