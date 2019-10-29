using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCom.Entities;
using eCom.Data;
using System.Data.Entity;

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

                return pageSizeConfig.Value != string.Empty ? int.Parse(pageSizeConfig.Value) : 10;
            }
        }
        public int ShopPageSize()
        {
            using (var context = new eComContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig.Value != string.Empty ? int.Parse(pageSizeConfig.Value) : 12;
            }
        }

        //update main picture in home page (by admin)
        public bool UpdateMainPicture(string key, string imageUrl)
        {
            using (var context = new eComContext())
            {
                var existConfig = context.Configurations.Where(c => c.Key == key).FirstOrDefault();
                if(existConfig != null) 
                {
                    existConfig.Value = imageUrl;
                    context.Entry(existConfig).State = EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
                return false;
            }
        }

        //get all keys of configurations
        public List<string> GetKeysConfig()
        {
            using (var context = new eComContext())
            {
                var keys = context.Configurations.Select(c => c.Key);

                foreach (var key in keys)
                {
                    var exceptKeys = new List<string>();
                    if (key.ToLower().Contains("image"))
                    {
                        exceptKeys.Add(key);
                    }

                    keys = keys.Except(exceptKeys);
                }

                return keys.ToList();
            }
        }

        //edit config value
        public bool EditConfig(Config config)
        {
            using (var context = new eComContext())
            {
                context.Entry(config).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }
    }
}
