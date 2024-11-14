using Microsoft.Win32;
using pz_2.Commands;
using pz_2.Data;
using pz_2.Models;
using pz_2.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace pz_2.ViewModel
{
    internal class AddBusinessViewModel : ViewModelBase
    {
        public string _action;
        public BusinessViewModel _business_to_edit;

        private readonly ObservableCollection<PropertiesViewModel> _cities;
        public IEnumerable<PropertiesViewModel> Cities => _cities;
        private PropertiesViewModel _selected_city;
        public PropertiesViewModel SelectedCity
        {
            get => _selected_city;
            set
            {
                _selected_city = value;
                OnPropertyChanged(nameof(SelectedCity));
                ValidateInput();
            }
        }


        private readonly ObservableCollection<PropertiesViewModel> _categories;
        public IEnumerable<PropertiesViewModel> Categories => _categories;
        private PropertiesViewModel _selected_categoty;
        public PropertiesViewModel SelectedCategory
        {
            get => _selected_categoty;
            set
            {
                _selected_categoty = value;
                OnPropertyChanged(nameof(SelectedCategory));
                ValidateInput();
            }
        }

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

        private string _adress;
        public string Adress
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        private bool _isPublic;
        public bool IsPublic
        {
            get => _isPublic;
            set
            {
                _isPublic = value;
                OnPropertyChanged(nameof(IsPublic));
            }
        }

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

        private void ValidateInput()
        {
            if (!_fatalError)
            {
                ErrorMessage = string.Empty;
                if (string.IsNullOrWhiteSpace(Name) ||
                    SelectedCategory == null ||
                    SelectedCity == null)
                    ErrorMessage += "Всі поля з * повинні бути заповнені\n";
            }
        }


        private ICommand _selectLogo;
        public ICommand SelectLogo
        {
            get
            {
                if (_selectLogo == null)
                {
                    _selectLogo = new RelayCommand(obj => {
                        LogoUrl = FileDialog();
                    });
                }
                return _selectLogo;
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


        public ICommand GoToBusinesses { get; }
        public ICommand CreateBusiness { get; }

        public AddBusinessViewModel(Navigation navigation,
                                    Func<ViewModelBase> businessesViewModel,
                                    string action, 
                                    BusinessViewModel business_to_edit,
                                    Func<int?, ViewModelBase>? businessBackViewModel) 
        {
            _action = action;

            _cities = new ObservableCollection<PropertiesViewModel>();
            List<City> cities = CityData.ReadAll();
            foreach (City city in cities)
            {
                _cities.Add(new PropertiesViewModel(city));
            }

            _categories = new ObservableCollection<PropertiesViewModel>();
            List<Category> categories = CategoryData.ReadAll();
            foreach (Category category in categories)
            {
                _categories.Add(new PropertiesViewModel(category));
            }


            HeaderText = $"{(action == "Create" ? "Додавання нового бізнесу до системи" : "Редагування існуючого бізнесу")}";
            
            ErrorMessage = string.Empty;
            Name = string.Empty;
            SelectedCategory = null;
            SelectedCity = null;
            Description = string.Empty;
            Adress = string.Empty;
            Owner = string.Empty;
            LogoUrl = string.Empty; 
            ImageUrl = string.Empty;
            IsPublic = false;

            if (action == "Edit")
            {
                _business_to_edit = business_to_edit;

                Name = business_to_edit?.Name;
                Description = business_to_edit?.Description;
                foreach (PropertiesViewModel category in Categories)
                {
                    if(category.Name == business_to_edit?._business.Category.Name)
                        SelectedCategory = category;
                }
                foreach (PropertiesViewModel city in Cities)
                {
                    if (city.Name == business_to_edit?._business.City.Name)
                        SelectedCity = city;
                }
                //SelectedCategory = new PropertiesViewModel(business_to_edit?._business.Category);
                //SelectedCity = new PropertiesViewModel(business_to_edit?._business.City);
                Adress = business_to_edit?.Address;
                Owner = business_to_edit?.Owner;
                if(business_to_edit?._business.IsLogoAdded == true)
                {
                    LogoUrl = "Logo added";
                }
                if (business_to_edit?._business.IsPhotoAdded == true)
                {
                    ImageUrl = "Image added";
                }
                IsPublic = (bool)business_to_edit?.IsPublic;
            }

            if (action == "Edit" && business_to_edit == null)
            {
                ErrorMessage = "Помилка. Поверніться та спробуйте ще раз.";
                _fatalError = true;
            }

            if (business_to_edit == null)
            {
                GoToBusinesses = new NavigateCommand(navigation, businessesViewModel);
                CreateBusiness = new CreateBusiness(this, navigation, businessesViewModel);
            }
            else
            {
                GoToBusinesses = new NavigateCommand(navigation, ()=>businessBackViewModel(business_to_edit.Id));
                CreateBusiness = new CreateBusiness(this, navigation, () => businessBackViewModel(business_to_edit.Id));
            }
        }
    }
}
