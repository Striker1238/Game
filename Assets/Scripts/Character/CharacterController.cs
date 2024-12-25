using UnityEngine;
//TODO: увеличивать количество поинтов, когда повышаетс€ уровень
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
    [SerializeField] private int _points = 27;
    public int Points { 
        get => _points;
        set
        {
            _points = value;
            UIManager.Instance.VisualizationCharacteristicsButton(_points > 0);//ѕоказываем кнопки дл€ прокачки статов
        }
    }

    public void HeroLevelUp()
    {
        
        //ѕредложить сразу улучшить характеристики

        PlayerEvents.OnPlayerLevelUp();
    }
}
