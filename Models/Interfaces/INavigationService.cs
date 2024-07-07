using System.Windows.Controls;
using VFMDesctop.Events;

namespace VFMDesctop.Models.Interfaces
{
    public interface INavigationService
    {
        event NavigationChangeEventHendler NavigationChange;
        void SetNavigate(Page Page);
        Page GetNavigate();
    }
}