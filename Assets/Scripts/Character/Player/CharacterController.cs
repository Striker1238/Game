using DialogueSystem;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(CombatComponent))]
[RequireComponent(typeof(PlayerHealthComponent))]
[RequireComponent(typeof(CollectorComponent))]
[RequireComponent(typeof(CharacterAnimatorController))]
[RequireComponent(typeof(StatsComponent))]
public class CharacterController : MonoBehaviour
{
    private MovementComponent movement;
    private CombatComponent combat;
    private HealthComponentBase health;
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
        health = GetComponent<HealthComponentBase>();
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
        
        animator.SetFlyingState(movement.IsFlying());
        
    }
    private void HandleInput()
    {
        if (combat.IsAttacking)
        {
            movement.DirectionMoving = 0;
            return;
        }


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
        //movement.Move(-1);
        movement.DirectionMoving = -1;
    }
    public void OnButtonRigthDown()
    {
        //movement.Move(1);
        movement.DirectionMoving = 1;
    }
    public void OnClickButtonJump()
    {
        movement.Jump();
    }
    public void OnButtonUp()
    {
        //movement.Move(0);
        movement.DirectionMoving = 0;
    }
    public void OnAttack()
    {
        combat.Attack();
    }
    public void OnCollectItems()
    {
        collector.TryCollectItems();
    }
    public void OnDialogue()
    {
        Collider2D? NPC = Physics2D.OverlapCircleAll(transform.position, 2).FirstOrDefault(obj => obj.CompareTag("NPC"));
        
        if (NPC == null)
        {
            Debug.Log("Я никого не вижу, чтоб с ним поговорить");
            return;
        }
        var dialogueComponent = NPC.GetComponent<DialogueComponent>();
        if(dialogueComponent == null) return;

        if (dialogueComponent.CanCommunicate())
        {
            dialogueComponent.ShowDialogue();
        }
        
    }
}
