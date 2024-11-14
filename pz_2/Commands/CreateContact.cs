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
    internal class CreateContact : CommandBase
    {
        private CreateContactViewModel _createContactViewModel;
        private readonly Navigate _navigate;

        public CreateContact(CreateContactViewModel createContactViewModel, Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _createContactViewModel = createContactViewModel;
            _navigate = new Navigate(navigation, viewModel);

            _createContactViewModel.PropertyChanged += ErrorPropertyChanged;
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
            return _createContactViewModel.ErrorMessage == String.Empty;
        }


        public override void Execute(object parameter)
        {

            try
            {
                switch (_createContactViewModel._action)
                {
                    case "Create":
                        ContactData.Create(
                            _createContactViewModel.InputText,
                            _createContactViewModel.SelectedType.Id,
                            _createContactViewModel._business_id);
                        break;
                    case "Edit":
                        ContactData.Update(
                            _createContactViewModel._contact.Id,
                            _createContactViewModel.InputText,
                            _createContactViewModel.SelectedType.Id);
                        break;
                    default:
                        break;
                }
                _navigate.NavigateTo();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _createContactViewModel.ErrorMessage = ex.InnerException.Message;
                else
                    _createContactViewModel.ErrorMessage = ex.Message;

            }
        }
    }
}
