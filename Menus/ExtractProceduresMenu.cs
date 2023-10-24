using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public class ExtractProceduresMenu
    {
        public static void Show(Connection connection)
        {
            string? storedProceduresString = string.Empty;
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Enter list of stored procedures (comma separated):");

            while (string.IsNullOrEmpty(storedProceduresString))
            {
                storedProceduresString = Console.ReadLine();
                Console.CursorVisible = false;
            }

            string[] storedProcedures = storedProceduresString.Split(',');

            int insertedStoredProcedures = storedProcedures.Count();
            int generatedStoredProcedures = 0;

            List<string> storedProceduresNotFound = new();

            for (int i = 0; i < storedProcedures.Length; i++)
            {
                storedProcedures[i] = storedProcedures[i].Trim();
            }

            DbService dbService = new(connection);

            try
            {
                dbService.OpenConnection();
            }
            catch (Exception ex)
            {
                ErrorMenu.Show($"Error opening database connection, check connection and try again! \n\n {ex.Message}");
                return;
            }

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
                else
                {
                    storedProceduresNotFound.Add(storedProcedure);
                }
            }

            dbService.CloseConnection();

            if (generatedStoredProcedures > 0)
            {
                SuccessMenu.Show(generatedStoredProcedures, insertedStoredProcedures, outputDirectoryPath, storedProceduresNotFound);
                return;
            }

            ErrorMenu.Show("No stored procedures found, check the name of the inserted procedures and the connection to the database!");
        }
    }
}
