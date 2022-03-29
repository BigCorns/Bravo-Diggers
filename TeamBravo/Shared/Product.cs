using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo.Shared
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = "https://via.placeholder.com/300x300";
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; } = decimal.Zero;
        public bool IsPublic { get; set; }
        public bool IsDelete { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }

    }
}
