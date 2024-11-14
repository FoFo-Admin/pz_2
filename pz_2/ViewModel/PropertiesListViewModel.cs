using pz_2.Commands;
using pz_2.Data;
using pz_2.Model;
using pz_2.Models;
using pz_2.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pz_2.ViewModel
{
    internal class PropertiesListViewModel : ViewModelBase
    {
        private PropertiesViewModel _selected;
        public PropertiesViewModel Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private readonly ObservableCollection<PropertiesViewModel> _cities;
        public IEnumerable<PropertiesViewModel> Cities => _cities;


        private readonly ObservableCollection<PropertiesViewModel> _categories;
        public IEnumerable<PropertiesViewModel> Categories => _categories;


        private readonly ObservableCollection<PropertiesViewModel> _types;
        public IEnumerable<PropertiesViewModel> Types => _types;


        public ICommand GoToBusinesses { get; }
        public ICommand AddCity{ get; }
        public ICommand AddType { get; }
        public ICommand AddContact { get; }
        public ICommand Edit { get; }

        
        private ICommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommand(obj =>
                    {
                        if (Selected != null && Selected?.ClassName == "Type")
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні типу контактів?\nВсі контакти з цим типом будуть видалені",
                                "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                TypeData.Delete(Selected.Id);
                                _types.Remove(Selected);
                                Selected = null;
                            }
                        }
                        if (Selected != null && Selected?.ClassName == "Category")
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні категорії?\nВсі бізнеси за цією категорію будуть видалені",
                                "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                CategoryData.Delete(Selected.Id);
                                _categories.Remove(Selected);
                                Selected = null;
                            }
                        }
                        if (Selected != null && Selected?.ClassName == "City")
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні міста?\nВсі бізнеси за цим містом будуть видалені",
                                "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                CityData.Delete(Selected.Id);
                                _cities.Remove(Selected);
                                Selected = null;
                            }
                        }
                    });
                }
                return _delete;
            }
        }


        public PropertiesListViewModel(Navigation navigation, Func<ViewModelBase> businessesViewModel, Func<string, string, PropertiesViewModel, ViewModelBase> createTypeViewModel)
        {
            _cities = new ObservableCollection<PropertiesViewModel>();
            List<City> cities = CityData.ReadAll();
            foreach (City city in cities)
            {
                _cities.Add(new PropertiesViewModel(city));
            }

            _types = new ObservableCollection<PropertiesViewModel>();
            List<Models.Type> types = TypeData.ReadAll();
            foreach (Models.Type type in types)
            {
                _types.Add(new PropertiesViewModel(type));
            }

            _categories = new ObservableCollection<PropertiesViewModel>();
            List<Category> categories = CategoryData.ReadAll();
            foreach (Category category in categories)
            {
                _categories.Add(new PropertiesViewModel(category));
            }


            GoToBusinesses = new NavigateCommand(navigation, businessesViewModel);

            AddCity = new NavigateCommand(navigation, ()=>createTypeViewModel("city", "Create", null));

            AddType = new NavigateCommand(navigation, () => createTypeViewModel("category", "Create", null));
            AddContact = new NavigateCommand(navigation, () => createTypeViewModel("type", "Create", null));
            Edit = new NavigateCommand(navigation, () => createTypeViewModel(Selected?.ClassName.ToLower(), "Edit", Selected));
        }
    }
}
