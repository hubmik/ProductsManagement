using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class SelectedDateEventArgs : EventArgs
    {
        public virtual DateTime SelectedDate { get; set; }
    }
}
