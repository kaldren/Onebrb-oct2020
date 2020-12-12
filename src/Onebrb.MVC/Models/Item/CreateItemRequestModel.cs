using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class CreateItemRequestModel
    {
        [Required(ErrorMessage = "Please choose a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please choose a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose a price")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Please choose a category")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
