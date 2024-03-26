using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.GenericModels.Configuration
{
    public class AppSettings
    {
        public AppSettingsDatabaseSchema DatabaseSchema { get; set; }

        public AppSettings() { }

    }

    public class AppSettingsDatabaseSchema
    {

        public string Default { get; set; }

        public AppSettingsDatabaseSchema() { }

    }
}
