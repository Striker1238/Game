using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    /// <summary>
    /// Вызывается при смерти какого-либо врага
    /// </summary>
    public static event Action EnemyDied;

    /// <summary>
    /// Вызывается при смерти игрока
    /// </summary>
    public static event Action HeroDied;

    /// <summary>
    /// Вызывается при нанесении урона игроку
    /// </summary>
    public static event Action<int,int> HitHero;//Нужен для удобного контроля всех событий, которые должны произойти при получении урона игроком

    /// <summary>
    /// Вызывается при запуске игры
    /// </summary>
    public static event Action StartGame;

    /// <summary>
    /// Вызывается при обновлении данных слотов инвентаря
    /// </summary>
    public static event Action UpdateSlotsData;

    /// <summary>
    /// Вызывается при получении нового уровня игроком
    /// </summary>
    public static event Action PlayerLevelUp;

    /// <summary>
    /// Вызывается при обновлении характеристик игрока
    /// </summary>
    public static event Action UpdateStatsData;






    /// <summary>
    /// Вызывает события, подписанные на смерть врага
    /// </summary>
    public static void OnEnemyDied() => EnemyDied?.Invoke();

    /// <summary>
    /// Вызывает события, подписанные на смерть игрока
    /// </summary>
    public static void OnHeroDied() => HeroDied?.Invoke();
    
    /// <summary>
    /// Вызывает события, подписанные изменение хп игрока(получение урона)
    /// </summary>
    public static void OnHitHero(int HealthPoint, int MaxHealthPoint) => HitHero?.Invoke(HealthPoint, MaxHealthPoint);

    /// <summary>
    /// Вызывает события, подписанные на старт игры
    /// </summary>
    public static void OnStartGame() => StartGame?.Invoke();

    /// <summary>
    /// Вызывает события, подписанные на обновление данных слотов
    /// </summary>
    public static void OnUpdateSlots() => UpdateSlotsData?.Invoke();

    /// <summary>
    /// Вызывает события, подписанные на получении уровня игрока
    /// </summary>
    public static void OnPlayerLevelUp() => PlayerLevelUp?.Invoke();

    /// <summary>
    /// Вызывает события, подписанные на обновление характеристик игрока
    /// </summary>
    public static void OnUpdateStatsData() => UpdateStatsData.Invoke();

}
