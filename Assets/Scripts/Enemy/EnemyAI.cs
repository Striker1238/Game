using Assets.Scripts.Enemy;
using DG.Tweening;
using Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyAI : MonoBehaviour, ICharacterHealthChanged, ICharacterMove, ICharacterAttack
{
    private bool IsAttacking;
  
    private EnemyAnimator Animations;
    private EnemyStats EnemyStatsController;
    public GameObject tetst;
    public void Start()
    {
        Animations = GetComponent<EnemyAnimator>();
        EnemyStatsController = GetComponent<EnemyStats>();

        StartCoroutine(attacker());
    }
    public void Update()
    {
        
    }

    public void DamageReceived(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";
        

        EnemyStatsController.HealthPoint -= damage;
        Animations.Hit();
    }

    public void Died()
    {
        Animations.Died();
        //Создаем объекты, которые будут выпадать с моба 

        StartCoroutine(DropItems());
        EnemyEvents.OnEnemyDied();
    }

    public IEnumerator DropItems()
    {
        for (int index = 0; index < EnemyStatsController.ItemDrop.Count; index++)
        {
            var obj = EnemyStatsController.ItemDrop[index];

            GameObject item = new GameObject(obj.name);
            item.AddComponent<SpriteRenderer>().sprite = obj.Image;
            item.GetComponent<SpriteRenderer>().sortingOrder = 5;
            item.transform.position = transform.position;

            Debug.Log($"{item.name}");


            yield return item.transform
                .DOJump(item.transform.position +Vector3.left*Random.Range(-1.5f,1.5f), 0.1f, 1, 1f)
                .SetEase(Ease.OutBounce)
                .WaitForCompletion();
        }
        yield return null;
    }
    
    public void HealthRecovery(int health)
    {
        EnemyStatsController.HealthPoint += health;
    }

    public void Move()
    {

    }

    public void Attack()
    {
        IsAttacking = true;
        Animations.Attack();

        Collider2D[] Hits = Physics2D.OverlapCircleAll((EnemyStatsController.CharacterSprite.flipX) ? 
            new Vector2(EnemyStatsController.AttackTriggerPosition.position.x - 2, EnemyStatsController.AttackTriggerPosition.position.y) :
            EnemyStatsController.AttackTriggerPosition.position, EnemyStatsController.AttackRange);

        foreach (var Hit in Hits)
        {
            Hit.GetComponent<CharacterMovement>()?.DamageReceived(EnemyStatsController.Damage);
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
