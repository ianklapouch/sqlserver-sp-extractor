using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    internal class ManageConnectionsMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();
            commandManager.AddCommand(new CreateNewConnectionCommand("Create new connection"));
            commandManager.AddCommand(new EditConnectionCommand("Edit connection"));
            commandManager.AddCommand(new DeleteConnectionCommand("Delete connection"));
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
