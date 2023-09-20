namespace sqlserver_sp_extractor.Commands
{
    public class CommandManager
    {
        private readonly List<Command> commands = new();
        public int selectedIndex = 0;

        public void AddCommand(Command command)
        {
            commands.Add(command);
        }

        public void ExecuteSelectedCommand()
        {
            if (selectedIndex >= 0 && selectedIndex < commands.Count)
            {
                commands[selectedIndex].Execute();
            }
        }

        public void DisplayMenu(string? menuDescription = null)
        {
            Console.Clear();

            if (!string.IsNullOrEmpty(menuDescription))
            {
                Console.WriteLine(menuDescription);
                Console.WriteLine(string.Empty);
            }

            for (int i = 0; i < commands.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine($"{i + 1}. {commands[i].Name}");
                Console.ResetColor();
            }
        }

        public void SelectNext()
        {
            selectedIndex = Math.Min(selectedIndex + 1, commands.Count - 1);
        }

        public void SelectPrevious()
        {
            selectedIndex = Math.Max(selectedIndex - 1, 0);
        }
    }
}
