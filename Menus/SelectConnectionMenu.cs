using sqlserver_sp_extractor.Commands;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public class SelectConnectionMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();

            Configuration configuration = ConfigurationService.GetConfiguration();

            if (configuration is not null && configuration.Connections.Count > 0)
            {
                foreach (var connection in configuration.Connections)
                {
                    commandManager.AddCommand(new SelectItemCommand(connection.Name));
                }
            }

            commandManager.AddCommand(new MainMenuCommand("GoBack"));

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
