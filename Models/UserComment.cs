using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liftoff_storefront.Models
{
    public class UserComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }

        public UserComment()
        {
        }

        public UserComment(string content)
        {
            Content = content;
        }
    }
}
