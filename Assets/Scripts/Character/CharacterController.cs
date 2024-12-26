using UnityEngine;
//TODO: увеличивать количество поинтов, когда повышается уровень
public class CharacterController : Stats
{
    private static CharacterController _instance;
    public static CharacterController Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<CharacterController>();
                if (_instance is null)
                {
                    GameObject singleton = new GameObject(typeof(CharacterController).ToString());
                    _instance = singleton.AddComponent<CharacterController>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    [Header("Other Stats")]
    public override int HealthPoint
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            _healthPoint = Mathf.Clamp(value, 0, MaxHealthPoint);

            if (_healthPoint <= 0) GetComponent<CharacterMovement>().Died();
        }
    }
    [SerializeField] private protected int _points = 20;
    protected internal int Points { 
        get => _points;
        set
        {
            _points = value;
            UIManager.Instance.VisualizationCharacteristicsButton(_points > 0);//Показываем кнопки для прокачки статов
        }
    }

    public void Start()
    {
        UIEvents.UpCharacteristic += IncreaseCharacteristics;
    }
    private void HeroLevelUp()
    {
        
        //Предложить сразу улучшить характеристики

        PlayerEvents.OnPlayerLevelUp();
    }
    private void IncreaseCharacteristics(string NameCharacteristic)
    {
        if (Points <= 0) return;
        Debug.Log(NameCharacteristic);
        
        switch (NameCharacteristic.ToLower())
        {
            case "strength":
                Strength++;
                break;
            case "agility":
                Agility++;
                break;
            case "constitution":
                Constitution++;
                break;
            case "intelligence":
                Intelligence++;
                break;
            case "wisdom":
                Wisdom++;
                break;
            case "charisma":
                Charisma++;
                break;
            default:
                Debug.Log($"Неизвестная характеристика: {NameCharacteristic}");
                return;
        }
        Points--;

        //Обновляем визуализацию текста в книге
        UIEvents.OnUpdateStatsData();
    }
    private void DecreaseCharacteristics()
    {

    }
}
