using FluentMigrator;
using Tools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Migrations
{
    [Tags("Migration")]
    [Migration(20230507_130900)]
    public class DB_20230507_130900_Initial : DBMigrationBase
    {
        public override void Down()
        {
            this.Delete.Table(Role);
            this.Delete.Table(User);
        }

        public override void Up()
        {
            if (this.Schema.Table(Role).Exists())
            {
                this.Delete.Table(Role);
            }

            this.Create.Table(Role)
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .IsActiveComponentColumn()
                .RecordTransactionComponentColumn();

            if (this.Schema.Table(User).Exists())
            {
                this.Delete.Table(User);
            }

            this.Create.Table(User)
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Email").AsString(250).NotNullable()
                .WithColumn("Password").AsString(250).NotNullable()
                .WithColumn("RoleId").AsGuid().NotNullable()
                .IsActiveComponentColumn()
                .RecordTransactionComponentColumn();
        }
    }
}
