using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    [Tags("DefaultDB")]
    public abstract class DBMigrationBase: Migration
    {
        // Schema name by default
        private protected const string DatabaseSchema = "vcs_db";

        #region Table name by default
        // Membership table
        private protected const string Role = "Role";
        private protected const string User = "Users";

        // Common table
        private protected const string Product = "Product";
        private protected const string ProductOnHand = "ProductOnHand";
        #endregion

        protected DBMigrationBase() { }
    }
}
