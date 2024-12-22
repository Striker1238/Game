using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    /// <summary>
    /// ���������� ��� ������ ������-���� �����
    /// </summary>
    public static event Action EnemyDied;

    /// <summary>
    /// ���������� ��� ������ ������
    /// </summary>
    public static event Action HeroDied;

    /// <summary>
    /// ���������� ��� ��������� ����� ������
    /// </summary>
    public static event Action<int,int> HitHero;//����� ��� �������� �������� ���� �������, ������� ������ ��������� ��� ��������� ����� �������

    /// <summary>
    /// ���������� ��� ������� ����
    /// </summary>
    public static event Action StartGame;

    /// <summary>
    /// ���������� ��� ���������� ������ ������ ���������
    /// </summary>
    public static event Action UpdateSlotsData;

    /// <summary>
    /// ���������� ��� ��������� ������ ������ �������
    /// </summary>
    public static event Action PlayerLevelUp;

    /// <summary>
    /// ���������� ��� ���������� ������������� ������
    /// </summary>
    public static event Action UpdateStatsData;






    /// <summary>
    /// �������� �������, ����������� �� ������ �����
    /// </summary>
    public static void OnEnemyDied() => EnemyDied?.Invoke();

    /// <summary>
    /// �������� �������, ����������� �� ������ ������
    /// </summary>
    public static void OnHeroDied() => HeroDied?.Invoke();
    
    /// <summary>
    /// �������� �������, ����������� ��������� �� ������(��������� �����)
    /// </summary>
    public static void OnHitHero(int HealthPoint, int MaxHealthPoint) => HitHero?.Invoke(HealthPoint, MaxHealthPoint);

    /// <summary>
    /// �������� �������, ����������� �� ����� ����
    /// </summary>
    public static void OnStartGame() => StartGame?.Invoke();

    /// <summary>
    /// �������� �������, ����������� �� ���������� ������ ������
    /// </summary>
    public static void OnUpdateSlots() => UpdateSlotsData?.Invoke();

    /// <summary>
    /// �������� �������, ����������� �� ��������� ������ ������
    /// </summary>
    public static void OnPlayerLevelUp() => PlayerLevelUp?.Invoke();

    /// <summary>
    /// �������� �������, ����������� �� ���������� ������������� ������
    /// </summary>
    public static void OnUpdateStatsData() => UpdateStatsData.Invoke();

}
