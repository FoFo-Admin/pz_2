using pz_2.Models;
using pz_2.Util;
using pz_2.View;
using pz_2.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace pz_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Navigation _navigation;

        public App()
        {
            _navigation = new Navigation();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new BusinessesListViewModel(_navigation, 
                                                                       CreatePropertiesListViewModel, 
                                                                       CreateAddBusinessViewModel,
                                                                       CreateBusinessInfoViewModel);
            MainWindow = new Main()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private BusinessesListViewModel CreateBusinessesListViewModel()
        {
            return new BusinessesListViewModel(_navigation,
                                               CreatePropertiesListViewModel,
                                               CreateAddBusinessViewModel,
                                               CreateBusinessInfoViewModel);
        }

        private PropertiesListViewModel CreatePropertiesListViewModel()
        {
            return new PropertiesListViewModel(_navigation,
                                               CreateBusinessesListViewModel,
                                               CreateCreateTypeViewModel);
        }

        private CreateTypeViewModel CreateCreateTypeViewModel(string type, string action, PropertiesViewModel item_to_edit)
        {
            return new CreateTypeViewModel(_navigation,
                                           CreatePropertiesListViewModel,
                                           type,
                                           action,
                                           item_to_edit);
        }

        private AddBusinessViewModel CreateAddBusinessViewModel(string action, BusinessViewModel business_to_edit)
        {
            return new AddBusinessViewModel(_navigation,
                                            CreateBusinessesListViewModel,
                                            action,
                                            business_to_edit,
                                            CreateBusinessInfoViewModel);
        }

        private CreateContactViewModel CreateCreateContactViewModel(string action, int businessId, Contact contact_to_edit)
        {
            return new CreateContactViewModel(_navigation,
                                            CreateBusinessInfoViewModel,
                                            action,
                                            businessId,
                                            contact_to_edit);

        }

        private CreateProductViewModel CreateCreateProductViewModel(string action, int businessId, ProductViewModel product_to_edit)
        {
            return new CreateProductViewModel(_navigation,
                                            CreateBusinessInfoViewModel,
                                            action,
                                            businessId,
                                            product_to_edit);
        }

        private BusinessInfoViewModel CreateBusinessInfoViewModel(int? businessId)
        {
            return new BusinessInfoViewModel(_navigation,
                                             CreateBusinessesListViewModel,
                                             CreateAddBusinessViewModel,
                                             CreateCreateContactViewModel,
                                             CreateCreateProductViewModel,
                                             businessId);
        }
    }

}
