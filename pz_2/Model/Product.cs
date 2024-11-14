using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Models
{
    [Table("products")]
    internal class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public float Price { get; set; }
        public float Discount { get; set; }
        public bool IsPhotoAdded { get; set; } = false;

        [Required]
        [ForeignKey("Business")]
        public int BusinessId { get; set; }


        public Business Business { get; set; }
    }
}
