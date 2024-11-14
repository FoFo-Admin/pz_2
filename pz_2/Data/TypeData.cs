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
    internal class TypeData
    {
        public static List<Models.Type> ReadAll()
        {
            using (BusinessContext db = new BusinessContext())
            {
                var types = (from t in db.Types select t).ToList();
                return types;
            }

        }

        public static void Create(string typeName)
        {
            using (BusinessContext db = new BusinessContext())
            {

                Models.Type type = new Models.Type { Name = typeName };
                db.Types.Add(type);
                db.SaveChanges();
            }
        }

        public static void Update(int oldTypeId, string typeName)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Models.Type type = db.Types.FirstOrDefault(d => d.Id == oldTypeId);
                if (type != null)
                {
                    type.Name = typeName;
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int typeId)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Models.Type typeDel = (from b in db.Types
                                       where b.Id == typeId
                                       select b)
                                           .Include(b => b.Contacts)
                                           .ToList()[0];

                if (typeDel != null)
                {
                    db.Types.Remove(typeDel);
                    db.SaveChanges();
                }
            }
        }
    }
}
