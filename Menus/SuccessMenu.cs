﻿using sqlserver_sp_extractor.Commands;

namespace sqlserver_sp_extractor.Menus
{
    public class SuccessMenu
    {
        public static void Show(int generated, int inserted, string outputFilesPath)
        {

            string menuTitleMessage;

            if (generated == inserted)
            {
                menuTitleMessage = "Files successfully generated in the \"Documents/SQLServerSpExtractor\" directory!";
            }
            else
            {
                menuTitleMessage = $"{generated} of the {inserted} stored procedures were generated the \"Documents/SQLServerSpExtractor\" directory!";
            }


            CommandManager commandManager = new();

            commandManager.AddCommand(new OpenOutputFilesDirectoryCommand("Open the output files directory"));
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
                        running = false;
                        break;
                }
            }
        }
    }
}
