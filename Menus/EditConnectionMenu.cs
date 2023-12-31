﻿using sqlserver_sp_extractor.Commands;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    internal class EditConnectionMenu
    {
        private readonly List<string> Attributes = new()
        {
            "Name: ",
            "Server Name: ",
            "DataBase: ",
            "Login: ",
            "Password: ",
            "1.Save",
            "2.GoBack"
        };

        private List<int> AttributesBaseLength = new()
        {
            6,
            13,
            10,
            7,
            10
        };

        int currentConnectionIndex = 0;
        int selectedIndex = 0;
        bool running = true;
        bool saveError = false;

        public void Show(Connection connection, int index)
        {
            currentConnectionIndex = index;
            LoadConnection(connection);

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
        private void LoadConnection(Connection connection)
        {
            Attributes[0] += connection.Name;
            Attributes[1] += connection.ServerName;
            Attributes[2] += connection.DataBase;
            Attributes[3] += connection.Login;
            Attributes[4] += connection.Password;
        }
        private void WriteConsole(bool saveError = false)
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
        private void SelectNext() => selectedIndex = selectedIndex == 6 ? 0 : selectedIndex + 1;
        private void SelectPrevious() => selectedIndex = selectedIndex == 0 ? 6 : selectedIndex - 1;
        private void ConcatText(char c) => Attributes[selectedIndex] += c;
        private void RemoveText()
        {
            if (Attributes[selectedIndex].Length > AttributesBaseLength[selectedIndex])
            {
                Attributes[selectedIndex] = Attributes[selectedIndex][..^1];
            }
        }
        private bool Save()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Attributes[i].Length == AttributesBaseLength[i])
                {
                    return true;
                }
            }

            string name = Attributes[0][AttributesBaseLength[0]..];
            string serverName = Attributes[1][AttributesBaseLength[1]..];
            string dataBase = Attributes[2][AttributesBaseLength[2]..];
            string login = Attributes[3][AttributesBaseLength[3]..];
            string password = Attributes[4][AttributesBaseLength[4]..];

            Connection connection = new()
            {
                Name = name,
                ServerName = serverName,
                DataBase = dataBase,
                Login = login,
                Password = password
            };

            ConnectionsService.UpdateConnection(connection, currentConnectionIndex);
            running = false;
            new MainMenuCommand("").Execute();

            return false;
        }
    }
}
