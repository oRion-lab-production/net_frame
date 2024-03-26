using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Util;

namespace Databases.Migrations
{
    [Tags("Migration")]
    [Migration(20230507_174800)]
    public class DB_20230507_174800_Initial : DBMigrationBase
    {
        public override void Down()
        {
            this.Delete.Table(Product);
        }

        public override void Up()
        {
            if (this.Schema.Table(Product).Exists()) {
                this.Delete.Table(Product);
            }

            this.Create.Table(Product)
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Description").AsString(250).Nullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable().WithDefaultValue(0)
                .IsActiveComponentColumn()
                .RecordTransactionComponentColumn();
        }
    }
}
