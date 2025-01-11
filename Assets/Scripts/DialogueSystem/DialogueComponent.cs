using NUnit.Framework;
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
            currentNode = new DialogueNode("Добро пожаловать, путник. Хочешь узнать об этих землях?")
            {
                Options = new List<IOption>(3)
                {
                    new DialogueOption("Да, расскажи мне о них.", new DialogueNode("Эти земли некогда были частью великого королевства Альтариона. Но после Великого Раскола, мир изменился навсегда.")
                    {
                        Options = new List<IOption>(2)
                        {
                            new DialogueOption("Что за Великий Раскол?", new DialogueNode("Около сотни лет назад маги и короли разорвали мир на части в битве за древнюю силу. Магическая буря уничтожила города и создала эти пустоши.")
                            {
                                Options = new List<IOption>(2)
                                {
                                    new DialogueOption("И что случилось с магами?", new DialogueNode("Маги... они исчезли. Кто-то говорит, что они скрылись в тенях, другие — что они заплатили своими жизнями за свое безумие. Но следы их магии до сих пор отравляют эти земли.")
                                    {
                                        Options = new List<IOption>(2)
                                        {
                                            new DialogueOption("Это звучит жутко. Спасибо за рассказ."),
                                            new DialogueOption("Что за сила была так важна?")
                                        }
                                    }),
                                    new DialogueOption("Похоже, это непростое место. Спасибо за информацию.")
                                }
                            }),
                            new DialogueOption("Пустоши — это опасно? Расскажи больше.")
                        }
                    }),
                    new DialogueOption("Мне неинтересно. Что ты продаешь?", new DialogueNode("У меня самые разные товары, выбирай любые, но с умом!")
                    {
                        Options = new List<IOption>(2)
                        {
                            new TradeOption("Посмотреть товары"),
                            new DialogueOption("Мне пора идти. До встречи.")
                        } 
                    }),
                    
                    new DialogueOption("Мне пора идти. До встречи.")
                }
            };


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
            dialogPanel.SetActive(true);

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

            if(CanCommunicate()) ShowDialogue();
            else dialogPanel.SetActive(false);
        }

        /// <summary>
        /// Checks if we can talk to NPC
        /// Is currentNode != null
        /// </summary>
        /// <returns></returns>
        public bool CanCommunicate()
        {
            return currentNode != null;
        }
    }
}
