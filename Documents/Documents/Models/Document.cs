using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Documents.Models
{
    public class Document
    {
        public int ID { get; set; }
        public string Name {get; set;}
        public string ReleaseDate { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }

        //public Document()
        //{
        //    ID = 0;
        //    Name = "";
        //    ReleaseDate = "";
        //    Author = "";
        //    Tags = "";
        //}
    }

    public class DocumentDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
    }
}