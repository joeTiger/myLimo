using myLimo.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myLimo.Controllers
{
    public class PageController : BaseController
    {
        ServiceReference1.Service1Client objService = new Service1Client();

        public ActionResult Index(int bizId, int lg, int catId, int subId, int id)
        {
            setViewBagVariables("Page", bizId, lg, catId, subId, id);
            setViewBagMenuModel(bizId, lg);
            setViewBagMenuCatModel(bizId, lg);
            setViewBagPageDataModel(id, lg);
            setViewBagSliderModel(bizId, lg, catId);
            setViewBagCartListModel(bizId, lg, true);

            return View();
        }
    }
}