using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueComponent : MonoBehaviour, ICommunicate
    {
        private DialogueNode currentNode;
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private Transform dialogParent;
        [SerializeField] private GameObject optionPrefab;


        private List<GameObject> optionObjects;
        public DialogueComponent(DialogueNode startNode)
        {
            currentNode = startNode;
        }

        public void Start() 
        {
            optionObjects = new List<GameObject>();
            //Тест системы диалогов
            currentNode = new DialogueNode("Тут какой то текст, по типу рассказ торговца")
            { Options = new List<IOption>(3) {   
                new DialogueOption("Привет!", new DialogueNode("Не хочешь что нибудь приобрести у меня в магазине?")
                { Options = new List<IOption>(2)
                {
                    new TradeOption("Посмотреть магазин"),
                    new DialogueOption("Пока")
                } }),
                new DialogueOption("Как я тут оказался?", new DialogueNode("Долгая история, когда нибудь я расскажу, лучше глянь в мой магазин")
                { Options = new List<IOption>(2)
                {
                    new TradeOption("Посмотреть магазин"),
                    new DialogueOption("Пока")
                } }),
                new DialogueOption("Пока")
            } };
            
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (CanCommunicate())
                {
                    ShowDialogue();
                }
                else
                {
                    Debug.Log("Не могу говорить");
                }
            }
            //if (Input.GetKeyDown(KeyCode.E)) RespondToDialogue(1);
        }

        public void ShowDialogue()
        {
            if (optionObjects != null && optionObjects.Count > 0)
            {
                foreach (var option in optionObjects)
                    Destroy(option);
                optionObjects.Clear();
            }
            

            dialogPanel.GetComponentInChildren<TextMeshPro>().text = currentNode.Text;
            for (int i = 0; i < currentNode.Options.Count; i++)
            {
                var optionButton = Instantiate(optionPrefab, dialogParent);

                optionButton.GetComponentInChildren<TextMeshPro>().text = $"{i + 1}. {currentNode.Options[i].Text}";
                //В данном цикле требуется создание локальной переменной, тк если мы будет просто
                //отправлять i в RespondToDialogue, то лямда выражение захватит именно i и на всех
                //кнопках будет одинаковый index
                int buttonIndex = i;
                optionButton.GetComponent<Button>().onClick.AddListener(() => RespondToDialogue(buttonIndex));
                optionObjects.Add(optionButton);
            }
        }

        public void RespondToDialogue(int optionIndex)
        {
            
            if (optionIndex < 0 || optionIndex >= currentNode.Options.Count)
            {
                Debug.Log($"Invalid option. Index: {optionIndex}");
                return;
            }
            
            var selectedOption = currentNode.Options[optionIndex];
            Debug.Log(selectedOption.Text);
            selectedOption.Action?.Invoke(); // Выполняем действие, связанное с выбором

            if (selectedOption is DialogueOption dialogueOption)
                currentNode = dialogueOption.NextNode;

            if(currentNode != null) ShowDialogue();
            else dialogPanel.SetActive(false);
        }

        public bool CanCommunicate()
        {
            return currentNode != null;
        }
    }
}
