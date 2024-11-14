using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.ViewModel
{
    internal class ContactViewModel
    {
        public readonly Contact _contact;

        public int Id => _contact.Id;
        public string Value => _contact.Value;
        public string Type => _contact.Type.Name;

        public ContactViewModel(Contact contact, int businessId)
        {
            _contact = contact;
        }

    }
}
