using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pz_2.Models;
using System.Windows.Media.Imaging;
using System.IO;

namespace pz_2.ViewModel
{
    internal class ProductViewModel : ViewModelBase
    {
        public readonly Product _product;


        public int Id => _product.Id;
        public string Name => _product.Name;
        public string Description => _product.Description;
        public float Amount => _product.Amount;
        public float Price => _product.Price;
        public float Discount => _product.Discount;

        private string _priceText;
        public string PriceText
        {
            get => _priceText;
            set
            {
                _priceText = value;
                OnPropertyChanged(nameof(PriceText));
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

        public ProductViewModel(Product product)
        {
            _product = product;

            string appPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            if (product.IsPhotoAdded)
            {
                string photosFolder = Path.Combine(appPath, "product-images");
                string[] files = Directory.GetFiles(photosFolder, $"{product.Id}.*");
                PhotoUrl = files.FirstOrDefault();
            }
            else
                PhotoUrl = Path.Combine(appPath, "product-images/placeholder.png");

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bi.UriSource = new Uri(PhotoUrl);
            bi.EndInit();
            Photo = bi;

            if (Discount == 0)
                PriceText = $"{Price}";
            else
                PriceText = $"Знижка!\n{Price * (1-(0.01*Discount))}\nЗамість {Price}";

        }
    }
}
