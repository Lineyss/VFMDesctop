using System;
using System.Windows.Controls;
using VFMDesctop.Events;
using VFMDesctop.Models.Interfaces;

namespace VFMDesctop.Models.Services
{
    public class NavigationService : INavigationService
    {
        public event NavigationChangeEventHendler NavigationChange;

        private Page CurrentPage { get; set; }
        public Page GetNavigate() => CurrentPage;

        public void SetNavigate(Page Page)
        {
            CurrentPage = Page;
            NavigationChange.Invoke();
        }
    }
}
