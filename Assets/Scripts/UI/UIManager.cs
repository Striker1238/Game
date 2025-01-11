using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//TODO: ���������� ������ ������ ����� ���� �������� � ����� ���� ����� �����
namespace UI
{
    [RequireComponent(typeof(UIAnimationHandler))]
    public class UIManager : MonoBehaviour
    {
        

        [Header("GameUI")]
        public Image HealthPointBar;
        public GameObject SlotPrefab;
        public GameObject InventoryPerent;
        public GameObject StatsBook;
        private UIAnimationHandler animationHandler;

        [Header("PlayerBook")]
        public TextMeshProUGUI Point_TMP;
        public TextMeshProUGUI Level_TMP;
        public List<StatTextBlock> StatTMPBlock;


        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<UIManager>();
                    if (_instance is null)
                    {
                        GameObject singleton = new GameObject(typeof(UIManager).ToString());
                        _instance = singleton.AddComponent<UIManager>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }

        public void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            animationHandler = GetComponent<UIAnimationHandler>();
        }

        public void Start()
        {
            PlayerEvents.HitHero += ChangeHealthPoint;
            UIEvents.UpdateStatsData += UpdateTextBlocks;
        }
        public void ChangeHealthPoint(int HealthPoint, int MaxHealthPoint)
        {
            HealthPointBar.fillAmount = (float)HealthPoint / MaxHealthPoint;
        }
        /// <summary>
        /// ����� �������� ���������
        /// </summary>
        public void InventoryOpen()
        {
            animationHandler.TogglePanel(InventoryPerent);
            if (!InventoryPerent.activeSelf) UIEvents.OnUpdateSlots();
        }
        /// <summary>
        /// ����� �������� ����� ������
        /// </summary>
        public void PlayerBookOpen()
        {
            animationHandler.TogglePanel(StatsBook);
            if (StatsBook.activeSelf) UIEvents.OnUpdateStatsData();
        }
        //TODO: �������� ��������� � GameManager
        // ��������� switch ������ ��� �������������� �� ����� ����������� � ������� ����������, � �������� 2-3 ����� �� ������.
        // �� ����� ���������� ������ ��� �� ���, �� ����� � ��������� ���� �� ��������, ���� �������� ����, �� ������.
        /// <summary>
        /// ��������� ����� � ����� ������
        /// </summary>
        private void UpdateTextBlocks()
        {
            var player = CharacterController.Instance.stats.AttributeHolder;

            Level_TMP.text = $"Level: {player.Level}";
            //Point_TMP.text = $"Points: {player.Points}";

            StatTMPBlock.ForEach(block => block.UpdateText(player));
        }
        /// <summary>
        /// Sets the visualization of buttons based on the input value
        /// </summary>
        /// <param name="visible">Input value</param>
        public void VisualizationCharacteristicsButton(bool visible)
        {
            foreach (var StatButtons in StatTMPBlock)
            {
                StatButtons.UpButton.SetActive(visible);
                StatButtons.DownButton.SetActive(visible);
            }
        }
        public void UpCharacteristics(string NameCharacteristic)
        {
            Debug.Log("Click");
            UIEvents.OnUpCharacteristic(NameCharacteristic);
        }
    }

}
