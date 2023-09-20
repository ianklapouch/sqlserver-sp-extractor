using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class SuccessMenu
    {
        public static void Show()
        {
            CommandManager commandManager = new();

            commandManager.AddCommand(new OpenOutputFilesDirectoryCommand("Open the output files directory"));
            commandManager.AddCommand(new MainMenuCommand("Return to main menu"));
            commandManager.AddCommand(new ExitCommand("Exit"));

            bool running = true;

            while (running)
            {
                commandManager.DisplayMenu("Files successfully generated in the \"Documents/SQLServerSpExtractor\" directory!");

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
                        if (commandManager.selectedIndex == 2)
                        {
                            running = false;
                        }
                        break;
                }
            }
        }
    }
}
