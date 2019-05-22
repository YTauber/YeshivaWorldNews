using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeshivaWorldNews.Data;
using YeshivaWorldNews.Web.Models;

namespace YeshivaWorldNews.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            NewsManager mgr = new NewsManager();
            return View(mgr.GetNews());
        }
    }
}
