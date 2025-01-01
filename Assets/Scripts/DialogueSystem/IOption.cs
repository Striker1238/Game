using System;

namespace DialogueSystem
{
    public interface IOption
    {
        string Text { get; }
        Action? Action { get;}
        ICondition? Condition { get; }
    }
}
