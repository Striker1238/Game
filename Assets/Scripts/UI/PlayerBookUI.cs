using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace UI.HandBook
{
    [RequireComponent(typeof(UIAnimationHandler))]
    // UI книги игрока с различными характеристиками игрока
    public class PlayerBookUI: MonoBehaviour
    {
        [Header("PlayerBook")]
        public GameObject statsBook;
        public TextMeshProUGUI point_TMP;
        public TextMeshProUGUI level_TMP;
        public List<StatTextBlock> statTMPBlock;
        private UIAnimationHandler animationHandler;

        public void Awake()
        {
            animationHandler = GetComponent<UIAnimationHandler>();
        }

        /// <summary>
        /// Обновляет текст в книге игрока
        /// </summary>
        protected internal void UpdateTextBlocks()
        {

            var player = CharacterController.Instance.stats.AttributeHolder;

            level_TMP.text = $"Level: {player.Level}";
            point_TMP.text = $"Points: {CharacterController.Instance.stats.Points}";

            statTMPBlock.ForEach(block => block.UpdateText(player));
        }

        /// <summary>
        /// Sets the visualization of buttons based on the input value
        /// </summary>
        /// <param name="visible">Input value</param>
        protected internal void VisualizationCharacteristicsButton(bool visible)
        {
            foreach (var StatButtons in statTMPBlock)
            {
                StatButtons.UpButton.SetActive(visible);
                StatButtons.DownButton.SetActive(visible);
            }
        }

        /// <summary>
        /// Метод открытия книги игрока
        /// </summary>
        public void PlayerBookOpen()
        {
            animationHandler.TogglePanel(statsBook);
            if (statsBook.activeSelf) UpdateTextBlocks();
        }
    }
}
