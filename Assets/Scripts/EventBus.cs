using System;

// �������, ��������� � ������� ���������
public static class GameEvents
{
    
    public static event Action StartGame;
    

    public static void OnStartGame() => StartGame?.Invoke();
    
}
// �������, ��������� � �������
public static class PlayerEvents
{
    
    public static event Action<int, int> HitHero;
    public static event Action HeroDied;
    public static event Action PlayerLevelUp;

    //����� ����� �������� ����
    public static void OnHitHero(int HealthPoint, int MaxHealthPoint) => HitHero?.Invoke(HealthPoint, MaxHealthPoint);
    //����� ����� �������
    public static void OnHeroDied() => HeroDied?.Invoke();
    //����� ����� �������� ����� �������
    public static void OnPlayerLevelUp() => PlayerLevelUp?.Invoke();
}

// �������, ��������� � �������
public static class EnemyEvents
{
    public static event Action EnemyDied;

    //����� ����� ���� ���� �������
    public static void OnEnemyDied() => EnemyDied?.Invoke();
}

// �������, ��������� � ����������
public static class UIEvents
{
    public static event Action UpdateSlotsData;
    public static event Action UpdateStatsData;
    //public static event Action ;

    //����� ����������� ����� ���������
    public static void OnUpdateSlots() => UpdateSlotsData?.Invoke();
    //����� ����������� �������������� ������ � �����
    public static void OnUpdateStatsData() => UpdateStatsData?.Invoke();
}
