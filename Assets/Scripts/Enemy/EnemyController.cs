using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyController : Stats, ICharacterHealthChanged, ICharacterMove, ICharacterAttack
{
    private bool IsAttacking;
    private SpriteRenderer CharacterSprite;
    public override int HealthPoint 
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            _healthPoint = value;
            if (_healthPoint > MaxHealthPoint) _healthPoint = MaxHealthPoint;
            if (_healthPoint <= 0)
            {
                _healthPoint = 0;
                Died();
            }
        }
    }

    private EnemyAnimator Animations;
    public void Start()
    {
        Animations = GetComponent<EnemyAnimator>();
        CharacterSprite = GetComponent<SpriteRenderer>();

        StartCoroutine(attacker());
    }
    public void Update()
    {
        
    }

    public void DamageReceived(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";
        

        HealthPoint -= damage;
        Animations.Hit();
    }

    public void Died()
    {
        Animations.Died();
        EventBus.OnEnemyDied();
        
    }

    public void HealthRecovery(int health)
    {
        HealthPoint += health;
    }

    public void Move()
    {

    }

    public void Attack()
    {
        IsAttacking = true;
        Animations.Attack();

        Collider2D[] Hits = Physics2D.OverlapCircleAll((CharacterSprite.flipX) ? new Vector2(AttackTriggerPosition.position.x - 2, AttackTriggerPosition.position.y) : AttackTriggerPosition.position, AttackRange);
        foreach (var Hit in Hits)
        {
            Hit.GetComponent<CharacterController>()?.DamageReceived(Damage);
        }
    }

    public IEnumerator attacker()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(2);
        }
    }
}
