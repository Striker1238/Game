public class EnemyHealthComponent : HealthComponentBase
{
    public override void Died()
    {
        HealthPoint = 0;
        GetComponent<EnemyAI>().Died();
    }
}