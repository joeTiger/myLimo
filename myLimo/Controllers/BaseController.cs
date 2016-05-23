﻿using myLimo.Models;
using myLimo.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myLimo.Controllers
{
    public class BaseController : Controller
    {
        ServiceReference1.Service1Client objService = new Service1Client();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {

            //Initialize...test
            base.Initialize(requestContext);
        }


        public void setViewBagVariables(string cn, int bizId, int lg, int catId, int subId, int id)
        {
            ViewBag.controllerName = cn;
            ViewBag.bizId = bizId;
            ViewBag.lg = lg;
            ViewBag.catId = catId;
            ViewBag.subId = subId;
            ViewBag.id = id;
            ViewBag.Dir = (lg == 1) ? "rtl" : "ltr";
            ViewBag.Lang = (lg == 1) ? "he" : "en";

            string s = @"firstPageId_" + bizId;
            if (Session[s] == null)
            {
                mylog(s + " is NULL !!!!!!!!!!!!!!!");
                Session[s] = id.ToString();
            }

            ViewBag.firstPageId = Session[s];

            setViewBagSettingModel(bizId, lg);
            setViewBagSettingLgModel(bizId, lg);
            setViewBagByBizId(bizId);

            mylog("ViewBag.controllerName   =" + ViewBag.controllerName);
            mylog("ViewBag.bizId            =" + ViewBag.bizId);
            mylog("ViewBag.lg               =" + ViewBag.lg);
            mylog("ViewBag.catId            =" + ViewBag.catId);
            mylog("ViewBag.subId            =" + ViewBag.subId);
            mylog("ViewBag.id               =" + ViewBag.id);
            mylog("ViewBag.Dir              =" + ViewBag.Dir);
            mylog("ViewBag.Lang             =" + ViewBag.Lang);
            mylog("ViewBag.firstPageId      =" + ViewBag.firstPageId);
            mylog("ViewBag.FirstLgName      =" + ViewBag.FirstLgName);

        }

        private void setViewBagByBizId(int bizId)
        {
            if (bizId == 72751 || bizId == 79357)
            {
                ViewBag.PortfolioController = "Portfolio";
                ViewBag.colorScheme = "~/css/colors/color-scheme5.css";
            }
            //kazablan
            else if (bizId == 75318 || bizId == 79472 || bizId == 73294)
            {
                //ViewBag.colorScheme = "~/css/colors/color-scheme3.css";
                //gray
                ViewBag.PortfolioController = "PortfolioNoFilter";
                ViewBag.colorScheme = "~/css/colors/color-default.css";
            }
        }

        public void setViewBagPageDataModel(int id, int lg)
        {
            string s = @"PageDataModel_" + id + "_" + lg;
            if (Session[s] == null)
            {
                mylog(s + " is NULL !!!!!!!!!!!!!!!");
                spGetEltPageDataByIdResult elt = objService.spGetEltPageDataById(id, lg);
                Session[s] = (elt == null)?string.Empty: elt.data;
            }
            ViewBag.PageDataModel = Session[s];
        }

        public void setViewBagMenuModel(int bizId, int lg)
        {
            if (ViewBag.MenuModel == null)
            {
                string s = "MenuModel_" + bizId + "_" + lg;
                if (Session[s] == null)
                {
                    mylog(s + " is NULL !!!!!!!!!!!!!!!");
                    //int bizId = 72751; int lg = 0;
                    //id, parId, typeEltPage, name, data, lastUpdate, pos
                    //Session["MenuModel"] = objService.GetTreeEltPage(bizId, 0, lg);
                    IEnumerable<spGetTreeEltPageResult> list = objService.GetTreeEltPage(ViewBag.bizId, 0, ViewBag.lg);
                    Session[s] = list.Where(x=>x.typeEltPage==0).ToList();
                    CheckListTreeEltPage(list);
                }
                ViewBag.MenuModel = Session[s];
            }
        }

        public void setViewBagSettingModel(int bizId, int lg)
        {
            if (ViewBag.MenuSettingModel == null)
            {
                string s = "MenuSettingModel_" + bizId;
                if (Session[s] == null)
                {
                    mylog(s + " is NULL !!!!!!!!!!!!!!!");
                    IEnumerable<spSettingGetResult> list = objService.SettingGet(bizId).ToList();
                    Session[s] = list.ToList();
                    CheckListSetting(s, list);
                }
                ViewBag.MenuSettingModel = Session[s];
            }
        }

        public void setViewBagSettingLgModel(int bizId, int lg)
        {
            string s = "MenuSettingLgModel_" + bizId;
            if (Session[s] == null)
            {
                mylog(s + " is NULL !!!!!!!!!!!!!!!");
                Session[s] = getSettingLgList();
            }
            ViewBag.FirstLgName     = ((IEnumerable<LgSetting>)Session[s]).ToList().Where(x => x.Value== lg.ToString()).First().Name;
            ViewBag.SettingLgModel  = ((IEnumerable<LgSetting>)Session[s]).ToList().Where(x => x.Value != lg.ToString()).ToList();
        }

        private object getSettingLgList()
        {
            string values = string.Empty;
            foreach (spSettingGetResult e in ViewBag.MenuSettingModel)
            {
                if (e.name == "LanguageValues")
                {
                    values = e.value;
                    break;
                }
            }

            //there are no LanguageValues defined 
            if (values == string.Empty) values = "01234";

            IList<LgSetting> list = new List<LgSetting>();
            foreach (char c in values)
            {
                string n = string.Empty;
                string v = string.Empty;
                switch (c)
                {
                    case '0': n = "English";    v = "0"; break;
                    case '1': n = "עברית";      v = "1"; break;
                    case '2': n = "français";   v = "2"; break;
                    case '3': n = "Russe";      v = "3"; break;
                    case '4': n = "Chinese";    v = "4"; break;
                }
                list.Add(new LgSetting { Name = n, Value = v });
            }

            CheckListSettingLg("LgSetting", list);
            return list.ToList();
        }
        

        private void CheckListSettingLg(string modelname, IList<LgSetting> list)
        {
            mylog(modelname + " list.count=" + list.Count());
            foreach (LgSetting elt in list)
            {
                object instance = elt;
                displayInstance(instance);
            }
            if (list.Count() == 0) mylog("Error !!! no entries ...");
        }

        public void CheckListSetting(string modelname, IEnumerable<spSettingGetResult> list)
        {
            mylog(modelname + " list.count=" + list.Count());
            foreach (spSettingGetResult elt in list)
            {
                object instance = elt;
                displayInstance(instance);
            }
            if (list.Count() == 0) mylog("Error !!! no entries ...");
        }

        public void setViewBagMenuCatModel(int bizId, int lg)
        {
            if (ViewBag.MenuCatModel == null)
            {
                string s = "MenuCatModel_" + bizId + "_" + lg;
                if (Session[s] == null)
                {
                    mylog(s + " is NULL !!!!!!!!!!!!!!!");
                    int catId = 0;
                    //id catId parId name ifn 
                    //bizId = 72751; bizName = myresto20; catId = 1348; category = Pie; descPdt=...
                    //id=73141;imageFileName=...jpg;imageFileName2=;minOrder=1;name=test;nameId=3090;parId=1243;pdtPos=3;price=44.00;slider=False
                    IEnumerable<spGetGridPdtByBizResult> list = objService.
                        GetGridPdtByBizMenu(catId, ViewBag.bizId, ViewBag.lg, false);

                    Session[s] = list.Where(x => x.category != "slider").ToList();
                    CheckListGridPdtByBiz(s, list);
                }
                ViewBag.MenuCatModel = Session[s];
            }
        }

        
        public void setViewBagCatPdtModel(int bizId, int lg, int catId, int subId)
        {
            mylog("setViewBagCatPdtModel..." );
            //string sCat = "Cat_" + bizId + "_" + lg + "_" + catId + "_" + subId;
            string sCat = "Cat_" + bizId + "_" + lg + "_" + catId + "_0";
            string sPdt = "Pdt_" + bizId + "_" + lg + "_" + catId + "_" + subId;
            if (Session[sPdt] == null)
            {
                mylog(sPdt + " is NULL !!!!!!!!!!!!!!!");
                IEnumerable<spGetGridPdtByBizResult> listPdt = null;

                //if subId=0 get all Pdts ordered by cat/subCat    => c/p1 c1/p11 c1/p12 c2/p2
                //else get all pdts by subId => c1/p11 c1/p12
                if (subId == 0)
                {
                    listPdt = getListPdtByLg(bizId, lg, catId);
                }
                else
                {
                    string s = "Pdt_" + bizId + "_" + lg + "_" + catId + "_0";
                    if (Session[s] == null)
                    {
                        mylog(s + " is NULL !!!!!!!!!!!!!!!");
                        listPdt = getListPdtByLg(bizId, lg, catId);
                        Session[s] = listPdt;
                    }
                    else //already exists
                    {
                        listPdt = (IEnumerable<spGetGridPdtByBizResult>)Session[s];
                    }
                }
                //listCat is calculated according to list
                // if listPdt=c/p1 c1/p11 c1/p12 c2/p2 then listCat=c c1 c2
               
                List<spGetGridPdtByBizResult> listCat = 
                    (Session[sCat] == null)?
                    getListCat(subId, listPdt):
                    (List<spGetGridPdtByBizResult>)Session[sCat];
               
                if (subId > 0) listPdt = listPdt.Where(x => x.catId == subId).ToList();
                CheckListGridPdtByBiz(sCat, listCat);
                CheckListGridPdtByBiz(sPdt, listPdt);
                Session[sCat] = listCat;
                Session[sPdt] = listPdt;
            }
            if (Session[sCat] == null)
            {
                mylog(sCat + " is NULL !!!!!!!!!!!!!!!");
                IEnumerable<spGetGridPdtByBizResult> listPdt = (IEnumerable<spGetGridPdtByBizResult>)Session[sPdt];
                List<spGetGridPdtByBizResult> listCat = getListCat(subId, listPdt );
                Session[sCat] = listCat;
                }
            if (Session[sPdt] == null)
            {
                mylog("setViewBagCatPdtModel...sPdt=" + sPdt + " IS NULL ??????????????????????????????");
            }
            ViewBag.Cat = Session[sCat];
            ViewBag.Pdt = Session[sPdt];
            ViewBag.CatName = ((IEnumerable<spGetGridPdtByBizResult>)Session[sPdt]).ToList().First().category;
            mylog("ViewBag.CatName=" + ViewBag.CatName);
        }

        private static List<spGetGridPdtByBizResult> getListCat(int subId, 
                    IEnumerable<spGetGridPdtByBizResult> list)
        {
                List<spGetGridPdtByBizResult> listCat = new List<spGetGridPdtByBizResult>();
            int crtCatId = list.ToList()[0].catId;
                listCat.Add(list.ToList()[0]);
                foreach (spGetGridPdtByBizResult elt in list)
                {
                if (elt.catId != crtCatId)
                    {
                        if ((subId > 0 && elt.catId == subId) || subId == 0)
                        {
                            listCat.Add(elt);
                        crtCatId = elt.catId;
                        }
                    }
                }

            return listCat;
            }

        private IEnumerable<spGetGridPdtByBizResult> getListPdtByLg(int bizId, int lg, int catId)
        {
            IEnumerable<spGetGridPdtByBizResult> list;
            if (catId == 0)
            {
                catId = objService.GetRootCatPdtByBiz(bizId);
            }
            //Get all pdts per cat and subCat order by cat/subcat
            // id, catId, parId, category, descPdt
            list = objService.GetGridPdtByBiz(catId, bizId, lg).
                OrderBy(x => x.catId).ToList();
            return list;
        }

        public void setViewBagSliderModel(int bizId, int lg, int catId)
        {
            string s = "Slider_" + bizId + "_" + lg + "_" + catId;
            if (Session[s] == null)
                {
                mylog(s + " is NULL !!!!!!!!!!!!!!!");
                if (catId == 0){
                    catId = objService.GetRootCatPdtByBiz(bizId);
                }
                IEnumerable<spGetGridPdtByBizResult> list =
                    objService.GetGridPdtByBiz(catId, bizId, lg)
                        .Where(x => x.slider == true).ToList();
                CheckListGridPdtByBiz(s, list);
                Session[s] = list;
            }
            ViewBag.Slider = Session[s];
        }

        
        public void CheckListGridPdtByBiz(string modelname, IEnumerable<spGetGridPdtByBizResult> list)
        {
            if (list == null)
            {
                mylog(modelname + " list IS NULL ???????????????" );
            }
            mylog(modelname + " list.count=" + list.Count());
            foreach (spGetGridPdtByBizResult elt in list)
            {
                object instance = elt;
                displayInstance(instance);
            }
            if (list.Count() == 0) mylog("Error !!! no entries ...");
            //mylog("MenuModel=" +
            //    ((IEnumerable<spGetTreeEltPageResult>) Session["MenuModel"]).ToList().Count);
        }

        public void displayInstance(object instance)
        {
            string s = string.Empty;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(instance))
            {
                string name = descriptor.Name;
                if (name == "ExtensionData") continue;
                object value = descriptor.GetValue(instance);
                //string type = descriptor.GetType().ToString();mylog(type);
                s += name + "=" + trunc(value) + ";";
            }
            mylog(s);
        }

        private string trunc(object value)
        {
            int maxLength = 30;
            if (value == null) return string.Empty;
            string str = value.ToString();
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        private void CheckListTreeEltPage(IEnumerable<spGetTreeEltPageResult> list)
        {
            mylog("MenuModel list.count=" + list.Count());
            foreach (spGetTreeEltPageResult elt in list)
            {
                object instance = elt;
                displayInstance(instance);
            }
            if (list.Count() == 0) mylog("Error !!! no entries for MenuModel...");
            //mylog("MenuModel=" +
            //    ((IEnumerable<spGetTreeEltPageResult>) Session["MenuModel"]).ToList().Count);
        }


        

        private void writeToLogfile(string s)
        {
            string path = getLogPath();

            if (!Directory.Exists(path)) return;
            try
            {
                FileStream fs = new FileStream(path + "\\logFileLimo.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                s = DateTime.Now + " " + s;
                sw.WriteLine(s);
                sw.Close();
            }
            catch (Exception)
            {
            }

            //try
            //{
            //    //FileStream fs = new FileStream("C:\\log\\logFileLimo.txt", FileMode.Append, FileAccess.Write);
            //    FileStream fs = new FileStream("E:\\web\\yelotagc\\myLimo\\logFileLimo.txt", FileMode.Append, FileAccess.Write);
            //    StreamWriter sw = new StreamWriter(fs);
            //    s = DateTime.Now + " " + s;
            //    sw.WriteLine(s);
            //    sw.Close();
            //}
            //catch (Exception)
            //{
            //}
        }
                
        public void mylog(string s)
        {
            return;
            //if (Trace.IsEnabled)
            {
                System.Diagnostics.Debug.WriteLine(s);
                //Trace.Write(s);
                writeToLogfile(s);
            }
        }

        public string getLogPath()
        {
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            
            string path;
            if (host.Contains("localhost"))
                path = @"C:\log";
            else
                path = @"E:\web\yelotagc\myLimo";

            return path;
        }
    }
}