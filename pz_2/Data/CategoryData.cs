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
    internal class CategoryData
    {
        public static List<Category> ReadAll()
        {
            using (BusinessContext db = new BusinessContext())
            {
                var categories = (from c in db.Categories select c).ToList();
                return categories;
            }

        }

        public static void Create(string categoryName)
        {
            using (BusinessContext db = new BusinessContext())
            {

                Category category = new Category { Name = categoryName };
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public static void Update(int oldCategoryId, string categoryName)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Category category = db.Categories.FirstOrDefault(d => d.Id == oldCategoryId);
                if (category != null)
                {
                    category.Name = categoryName;
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int categoryId)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Category categoryDel = (from b in db.Categories
                                       where b.Id == categoryId
                                       select b)
                                           .Include(b => b.Businesses)
                                                .ThenInclude(a => a.Contacts)
                                            .Include(b => b.Businesses)
                                                .ThenInclude(p => p.Products)
                                           .ToList()[0];

                if (categoryDel != null)
                {
                    foreach (Business business in categoryDel.Businesses)
                    {
                        BusinessData.DeleteImage("images", business.Id);
                        BusinessData.DeleteImage("logos", business.Id);

                        foreach (Product product in business.Products)
                            BusinessData.DeleteImage("product-images", product.Id);
                    }

                    db.Categories.Remove(categoryDel);
                    db.SaveChanges();
                }
            }
        }
    }
}
