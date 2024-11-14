using pz_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.ViewModel
{
    internal class PropertiesViewModel : ViewModelBase
    {
        public readonly IProperty _property;

        public int Id => _property.Id;
        public string Name => _property.Name;
        public string ClassName => _property.GetType().Name;

        public PropertiesViewModel(IProperty property)
        {
            _property = property;
        }
    }
}
