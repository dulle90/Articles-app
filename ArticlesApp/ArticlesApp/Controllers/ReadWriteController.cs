using ArticlesApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            return View();
        }

        // POST: ReadWrite/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadWrite/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: ReadWrite/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
