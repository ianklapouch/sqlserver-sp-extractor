using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class ErrorMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();

            commandManager.AddCommand(new MainMenuCommand("Return to main menu"));
            commandManager.AddCommand(new ExitCommand("Exit"));

            bool running = true;

            while (running)
            {
                commandManager.DisplayMenu("No stored procedures found, check the name of the inserted procedures and the connection to the database!");

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
