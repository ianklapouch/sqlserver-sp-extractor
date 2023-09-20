﻿using sqlserver_sp_extractor.Menus;
using sqlserver_sp_extractor.Models;
using sqlserver_sp_extractor.Services;

namespace sqlserver_sp_extractor.Commands
{
    public class SelectItemCommand : Command
    {
        private readonly string name;

        public SelectItemCommand(string name) : base(name)
        {
            this.name = name;
        }

        public override void Execute()
        {
            Connection connection = ConfigurationService.GetConnectionByName(name);
            if (connection is not null)
            {
                StoredProceduresMenu.Show(connection);
            }
        }
    }
}
