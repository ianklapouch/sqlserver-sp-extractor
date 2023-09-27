using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public class ExtractProceduresMenu
    {
        public static void Show(Connection connection)
        {
            bool running = true;
            string? storedProceduresString = string.Empty;
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Enter list of stored procedures (comma separated):");

            while (running)
            {
                storedProceduresString = Console.ReadLine();
                if (!string.IsNullOrEmpty(storedProceduresString))
                {
                    running = false;
                    Console.CursorVisible = false;
                }
            }

            string[] storedProcedures = storedProceduresString.Split(',');

            int insertedStoredProcedures = storedProcedures.Count();
            int generatedStoredProcedures = 0;

            for (int i = 0; i < storedProcedures.Length; i++)
            {
                storedProcedures[i] = storedProcedures[i].Trim();
            }

            DbService dbService = new(connection);

            string outputDirectory = $"{connection.Name}_{DateTime.Now:yyyy-MM-dd_HHmmss}";
            string outputFilesPath = OutputFilesService.GetOutputFilesPath();
            string outputDirectoryPath = Path.Combine(outputFilesPath, outputDirectory);

            Directory.CreateDirectory(outputDirectoryPath);

            foreach (string storedProcedure in storedProcedures)
            {
                string storedProcedureText = dbService.GetStoredProcedureText(storedProcedure);
                if (!string.IsNullOrEmpty(storedProcedureText))
                {
                    generatedStoredProcedures++;
                    storedProcedureText = storedProcedureText.Replace("CREATE", "ALTER");
                    string filePath = Path.Combine(outputDirectoryPath, $"{storedProcedure}.sql");
                    File.WriteAllText(filePath, storedProcedureText);
                }
            }

            dbService.CloseConnection();

            if (generatedStoredProcedures > 0)
            {
                SuccessMenu.Show(generatedStoredProcedures, insertedStoredProcedures, outputFilesPath);
            }
            else
            {
                ErrorMenu.Show();
            }
        }
    }
}
