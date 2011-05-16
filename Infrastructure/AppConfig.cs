using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public static class AppConfig
    {
        public static string ConnectionStringName
        {
            get
            {
                return "RavenDB";
            }
        }
    }
}
