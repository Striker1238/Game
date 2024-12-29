using Enemy;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyCombatComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(EnemyAnimatorComponent))]
[RequireComponent(typeof(DropItemController))]


public class EnemyAI : MonoBehaviour
{
    //private MovementComponent movement;
    private EnemyCombatComponent combat;
    private HealthComponent health;
    private EnemyAnimatorComponent animator;
    private StatsComponent stats;
    private DropItemController dropItemController;

    public void Start()
    {
        //movement = GetComponent<MovementComponent>();
        combat = GetComponent<EnemyCombatComponent>();
        health = GetComponent<HealthComponent>();
        animator = GetComponent<EnemyAnimatorComponent>();
        stats = GetComponent<StatsComponent>();
        dropItemController = GetComponent<DropItemController>();

        StartCoroutine(attacker());
    }

    public void Update()
    {

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
    }
}
