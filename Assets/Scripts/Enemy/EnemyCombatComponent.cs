using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyCombatComponent: MonoBehaviour, ICombat
{
    [Header("Attack")]
    public int Damage { get; private set; }
    public float AttackRange { get; private set; }
    public bool IsAttacking { get; private set; }
    public Transform AttackTriggerPosition;

    public void Attack()
    {
        if (IsAttacking) return;

        IsAttacking = true;
        CharacterAnimatorController.Instance.TriggerAttack();

        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll((GetComponent<SpriteRenderer>().flipX) ?
            new Vector2(AttackTriggerPosition.position.x - 2, AttackTriggerPosition.position.y) :
            AttackTriggerPosition.position, AttackRange);

        foreach (var Enemies in HitEnemies)
        {
            if (Enemies.tag == "Enemy") continue;
            Enemies.GetComponent<HealthComponent>()?.DecreaseHealth(Damage);
        }

    }

    public void EndAttack()
    {
        IsAttacking = false;
    }
}
