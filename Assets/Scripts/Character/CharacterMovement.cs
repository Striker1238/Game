
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour, ICharacterHealthChanged, ICharacterMove, ICharacterAttack
{
    [Header("")]

    [Header("Move Stats")]
    [SerializeField] private float normalSpeed = 6;
    private float Speed = 0;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private Vector3 GroundCheckOffset;


    private Vector3 Force;
    private bool IsMoving = false;
    private bool IsGrounded = true;
    private bool IsAttacking = false;

   


    private Rigidbody2D rb;
    private SpriteRenderer CharacterSprite;
    private CharacterAnimator Animations;
    private CharacterController Controller;
    

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CharacterSprite = GetComponent<SpriteRenderer>();
        Animations = GetComponent<CharacterAnimator>();
        Controller = GetComponent<CharacterController>();
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

            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll((CharacterSprite.flipX) ?
                new Vector2(Controller.AttackTriggerPosition.position.x - 2, Controller.AttackTriggerPosition.position.y) :
                Controller.AttackTriggerPosition.position, Controller.AttackRange);

            foreach (var Enemies in HitEnemies)
            {
                Enemies.GetComponent<EnemyAI>()?.DamageReceived(Controller.Damage);
            }
        }
    }
    public void OnEndAttack()
    {
        IsAttacking = false;
    }

    public void HealthRecovery(int health)
    {
        Controller.HealthPoint += health;
    }
    public void DamageReceived(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";
        Controller.HealthPoint -= damage;
        PlayerEvents.OnHitHero(Controller.HealthPoint, Controller.MaxHealthPoint);
    }
    public void Died()
    {
        Speed = 0f;
        PlayerEvents.OnHeroDied();
    }
}
