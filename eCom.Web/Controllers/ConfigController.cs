using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;

namespace eCom.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigController : Controller
    {
        public JsonResult UpdateMainPicture(string imageUrl)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool result = false;

            if(!string.IsNullOrEmpty(imageUrl))
            {
                result = ConfigurationService.Instance.UpdateMainPicture("MainImage", imageUrl);
            }

            if (result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Something went wrong." };

            return json;
        }
    }
}