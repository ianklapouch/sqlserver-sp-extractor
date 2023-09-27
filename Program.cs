using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Services;

Console.CursorVisible = false;

ConnectionsService.EnsureConnectionsFileExists();
OutputFilesService.EnsureOutputFilesDirectoryExists();

MainMenu.Show();