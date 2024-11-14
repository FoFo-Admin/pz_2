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
    internal class BusinessesListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BusinessViewModel> _businesses;
        public IEnumerable<BusinessViewModel> Businesses => _businesses;


        private readonly ObservableCollection<string> _cities;
        public IEnumerable<string> Cities => _cities;
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                UpdateView();
            }
        }


        private readonly ObservableCollection<string> _categories;
        public IEnumerable<string> Categories => _categories;

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                UpdateView();
            }
        }


        private BusinessViewModel _selectedBusiness;
        public BusinessViewModel SelectedBusiness
        {
            get { return _selectedBusiness; }
            set
            {
                _selectedBusiness = value;
                OnPropertyChanged(nameof(SelectedBusiness));
            }
        }

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
                UpdateView();
            }
        }

        private void UpdateView()
        {
            _businesses.Clear();

            List<Business> businesses = BusinessData.ReadByFilter(
                SelectedCity == "Всі міста" ? string.Empty : SelectedCity,
                SelectedCategory == "Всі категорії" ? string.Empty : SelectedCategory,
                InputText
                );

            foreach (Business business in businesses)
            {
                _businesses.Add(new BusinessViewModel(business));
            }
        }

        public ICommand GoToTypes { get; }
        public ICommand GoToCreateBusiness { get; }
        public ICommand GoToSelectedBusiness { get; }

        public BusinessesListViewModel(Navigation navigation,
                                       Func<ViewModelBase> typesViewModel,
                                       Func<string, BusinessViewModel, ViewModelBase> createBusinessViewModel,
                                       Func<int?, ViewModelBase> createBusinessInfoViewModel)
        {
            _businesses = new ObservableCollection<BusinessViewModel>();
            List<Business> businesses = BusinessData.ReadAll();
            foreach (Business business in businesses)
            {
                _businesses.Add(new BusinessViewModel(business));
            }

            _cities = new ObservableCollection<string>();
            _cities.Add("Всі міста");
            List<City> cities = CityData.ReadAll();
            foreach (City city in cities)
            {
                _cities.Add(city.Name);
            }

            _categories = new ObservableCollection<string>();
            _categories.Add("Всі категорії");
            List<Category> categories = CategoryData.ReadAll();
            foreach (Category category in categories)
            {
                _categories.Add(category.Name);
            }

            SelectedCategory = "Всі категорії";
            SelectedCity = "Всі міста";

            GoToTypes = new NavigateCommand(navigation, typesViewModel);
            GoToCreateBusiness = new NavigateCommand(navigation, ()=>createBusinessViewModel("Create", null));
            GoToSelectedBusiness = new NavigateCommand(navigation, () => createBusinessInfoViewModel(SelectedBusiness?.Id));
        }
    }
}
