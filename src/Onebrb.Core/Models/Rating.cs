using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Onebrb.Core.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
        public int Value { get; set; }
    }
}
