using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class SuccessMenu
    {
        public static void Show(int generated, int inserted, string outputFilesPath, List<string> storedProceduresNotFound)
        {

            string menuTitleMessage;

            if (generated == inserted)
            {
                menuTitleMessage = "Files successfully generated in the \"Documents/SQLServerSpExtractor\" directory!";
            }
            else
            {
                menuTitleMessage = $"{generated} of the {inserted} stored procedures were generated the \"Documents/SQLServerSpExtractor\" directory! \n\n";
                menuTitleMessage += "Stored Procedures not found: \n";
                foreach (string storedProcedure in storedProceduresNotFound)
                {
                    menuTitleMessage += $"- {storedProcedure}\n";
                }
            }


            CommandManager commandManager = new();

            commandManager.AddCommand(new OpenFileCommand("Open the output files directory", outputFilesPath));
            commandManager.AddCommand(new MainMenuCommand("Return to main menu"));
            commandManager.AddCommand(new ExitCommand("Exit"));

            bool running = true;

            while (running)
            {
                commandManager.DisplayMenu(menuTitleMessage);

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
                        if (commandManager.selectedIndex != 0)
                        {
                            running = false;
                        }
                        break;
                }
            }
        }
    }
}
