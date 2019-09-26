using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCom.Web.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage()
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);

                file.SaveAs(filePath);

                json.Data = new { success = true, imageURL = string.Format("/Content/Images/{0}",fileName) };
            }
            catch (Exception ex)
            {
                json.Data = new { success = false, message = ex.Message };
            }

            return json;
        }
    }
}