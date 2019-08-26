namespace ArticlesApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class articlesEntities : DbContext
    {        
        public articlesEntities()
            : base("name=articlesEntities")
        {
            Database.SetInitializer<articlesEntities>(null);
        }        
        public virtual DbSet<tblArticle> tblArticles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblArticle>()
                .Property(e => e.Price)
                .HasPrecision(4, 2);
        }
    }
}
