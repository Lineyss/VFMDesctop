using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewController.Base;

namespace VFMDesctop.ViewController.Windows
{
    internal class MainWindowsController : NotificationChange
    {
        private Page _frameContent;
        public Page frameContent
        {
            get => frameContent;
            set
            {
                _frameContent = new ProfilPage();
                OnPropertyChanged(nameof(frameContent));
            }
        }
    }
}
