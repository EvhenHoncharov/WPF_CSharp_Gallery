using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exam_Gallery.Command
{
    public class UserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> execute;
        private Predicate<object> canExecute;

        public UserCommand(Action<object> _execute) : this(_execute, null) { }
        public UserCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            execute = _execute ?? throw new ArgumentNullException(nameof(_execute));
            canExecute = _canExecute;
        }
      
        public bool CanExecute(object parameter) => (canExecute == null) ? true : canExecute(parameter);
        public void Execute(object parameter) => execute(parameter);
    }
}