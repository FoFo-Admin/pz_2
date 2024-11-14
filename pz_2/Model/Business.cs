using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Models
{
    [Table("businesses")]
    internal class Business
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public bool IsLogoAdded { get; set; } = false;
        public bool IsPhotoAdded { get; set; } = false;
        public bool IsPublic { get; set; } = false;


        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public City City { get; set; }
        public Category Category { get; set; }

        public List<Contact> Contacts { get; set; }
        public List<Product> Products { get; set; }
    }
}
