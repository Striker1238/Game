using System;

// События, связанные с игровым процессом
public static class GameEvents
{
    
    public static event Action StartGame;
    

    public static void OnStartGame() => StartGame?.Invoke();
    
}
// События, связанные с игроком
public static class PlayerEvents
{
    
    public static event Action<int, int> HitHero;
    public static event Action HeroDied;
    public static event Action PlayerLevelUp;

    //Когда игрок получает урон
    public static void OnHitHero(int HealthPoint, int MaxHealthPoint) => HitHero?.Invoke(HealthPoint, MaxHealthPoint);
    //Когда игрок умирает
    public static void OnHeroDied() => HeroDied?.Invoke();
    //Когда игрок получает новый уровень
    public static void OnPlayerLevelUp() => PlayerLevelUp?.Invoke();
}

// События, связанные с Врагами
public static class EnemyEvents
{
    public static event Action EnemyDied;

    //Когда какой либо враг умирает
    public static void OnEnemyDied() => EnemyDied?.Invoke();
}

// События, связанные с инвентарем
public static class UIEvents
{
    public static event Action UpdateSlotsData;
    public static event Action UpdateStatsData;
    //public static event Action ;

    //Когда обновляются слоты инвенторя
    public static void OnUpdateSlots() => UpdateSlotsData?.Invoke();
    //Когда обновляются характеристики игрока в книге
    public static void OnUpdateStatsData() => UpdateStatsData?.Invoke();
}
