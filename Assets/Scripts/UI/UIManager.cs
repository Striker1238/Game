using System.Security.Cryptography.X509Certificates;
using UI.HandBook;
using UI.Inventory;
using UnityEngine;
using UnityEngine.UI;
//TODO: уменьшение статов только когда идет прокачка и выбор куда влить поинт
namespace UI
{
    
    [RequireComponent(typeof(PlayerBookUI))]
    [RequireComponent(typeof(InventoryUI))]
    public class UIManager : MonoBehaviour
    {
        [Header("GameUI")]
        public Image HealthPointBar;
        private InventoryUI inventoryUI;
        private PlayerBookUI playerBookUI;


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
            inventoryUI = GetComponent<InventoryUI>();
            playerBookUI = GetComponent<PlayerBookUI>(); 
        }
        public void Start()
        {
            PlayerEvents.HitHero += ChangeHealthPoint;
        }
        public void ChangeHealthPoint(int HealthPoint, int MaxHealthPoint)
        {
            HealthPointBar.fillAmount = (float)HealthPoint / MaxHealthPoint;
        }
    }
}
