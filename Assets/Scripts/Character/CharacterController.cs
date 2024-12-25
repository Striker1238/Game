
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class CharacterController : Stats, ICharacterHealthChanged, ICharacterMove, ICharacterAttack
{
    public override int HealthPoint
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            _healthPoint = Mathf.Clamp(value, 0, MaxHealthPoint);

            if (_healthPoint <= 0) Died();
        }
    }
    [SerializeField]private int _points;
    public int Points { get => _points; set => _points = value; }
    public override int Level
    {
        set 
        {
            base.Level = value;
           // UIManager.Instance
        }
    }



    [Header("Move Stats")]
    [SerializeField] private float normalSpeed = 6;
    private float Speed = 0;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private Vector3 GroundCheckOffset;

    private Vector3 Force;
    [SerializeField]
    private bool IsMoving = false;
    [SerializeField]
    private bool IsGrounded = true;
    [SerializeField]
    private bool IsAttacking = false;

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


    private Rigidbody2D rb;
    private SpriteRenderer CharacterSprite;
    private CharacterAnimator Animations;
    

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CharacterSprite = GetComponent<SpriteRenderer>();
        Animations = GetComponent<CharacterAnimator>();
    }

    public void Update()
    {
        if (!IsAttacking)
        {
            Move();
        }

        GroundCheck();
        //Для проверок в editor
        if (Input.GetKeyDown(KeyCode.Space))
        {
           OnClickButtonJump();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnButtonLeftDown();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnButtonRigthDown();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            OnButtonUp();
        }

        Animations.IsMoving = IsMoving;
        Animations.IsFlying = IsFlying();
    }
    private bool IsFlying()
    {
        if (rb.linearVelocity.y < 0) return true;
        else return false;
    }
    private void GroundCheck()
    {
        float rayLength = 0.3f;
        Vector3 rayStartPosition = transform.position + GroundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, rayStartPosition + Vector3.down,rayLength);

        IsGrounded = hit.collider != null && hit.collider.CompareTag("Ground");
    }


    public void Move()
    {
        //rb?.AddForce(Force * Speed);
        transform.position += new Vector3(Speed * Time.deltaTime,0,0);
        IsMoving = (Mathf.Abs(Speed) > 0) ? true : false;

        if (IsMoving && CharacterSprite != null) CharacterSprite.flipX = (Speed > 0) ? false : true;

    }
    public void Jump()
    {
        if (IsGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            Animations.Jump();
        }
    }
    public void OnButtonLeftDown() => Speed = -normalSpeed;
    public void OnButtonRigthDown() => Speed = normalSpeed;
    public void OnClickButtonJump() => Jump();
    public void OnButtonUp() => Speed = 0f;

    public void Attack()
    {
        if (IsGrounded && !IsAttacking)
        {
            IsAttacking = true;
            Animations.Attack();

            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll((CharacterSprite.flipX) ? new Vector2(AttackTriggerPosition.position.x - 2, AttackTriggerPosition.position.y) : AttackTriggerPosition.position, AttackRange);

            foreach (var Enemies in HitEnemies)
            {
                Enemies.GetComponent<EnemyController>()?.DamageReceived(Damage);
            }
        }
    }
    public void OnEndAttack()
    {
        IsAttacking = false;
    }

    public void HealthRecovery(int health)
    {
        HealthPoint += health;
    }
    public void DamageReceived(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";

        HealthPoint -= damage;
        EventBus.OnHitHero(HealthPoint, MaxHealthPoint);
    }
    public void Died()
    {
        Speed = 0f;
        EventBus.OnHeroDied();
    }
}
