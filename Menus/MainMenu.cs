using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class MainMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();

            commandManager.AddCommand(new SelectConnectionCommand("Select connection"));
            commandManager.AddCommand(new CreateNewConnectionCommand("Create new connection"));
            commandManager.AddCommand(new OpenConfigurationFileCommand("Open configuration file"));
            commandManager.AddCommand(new ExitCommand("Exit"));

            bool running = true;

            while (running)
            {
                commandManager.DisplayMenu();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        commandManager.SelectPrevious();
                        break;

                    case ConsoleKey.DownArrow:
                        commandManager.SelectNext();
                        break;

                    case ConsoleKey.Enter:
                        commandManager.ExecuteSelectedCommand();
                        //if (commandManager.selectedIndex == 2)
                        //{
                            running = false;
                        //}
                        break;
                }
            }
        }
    }
}
