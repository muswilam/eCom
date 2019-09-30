using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCom.Entities;
using eCom.Data;

namespace eCom.Services
{
    public class ConfigurationService
    {
        //public static ConfigurationService ClassObject
        //{

        //    get
        //    {
        //        if (privateInMemory == null) privateInMemory = new ConfigurationService();

        //        return privateInMemory;
        //    }
        //}
        //private static ConfigurationService privateInMemory { get; set; }

        //private ConfigurationService()
        //{
        //}

        public Config GetConfig(string key)
        {
            using (var context = new eComContext())
            {
                return context.Configurations.Find(key);
            }
        }
    }
}
