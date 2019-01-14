using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Input;
using ClientApp.Interfaces;

namespace ClientApp.ViewModels
{
    public class UserLoginViewModel : Prism.Mvvm.BindableBase, ICloseable
    {
        private bool executing;
        private string accessKey;

        public event EventHandler CloseRequest;
        public string AccessKey { get => this.accessKey; set => this.SetProperty(ref accessKey, value); }
        public Prism.Commands.DelegateCommand LoginCommand { get; }

        public UserLoginViewModel()
        {
            LoginCommand = new Prism.Commands.DelegateCommand(SignInUser, () => !Executing);
        }

        public bool Executing
        {
            get => this.executing;
            set
            {
                if (SetProperty(ref this.executing, value))
                    LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public async void SignInUser()
        {
            bool isCorrect = false;

            using (var client = new WcfService.Service1Client())
            {
                isCorrect = await client.IsUserAuthenticatedAsync(this.AccessKey);
            }

            if (isCorrect)
            {
                Models.UserCredentials.SessionKey = this.AccessKey;
                Views.ApplicationView appView = new Views.ApplicationView();
                appView.Show();

                App.Current.MainWindow.Close();
                App.Current.MainWindow = appView;
            }
        }

        protected void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
