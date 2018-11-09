using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class HomeViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        public string Name => "Home";

        public HomeViewModel()
        {

        }
    }
}
