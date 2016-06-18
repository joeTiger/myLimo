using System.Web.Mvc;
using myLimo.ServiceReference1;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System;

namespace myLimo.Controllers
{

    public class ShoppingCartController : BaseController
    {
        ServiceReference1.Service1Client objService = new Service1Client();

        //public ActionResult Index(int bizId=72751, int lg=0, int catId=1306, int subId=1498, int id = 0)/*73212*/
        public ActionResult Index(int bizId = 73294, int lg = 1, int catId = 1509, int subId = 0, int id = 73356)
        {
            setViewBagLocal(lg);
            setViewBagVariables("ShoppingCart", bizId, lg, catId, subId, id);
            setViewBagMenuModel(bizId, lg);
            setViewBagMenuCatModel(bizId, lg);
            setViewBagCatPdtModel(bizId, lg, catId, subId);
                       
            return View();
        }

        

        



        private void setViewBagLocal(int lg)
        {
            switch (lg)
            {   case 0:
                    ViewBag.tellAFriend = "tell friends";
                    ViewBag.FreeDelivery = "Free Delivery";
                    ViewBag.SafeBuy = "Safe Buy";
                    break;
                case 1:
                    ViewBag.tellAFriend = "תגיד לחברים...";
                    ViewBag.FreeDelivery = "משלוח חינם";
                    ViewBag.SafeBuy = "קניה בטוחה";
                    break;
                default:
                    ViewBag.tellAFriend = "tell friends";
                    ViewBag.FreeDelivery = "Free Delivery";
                    ViewBag.SafeBuy = "Safe Buy";
                    break;
            }
        }

        //public spGetPdtByIdResult GetPdt(int lg, int id)
        //{

        //    spGetPdtByIdResult elt = objService.GetPdtById(id, lg);
        //    CheckPdtById(elt);
        //    return elt;
        //}


        //public void CheckPdtById(spGetPdtByIdResult elt)
        //{
        //    mylog("PdtById...");

        //    object instance = elt;
        //    displayInstance(instance);

        //    if (elt == null) mylog("Error !!! elt is null ????");

        //}
    }
}
