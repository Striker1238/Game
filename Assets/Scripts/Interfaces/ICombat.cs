using UnityEngine;

public interface ICombat
{
    int Damage { get; }
    float AttackRange { get; }
    bool IsAttacking { get; }
    void Attack();
    void EndAttack();
}
