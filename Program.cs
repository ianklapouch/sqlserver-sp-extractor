using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Services;

ConfigurationService.CheckConfigurations();
OutputFilesService.CheckOutputFilesDirectory();

Console.CursorVisible = false;

MainMenu.Show();