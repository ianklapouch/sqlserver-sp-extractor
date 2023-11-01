# SQLServer Stored Procedure Extractor

A command-line interface (CLI) tool that automates stored procedure extraction in files from a SQL Server database.

## Features

- Extract stored procedures from SQL Server databases and save them as files.
- Supports .NET 7.

## Installation

### Install with dotnet tool

1. Download the Nupkg folder from the latest release.
2. Navigate to the directory where you extracted the Nupkg folder and open a terminal window.
3. Run the following command to install the tool globally:<br /> `dotnet tool install --global --add-source ./nupkg sqlserver-sp-extractor`<br /> This command installs the sqlserver-sp-extractor tool globally on your system.

### Use as an executable

1. Download the folder corresponding to your operating system from the latest release.
2. Add the directory to PATH.

## Usage

After installation, you can use the tool globally by running the following command in your terminal:<br /> `sp-extractor`<br /> You can now start extracting stored procedures from your SQL Server database using this command.
