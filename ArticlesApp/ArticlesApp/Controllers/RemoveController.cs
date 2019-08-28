using ArticlesApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticlesApp.Controllers
{
    public class RemoveController : Controller
    {
        // GET: Remove
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                List<ArticleModel> item = new List<ArticleModel>();
                ReadWriteJson check = new ReadWriteJson();

                string mainPath = AppDomain.CurrentDomain.BaseDirectory;
                string path = mainPath + @"\App_Data\source.json";
                item = JsonConvert.DeserializeObject<List<ArticleModel>>(check.Read(path));

                int index = item.FindIndex(x => x.Id == id);
                item.RemoveAt(index);
                string jSONString = JsonConvert.SerializeObject(item);
                check.Insert(path, jSONString);

                return RedirectToAction("Index", "ShowItems");                
            }
            catch
            {
                return View();
            }
        }
    }
}