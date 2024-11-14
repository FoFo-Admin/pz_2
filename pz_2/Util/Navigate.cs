using pz_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Util
{
    internal class Navigate
    {
        private readonly Navigation _navigation;
        private readonly Func<ViewModelBase> _viewModel;

        public Navigate(Navigation navigation, Func<ViewModelBase> viewModel)
        {
            _navigation = navigation;
            _viewModel = viewModel;
        }

        public void NavigateTo()
        {
            _navigation.CurrentViewModel = _viewModel();
        }
    }
}
