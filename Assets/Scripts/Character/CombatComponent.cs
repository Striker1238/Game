using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CombatComponent : MonoBehaviour, ICombat
{
    [Header("Attack")]
    [SerializeField] private int damage;
    public int Damage { get => damage; private set => damage = value; }
    [SerializeField] private float attackRange;
    public float AttackRange { get => attackRange; private set => attackRange = value; }
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
            if (Enemies.tag == "Player") continue;
            Enemies.GetComponent<HealthComponent>()?.DecreaseHealth(Damage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (AttackTriggerPosition == null) return;

        // Установка цвета для Gizmos
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((GetComponent<SpriteRenderer>().flipX) ?
            new Vector2(AttackTriggerPosition.position.x - 2, AttackTriggerPosition.position.y) :
            AttackTriggerPosition.position, AttackRange);
    }
    public void EndAttack()
    {
        IsAttacking = false;
    }
}