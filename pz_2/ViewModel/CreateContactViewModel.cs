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
    internal class CreateContactViewModel : ViewModelBase
    {
        public string _action;
        public int _business_id;
        public Contact _contact;


        private readonly ObservableCollection<PropertiesViewModel> _types;
        public IEnumerable<PropertiesViewModel> Types => _types;
        private PropertiesViewModel _selected_type;
        public PropertiesViewModel SelectedType
        {
            get => _selected_type;
            set
            {
                _selected_type = value;
                OnPropertyChanged(nameof(SelectedType));
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
                ErrorMessage = string.Empty;
                if (string.IsNullOrWhiteSpace(InputText) ||
                    SelectedType == null)
                    ErrorMessage += "Всі поля повинні бути заповнені\n";
            }
        }


        public ICommand GoToBusiness { get; }
        public ICommand CreateContact { get; }
        


        public CreateContactViewModel(Navigation navigation,
                                    Func<int?, ViewModelBase>? businessBackViewModel,
                                    string action,
                                    int businessId,
                                    Contact contact_to_edit)
        {
            _action = action;
            _business_id = businessId;

            _types = new ObservableCollection<PropertiesViewModel>();
            List<Models.Type> types = TypeData.ReadAll();
            foreach (Models.Type type in types)
            {
                _types.Add(new PropertiesViewModel(type));
            }

            HeaderText = $"{(action == "Create" ? "Додавання нового контакту до бізнесу" : "Редагування існуючого контакту")}";

            ErrorMessage = string.Empty;
            SelectedType = null;
            InputText = string.Empty;

            if(action == "Edit")
            {
                _contact = contact_to_edit;

                InputText = contact_to_edit?.Value;
                foreach(PropertiesViewModel type in Types)
                {
                    if (type.Id == contact_to_edit.Type.Id)
                    {
                        SelectedType = type;
                    }
                }
            }
            if (action == "Edit" && contact_to_edit == null)
            {
                ErrorMessage = "Помилка. Поверніться та спробуйте ще раз.";
                _fatalError = true;
            }

            GoToBusiness = new NavigateCommand(navigation, () => businessBackViewModel(_business_id));
            CreateContact = new CreateContact(this, navigation, () => businessBackViewModel(_business_id));
        }
    }
}
