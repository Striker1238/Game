using System;

namespace DialogueSystem
{
    public interface IOption
    {
        string Text { get; }
        //IOption NextNode { get; }
        Action? Action { get;}
        ICondition? Condition { get; }
    }
}
