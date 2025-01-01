using System;

namespace DialogueSystem
{
    public class TradeOption: IOption
    {
        public string Text { get; private set; }
        public Action? Action { get; private set; }
        public ICondition? Condition { get; private set; }
    }
}
