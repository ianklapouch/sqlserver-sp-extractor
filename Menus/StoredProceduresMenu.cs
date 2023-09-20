using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Menus
{
    public class StoredProceduresMenu
    {
        public static void Show(Connection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter list of stored procedures (comma separated):");

            string storedProceduresString = Console.ReadLine();

            if (!string.IsNullOrEmpty(storedProceduresString))
            {
                string[] storedProcedures = storedProceduresString.Split(',');

                for (int i = 0; i < storedProcedures.Length; i++)
                {
                    storedProcedures[i] = storedProcedures[i].Trim();
                }

                DbService dbService = new(connection);

                string outputFilesPath = OutputFilesService.GetOutputFilesPath();
                foreach (string storedProcedure in storedProcedures)
                {
                    string storedProcedureText = dbService.GetStoredProcedureText(storedProcedure);
                    if (!string.IsNullOrEmpty(storedProcedureText))
                    {
                        storedProcedureText = storedProcedureText.Replace("CREATE", "ALTER");
                        string filePath = Path.Combine(outputFilesPath, $"{storedProcedure}.sql");
                        File.WriteAllText(filePath, storedProcedureText);
                    }
                }

                dbService.CloseConnection();
                SuccessMenu.Show();


            }
        }
    }
}
