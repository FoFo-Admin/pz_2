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
using System.Windows;
using System.Windows.Input;

namespace pz_2.ViewModel
{
    internal class BusinessInfoViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Contact> _contacts;
        public IEnumerable<Contact> Contacts => _contacts;
        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }


        private readonly ObservableCollection<ProductViewModel> _products;
        public IEnumerable<ProductViewModel> Products => _products;
        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }


        private BusinessViewModel _business;
        public BusinessViewModel Business
        {
            get => _business;
            set
            {
                _business = value;
                OnPropertyChanged(nameof(Business));
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

        public ICommand GoToBusinesses { get; }
        public ICommand EditBusiness {  get; }
        public ICommand CreateContact { get; }
        public ICommand EditContact { get; }
        public ICommand CreateProduct { get; }
        public ICommand EditProduct { get; }


        private ICommand _deleteContact;
        public ICommand DeleteContact
        {
            get
            {
                if (_deleteContact == null)
                {
                    _deleteContact = new RelayCommand(obj =>
                    {
                        if (SelectedContact != null) {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні контакту?", 
                                "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                ContactData.Delete(SelectedContact);
                                _contacts.Remove(SelectedContact);
                                SelectedContact = null;
                            }
                        }
                    });
                }
                return _deleteContact;
            }
        }

        private ICommand _deleteProduct;
        public ICommand DeleteProduct
        {
            get
            {
                if (_deleteProduct == null)
                {
                    _deleteProduct = new RelayCommand(obj =>
                    {
                        if (SelectedProduct != null)
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні продукту?",
                                "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                ProductData.Delete(SelectedProduct._product);
                                _products.Remove(SelectedProduct);
                                SelectedProduct = null;
                            }
                        }
                    });
                }
                return _deleteProduct;
            }
        }


        private ICommand _deleteBusiness;
        public ICommand DeleteBusiness
        {
            get
            {
                if (_deleteBusiness == null)
                {
                    _deleteBusiness = new RelayCommand(obj =>
                    {

                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевненні у видаленні бізнесу?\nВсі пов'язані з ним контакти та продукти будуть видалені.",
                            "Підтвердження видалення", System.Windows.MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            BusinessData.Delete(_business._business);
                            GoToBusinesses.Execute(obj);
                        }
                        
                    });
                }
                return _deleteBusiness;
            }
        }




        public BusinessInfoViewModel(Navigation navigation,
                                     Func<ViewModelBase> businessesViewModel, 
                                     Func<string, BusinessViewModel, ViewModelBase> editBusinessViewModel,
                                     Func<string, int, Contact, ViewModelBase> createContactViewModel,
                                     Func<string, int, ProductViewModel, ViewModelBase> createProductViewModel,
                                     int? businessId)
        {
            GoToBusinesses = new NavigateCommand(navigation, businessesViewModel);

            if (businessId == null)
            {
                ErrorMessage = "Помилка. Поверніться та спробуйте ще раз.";
                _fatalError = true;
            }
            else
            {
                Business business = BusinessData.ReadById((int)businessId);
                _business = new BusinessViewModel(business);

                _contacts = new ObservableCollection<Contact>();
                foreach (Contact contact in _business.Contacts)
                    _contacts.Add(contact);

                _products = new ObservableCollection<ProductViewModel>();
                foreach (Product product in _business.Products)
                    _products.Add(new ProductViewModel(product));

                EditBusiness = new NavigateCommand(navigation,
                                                   () => editBusinessViewModel("Edit", _business));
                CreateContact = new NavigateCommand(navigation, () => createContactViewModel("Create", _business.Id, null));
                EditContact = new NavigateCommand(navigation, () => createContactViewModel("Edit", _business.Id, SelectedContact));

                CreateProduct = new NavigateCommand(navigation, () => createProductViewModel("Create", _business.Id, null));
                EditProduct = new NavigateCommand(navigation, () => createProductViewModel("Edit", _business.Id, SelectedProduct));
            }
        }
    }
}
