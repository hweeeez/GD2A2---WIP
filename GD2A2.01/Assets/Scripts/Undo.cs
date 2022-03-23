using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command invoker. Collects command into buffer to execute them at once.
/// </summary>
public class Undo : MonoBehaviour
{
    // Command history
    protected List<Command> commandHistory = new List<Command>();
    protected int executionIndex = 0;

    /// <summary>
    /// Method used to execute current command.
    /// </summary>
    /// <returns>The command index.</returns>
    public virtual int ExecuteCommand()
    {
        if (executionIndex < commandHistory.Count)
        {
            commandHistory[executionIndex].Execute();
            executionIndex++;
        }

        return executionIndex;
    }

    /// <summary>
    /// Method used to add and execute command.
    /// </summary>
    /// <returns>The command index.</returns>
    /// <param name="command">New command.</param>
    public virtual int ExecuteCommand(Command command)
    {
        for (int i = commandHistory.Count - 1; i >= executionIndex; i--)
        {
            commandHistory.RemoveAt(i);
        }

        commandHistory.Add(command);
        return ExecuteCommand();
    }

    /// <summary>
    /// Method used to undo command.
    /// </summary>
    /// <returns>The command index.</returns>
    public virtual int UndoCommand()
    {
        if (executionIndex > 0)
        {
            executionIndex--;
            commandHistory[executionIndex].Undo();
        }

        return executionIndex;
    }

    /// <summary>
    /// Method used to clear command buffer.
    /// </summary>
    public virtual void ClearCommandHistory()
    {
        commandHistory.Clear();
        executionIndex = 0;
    }

    /// <summary>
    /// Method used to get list of command names.
    /// </summary>
    /// <returns>The command names.</returns>
    public virtual string[] GetCommandNames()
    {
        var commandNames = new string[commandHistory.Count];
        for (int i = 0; i < commandHistory.Count; i++)
        {
            commandNames[i] = commandHistory[i].ToString();
        }

        return commandNames;
    }

}
