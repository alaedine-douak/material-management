namespace GM.Commands;

public abstract class AsyncCommandBase : CommandBase
{
    private bool isExecuting;
    public bool IsExecuting
    {
        get => isExecuting;
        set
        {
            isExecuting = value;
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter) // task
    {
        try
        {
            await ExecuteAsync(parameter);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}
