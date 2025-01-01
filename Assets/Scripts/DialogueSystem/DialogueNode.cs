using System.Collections.Generic;
using System.Linq;

namespace DialogueSystem
{
    public class DialogueNode
    {
        public string Text { get; set; }
        public List<IOption> Options { get; set; } = new List<IOption>(); //Список всех возможных ответов/действий

        public DialogueNode(string text)
        {
            Text = text;
        }
        public List<IOption> GetAvailableOptions()
        {
            return Options.Where(option => option.Condition?.IsMet() ?? true).ToList();
        }

    }
}
