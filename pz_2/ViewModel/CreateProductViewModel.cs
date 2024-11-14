using Microsoft.Win32;
using pz_2.Commands;
using pz_2.Models;
using pz_2.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pz_2.ViewModel
{
    internal class CreateProductViewModel : ViewModelBase
    {
        public string _action;
        public ProductViewModel _product;
        public int _business_id;


        private string _headerText;
        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                OnPropertyChanged(nameof(HeaderText));
            }
        }

        private string _errorMessage;
        private bool _fatalError;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (!_fatalError)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                ValidateInput();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _amount;
        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                ValidateInput();
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
                ValidateInput();
            }
        }

        private string _discount;
        public string Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
                ValidateInput();
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }


        private ICommand _selectImage;
        public ICommand SelectImage
        {
            get
            {
                if (_selectImage == null)
                {
                    _selectImage = new RelayCommand(obj => {
                        ImageUrl = FileDialog();
                    });
                }
                return _selectImage;
            }
        }

        private string FileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.jpg, *.jpeg, *.gif) | *.png; *.jpg; *.jpeg; *.gif";
            openFileDialog.Title = "Оберіть зображення";

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            else
                return string.Empty;
        }

        private void ValidateInput()
        {
            if (!_fatalError)
            {
                ErrorMessage = string.Empty;
                if (string.IsNullOrWhiteSpace(Name))
                    ErrorMessage += "Всі поля з * повинні бути заповнені\n";
                float a, p, d;
                if (!float.TryParse(Amount, out a))
                    ErrorMessage += "Поле кількість повинно бути числом\n";
                else
                {
                    if (a <= 0)
                        ErrorMessage += "Поле кількість повинно бути більше 0\n";
                }
                if (!float.TryParse(Price, out p))
                    ErrorMessage += "Поле ціна повинно бути числом\n";
                else
                {
                    if (p <= 0)
                        ErrorMessage += "Поле ціна повинно бути більше 0\n";
                }
                if (!float.TryParse(Discount, out d))
                    ErrorMessage += "Поле дисконт повинно бути числом\n";
                else
                {
                    if (d < 0 || d > 100)
                        ErrorMessage += "Поле дисконт повинно бути в межах від 0 до 100\n";
                }
            }
        }

        public ICommand GoToBusiness { get; }
        public ICommand CreateProduct { get; }

        public CreateProductViewModel(Navigation navigation,
                                    Func<int?, ViewModelBase>? businessBackViewModel,
                                    string action,
                                    int businessId,
                                    ProductViewModel product_to_edit)
        {
            _action = action;
            _business_id = businessId;

            HeaderText = $"{(action == "Create" ? "Додавання нового продукту до бізнесу" : "Редагування існуючого продукту")}";

            ErrorMessage = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            ImageUrl = string.Empty;
            Amount = "1,0";
            Price = "1,0";
            Discount = "0,0";

            if (action == "Edit")
            {
                _product = product_to_edit;

                Name = product_to_edit?.Name;
                Description = product_to_edit?.Description;
                Amount = product_to_edit?.Amount.ToString();
                Price = product_to_edit?.Price.ToString();
                Discount = product_to_edit?.Discount.ToString();

                if (product_to_edit?._product.IsPhotoAdded == true)
                {
                    ImageUrl = "Image added";
                }
            }
            if (action == "Edit" && product_to_edit == null)
            {
                ErrorMessage = "Помилка. Поверніться та спробуйте ще раз.";
                _fatalError = true;
            }

            GoToBusiness = new NavigateCommand(navigation, () => businessBackViewModel(_business_id));
            CreateProduct = new CreateProduct(this, navigation, () => businessBackViewModel(_business_id));
        }

    }
}
