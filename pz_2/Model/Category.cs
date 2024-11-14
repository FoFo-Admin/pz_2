using Microsoft.EntityFrameworkCore;
using pz_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Models
{
    [Table("categories")]
    internal class Category : IProperty
    {
        public List<Business> Businesses { get; set; }
    }
}
