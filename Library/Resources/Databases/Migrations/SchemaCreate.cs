using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Util;

namespace Databases.Migrations
{
    [Maintenance(MigrationStage.BeforeAll, TransactionBehavior.Default)]
    public class SchemaCreate : DBMigrationBase
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            if (!this.Schema.Schema(DatabaseSchema).Exists())
            {
                this.Create.Schema(DatabaseSchema);
            }
        }
    }
}
