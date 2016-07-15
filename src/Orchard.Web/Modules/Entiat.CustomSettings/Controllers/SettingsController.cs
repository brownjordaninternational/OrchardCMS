using Entiat.CustomSettings.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entiat.CustomSettings.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService _service;
        public SettingsController(ISettingsService service)
        {
            _service = service;
        }
        public JsonResult GetChannels(string companycode)
        {
            return Json(_service.GetAllChannels(companycode), JsonRequestBehavior.AllowGet);
        }
    }
}