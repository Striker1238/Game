using System;

namespace DialogueSystem
{
    public class DialogueOption: IOption
    {
        public string Text { get; private set; }
        public DialogueNode NextNode { get; private set; }
        public Action? Action { get; private set; }
        public ICondition? Condition { get; private set; }

        public DialogueOption(string text, DialogueNode nextNode, Action? action = null, ICondition? condition = null)
        {
            Text = text;
            NextNode = nextNode;
            Action = action;
            Condition = condition;
        }
    }
}
