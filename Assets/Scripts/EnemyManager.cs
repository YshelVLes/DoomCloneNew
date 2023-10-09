using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager 
{
    //статическое поле - работает всю жизнь программы, его можно достать изо всех точек кода 
    //HashSet - все эл уликальные 
    static HashSet<DamagableComponent> damagableComponents = new HashSet<DamagableComponent>();

    //Можно IEnumerator - это объекты которые можно перечислять 
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
