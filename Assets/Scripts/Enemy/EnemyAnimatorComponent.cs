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

    private Animator AnimatorController;

    public bool IsMoving { private get; set; }
    public void Start()
    {
        AnimatorController = GetComponent<Animator>();
    }
    public void Update()
    {
        AnimatorController?.SetBool(NameMovingAnimation, IsMoving);
    }

    public void Attack()
    {
        AnimatorController.SetTrigger(NameAttackAnimation);
    }
    public void Died()
    {
        AnimatorController.SetTrigger(NameDiedAnimation);
    }
    public void Hit()
    {
        AnimatorController.SetTrigger(NameHitAnimation);
    }
}
