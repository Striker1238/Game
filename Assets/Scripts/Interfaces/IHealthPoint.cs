public interface IHealthPoint
{
    int MaxHealthPoint { get; }
    int HealthPoint { get; }
    void RestoringHealth(int healthPoint);
    void DecreaseHealth(int damage);
}
