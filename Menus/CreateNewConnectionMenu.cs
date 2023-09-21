using sqlserver_sp_extractor.Commands;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public static class CreateNewConnectionMenu
    {
        private static readonly List<string> Attributes = new()
        {
            "Name: ",
            "Server Name: ",
            "DataBase: ",
            "Login: ",
            "Password: ",
            "1.Save",
            "2.GoBack"
        };

        private static List<int> AttributesBaseLength = new()
        {
            6,
            13,
            10,
            7,
            10
        };

        static int  selectedIndex = 0;
        static bool running = true;
        static bool saveError = false;

        public static void Show()
        {
            WriteConsole();
            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        saveError = false;
                        SelectPrevious();
                        break;
                    case ConsoleKey.DownArrow:
                        saveError = false;
                        SelectNext();
                        break;
                    case ConsoleKey.Tab:
                        saveError = false;
                        if (keyInfo.Modifiers == ConsoleModifiers.Shift)
                        {
                            SelectPrevious();
                        }
                        else
                        {
                            SelectNext();
                        }
                        break;
                    case ConsoleKey.Backspace:
                        RemoveText();
                        break;
                    case ConsoleKey.Escape:
                        if (selectedIndex < 6)
                        {
                            selectedIndex = 6;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (selectedIndex < 5)
                        {
                            selectedIndex = 5;
                        }
                        else if (selectedIndex == 5)
                        {
                            saveError = Save();
                        }
                        else if (selectedIndex == 6)
                        {
                            running = false;
                            new MainMenuCommand("").Execute();
                        }

                        break;
                    default:
                        if (selectedIndex < 5)
                        {
                            ConcatText(keyInfo.KeyChar);
                        }
                        break;


                }

                if (running)
                {
                    WriteConsole(saveError);
                }
            }
        }
        private static void WriteConsole(bool saveError = false)
        {
            Console.Clear();
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(Attributes[i]);
                Console.ResetColor();
            }
            if (saveError)
            {
                Console.WriteLine();
                Console.WriteLine("Fill in all fields!");
            }

        }
        private static void SelectNext() => selectedIndex = selectedIndex == 6 ? 0 : selectedIndex + 1;
        private static void SelectPrevious() => selectedIndex = selectedIndex == 0 ? 6 : selectedIndex - 1;
        private static void ConcatText(char c) => Attributes[selectedIndex] += c;
        private static void RemoveText()
        {
            if (Attributes[selectedIndex].Length > AttributesBaseLength[selectedIndex])
            {
                Attributes[selectedIndex] = Attributes[selectedIndex][..^1];
            }
        }
        private static bool Save()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Attributes[i].Length == AttributesBaseLength[i])
                {
                    return true;
                }
            }

            string name = Attributes[0].Substring(AttributesBaseLength[0]);
            string serverName = Attributes[1].Substring(AttributesBaseLength[1]);
            string dataBase = Attributes[2].Substring(AttributesBaseLength[2]);
            string login = Attributes[3].Substring(AttributesBaseLength[3]);
            string password = Attributes[4].Substring(AttributesBaseLength[4]);

            Connection connection = new()
            {
                Name = name,
                ServerName = serverName,
                DataBase = dataBase,
                Login = login,
                Password = password
            };

            ConfigurationService.AddConnection(connection);
            running = false;
            new MainMenuCommand("").Execute();

            return false;
        }
    }
}
