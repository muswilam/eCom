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
        #region Singleton Non-Thread Safety
        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationService();

                return instance;
            }
        }
        private static ConfigurationService instance { get; set; }
        private ConfigurationService()
        {
        }
        #endregion

        public Config GetConfig(string key)
        {
            using (var context = new eComContext())
            {
                return context.Configurations.Find(key);
            }
        }

        public int PageSize()
        {
            using (var context = new eComContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 10;
            }
        }
        public int ShopPageSize()
        {
            using (var context = new eComContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
