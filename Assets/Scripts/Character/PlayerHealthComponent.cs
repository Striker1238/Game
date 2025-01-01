using TMPro;
using UnityEngine;

public abstract class PlayerHealthComponent : HealthComponentBase
{
    public override void Died()
    {
        HealthPoint = 0;
        GetComponent<CharacterAnimatorController>().TriggerDied();
        PlayerEvents.OnHeroDied();
    }
}
