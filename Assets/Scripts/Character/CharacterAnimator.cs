using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator AnimatorController;

    public bool IsMoving { private get; set; }
    public bool IsFlying { private get; set; }

    public void Start()
    {
        AnimatorController = GetComponent<Animator>();
        PlayerEvents.HeroDied += Died;
    }

    public void Update()
    {
        AnimatorController?.SetBool("IsMoving", IsMoving);
        AnimatorController?.SetBool("IsFlying", IsFlying);
    }

    public void Jump()
    {
        AnimatorController.SetTrigger("Jump");
    }
    public void Attack()
    {
        AnimatorController.SetTrigger("Attack");
    }
    public void Died()
    {
        AnimatorController.SetTrigger("Died");
    }
}
