using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Interfaces
{
    public interface IOService
    {
        string OpenFileDialog(string defaultPath);
    }
}
