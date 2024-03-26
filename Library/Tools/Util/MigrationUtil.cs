using FluentMigrator;
using FluentMigrator.Builders.Create.Index;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Delete;
using FluentMigrator.Builders.Delete.Column;
using FluentMigrator.Builders.Insert;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Util
{
    public static class MigrationUtil
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax IsActiveComponentColumn(this ICreateTableColumnOptionOrWithColumnSyntax columns)
            => columns.WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(Convert.ToByte(true));

        public static ICreateTableColumnOptionOrWithColumnSyntax RecordTransactionComponentColumn(this ICreateTableColumnOptionOrWithColumnSyntax columns)
        {
            return columns.WithColumn("CreatedBy").AsString(100).NotNullable()
            .WithColumn("CreatedDt").AsDateTime().NotNullable()
            .WithColumn("ModifiedBy").AsString(100).Nullable()
            .WithColumn("ModifiedDt").AsDateTime().Nullable();
        }
    }
}
