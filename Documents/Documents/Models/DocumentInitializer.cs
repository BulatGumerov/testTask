using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Documents.Models
{
    public class DocumentInitializer : DropCreateDatabaseIfModelChanges<DocumentDbContext>
    {
        protected override void Seed(DocumentDbContext context)
        {
           
        }
    }
}