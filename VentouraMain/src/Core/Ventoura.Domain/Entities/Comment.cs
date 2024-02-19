using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public Guid GuestId { get; set; }
        public string CommentDescription { get; set; }
        public int Rating { get; set; }
        public DateTime CommentedOn { get; set; }


    }
}
