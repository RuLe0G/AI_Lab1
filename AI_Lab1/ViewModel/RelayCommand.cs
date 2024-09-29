using System.Windows.Input;

namespace AI_Lab1.ViewModel;

public class RelayCommand : ICommand
{
    private readonly Action m_Execute;
    private readonly Func<bool> m_CanExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        m_Execute = execute;
        m_CanExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return m_CanExecute == null || m_CanExecute();
    }

    public void Execute(object parameter)
    {
        m_Execute();
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}