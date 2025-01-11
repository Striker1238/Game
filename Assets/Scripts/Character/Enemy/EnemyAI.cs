using Enemy;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyCombatComponent))]
[RequireComponent(typeof(EnemyHealthComponent))]
[RequireComponent(typeof(EnemyAnimatorComponent))]
[RequireComponent(typeof(DropItemController))]


public class EnemyAI : MonoBehaviour
{
    //private MovementComponent movement;
    private EnemyCombatComponent combat;
    private HealthComponentBase health;
    private EnemyAnimatorComponent animator;
    private EnemyStats stats;
    private DropItemController dropItemController;

    public void Start()
    {
        //movement = GetComponent<MovementComponent>();
        combat = GetComponent<EnemyCombatComponent>();
        health = GetComponent<HealthComponentBase>();
        animator = GetComponent<EnemyAnimatorComponent>();
        stats = GetComponent<EnemyStats>();
        dropItemController = GetComponent<DropItemController>();

        StartCoroutine(attacker());
    }
    public IEnumerator attacker()
    {
        while (true)
        {
            combat.Attack();
            yield return new WaitForSeconds(2);
        }
    }
    public void Died()
    {
        //Временное решение, голова уже не шибко варит, а протестить хочется
        StartCoroutine(dropItemController.DropItems());
        GetComponent<EnemyAnimatorComponent>().TriggerDied();
    }
}
