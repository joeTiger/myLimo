using myLimo.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myLimo.Controllers
{
    public class HomeController : BaseController
    {
        ServiceReference1.Service1Client objService = new Service1Client();

        //72751/14318-pixtor
        //73294/14327-kazablan local - 79472/14549 remote
        //public ActionResult Index(int bizId = 79357, int lg = 0, int catId = 0, int subId = 0, int id = 14536)
        public ActionResult Index(int bizId = 73294, int lg = 0, int catId = 0, int subId = 0, int id = 14327)
        //public ActionResult Index(int bizId = 79472, int lg = 1, int catId = 0, int subId = 0, int id = 14549)
        {
            setViewBagVariables("Home", bizId, lg, catId, subId, id);
            setViewBagMenuModel(bizId, lg);
            setViewBagMenuCatModel(bizId, lg);
            setViewBagPageDataModel(id, lg);
            setViewBagSliderModel(bizId, lg, catId);

            return View();

            //var article = new Article { Name = "Album " };
            //var articles = GenerateArticles();
            //return View(articles);

        }
    }
}