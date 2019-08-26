using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArticlesApp.Models;

namespace ArticlesApp.Controllers
{
    public class InsertController : Controller
    {

        articlesEntities db = new articlesEntities();
        // GET: Insert
        public ActionResult InsertDataInDB()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult InsertDataInDB(tblArticle article)
        {
            tblArticle tbl = new tblArticle();            
            tbl.Name = article.Name;
            tbl.Description = article.Description;
            tbl.Categorie = article.Categorie;
            tbl.Manufacturer = article.Manufacturer;
            tbl.Supplier = article.Supplier;
            tbl.Price = article.Price;

            db.tblArticles.Add(tbl);
            db.SaveChanges();
            return View();
        }      
    }
}