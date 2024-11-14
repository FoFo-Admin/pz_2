using pz_2.Commands;
using pz_2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pz_2.ViewModel
{
    internal class CreateTypeViewModel : ViewModelBase
    {
        public string _type;
        public string _action;

        public PropertiesViewModel _itemToEdit;


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

        private string _inputLabel;
        public string InputLabel
        {
            get => _inputLabel;
            set
            {
                _inputLabel = value;
                OnPropertyChanged(nameof(InputLabel));
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
                ValidateInput();
            }
        }

        private void ValidateInput()
        {
            if (!_fatalError)
            {
                if (string.IsNullOrWhiteSpace(InputText))
                    ErrorMessage = "Поле не може бути порожнім";
                else
                    ErrorMessage = string.Empty;
            }
        }

        public ICommand GoToTypes { get; }
        public ICommand CreateType { get; }

        public CreateTypeViewModel(Navigation navigation, Func<ViewModelBase> typesViewModel, string type, string action, PropertiesViewModel item_to_edit)
        {
            _type = type;
            _action = action;


            HeaderText = $"{(action == "Create" ? "Додавання" : "Редагування")} " +
                $"{(type == "city" ? "міста" :
                                       type == "category" ? "типу закладів" :
                                       "контакту")}";
            InputLabel = $"Назва {(type == "city" ? "міста" :
                                       type == "category" ? "типу закладів" :
                                       "контакту")}";

            ErrorMessage = string.Empty;
            InputText = string.Empty;

            if (action == "Edit")
            {
                _itemToEdit = item_to_edit;
                InputText = item_to_edit?.Name;
            }

            if (action == "Edit" && item_to_edit == null)
            {
                ErrorMessage = "Помилка. Поверніться та спробуйте ще раз.";
                _fatalError = true;
            }

            GoToTypes = new NavigateCommand(navigation, typesViewModel);
            CreateType = new CreateType(this, navigation, typesViewModel);
        }
    }
}
