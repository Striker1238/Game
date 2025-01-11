using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorComponent : MonoBehaviour
{
    [SerializeField]private string NameMovingAnimation;
    [SerializeField]private string NameAttackAnimation;
    [SerializeField]private string NameDiedAnimation;
    [SerializeField]private string NameHitAnimation;

    private Animator animator;

    public bool IsMoving { private get; set; }
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        animator?.SetBool(NameMovingAnimation, IsMoving);
    }
    public void TriggerJump()
    {
        animator?.SetTrigger("Jump");
    }
    public void TriggerAttack()
    {
        animator?.SetTrigger(NameAttackAnimation);
    }
    public void TriggerHit()
    {
        animator.SetTrigger(NameHitAnimation);
    }
    public void TriggerDied()
    {
        animator?.SetTrigger(NameDiedAnimation);
    }    
}
