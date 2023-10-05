# SQLServer Stored Procedure extractor
Tool to automate stored procedure extraction in files from a SQL Server database.

## Installation
- **Download the Nupkg Folder**: First, download the Nupkg folder from the [latest release](https://github.com/ianklapouch/sqlserver-sp-extractor/releases)
- **Install the Tool**: Navigate to the directory where you extracted the Nupkg folder and open a terminal window. Run the following command to install the tool globally:<br />
```dotnet tool install --global --add-source ./nupkg sqlserver-sp-extractor```<br />
This command installs the sqlserver-sp-extractor tool globally on your system.
- **Usage**: After installation, you can use the tool globally by running the following command in your terminal:<br />
```sp-extractor```<br />
You can now start extracting stored procedures from your SQL Server database using this command.

## Uninstallation
- Open a terminal window.
- Run the following command to uninstall the tool globally:<br />
```dotnet tool uninstall sqlserver-sp-extractor --global```<br />
This will remove the tool from your system.

