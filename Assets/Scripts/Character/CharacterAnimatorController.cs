using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    private static CharacterAnimatorController _instance;
    public static CharacterAnimatorController Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<CharacterAnimatorController>();
                if (_instance is null)
                {
                    GameObject singleton = new GameObject(typeof(CharacterAnimatorController).ToString());
                    _instance = singleton.AddComponent<CharacterAnimatorController>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetMovementState(bool isMoving)
    {
        animator?.SetBool("IsMoving", isMoving);
    }

    public void SetFlyingState(bool isFlying)
    {
        animator?.SetBool("IsFlying", isFlying);
    }

    public void TriggerJump()
    {
        animator?.SetTrigger("Jump");
    }

    public void TriggerAttack()
    {
        animator?.SetTrigger("Attack");
    }

    public void TriggerDied()
    {
        animator?.SetTrigger("Died");
    }
}
