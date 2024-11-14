using pz_2.Data;
using pz_2.Util;
using pz_2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pz_2.Commands
{
    internal class CreateType : CommandBase
    {
        private CreateTypeViewModel _createTypeViewModel;
        private readonly Navigate _navigate;

        public CreateType(CreateTypeViewModel createTypeViewModel, Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _createTypeViewModel = createTypeViewModel;
            _navigate = new Navigate(navigation, viewModel);

            _createTypeViewModel.PropertyChanged += ErrorPropertyChanged;
        }

        public void ErrorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CreateTypeViewModel.ErrorMessage))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _createTypeViewModel.ErrorMessage == String.Empty;
        }

        public override void Execute(object parameter)
        {
            
            try
            {
                switch(_createTypeViewModel._action)
                {
                    case "Create":
                        switch(_createTypeViewModel._type)
                        {
                            case "city":
                                CityData.Create(_createTypeViewModel.InputText);
                                break;
                            case "category":
                                CategoryData.Create(_createTypeViewModel.InputText);
                                break;
                            case "type":
                                TypeData.Create(_createTypeViewModel.InputText);
                                break;
                        }
                        break;
                    case "Edit":
                        switch (_createTypeViewModel._type)
                        {
                            case "city":
                                CityData.Update(_createTypeViewModel._itemToEdit.Id, _createTypeViewModel.InputText);
                                break;
                            case "category":
                                CategoryData.Update(_createTypeViewModel._itemToEdit.Id, _createTypeViewModel.InputText);
                                break;
                            case "type":
                                TypeData.Update(_createTypeViewModel._itemToEdit.Id, _createTypeViewModel.InputText);
                                break;
                        }
                        break;
                    default:
                        break;
                }
                _navigate.NavigateTo();
                
            }
            catch (Exception ex)
            {
                _createTypeViewModel.ErrorMessage = ex.InnerException.Message;
            }
        }
    
    }
}
