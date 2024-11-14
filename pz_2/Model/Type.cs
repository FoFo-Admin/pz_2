using Microsoft.EntityFrameworkCore;
using pz_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace pz_2.Models
{
    [Table("types")]
    internal class Type : IProperty
    {
        public List<Contact> Contacts { get; set; }
    }
}
