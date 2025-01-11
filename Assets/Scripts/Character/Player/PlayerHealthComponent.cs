using TMPro;
using UnityEngine;

public class PlayerHealthComponent : HealthComponentBase
{
    public override void Died()
    {
        HealthPoint = 0;
        GetComponent<CharacterAnimatorController>().TriggerDied();
        PlayerEvents.OnHeroDied();
    }
}
