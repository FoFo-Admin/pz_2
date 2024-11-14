using pz_2.Data;
using pz_2.Util;
using pz_2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Commands
{
    internal class CreateBusiness : CommandBase
    {
        private AddBusinessViewModel _addBusinessViewModel;
        private readonly Navigation _navigation;
        private readonly Func<ViewModelBase> _viewModel;
        //private readonly Navigate _navigate;

        public CreateBusiness(AddBusinessViewModel addBusinessViewModel, Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _addBusinessViewModel = addBusinessViewModel;

            _navigation = navigation;
            _viewModel = viewModel;
            //_navigate = new Navigate(navigation, viewModel);

            _addBusinessViewModel.PropertyChanged += ErrorPropertyChanged;
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
            return _addBusinessViewModel.ErrorMessage == String.Empty;
        }

        public override void Execute(object parameter)
        {

            try
            {
                switch (_addBusinessViewModel._action)
                {
                    case "Create":
                        BusinessData.Create(
                            _addBusinessViewModel.Name,
                            _addBusinessViewModel.Adress,
                            _addBusinessViewModel.Description,
                            _addBusinessViewModel.Owner,
                            _addBusinessViewModel.LogoUrl,
                            _addBusinessViewModel.ImageUrl,
                            _addBusinessViewModel.IsPublic,
                            _addBusinessViewModel.SelectedCity.Id,
                            _addBusinessViewModel.SelectedCategory.Id);
                        break;
                    case "Edit":
                        BusinessData.Update(
                            _addBusinessViewModel._business_to_edit.Id,
                            _addBusinessViewModel.Name,
                            _addBusinessViewModel.Adress,
                            _addBusinessViewModel.Description,
                            _addBusinessViewModel.Owner,
                            (_addBusinessViewModel.LogoUrl != "Logo added"),
                            (_addBusinessViewModel.LogoUrl == "Logo added" ? string.Empty : _addBusinessViewModel.LogoUrl),
                            (_addBusinessViewModel.ImageUrl != "Image added"),
                            (_addBusinessViewModel.ImageUrl == "Image added" ? string.Empty : _addBusinessViewModel.ImageUrl),
                            _addBusinessViewModel.IsPublic,
                            _addBusinessViewModel.SelectedCity.Id,
                            _addBusinessViewModel.SelectedCategory.Id);

                        break;
                    default:
                        break;
                }
                Navigate nav = new Navigate(_navigation, _viewModel);
                nav.NavigateTo();
                //_navigate.NavigateTo();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _addBusinessViewModel.ErrorMessage = ex.InnerException.Message;
                else
                    _addBusinessViewModel.ErrorMessage = ex.Message;

            }
        }
    }
}
