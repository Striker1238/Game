using System;
namespace DialogueSystem
{
    public class Condition : ICondition
    {
        private Func<bool> condition; //функция для проверки условия

        public Condition(Func<bool> condition)
        {
            this.condition = condition;
        }

        public bool IsMet()
        {
            return condition();
        }
    }
}