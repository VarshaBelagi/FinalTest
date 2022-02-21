using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcExam.Models;

namespace WebMvcExam.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {

            var com = new DataComponent();
            var dress = com.GetAllDresses();

            return View(dress);
        }
        
    }
}