using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp.Views
{
    /// <summary>
    /// Interaction logic for ChangeOrderView.xaml
    /// </summary>
    public partial class ChangeOrderView : Window
    {
        public ChangeOrderView()
        {
            InitializeComponent();
            ViewModels.ChangeOrderViewModel vm = new ViewModels.ChangeOrderViewModel();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }
    }
}
