using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Migrations
{
    [Tags("Seeder")]
    [Migration(20230507_133500)]
    public class DB_20230507_133500_Seeder : DBMigrationBase
    {
        public override void Down()
        {
            this.Delete.FromTable(Role);
        }

        public override void Up()
        {
            var roleAdmin_PK = Guid.NewGuid();
            var roleUser_PK = Guid.NewGuid();

            this.Insert.IntoTable(Role)
                .Row(new
                {
                    Id = roleAdmin_PK,
                    Name = "Admin",
                    IsActive = true,
                    CreatedBy = "system",
                    CreatedDt = DateTime.Now,
                })
                .Row(new
                {
                    Id = roleUser_PK,
                    Name = "User",
                    IsActive = true,
                    CreatedBy = "system",
                    CreatedDt = DateTime.Now,
                });
        }
    }
}
