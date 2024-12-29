using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(CombatComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(CollectorComponent))]
[RequireComponent(typeof(CharacterAnimatorController))]
[RequireComponent(typeof(StatsComponent))]
public class CharacterController : MonoBehaviour
{
    private MovementComponent movement;
    private CombatComponent combat;
    private HealthComponent health;
    private CollectorComponent collector;
    private CharacterAnimatorController animator;
    protected internal StatsComponent stats = new();

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


    private void Awake()
    {
        movement = GetComponent<MovementComponent>();
        combat = GetComponent<CombatComponent>();
        health = GetComponent<HealthComponent>();
        collector = GetComponent<CollectorComponent>();
        animator = GetComponent<CharacterAnimatorController>();
        stats = GetComponent<StatsComponent>();
    }
    public void Start()
    {
        //UIEvents.UpCharacteristic += IncreaseCharacteristics;
    }
    private void Update()
    {
        HandleInput();
        movement.GroundCheck();
        animator.SetFlyingState(movement.IsFlying());
        
    }

    private void HandleInput()
    {
        if (combat.IsAttacking) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClickButtonJump();
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnButtonLeftDown();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnButtonRigthDown();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            OnButtonUp();
        }
    }
    public void OnButtonLeftDown()
    {
        movement.Move(-1);
    }
    public void OnButtonRigthDown()
    {
        movement.Move(1);
    }
    public void OnClickButtonJump()
    {
        movement.Jump();
    }
    public void OnButtonUp()
    {
        movement.Move(0);
        
    }
    public void OnAttack()
    {
        combat.Attack();
    }
}


/*
public class asd : Stats
{

    [Header("Other Stats")]
    
    [SerializeField] private protected int _points = 20;
    protected internal int Points { 
        get => _points;
        set
        {
            _points = value;
            UIManager.Instance.VisualizationCharacteristicsButton(_points > 0);//Показываем кнопки для прокачки статов
        }
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
*/