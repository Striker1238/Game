using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("MenuUI")]
    public GameObject Background;
    public float BackgroundSpeed;
    public Vector2 MaxPosition;

    [Header("GameUI")]
    public Image HealthPointBar;
    public GameObject SlotPrefab;
    public GameObject InventoryPerent;
    public GameObject StatsBook;

    [Header("PlayerBook")]
    public TextMeshProUGUI Point_TMP;
    public TextMeshProUGUI Level_TMP;


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
    }

    public void Start()
    {
        EventBus.HitHero += ChangeHealthPoint;
        
    }

    public void ChangeHealthPoint(int HealthPoint, int MaxHealthPoint)
    {
        HealthPointBar.fillAmount = (float)HealthPoint / MaxHealthPoint;
    }

    /// <summary>
    /// Метод открытия инвенторя
    /// </summary>
    public void InventoryOpen()
    {
        InventoryPerent.SetActive(!InventoryPerent.activeSelf);
        EventBus.OnUpdateSlots();
    }

    /// <summary>
    /// Метод открытия книги игрока
    /// </summary>
    public void PlayerBookOpen()
    {
        StatsBook.SetActive(!StatsBook.activeSelf);
        //EventBus.OnUpdateStatsData();
    }

    private void LevelTMPInBook(int level) => Level_TMP.text = $"Level: {level}";
    private void PointTMPInBook(int point) => Level_TMP.text = $"Point: {point}";


    public void Update()
    {
        //Смотрим позицию мышки относительно экрана
        Vector2 MousePosition = Input.mousePosition;
        //if(Background  is not null)
        //Background.transform.position = new Vector3((MousePosition.x - 960) / 1000, 0.8f + ((MousePosition.y - 540)/1000), 0);
    }
    public IEnumerator MoveBackground()
    {
        Vector3 LastPosition = Background.transform.position;

        if (LastPosition.x >= MaxPosition.x || LastPosition.x <= MaxPosition.y)
        {
            BackgroundSpeed *= -1;
        }

        Background.transform.position = new Vector3(
            LastPosition.x + BackgroundSpeed * Time.deltaTime,
            LastPosition.y,
            LastPosition.z);
        yield return new WaitForSeconds(1);
    }
}
