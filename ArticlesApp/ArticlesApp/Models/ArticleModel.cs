using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticlesApp.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
    }
}