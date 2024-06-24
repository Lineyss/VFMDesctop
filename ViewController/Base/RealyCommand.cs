using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VFMDesctop.ViewController
{
    internal class RealyCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
 
        public RealyCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
 
        // Определяет может ли команда выполнится
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
 
        // Выполняет логику команды
        public void Execute(object parameter)
        {
            execute(parameter);
        }

    }
}
