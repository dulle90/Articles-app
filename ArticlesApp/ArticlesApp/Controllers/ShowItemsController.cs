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

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string jsonSrc = path + @"\App_Data\source.json";
            string jsonText;

            using (StreamReader strRead = new StreamReader(jsonSrc))
            {
                jsonText = strRead.ReadToEnd();
            }
            return View(jsonText);          
        }

        public ActionResult Create(ArticleModel itemModel)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string jsonSrc = path + @"\App_Data\source.json";
            string jsonText = System.IO.File.ReadAllText(jsonSrc);
            var sr = JsonConvert.DeserializeObject<List<ArticleModel>>(jsonText);
            List<ArticleModel> items = new List<ArticleModel>();

            items = JsonConvert.DeserializeObject<List<ArticleModel>>(jsonText);

            ArticleModel model = items.FirstOrDefault(x => x.Id == itemModel.Id);

            if (model == null)
            {
                items.Add(itemModel);
            }
            else
            {
                int index = items.FindIndex(x => x.Id == itemModel.Id);
                items[index] = itemModel;
            }

            string jSONString = JsonConvert.SerializeObject(items);
            System.IO.File.WriteAllText(jsonSrc, JsonConvert.SerializeObject(jSONString));

            return View(items);
            
        }     

    }
}