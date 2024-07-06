using System.Windows.Controls;
using VFMDesctop.Events;

namespace VFMDesctop.Models.Interfaces
{
    public interface INavigationService
    {
        event NavigationCommand NavigationChange;
        void SetNavigate(Page Page);
        Page GetNavigate();
    }
}