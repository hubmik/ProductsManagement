using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class ImageHandler
    {
        public string ImagePath { get; set; }
        public byte[] ImageToByte { get; set; }

        public delegate void SetImage();
        public event SetImage ImageSetEventArgs;
    }
}
