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
        [HttpPost]
        public ActionResult Create(ArticleModel articleModel)
        {
            try
            {
                List<ArticleModel> item = new List<ArticleModel>();
                ReadWriteJson readInsert = new ReadWriteJson();

                string mainPath = AppDomain.CurrentDomain.BaseDirectory;
                string path = mainPath + @"\App_Data\source.json";
                item = JsonConvert.DeserializeObject<List<ArticleModel>>(readInsert.Read(path));


                ArticleModel article = item.FirstOrDefault(x => x.Id == articleModel.Id);

                if (article == null)
                {
                    item.Add(articleModel);
                }
                else
                {
                    int index = item.FindIndex(x => x.Id == articleModel.Id);
                    item[index] = articleModel;
                }

                string result = JsonConvert.SerializeObject(item);
                readInsert.Insert(path, result);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
    public class ReadWriteJson
    {
        public ReadWriteJson() { }

        public string Read(string path)
        {
            string result;
            using (StreamReader strRead = new StreamReader(path))
            {
                result = strRead.ReadToEnd();
            }
            return result;
        }
        public void Insert(string path, string jSonString)
        {
            using (var strInsert = File.CreateText(path))
            {
                strInsert.Write(jSonString);
            }
        }

    }
}