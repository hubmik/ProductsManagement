using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class CheckOrdersViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        public string Name => "Check Orders";
    }
}
