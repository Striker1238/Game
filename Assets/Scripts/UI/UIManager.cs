using DG.Tweening;
using Inventory;
using Mono.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//TODO: уменьшение статов только когда идет прокачка и выбор куда влить поинт
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
    private Vector3 StartPositionInventory;
    private Vector3 EndPositionInventory;
    private Sequence InventoryAnimation;

    public GameObject StatsBook;
    private Vector3 StartPositionStatsBook;
    private Vector3 EndPositionStatsBook;
    private Sequence StatsBookAnimation;

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

        
        
    }

    public void Start()
    {
        PlayerEvents.HitHero += ChangeHealthPoint;
        UIEvents.UpdateStatsData += UpdateTextBlocks;

        EndPositionInventory = InventoryPerent.GetComponent<RectTransform>().anchoredPosition;
        StartPositionInventory = new Vector3(EndPositionInventory.x - 300, EndPositionInventory.y, EndPositionInventory.z);


        EndPositionStatsBook = StatsBook.GetComponent<RectTransform>().anchoredPosition;
        StartPositionStatsBook = new Vector3(EndPositionStatsBook.x - 200, EndPositionStatsBook.y,EndPositionStatsBook.z);
    }

    public void ChangeHealthPoint(int HealthPoint, int MaxHealthPoint)
    {
        HealthPointBar.fillAmount = (float)HealthPoint / MaxHealthPoint;
    }

    /// <summary>
    /// Метод открытия инвентаря
    /// </summary>
    public void InventoryOpen()
    {
        bool State = InventoryPerent.activeSelf;
        if (InventoryAnimation != null) InventoryAnimation.Complete();
        InventoryAnimation = DOTween.Sequence();
        InventoryAnimation
            .OnStart(() => InventoryPerent.SetActive(true))
            .Append(InventoryPerent.GetComponent<CanvasGroup>().DOFade((!State ? 1 : 0), 0.7f))
            .Join(InventoryPerent.GetComponent<RectTransform>().DOAnchorPos((State ? StartPositionInventory : EndPositionInventory), 0.7f).From(State ? EndPositionInventory : StartPositionInventory))
            .OnComplete(() =>
            {
                if (InventoryPerent.GetComponent<CanvasGroup>().alpha == 0) InventoryPerent.SetActive(false);
            });

        if (StatsBook.activeSelf) UIEvents.OnUpdateSlots();
    }

    /// <summary>
    /// Метод открытия книги игрока
    /// </summary>
    public void PlayerBookOpen()
    {
        bool State = StatsBook.activeSelf;
        if (StatsBookAnimation != null) StatsBookAnimation.Complete();
        StatsBookAnimation = DOTween.Sequence();
        StatsBookAnimation
            .OnStart(() => StatsBook.SetActive(true))
            .Append(StatsBook.GetComponent<CanvasGroup>().DOFade((!State ? 1 : 0), 0.7f))
            .Join(StatsBook.GetComponent<RectTransform>().DOAnchorPos((State ? StartPositionStatsBook : EndPositionStatsBook), 0.7f).From(State ? EndPositionStatsBook : StartPositionStatsBook))
            .OnComplete(() =>
            {
                if (StatsBook.GetComponent<CanvasGroup>().alpha == 0) StatsBook.SetActive(false);
            });


        if (StatsBook.activeSelf) UIEvents.OnUpdateStatsData();
    }

    //TODO: ВОЗМОЖНО ПЕРЕНЕСТИ В GameManager
    // Использую switch потому что характеристики не будут дополняться в большом количестве, а добавить 2-3 будет не сложно.
    // Не самый практичный способ как по мне, но лучше к сожалению пока не придумал, если появится идея, то изменю.
    /// <summary>
    /// Обновляет текст в книге игрока
    /// </summary>
    private void UpdateTextBlocks()
    {
        var player = CharacterController.Instance;

        Level_TMP.text =  $"Level: {player.Level}";
        Point_TMP.text = $"Points: {player.Points}";

        foreach (var statBlock in StatTMPBlock)
        {
            switch (statBlock.TextValue.ToLower())
            {
                case "strength":
                    statBlock.PointText.text = player.Strength.ToString();
                    statBlock.ModificationText.text = player.StrengthModifier.ToString();
                    break;
                case "agility":
                    statBlock.PointText.text = player.Agility.ToString();
                    statBlock.ModificationText.text = player.AgilityModifier.ToString();
                    break;
                case "constitution":
                    statBlock.PointText.text = player.Constitution.ToString();
                    statBlock.ModificationText.text = player.ConstitutionModifier.ToString();
                    break;
                case "intelligence":
                    statBlock.PointText.text = player.Intelligence.ToString();
                    statBlock.ModificationText.text = player.IntelligenceModifier.ToString();
                    break;
                case "wisdom":
                    statBlock.PointText.text = player.Wisdom.ToString();
                    statBlock.ModificationText.text = player.WisdomModifier.ToString();
                    break;
                case "charisma":
                    statBlock.PointText.text = player.Charisma.ToString();
                    statBlock.ModificationText.text = player.CharismaModifier.ToString();
                    break;
                default:
                    Debug.Log($"Неизвестная характеристика: {statBlock.TextValue}");
                    break;
            }
        }
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
