using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Views.UserLoginView app = new Views.UserLoginView();
            app.Show();
            
            //Views.ApplicationView app = new Views.ApplicationView();
            //ViewModels.ApplicationViewModel context = new ViewModels.ApplicationViewModel();
            //app.DataContext = context;
            //app.Show();
        }
    }
}
