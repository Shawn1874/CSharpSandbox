using System;
using System.Windows.Input;

namespace SetFocusInTextBoxProblem
{
    /// <summary>
    /// Implementation of ICommand used to bind control behaviors to delegates.
    /// </summary>
    internal class RelayCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        /// <summary>
        /// Construct a command with only an execute method.  This type of command is always 
        /// enabled.
        /// </summary>
        /// <param name="executeMethod"></param>
        public RelayCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        /// <summary>
        /// Construct a command with both an execute method and a can execute method so that
        /// the command can be programmatically enabled or disabled.
        /// </summary>
        /// <param name="executeMethod"></param>
        /// <param name="canExecuteMethod"></param>
        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Raise the event to cause the binding to be refreshed.  This is done when the 
        /// state of the command might be changed from enabled to disabled or vice versa.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the CanExecute delegate.
        /// </summary>
        /// <param name="parameter">not used</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        /// <summary>
        /// Invoke the Execute delegate. 
        /// </summary>
        /// <param name="parameter">not used</param>
        public void Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }
    }
}