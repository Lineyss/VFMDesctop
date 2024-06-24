using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VFMDesctop.ViewModels.Help
{
    class propertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}