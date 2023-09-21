using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class MainMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();

            commandManager.AddCommand(new SelectConnectionCommand("Extract procedures"));
            commandManager.AddCommand(new ManageConnectionsCommand("Manage connections"));
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

                    case ConsoleKey.Tab:
                        if (keyInfo.Modifiers == ConsoleModifiers.Shift)
                        {
                            commandManager.SelectPrevious();
                        }
                        else
                        {
                            commandManager.SelectNext();
                        }
                        break;

                    case ConsoleKey.Enter:
                        commandManager.ExecuteSelectedCommand();
                        running = false;
                        break;
                }
            }
        }
    }
}
