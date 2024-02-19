using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
    }
}
