using Microsoft.EntityFrameworkCore;
using pz_2.Context;
using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Data
{
    internal class ContactData
    {
        public static void Create(string Value, int TypeId, int BusinessId)
        {
            using (BusinessContext db = new BusinessContext())
            {

                Contact contact = new Contact
                {
                    Value = Value,
                    TypeId = TypeId,
                    BusinessId = BusinessId
                };
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        public static void Update(int oldContactId, string Value, int TypeId)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Contact contact = db.Contacts.FirstOrDefault(d => d.Id == oldContactId);
                if (contact != null)
                {
                    contact.Value = Value;
                    contact.TypeId = TypeId;
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(Contact contact)
        {
            using (BusinessContext db = new BusinessContext())
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
        }
    }
}
