using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager 
{
    //����������� ���� - �������� ��� ����� ���������, ��� ����� ������� ��� ���� ����� ���� 
    //HashSet - ��� �� ���������� 
    static HashSet<DamagableComponent> damagableComponents = new HashSet<DamagableComponent>();

    //����� IEnumerator - ��� ������� ������� ����� ����������� 
    public static IReadOnlyCollection<DamagableComponent> Enemies => damagableComponents;

    public static void RegisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Add(damagable);
    }

    public static void UnregisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Remove(damagable);
    }


}
