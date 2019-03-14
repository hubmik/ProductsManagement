using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class HomeViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        private string _userName;
        private DateTime _birthDate;
        private DateTime _hireDate;
        private string _jobPosition;

        public string Name => "Home";
        public string UserName { get => this._userName; set => this.SetProperty(ref _userName, value); }
        public DateTime BirthDate { get => this._birthDate; set => this.SetProperty(ref _birthDate, value); }
        public DateTime HireDate { get => this._hireDate; set => this.SetProperty(ref _hireDate, value); }
        public string JobPosition { get => this._jobPosition; set => this.SetProperty(ref _jobPosition, value); }
        
        public HomeViewModel()
        {
            UserController();
        }

        public void UserController()
        {
            Employees employee = null;
            Models.UsersImplements usersImplements = new Models.UsersImplements();
            employee = usersImplements.GetUserCredentials(Models.UserCredentials.SessionKey);

            this.UserName = employee.FirstName + " " + employee.LastName;
            this.BirthDate = employee.BirthDate;
            this.HireDate = employee.HireDate;
            this.JobPosition = employee.JobPosition;
        }
    }
}
