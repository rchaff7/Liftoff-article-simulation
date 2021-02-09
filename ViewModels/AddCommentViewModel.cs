using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace liftoff_storefront.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
