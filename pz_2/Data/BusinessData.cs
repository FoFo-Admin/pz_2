using Microsoft.EntityFrameworkCore;
using pz_2.Context;
using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Data
{
    internal class BusinessData
    {
        public static List<Business> ReadAll()
        {
            using (BusinessContext db = new BusinessContext())
            {
                var businesses = (from b in db.Businesses select b)
                           .Include(b => b.City)        
                           .Include(b => b.Category)  
                           .ToList();
                return businesses;
            }
        }

        public static List<Business> ReadByFilter(string cityName, string categoryName, string partOfName)
        {
            using (BusinessContext db = new BusinessContext())
            {
                var businesses = (from b in db.Businesses
                                  join c in db.Cities on b.CityId equals c.Id
                                  join cat in db.Categories on b.CategoryId equals cat.Id
                                  where (string.IsNullOrEmpty(cityName) || c.Name == cityName) &&
                                        (string.IsNullOrEmpty(categoryName) || cat.Name == categoryName) &&
                                        (string.IsNullOrEmpty(partOfName) || b.Name.Contains(partOfName))
                                  select b)
                                           .Include(b => b.City)
                                           .Include(b => b.Category)
                                           .ToList();
                return businesses;
            }
        }

        public static Business ReadById(int id)
        {
            using (BusinessContext db = new BusinessContext())
            {
                var business = (from b in db.Businesses 
                                where b.Id == id
                                select b)
                           .Include(b => b.City)
                           .Include(b => b.Category)
                           .Include(b => b.Contacts)
                                    .ThenInclude(c => c.Type)
                           .Include(b => b.Products)
                           .ToList();
                return business[0];
            }
        }

        public static void Create(string name,
                                  string adress,
                                  string description,
                                  string owner,
                                  string logoUrl,
                                  string photoUrl, 
                                  bool isPublic,
                                  int cityId,
                                  int categoryId)
        {
            using (BusinessContext db = new BusinessContext())
            {

                Business business = new Business { 
                                           Name = name,
                                           Address = adress,
                                           Description = description,
                                           Owner = owner,
                                           IsLogoAdded = (logoUrl != string.Empty),
                                           IsPhotoAdded = (photoUrl != string.Empty),
                                           IsPublic = isPublic,
                                           CityId = cityId,
                                           CategoryId = categoryId
                };
                db.Businesses.Add(business);
                db.SaveChanges();

                if (logoUrl != string.Empty)
                    SaveImage("logos", logoUrl, business.Id);
                if (photoUrl != string.Empty)
                    SaveImage("images", photoUrl, business.Id);

                
            }
        }

        public static void SaveImage(string folder, string path, int Id)
        {
            string extension = Path.GetExtension(path);
            string imagesFolder = Path.Combine(".", folder);
            Directory.CreateDirectory(imagesFolder);

            string fileName = $"{Id}{extension}";
            string destinationFile = Path.Combine(imagesFolder, fileName);

            File.Copy(path, destinationFile, true);
        }

        public static void DeleteImage(string folder, int Id)
        {
            string imagesFolder = Path.Combine(".", folder);
            string[] files = Directory.GetFiles(imagesFolder, $"{Id}.*");

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(file);
                }
            }
        }

        public static void Update(int oldBusinessId,
                                  string name,
                                  string adress,
                                  string description,
                                  string owner,
                                  bool isLogoChanged,
                                  string logoUrl,
                                  bool isPhotoChanged,
                                  string photoUrl,
                                  bool isPublic,
                                  int cityId,
                                  int categoryId)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Business business = db.Businesses.FirstOrDefault(d => d.Id == oldBusinessId);
                if (business != null)
                {
                    business.Name = name;
                    business.Address = adress;
                    business.Description = description;
                    business.Owner = owner;
                    if (isLogoChanged)
                        business.IsLogoAdded = (logoUrl != string.Empty);
                    if (isPhotoChanged)
                        business.IsPhotoAdded = (photoUrl != string.Empty);
                    business.IsPublic = isPublic;
                    business.CityId = cityId;
                    business.CategoryId = categoryId;
                    db.SaveChanges();

                    if (logoUrl != string.Empty && isLogoChanged)
                    {
                        DeleteImage("logos", business.Id);
                        SaveImage("logos", logoUrl, business.Id);
                    }
                    if (photoUrl != string.Empty && isPhotoChanged)
                    {
                        DeleteImage("images", business.Id);
                        SaveImage("images", photoUrl, business.Id);
                    }  
                }
            }
        }


        public static void Delete(Business business)
        {
            using (BusinessContext db = new BusinessContext())
            {
                Business businessDel = (from b in db.Businesses
                                        where b.Id == business.Id
                                        select b)
                                           .Include(b => b.Contacts)
                                           .Include(b => b.Products)
                                           .ToList()[0];

                if (businessDel != null) {
                    BusinessData.DeleteImage("images", business.Id);
                    BusinessData.DeleteImage("logos", business.Id);

                    foreach (Product product in business.Products.ToList())
                        DeleteImage("product-images", product.Id);

                    db.Businesses.Remove(businessDel);
                    db.SaveChanges();
                }
            }
        }
    }


}
