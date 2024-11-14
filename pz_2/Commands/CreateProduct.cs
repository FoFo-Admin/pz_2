using pz_2.Data;
using pz_2.Util;
using pz_2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Commands
{
    internal class CreateProduct : CommandBase
    {
        private CreateProductViewModel _createProductViewModel;
        private readonly Navigate _navigate;

        public CreateProduct(CreateProductViewModel createProductViewModel, Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _createProductViewModel = createProductViewModel;
            _navigate = new Navigate(navigation, viewModel);

            _createProductViewModel.PropertyChanged += ErrorPropertyChanged;
        }

        public void ErrorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateTypeViewModel.ErrorMessage))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _createProductViewModel.ErrorMessage == String.Empty;
        }

        public override void Execute(object parameter)
        {

            try
            {
                switch (_createProductViewModel._action)
                {
                    case "Create":
                        ProductData.Create(
                            _createProductViewModel.Name,
                            _createProductViewModel.Description,
                            float.Parse(_createProductViewModel.Amount),
                            float.Parse(_createProductViewModel.Price),
                            float.Parse(_createProductViewModel.Discount),
                            _createProductViewModel.ImageUrl,
                            _createProductViewModel._business_id);
                        break;
                    case "Edit":
                        ProductData.Update(
                            _createProductViewModel._product.Id,
                            _createProductViewModel.Name,
                            _createProductViewModel.Description,
                            float.Parse(_createProductViewModel.Amount),
                            float.Parse(_createProductViewModel.Price),
                            float.Parse(_createProductViewModel.Discount),
                            (_createProductViewModel.ImageUrl != "Image added"),
                            (_createProductViewModel.ImageUrl == "Image added" ? string.Empty : _createProductViewModel.ImageUrl));
                        break;
                    default:
                        break;
                }
                _navigate.NavigateTo();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _createProductViewModel.ErrorMessage = ex.InnerException.Message;
                else
                    _createProductViewModel.ErrorMessage = ex.Message;

            }
        }
    }
}
