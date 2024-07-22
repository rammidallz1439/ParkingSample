using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Stack<ICommand> _commands = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commands.Push(command);
    }

    public void UndoCommand()
    {
        if (_commands.Count > 0)
        {
            ICommand command = _commands.Pop();
            command.Undo();
        }
    }
}
