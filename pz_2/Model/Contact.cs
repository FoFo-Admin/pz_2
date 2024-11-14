using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Models
{
    [Table("contacts")]
    internal class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }


        [Required]
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        [Required]
        [ForeignKey("Business")]
        public int BusinessId { get; set; }


        public Type Type { get; set; }
        public Business Business { get; set; }
    }
}
