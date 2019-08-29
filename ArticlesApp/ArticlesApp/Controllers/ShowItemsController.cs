using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ArticlesApp.Models;
using Newtonsoft.Json;

namespace ArticlesApp.Controllers
{
    public class ShowItemsController : Controller
    {
        // GET: ShowItems
        public ActionResult Index()
        {
            List<ArticleModel> item = new List<ArticleModel>();
            ReadWriteJson view = new ReadWriteJson();

            string mainPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = mainPath + @"\App_Data\source.json";

            item = JsonConvert.DeserializeObject<List<ArticleModel>>(view.Read(path));
            
            return View(item);          
        }       
    }   
}