using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterHealthChanged
{
    void HealthRecovery(int a);
    void DamageReceived(int d);
    void Died();
}
