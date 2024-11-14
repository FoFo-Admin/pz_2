using Microsoft.Identity.Client.NativeInterop;
using pz_2.Context;
using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Data
{
    internal class ProductData
    {
        public static void Create(string name,
                                  string description,
                                  float amount,
                                  float price,
                                  float discount,
                                  string photoUrl,
                                  int businessId)
        {
            using (BusinessContext db = new BusinessContext())
            {

                Product product = new Product
                {
                    Name = name,
                    Description = description,
                    Amount = amount,
                    Price = price,
                    Discount = discount,
                    IsPhotoAdded = (photoUrl != string.Empty),
                    BusinessId = businessId
                };
                db.Products.Add(product);
                db.SaveChanges();

                if (photoUrl != string.Empty)
                    BusinessData.SaveImage("product-images", photoUrl, product.Id);
            }
        }

        public static void Update(int oldProductId,
                                  string name,
                                  string description,
                                  float amount,
                                  float price,
                                  float discount,
                                  bool isPhotoChanged,
                                  string photoUrl)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Product product = db.Products.FirstOrDefault(d => d.Id == oldProductId);
                if (product != null)
                {
                    product.Name = name;
                    product.Description = description;
                    product.Amount = amount;
                    product.Price = price;
                    product.Discount = discount;
                    if (isPhotoChanged)
                        product.IsPhotoAdded = (photoUrl != string.Empty);
                    db.SaveChanges();

                    if (photoUrl != string.Empty && isPhotoChanged)
                    {
                        BusinessData.DeleteImage("product-images", product.Id);
                        BusinessData.SaveImage("product-images", photoUrl, product.Id);
                    }
                }
            }
        }

        public static void Delete(Product product)
        {
            using (BusinessContext db = new BusinessContext())
            {
                BusinessData.DeleteImage("product-images", product.Id);

                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
