using UnityEngine;

namespace DialogueSystem
{
    public class DialogueComponent : ICommunicate
    {
        private DialogueNode currentNode;

        public DialogueComponent(DialogueNode startNode)
        {
            currentNode = startNode;
        }

        public void ShowDialogue()
        {
            Debug.Log(currentNode.Text);
            for (int i = 0; i < currentNode.Options.Count; i++)
            {
                Debug.Log($"{i + 1}: {currentNode.Options[i].Text}");
            }
        }

        public void RespondToDialogue(int optionIndex)
        {
            if (optionIndex < 0 || optionIndex >= currentNode.Options.Count)
            {
                Debug.Log("Invalid option.");
                return;
            }

            var selectedOption = currentNode.Options[optionIndex];
            selectedOption.Action?.Invoke(); // Выполняем действие, связанное с выбором
            currentNode = selectedOption.NextNode;
        }

        public bool CanCommunicate()
        {
            return currentNode != null;
        }
    }
    public interface ITrade
    {
        DialogueOption DialogueOption { get; }
    }
}
