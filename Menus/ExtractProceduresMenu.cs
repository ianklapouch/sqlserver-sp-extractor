using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public class ExtractProceduresMenu
    {
        public static void Show(Connection connection)
        {
            bool running = true;
            string storedProceduresString = string.Empty;
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Enter list of stored procedures (comma separated):");

            while (running)
            {
                storedProceduresString = Console.ReadLine();
                if (!string.IsNullOrEmpty(storedProceduresString)){
                    running = false;
                }
            }

            string[] storedProcedures = storedProceduresString.Split(',');

            for (int i = 0; i < storedProcedures.Length; i++)
            {
                storedProcedures[i] = storedProcedures[i].Trim();
            }

            DbService dbService = new(connection);

            string outputFilesPath = OutputFilesService.GetOutputFilesPath();
            string outputDirectoryPath = Path.Combine(outputFilesPath, DateTime.Now.ToString("yyyy-MM-dd_HHmmss"));

            Directory.CreateDirectory(outputDirectoryPath);

            foreach (string storedProcedure in storedProcedures)
            {
                string storedProcedureText = dbService.GetStoredProcedureText(storedProcedure);
                if (!string.IsNullOrEmpty(storedProcedureText))
                {
                    storedProcedureText = storedProcedureText.Replace("CREATE", "ALTER");
                    string filePath = Path.Combine(outputDirectoryPath, $"{storedProcedure}.sql");
                    File.WriteAllText(filePath, storedProcedureText);
                }
            }

            dbService.CloseConnection();
            SuccessMenu.Show();

        }
    }
}
