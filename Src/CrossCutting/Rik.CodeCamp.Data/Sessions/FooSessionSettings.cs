
using System.Configuration;

namespace Rik.CodeCamp.Data.Sessions
{
    public class FooSessionSettings : IFooSessionSettings
    {
        public FooSessionSettings()
        {
            var  connectionStringSetting = ConfigurationManager.ConnectionStrings["Foo"];
            ConnectionString = connectionStringSetting.ConnectionString;
            ProviderName = connectionStringSetting.ProviderName;
            Name = connectionStringSetting.Name;
        }

        public string Name { get;  }

        public string ProviderName { get;  }

        public string  ConnectionString { get;  }
    }
}
