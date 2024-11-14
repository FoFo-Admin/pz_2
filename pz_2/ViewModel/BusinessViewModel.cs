using pz_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace pz_2.ViewModel
{
    internal class BusinessViewModel : ViewModelBase
    {
        public readonly Business _business;

        public int Id => _business.Id;
        public string Name => _business.Name;
        public string Address => _business.Address;
        public string Description => _business.Description;
        public string Owner => _business.Owner;

        
        
        public bool IsPublic => _business.IsPublic;


        public string City => _business.City.Name;
        public string Category => _business.Category.Name;

        public List<Contact> Contacts => _business.Contacts;
        public List<Product> Products => _business.Products;



        private string _logoUrl;
        public string LogoUrl
        {
            get => _logoUrl;
            set
            {
                _logoUrl = value;
                OnPropertyChanged(nameof(LogoUrl));
            }
        }

        private BitmapImage _logo;
        public BitmapImage Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged(nameof(Logo));

            }
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get => _photoUrl;
            set
            {
                _photoUrl = value;
                OnPropertyChanged(nameof(PhotoUrl));
            }
        }

        private BitmapImage _photo;
        public BitmapImage Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                OnPropertyChanged(nameof(Photo));

            }
        }


        public BusinessViewModel(Business business)
        {
            _business = business;

            string appPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            if (business.IsLogoAdded)
            {
                string logosFolder = Path.Combine(appPath, "logos");
                string[] files = Directory.GetFiles(logosFolder, $"{business.Id}.*");
                LogoUrl = files.FirstOrDefault();
            }
            else
                LogoUrl = Path.Combine(appPath, "logos/placeholder.png");

            if (business.IsPhotoAdded)
            {
                string photosFolder = Path.Combine(appPath, "images");
                string[] files = Directory.GetFiles(photosFolder, $"{business.Id}.*");
                PhotoUrl = files.FirstOrDefault();
            }
            else
                PhotoUrl = Path.Combine(appPath, "images/placeholder.jpg");


            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            try
            {
                bi.UriSource = new Uri(LogoUrl);
            }
            catch
            {
                LogoUrl = Path.Combine(appPath, "logos/placeholder.png");
                bi.UriSource = new Uri(LogoUrl);
            }
            bi.EndInit();
            Logo = bi;


            BitmapImage bi2 = new BitmapImage();
            bi2.BeginInit();
            bi2.CacheOption = BitmapCacheOption.OnLoad;
            bi2.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            try
            {
                bi2.UriSource = new Uri(PhotoUrl);
            }
            catch
            {
                PhotoUrl = Path.Combine(appPath, "images/placeholder.jpg");
                bi2.UriSource = new Uri(PhotoUrl);
            }
            bi2.EndInit();
            Photo = bi2;
        }
    }
}
