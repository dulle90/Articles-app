using ArticlesApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ArticlesApp.Controllers
{
    public class ReadWriteController : Controller
    {
        // GET: ReadWrite
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReadWrite/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReadWrite/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReadWrite/Create
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

                if (item.Count == 0)
                {
                    articleModel.Id = 1;
                    item.Add(articleModel);
                }
                else
                {
                    int maxId = item.Max(x => x.Id);
                    int nextIndex = maxId + 1;
                    articleModel.Id = nextIndex;
                    item.Add(articleModel);
                }           

                string result = JsonConvert.SerializeObject(item);
                readInsert.Insert(path, result);

                return RedirectToAction("Index", "ShowItems");                
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadWrite/Edit/5
        public ActionResult Edit(int id)
        {
            List<ArticleModel> item = new List<ArticleModel>();
            ReadWriteJson check = new ReadWriteJson();

            string mainPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = mainPath + @"\App_Data\source.json";
            item = JsonConvert.DeserializeObject<List<ArticleModel>>(check.Read(path));

            ArticleModel article = item.FirstOrDefault(x => x.Id == id);
            return View(article);
        }

        // POST: ReadWrite/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, ArticleModel articleModel)
        {
            try
            {
                List<ArticleModel> item = new List<ArticleModel>();
                ReadWriteJson edit = new ReadWriteJson();

                string mainPath = AppDomain.CurrentDomain.BaseDirectory;
                string path = mainPath + @"\App_Data\source.json";
                item = JsonConvert.DeserializeObject<List<ArticleModel>>(edit.Read(path));

                ArticleModel article = item.FirstOrDefault(x => x.Id == articleModel.Id);

                int index = item.FindIndex(x => x.Id == id);
                item[index] = articleModel;

                string jSONString = JsonConvert.SerializeObject(item);
                edit.Insert(path, jSONString);

                return RedirectToAction("Index", "ShowItems");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadWrite/Delete/5
        public ActionResult Delete(int? id)
        {
            List<ArticleModel> item = new List<ArticleModel>();
            ReadWriteJson check = new ReadWriteJson();

            string mainPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = mainPath + @"\App_Data\source.json";
            item = JsonConvert.DeserializeObject<List<ArticleModel>>(check.Read(path));

            ArticleModel article = item.FirstOrDefault(x => x.Id == id);
            
            return View(article);
        }

        // POST: ReadWrite/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
