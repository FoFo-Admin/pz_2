using pz_2.Util;
using pz_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Commands
{
    internal class NavigateCommand : CommandBase
    {
        private readonly Navigation _navigation;
        private readonly Func<ViewModelBase> _viewModel;

        public NavigateCommand(Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _navigation = navigation;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = _viewModel();
        }
    }
}
