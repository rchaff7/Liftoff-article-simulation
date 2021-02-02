using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liftoff_storefront.Models
{
    public class UserComment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string OwnerId { get; set; }

        public UserComment()
        {
        }

        public UserComment(string commentText, string ownerId)
        {
            CommentText = commentText;
            OwnerId = ownerId;
        }
    }
}
