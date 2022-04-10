using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOOSeal.Core
{
    public sealed class RelayCommand: ICommand
    {
        private readonly Action<object> m_execute;
        private readonly Func<object, bool> m_canExecute;
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove=> CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> mExecute, Func<object, bool> mCanExecute = null)
        {
            m_execute = mExecute;
            m_canExecute = mCanExecute;
        }

        public bool CanExecute(object parameter) => m_canExecute == null || m_canExecute(parameter);

        public void Execute(object parameter) => m_execute(parameter);

    }
}
