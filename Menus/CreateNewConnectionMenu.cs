namespace sqlserver_sp_extractor.Menus
{
    public class CreateNewConnectionMenu
    {
        private List<string> Attributes = new()
        {
            "Name: ",
            "DataBase: ",
            "Login: ",
            "Password: "
        };

        private List<int> AttributesBaseLength = new()
        {
            6,
            10,
            7,
            10
        };

        int selectedIndex = 0;
        bool exitConfirmation = false;
        bool saveConfirmation = false;

        public void Show()
        {
            bool running = true;

            WriteConsole();
            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        SelectPrevious();
                        break;
                    case ConsoleKey.DownArrow:
                        SelectNext();
                        break;
                    case ConsoleKey.Backspace:
                        RemoveText();
                        break;
                    case ConsoleKey.Escape:
                        exitConfirmation = true;
                        break;
                    case ConsoleKey.Enter:
                        //saveConfirmation = true;
                        break;
                    default:
                        ConcatText(keyInfo.KeyChar);
                        break;

                }
                WriteConsole(saveConfirmation, exitConfirmation);
            }
        }
        private void WriteConsole(bool saveConfirmation = false, bool exitConfirmation = false)
        {
            Console.Clear();
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (!saveConfirmation && !exitConfirmation &&i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(Attributes[i]);
                Console.ResetColor();
            }

            if (exitConfirmation)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Exit? ");
                Console.ResetColor();
                Console.WriteLine("\"ENTER\" - YES");
                Console.WriteLine("\"ESC\"   - NO");
            }

        }
        private void SelectNext() => selectedIndex = selectedIndex == 3 ? 0 : selectedIndex + 1;
        private void SelectPrevious() => selectedIndex = selectedIndex == 0 ? 3 : selectedIndex - 1;
        private void ConcatText(char c) => Attributes[selectedIndex] += c;
        private void RemoveText()
        {
            if (Attributes[selectedIndex].Length > AttributesBaseLength[selectedIndex])
            {
                Attributes[selectedIndex] = Attributes[selectedIndex][..^1];
            }
        }
    }
}
